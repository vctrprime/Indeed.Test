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

        public async override Task<Worker> Create(Worker item)
        {
            item.Id = Context.Workers.Count > 0 ? Context.Workers.Max(w => w.Id) + 1 : 1;
            Context.Workers.Add(item);
            SaveContext();
            return item;
        }

        public async override Task<Worker> Get(int id)
        {
            return Context.Workers.Find(w => w.Id == id);
        }

        public async override Task<IEnumerable<Worker>> GetAll()
        {
            return Context.Workers;
        }

        public async override Task<int> Remove(int id)
        {
            Worker entryItem = Context.Workers.Find(w => w.Id == id);
            Context.Workers.Remove(entryItem);
            id = 0;
            SaveContext();
            return id;
        }

        public async override Task<Worker> Update(Worker item)
        {
            Worker entryItem = Context.Workers.Find(w => w.Id == item.Id);
            entryItem = item;
            SaveContext();
            return item;
        }
    }
}
