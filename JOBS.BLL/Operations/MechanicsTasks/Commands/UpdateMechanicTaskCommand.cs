using JOBS.DAL.Data;
using JOBS.DAL.Entities;
using JOBS.DAL.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceCenterPayment.ContractDefinition;
using ServiceCenterPayment;
using System.Numerics;

namespace JOBS.BLL.Operations.MechanicsTasks.Commands;

public record UpdateMechanicTaskCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid MechanicId { get; set; }
    public Guid? JobId { get; set; }
    public string Task { get; set; }
    public Status Status { get; set; }
    public string Name { get; set; }
    public decimal? Price { get; set; }

}

public class UpdateMechanicTaskCommandHandler : IRequestHandler<UpdateMechanicTaskCommand>
{
    private readonly ServiceStationDBContext _context;
    public IServiceCenterPaymentServiceFactory Factory { get; }
    public UpdateMechanicTaskCommandHandler(ServiceStationDBContext context, IServiceCenterPaymentServiceFactory factory)
    {
        _context = context;
        Factory = factory;
    }

    public async Task Handle(UpdateMechanicTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.MechanicsTasks.Include(p=>p.Mechanic).Include(p=>p.Job).AsQueryable().FirstAsync(p=>p.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(DAL.Entities.MechanicsTasks), request.Id);
        }
        var mechanic = _context.Mechanics.FirstOrDefault(p => p.MechanicId == request.MechanicId);
        entity.Mechanic = mechanic;
        var gob = _context.Jobs.FirstOrDefault(p => p.Id == request.JobId);
        entity.Job = gob;
        entity.Task = request.Task;
        entity.Status = request.Status;
        entity.Name = request.Name;
        entity.Price = request.Price;
        var job = await _context.Jobs
            .Include(j => j.Tasks)
            .FirstOrDefaultAsync(j => j.Id == entity.JobId, cancellationToken);

        if (job == null)
        {
            throw new NotFoundException();
        }

        // Перевіряємо статуси завдань
        if (job.Tasks.All(t => t.Status == Status.Completed))
        {
            job.Status = Status.Completed; // Якщо всі завдання завершені
        }
        else
        {
            job.Status = Status.InProgress; // Якщо хоча б одне завдання не завершене
        }
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
    }
}
