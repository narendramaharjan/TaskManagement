using AnnalectIO.DomainModel.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnalectIO.DomainService.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository<TaskModel> _taskRepository;
        public TaskService(ITaskRepository<TaskModel> taskRepository)
        {
            this._taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskModel>> GetAll()
        {
            try
            {
                return await this._taskRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TaskModel> GetById(Guid id)
        {
            try
            {
                return await this._taskRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Create(TaskModel task)
        {
            try
            {
                await this._taskRepository.Create(task);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Edit(TaskModel task)
        {
            try
            {
                task.DateUpdated = DateTime.UtcNow;
                await this._taskRepository.Edit(task);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                await this._taskRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
