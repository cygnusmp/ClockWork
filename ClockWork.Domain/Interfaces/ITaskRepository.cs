using ClockWork.Domain.Entities;

namespace ClockWork.Domain.Interfaces
{
    public interface ITaskRepository
    {
        IEnumerable<TaskEntity> GetAll();
        TaskEntity? GetById(int id);
        TaskEntity Add(TaskEntity task);
        void Update(TaskEntity task);
        void Delete(TaskEntity task);
    }
}
