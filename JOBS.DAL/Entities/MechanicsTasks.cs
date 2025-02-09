using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace JOBS.DAL.Entities
{
    public class MechanicsTasks
    {
        public Guid Id { get; set; }
        public Guid? MechanicId { get; set; }
        public Guid? JobId { get; set; }
        public string? Name { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public Job? Job { get; set; }
        public string Task { get; set; }
        public Status Status { get; set; }
        public decimal? Price { get; set; }  
        public Mechanic Mechanic { get; set; }
    }

}
