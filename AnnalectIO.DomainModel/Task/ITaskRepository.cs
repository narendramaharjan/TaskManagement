using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnalectIO.DomainModel.Task
{
    public interface ITaskRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        System.Threading.Tasks.Task Create(T task);
        System.Threading.Tasks.Task Edit(T task);
        System.Threading.Tasks.Task Delete(Guid id);
    }
}
