using AnnalectIO.DomainModel.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnalectIO.DomainService.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskModel>> GetAll();
        Task<TaskModel> GetById(Guid id);
        Task Create(TaskModel task);
        Task Edit(TaskModel task);
        Task Delete(Guid id);
    }
}
