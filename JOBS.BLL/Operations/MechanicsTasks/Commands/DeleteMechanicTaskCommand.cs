using JOBS.DAL.Data;
using JOBS.DAL.Exceptions;
using MediatR;
using ServiceCenterPayment.ContractDefinition;
using ServiceCenterPayment;
using System.Numerics;
using Microsoft.EntityFrameworkCore;

namespace JOBS.BLL.Operations.MechanicsTasks.Commands
{
    public class DeleteMechanicTaskCommand : IRequest
    {
        public Guid Id { get; set; }   
    }
    public class DeleteMechanicTaskHandler : IRequestHandler<DeleteMechanicTaskCommand>
        {
            private readonly ServiceStationDBContext _context;
        public IServiceCenterPaymentServiceFactory Factory { get; }

        public DeleteMechanicTaskHandler(ServiceStationDBContext context, IServiceCenterPaymentServiceFactory factory)
        {
            _context = context;
            Factory = factory;
        }
        async Task IRequestHandler<DeleteMechanicTaskCommand>.Handle(DeleteMechanicTaskCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.MechanicsTasks
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(DAL.Entities.MechanicsTasks), request.Id);
                }

                _context.MechanicsTasks.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);
            var job = _context.Jobs.Include(p => p.Tasks).First(p => p.Id == entity.JobId);
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
}
