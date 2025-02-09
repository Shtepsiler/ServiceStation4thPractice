using JOBS.DAL.Data;
using JOBS.DAL.Entities;
using MediatR;
using ServiceCenterPayment.ContractDefinition;
using ServiceCenterPayment;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace JOBS.BLL.Operations.MechanicsTasks.Commands;

public record CreateMechanicTaskCommand : IRequest<Guid>
{
    public Guid? Id { get; set; }
    public Guid MechanicId { get; set; }
    public Guid? JobId { get; set; }
    public string Task { get; set; }
    public Status Status { get; set; }
    public string? Name { get; set; }
    public decimal? Price { get; set; }

}

public class CreateMechanicTaskCommandHandler : IRequestHandler<CreateMechanicTaskCommand, Guid>
{
    private readonly ServiceStationDBContext _context;
    public IServiceCenterPaymentServiceFactory Factory { get; }

    public CreateMechanicTaskCommandHandler(ServiceStationDBContext context, IServiceCenterPaymentServiceFactory factory)
    {
        _context = context;
        Factory = factory;
    }

    public async Task<Guid> Handle(CreateMechanicTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = new DAL.Entities.MechanicsTasks()
        {
            MechanicId = request.MechanicId,
            JobId = request.JobId,
            Task = request.Task,
            Status = request.Status,
            Name = request.Name,
            Price = request.Price,

        };

        var job = _context.Jobs.Include(p=>p.Tasks).First(p => p.Id == entity.JobId);
        job.Status = Status.InProgress;
        await _context.MechanicsTasks.AddAsync(entity);
        job.Price = job.Tasks?.Sum(t => t.Price ?? 0) ?? 0;
        job.WEIPrice = (await EthereumPriceConverter.ConvertUsdToEtherAsync(job.Price.Value, 18)).ToString();
        await _context.SaveChangesAsync(cancellationToken);

        var serv = await Factory.CreateServiceAsync();
        await serv.UpdateJobRequestAndWaitForReceiptAsync(new UpdateJobFunction()
        {
            JobIndex = (BigInteger)job.jobIndex.Value,
            JobId = job.Id.ToString(),
            Price = BigInteger.Parse(job.WEIPrice),
            UserId = job.ClientId.Value.ToString()
        });

        return entity.Id;
    }
}
