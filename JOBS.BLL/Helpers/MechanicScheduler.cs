using JOBS.DAL.Data;
using JOBS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace JOBS.BLL.Helpers
{
    public class MechanicScheduler
    {
        public static Mechanic? AssignTaskToLeastBusyMechanic(
            ServiceStationDBContext context,
            Specialisation requiredSpecialisation,
            DateTime plannedStartDate,
            TimeSpan estimatedTaskDuration)
        {
            // Filter mechanics by specialization at DB level
            var availableMechanic = context.Mechanics
                .Include(m => m.Specialisation)
                .Include(m => m.MechanicsTasks)
                .Where(m => m.Specialisation.Id == requiredSpecialisation.Id)
                .AsEnumerable() // Switch to client-side evaluation for complex calculations
                .Select(m => new
                {
                    Mechanic = m,
                    NextAvailableTime = GetNextAvailableTime(m, plannedStartDate),
                    ActiveWorkload = m.MechanicsTasks
                        .Where(t => t.FinishDate == null || t.FinishDate > DateTime.Now)
                        .Sum(t => GetTaskDuration(t, DateTime.Now))
                })
                .OrderBy(m => m.NextAvailableTime)
                .ThenBy(m => m.ActiveWorkload)
                .FirstOrDefault();

            return availableMechanic?.Mechanic;
        }

        private static DateTime GetNextAvailableTime(Mechanic mechanic, DateTime plannedStartDate)
        {
            var activeTasks = mechanic.MechanicsTasks
                .Where(t => t.FinishDate == null || t.FinishDate > plannedStartDate)
                .OrderBy(t => t.IssueDate)
                .ToList();

            if (!activeTasks.Any())
                return plannedStartDate;

            // Find the latest finish time among active tasks
            DateTime lastTaskFinish = activeTasks
                .Max(t => t.FinishDate ?? t.IssueDate.AddDays(1)); // More realistic default duration

            return lastTaskFinish > plannedStartDate ? lastTaskFinish : plannedStartDate;
        }

        private static double GetTaskDuration(MechanicsTasks task, DateTime referenceDate)
        {
            var endDate = task.FinishDate ?? referenceDate;
            return Math.Max(0, endDate.Subtract(task.IssueDate).TotalHours);
        }
    }

}
