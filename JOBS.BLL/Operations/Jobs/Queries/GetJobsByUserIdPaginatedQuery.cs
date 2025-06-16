using AutoMapper;
using JOBS.BLL.Common.Helpers;
using JOBS.BLL.DTOs.Respponces;
using JOBS.DAL.Data;
using JOBS.DAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JOBS.BLL.Operations.Jobs.Queries
{
    public class GetJobsByUserIdPaginatedQuery : PaginationParams, IRequest<Pagination<JobDTO>>
    {
        public Guid UserId { get; set; }
        public string? SearchTerm { get; set; } = "";
        public Status? Status { get; set; }
    }

    public class GetJobsByUserIdPaginatedQueryHandler : IRequestHandler<GetJobsByUserIdPaginatedQuery, Pagination<JobDTO>>
    {
        private readonly ServiceStationDBContext _context;
        private readonly IMapper _mapper;

        public GetJobsByUserIdPaginatedQueryHandler(ServiceStationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Pagination<JobDTO>> Handle(GetJobsByUserIdPaginatedQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Jobs.Where(j => j.ClientId == request.UserId);

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(j =>
                    j.Description.Contains(request.SearchTerm) ||
                    j.ModelName.Contains(request.SearchTerm) ||
                    j.Id.ToString().Contains(request.SearchTerm));
            }

            // Apply status filter
            if (request.Status.HasValue)
            {
                query = query.Where(j => j.Status == request.Status.Value);
            }

            // Order by date descending
            query = query.OrderByDescending(j => j.IssueDate);

            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var mappedItems = _mapper.Map<List<JobDTO>>(items);

            return new Pagination<JobDTO>(mappedItems, totalCount, request.PageNumber, request.PageSize);
        }
    }
}