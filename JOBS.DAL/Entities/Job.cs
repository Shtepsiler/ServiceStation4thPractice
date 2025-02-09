using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace JOBS.DAL.Entities
{
    public class Job
    {
        public Guid Id { get; set; }
        public Guid? ManagerId { get; set; }
        public Guid? VehicleId { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? MechanicId { get; set; }
        public Guid? OrderId { get; set; }
        public string? ModelName { get; set; }
        public Status Status { get; set; } = Status.New;
        public DateTime IssueDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public bool IsPaid { get; set; } = false;
        public string? TransactionHash { get; set; }
        public float? ModelConfidence { get; set; } = null;
        public bool ModelAproved { get; set; } = false;
        public int? jobIndex { get; set; }
        public string? WEIPrice { get; set; }

        public List<MechanicsTasks>? Tasks { get; set; }
        public Mechanic Mechanic { get; set; }

    }

    public enum Status
    {
        New,
        Pending,
        InProgress,
        Completed
    }
}
