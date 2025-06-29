﻿using JOBS.DAL.Data;
using JOBS.DAL.Entities;
using JOBS.DAL.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JOBS.BLL.Operations.MechanicsTasks.Commands;

public class UpdateMechanicTaskStatusCommand : IRequest
{
    [FromQuery]
    public Guid Id { get; set; }
    [FromQuery]
    public Status Status { get; set; }

}

public class UpdateMechanicTaskStatusCommandCommandHandler : IRequestHandler<UpdateMechanicTaskStatusCommand>
{
    private readonly ServiceStationDBContext _context;

    public UpdateMechanicTaskStatusCommandCommandHandler(ServiceStationDBContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateMechanicTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.MechanicsTasks.FirstAsync(p => p.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(UpdateMechanicTaskStatusCommand), request.Id);
        }

        entity.Status = request.Status;

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
            job.Status = Status.Completed;
        }
        else
        {
            job.Status = Status.InProgress;
        }
        await _context.SaveChangesAsync(cancellationToken);
    }
}