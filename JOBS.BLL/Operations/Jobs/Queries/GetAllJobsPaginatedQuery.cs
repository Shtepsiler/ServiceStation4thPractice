using AutoMapper;
using JOBS.BLL.Common.Helpers;
using JOBS.BLL.DTOs.Respponces;
using JOBS.DAL.Data;
using JOBS.DAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JOBS.BLL.Operations.Jobs.Queries
{
    public class GetAllJobsPaginatedQuery : PaginationParams, IRequest<Pagination<JobDTO>>
    {
        public string? SearchTerm { get; set; } = "";
        public Status? Status { get; set; }
    }

    public class GetAllJobsPaginatedQueryHandler : IRequestHandler<GetAllJobsPaginatedQuery, Pagination<JobDTO>>
    {
        private readonly ServiceStationDBContext _context;
        private readonly IMapper _mapper;

        public GetAllJobsPaginatedQueryHandler(ServiceStationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Pagination<JobDTO>> Handle(GetAllJobsPaginatedQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Jobs.AsQueryable();

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
                if (request.Status.Value == Status.New)
                {
                    query = query.Where(j => j.Status == Status.New || j.MechanicId == null);
                }
                else
                {
                    query = query.Where(j => j.Status == request.Status.Value);
                }
            }

            // Order by date
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