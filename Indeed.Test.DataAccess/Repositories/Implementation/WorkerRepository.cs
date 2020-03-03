using Indeed.Test.Factories;
using Indeed.Test.Models.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indeed.Test.DataAccess.Repositories.Implementation
{
    public class WorkerRepository : Repository<Worker>
    {
        public WorkerRepository(string jsonDataFileName) : base(jsonDataFileName)
        {
        }

        public async override Task<Worker> CreateAsync(Worker item)
        {
            item.Id = Context.Workers.Count > 0 ? Context.Workers.Max(w => w.Id) + 1 : 1;
            Context.Workers.Add(item);
            await SaveContext();
            return item;
        }

        public override Worker Get(int id)
        {
            return Context.Workers.SingleOrDefault(w => w.Id == id);
        }

        public override IEnumerable<Worker> GetAll()
        {
            return Context.Workers;
        }

        public async override Task<int> RemoveAsync(int id)
        {
            Worker entryItem = Context.Workers.Find(w => w.Id == id);
            if (entryItem.WorkingRequestId.HasValue)
                throw new Exception("Нельзя удалить работника при исполнении запроса!");
            Context.Workers.Remove(entryItem);
            id = 0;
            await SaveContext();
            return id;
        }

        public async override Task<Worker> UpdateAsync(Worker item)
        {
            while(true)
            {
                try
                {
                    var entryItem = Context.Workers.Find(w => w.Id == item.Id);
                    if ((item.Name != entryItem.Name || item.Position != entryItem.Position) && entryItem.WorkingRequestId.HasValue)
                        throw new Exception("Нельзя изменить работника при исполнении запроса!");
                    int index = Context.Workers.IndexOf(entryItem);
                    Context.Workers[index] = item;
                    await SaveContext();
                    break;
                }
                catch { }
            }
            return item;
        }
    }
}
