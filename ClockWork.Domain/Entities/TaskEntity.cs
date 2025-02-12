using ClockWork.Domain.Common;

namespace ClockWork.Domain.Entities
{
    public class TaskEntity : AuditableEntity
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public string? Description { get; set; }

        public TaskEntity() { }

        public TaskEntity(int id, string taskName, DateTime from, DateTime? to, string? description = null)
        {
            (Id, TaskName, From, To, Description) = (id, taskName, from, to, description);
        }
    }
}
