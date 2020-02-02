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
        private readonly WorkerFactory workerFactory;
        public WorkerRepository(string jsonDataFileName) : base(jsonDataFileName)
        {
            workerFactory = new WorkerFactory();
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
            if (entryItem.WorkingRequestId.HasValue)
                throw new Exception("Нельзя удалить работника при исполнении запроса!");
            Context.Workers.Remove(entryItem);
            id = 0;
            SaveContext();
            return id;
        }

        public async override Task<Worker> Update(Worker item)
        {
            var entryItem = Context.Workers.Find(w => w.Id == item.Id);
            if ((item.Name != entryItem.Name || item.Position != entryItem.Position) && entryItem.WorkingRequestId.HasValue)
                throw new Exception("Нельзя изменить работника при исполнении запроса!");
            int index = Context.Workers.IndexOf(entryItem);
            Context.Workers[index] = item;
            SaveContext();
            return item;
        }
    }
}
