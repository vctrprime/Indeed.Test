using Indeed.Test.DataAccess.Repositories;
using Indeed.Test.DataAccess.Repositories.Implementation;
using Indeed.Test.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indeed.Test.UnitTests.Repositories
{
    public class RequestRepositoryTest : IRepository<Request>
    {
        private readonly List<Request> _requests;
        public RequestRepositoryTest()
        {
            _requests = new List<Request>()
            {
                new Request()
                {
                    Id = 1,
                    Name= "1",
                    CreatedDate = 637162816542435431,
                    TakenDate = 637162816551146125,
                    ExecutedDate = 637162816901153169,
                    Executor = "Operator Alisa",
                    IsComplete = true
                },
                new Request()
                {
                    Id = 2,
                    Name= "2",
                    CreatedDate = 637162816555948698,
                    TakenDate = 637162816661132335,
                    ExecutedDate = 637162817021132397,
                    Executor = "Manager Bob",
                    IsComplete = false
                }
                ,
                new Request()
                {
                    Id = 3,
                    Name= "3",
                    CreatedDate = 637162816567843209,
                    TakenDate = null,
                    ExecutedDate = null,
                    Executor = null,
                    IsComplete = false
                }
            };
        }
        public async Task<Request> CreateAsync(Request item)
        {
            item.Id = _requests.Count > 0 ? _requests.Max(w => w.Id) + 1 : 1;
            _requests.Add(item);
            return item;
        }

        public Request Get(int id)
        {
            return _requests.Find(w => w.Id == id);
        }

        public IEnumerable<Request> GetAll()
        {
            return _requests;
        }

        public async Task<int> RemoveAsync(int id)
        {
            Request entryItem = _requests.Find(w => w.Id == id);
            if (!string.IsNullOrEmpty(entryItem.Executor))
                throw new Exception("Нельзя отменить исполняемый или завершенный запрос!");
            _requests.Remove(entryItem);
            id = 0;
            return id;
        }

        public async Task<Request> UpdateAsync(Request item)
        {
            Request entryItem = _requests.Find(w => w.Id == item.Id);
            entryItem.TakenDate = item.TakenDate;
            entryItem.Executor = item.Executor;
            entryItem.ExecutedDate = item.ExecutedDate;
            entryItem.IsComplete = item.IsComplete;
            return item;
        }
    }
}
