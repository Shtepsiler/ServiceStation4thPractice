using JOBS.BLL.DTOs.Requests;
using JOBS.BLL.DTOs.Respponces;
using JOBS.BLL.Helpers;
using JOBS.DAL.Data;
using JOBS.DAL.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ServiceCenterPayment;

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
    public IServiceCenterPaymentServiceFactory Fack { get; }

    public CreateJobHandler(ServiceStationDBContext context,IHttpClientFactory httpClientFactory, IServiceCenterPaymentServiceFactory fack)
    {
        _context = context;
        HttpClientFactory = httpClientFactory;
        Fack = fack;
    }


    public async Task<Guid> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var ser = Fack.CreateServiceAsync();
        }
        catch(Exception e)
        {
            throw e;
        }
        var entity = new Job()
        {
            /* ManagerId = request.ManagerId,*/
            VehicleId = request.VehicleId,
            ClientId = request.ClientId,
            IssueDate = request.IssueDate,
            Description = request.Description
        };

        ApiHttpClient apiCliet = new(HttpClientFactory.CreateClient("Model"));
        var modelreq = new ModelPredictReqest();
        modelreq.description = entity.Description;
        ModelPredictResoponce modelPredictResoponce = null;
        try
        {
            modelPredictResoponce = await apiCliet.PostAsync<ModelPredictReqest, ModelPredictResoponce>("predict", modelreq);
        }
        catch(Exception e)
        {
            throw;
        }

        if (modelPredictResoponce == null)
            throw new Exception("модель не повернула результат");


        var spec = _context.Specialisations.Where(p=>p.Name == modelPredictResoponce.predicted_class).FirstOrDefault();
        if (spec != null)
        {
            var mechanic = MechanicScheduler.AssignTaskToLeastBusyMechanic(_context, spec, entity.IssueDate, TimeSpan.FromHours(1));
            entity.Mechanic = mechanic;
        }
        entity.Status = Status.New;
        entity.ModelConfidence = modelPredictResoponce.confidence;
        entity.ModelAproved = modelPredictResoponce.confidence > 0.7;
        await _context.Jobs.AddAsync(entity);


        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

}
