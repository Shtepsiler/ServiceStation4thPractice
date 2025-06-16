using AutoMapper;
using JOBS.BLL.Common.Helpers;
using JOBS.BLL.DTOs.Respponces;
using JOBS.DAL.Data;
using JOBS.DAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JOBS.BLL.Operations.MechanicsTasks.Queries
{
    public class GetMechanicTaskByMechanicIdPaginatedQuery : PaginationParams, IRequest<Pagination<MechanicsTasksDTO>>
    {
        public Guid MechanicId { get; set; }
        public string SearchTerm { get; set; } = "";
        public Status? Status { get; set; }
    }

    public class GetMechanicTaskByMechanicIdPaginatedQueryHandler : IRequestHandler<GetMechanicTaskByMechanicIdPaginatedQuery, Pagination<MechanicsTasksDTO>>
    {
        private readonly ServiceStationDBContext _context;
        private readonly IMapper _mapper;

        public GetMechanicTaskByMechanicIdPaginatedQueryHandler(ServiceStationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Pagination<MechanicsTasksDTO>> Handle(GetMechanicTaskByMechanicIdPaginatedQuery request, CancellationToken cancellationToken)
        {
            var query = _context.MechanicsTasks.Where(t => t.MechanicId == request.MechanicId);

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(t =>
                    t.Name.Contains(request.SearchTerm) ||
                    t.Task.Contains(request.SearchTerm));
            }

            // Apply status filter
            if (request.Status.HasValue)
            {
                query = query.Where(t => t.Status == request.Status.Value);
            }

            // Order by creation date or status priority
            query = query.OrderByDescending(t => t.IssueDate);

            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var mappedItems = _mapper.Map<List<MechanicsTasksDTO>>(items);

            return new Pagination<MechanicsTasksDTO>(mappedItems, totalCount, request.PageNumber, request.PageSize);
        }
    }
}