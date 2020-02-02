using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Indeed.Test.DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(int id);
        Task<TEntity> Create(TEntity item);
        Task<TEntity> Update(TEntity item);
        Task<int> Remove(int id);

    }

}
