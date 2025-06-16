using JOBS.BLL.DTOs.Requests;
using JOBS.BLL.DTOs.Respponces;
using JOBS.BLL.Helpers;
using JOBS.DAL.Data;
using JOBS.DAL.Entities;
using MediatR;
using ServiceCenterPayment;
using ServiceCenterPayment.ContractDefinition;
using System.Numerics;

namespace JOBS.BLL.Operations.Jobs.Commands;

public record CreateJobCommand : IRequest<Guid>
{
    /*    public int? ManagerId { get; set; }*/
    public Guid? ClientId { get; set; }
    public Guid? VehicleId { get; set; }
    public DateTime IssueDate { get; set; }
    public string Description { get; set; }
}

public class CreateJobHandler : IRequestHandler<CreateJobCommand, Guid>
{
    private readonly ServiceStationDBContext _context;
    public IHttpClientFactory HttpClientFactory { get; }
    public IServiceCenterPaymentServiceFactory Factory { get; }

    public CreateJobHandler(ServiceStationDBContext context, IHttpClientFactory httpClientFactory, IServiceCenterPaymentServiceFactory factory)
    {
        _context = context;
        HttpClientFactory = httpClientFactory;
        Factory = factory;
    }


    public async Task<Guid> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {

        var entity = new Job()
        {
            /* ManagerId = request.ManagerId,*/
            VehicleId = request.VehicleId,
            ClientId = request.ClientId,
            IssueDate = request.IssueDate,
            Description = request.Description
        };

        ApiHttpClient apiCliet = new(HttpClientFactory.CreateClient("MechanicModel"));
        var modelreq = new ModelPredictReqest();
        modelreq.description = entity.Description;
        ModelPredictResoponce modelPredictResoponce = null;
        try
        {
            modelPredictResoponce = await apiCliet.PostAsync<ModelPredictReqest, ModelPredictResoponce>("predict", modelreq);
        }
        catch (Exception e)
        {
            throw e;
        }

        if (modelPredictResoponce == null)
            throw new Exception("модель не повернула результат");

        PartsMLApiClient ppapiCliet = new(HttpClientFactory.CreateClient("PartsModel"));
        var ppmodelreq = new PredictionRequest();
        ppmodelreq.ProblemDescription = entity.Description;
        ppmodelreq.UserId = entity.ClientId.ToString();
        PredictionResponse ppmodelPredictResoponce = null;
        try
        {
            ppmodelPredictResoponce = await ppapiCliet.PredictPartsAsync(ppmodelreq);
        }
        catch (Exception e)
        {
            throw e;
        }

        if (ppmodelPredictResoponce == null)
            throw new Exception("pp модель не повернула результат");
        entity.PredictionId = ppmodelPredictResoponce.PredictionId;
        ApiHttpClient partsapiCliet = new(HttpClientFactory.CreateClient("PartsApi"));

        var parameters = new Dictionary<string, string> {
            {"userId",entity.ClientId?.ToString() },
            {"jobId",entity.Id.ToString() }
        };
        var order = await partsapiCliet.GetAsync<OrderDTO>("api/Order/GetNewOrder", parameters);
        if (order != null)
        {
            entity.OrderId = order.Id;
            var req = new AddPartsByCategoryRequest();
            req.OrderId = order.Id.Value;
            req.OnlyUniversal = true;
            req.Categories = ppmodelPredictResoponce.Predictions.Select(p => p.PartName).ToList();
            var resp = await partsapiCliet.PostAsync<AddPartsByCategoryRequest, IEnumerable<MechanicsTasksDTO>>("api/Order/AddPartsByCategory", req);






        }

        var spec = _context.Specialisations.Where(p => p.Name == modelPredictResoponce.predicted_class).FirstOrDefault();
        if (spec != null)
        {
            var mechanic = MechanicScheduler.AssignTaskToLeastBusyMechanic(_context, spec, entity.IssueDate, TimeSpan.FromHours(1));
            entity.Mechanic = mechanic;
        }
        entity.Status = Status.New;
        entity.ModelConfidence = modelPredictResoponce.confidence;
        entity.ModelAproved = modelPredictResoponce.confidence > 0.7;
        entity.Price = entity.Tasks?.Sum(t => t.Price ?? 0) ?? 0;
        entity.WEIPrice = (await EthereumPriceConverter.ConvertUsdToEtherAsync(entity.Price.Value, 18)).ToString();

        await _context.Jobs.AddAsync(entity);
        await _context.SaveChangesAsync(cancellationToken);

        var serv = await Factory.CreateServiceAsync();

        await serv.AddJobRequestAndWaitForReceiptAsync(new AddJobFunction()
        {
            JobId = entity.Id.ToString(),
            Price = BigInteger.Parse(entity.WEIPrice),
            UserId = entity.ClientId.ToString()
        });


        return entity.Id;
    }

}
