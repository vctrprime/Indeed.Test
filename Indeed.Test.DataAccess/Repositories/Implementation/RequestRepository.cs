using Indeed.Test.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indeed.Test.DataAccess.Repositories.Implementation
{
    public class RequestRepository : Repository<Request>
    {
        public RequestRepository(string jsonDataFileName) : base(jsonDataFileName)
        {
        }
        public async override Task<Request> Create(Request item)
        {
            item.Id = Context.Requests.Count > 0 ? Context.Requests.Max(w => w.Id) + 1 : 1;
            Context.Requests.Add(item);
            SaveContext();
            return item;
        }

        public async override Task<Request> Get(int id)
        {
            return Context.Requests.Find(w => w.Id == id);
        }

        public async override Task<IEnumerable<Request>> GetAll()
        {
            return Context.Requests;
        }

        public async override Task<int> Remove(int id)
        {
            Request entryItem = Context.Requests.Find(w => w.Id == id);
            if (!string.IsNullOrEmpty(entryItem.Executor))
                throw new Exception("Нельзя отменить исполняемый или завершенный запрос!");
            Context.Requests.Remove(entryItem);
            id = 0;
            SaveContext();
            return id;
        }

        public async override Task<Request> Update(Request item)
        {
            Request entryItem = Context.Requests.Find(w => w.Id == item.Id);
            entryItem = item;
            SaveContext();
            return item;
        }
    }
}
