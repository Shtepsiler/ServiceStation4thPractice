using AutoMapper;
using JOBS.BLL.Common.Helpers;
using JOBS.BLL.DTOs.Respponces;
using JOBS.DAL.Data;
using JOBS.DAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JOBS.BLL.Operations.MechanicsTasks.Queries
{
    public class GetMechanicsTasksPaginatedQuery : PaginationParams, IRequest<Pagination<MechanicsTasksDTO>>
    {
        public string? SearchTerm { get; set; } = "";
        public string? Status { get; set; }
        public Guid? MechanicId { get; set; }
    }

    public class GetMechanicsTasksPaginatedQueryHandler : IRequestHandler<GetMechanicsTasksPaginatedQuery, Pagination<MechanicsTasksDTO>>
    {
        private readonly ServiceStationDBContext _context;
        private readonly IMapper _mapper;

        public GetMechanicsTasksPaginatedQueryHandler(ServiceStationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Pagination<MechanicsTasksDTO>> Handle(GetMechanicsTasksPaginatedQuery request, CancellationToken cancellationToken)
        {
            var query = _context.MechanicsTasks.AsQueryable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(t =>
                    t.Name.Contains(request.SearchTerm) ||
                    t.Task.Contains(request.SearchTerm) ||
                    t.Id.ToString().Contains(request.SearchTerm));
            }

            // Apply status filter
            if (!string.IsNullOrWhiteSpace(request.Status))
            {
                var isstatus = Status.TryParse(request.Status, true, out Status status);
                if (isstatus)
                    query = query.Where(t => t.Status == status);

            }

            // Apply mechanic filter
            if (request.MechanicId.HasValue)
            {
                query = query.Where(t => t.MechanicId == request.MechanicId.Value);
            }

            // Order by issue date
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