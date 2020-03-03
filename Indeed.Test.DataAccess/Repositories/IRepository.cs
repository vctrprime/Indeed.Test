using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Indeed.Test.DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        Task<TEntity> CreateAsync(TEntity item);
        Task<TEntity> UpdateAsync(TEntity item);
        Task<int> RemoveAsync(int id);

    }

}
