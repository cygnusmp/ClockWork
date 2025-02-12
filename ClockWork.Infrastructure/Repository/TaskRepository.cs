using ClockWork.Domain.Entities;
using ClockWork.Domain.Interfaces;

namespace ClockWork.Infrastructure.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private static readonly ISet<TaskEntity> _tasks = new HashSet<TaskEntity>()
        {
            new (1, "PDA-1", new DateTime(2025, 1, 2, 9, 0, 0), new DateTime(2025, 1, 2, 9, 33, 0), "Konsultacje"),
            new (2, "PDA-2", new DateTime(2025, 1, 2, 9, 33, 0), new DateTime(2025, 1, 2, 9, 58, 0)),
            new (3, "TRU-2", new DateTime(2025, 1, 2, 9, 58, 0), new DateTime(2025, 1, 2, 10, 41, 0)),
            new (4, "WSI-5", new DateTime(2025, 1, 2, 11, 0, 0), new DateTime(2025, 1, 2, 11, 17, 0), "SU")
        };

        public IEnumerable<TaskEntity> GetAll()
        {
            return _tasks;
        }

        public TaskEntity? GetById(int id)
        {
            return _tasks.SingleOrDefault(i => i.Id == id);
        }

        public TaskEntity Add(TaskEntity task)
        {
            task.Id = _tasks.Count() + 1;
            task.Created = DateTime.UtcNow;
            _tasks.Add(task);

            return task;
        }

        public void Update(TaskEntity task)
        {
            task.LastModified = DateTime.UtcNow;
        }

        public void Delete(TaskEntity task)
        {
            _tasks.Remove(task);
        }
    }
}
