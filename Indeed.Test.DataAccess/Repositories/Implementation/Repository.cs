using Indeed.Test.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Indeed.Test.DataAccess.Repositories.Implementation
{
    public abstract partial class Repository<T> : IRepository<T> where T : class, new()
    {
        protected readonly string _jsonDataFileName;
        public abstract Task<IEnumerable<T>> GetAll();
        public abstract Task<T> Get(int id);
        public abstract Task<T> Create(T item);
        public abstract Task<T> Update(T item);
        public abstract Task<int> Remove(int id);

        protected Repository(string jsonDataFileName)
        {
            _jsonDataFileName = jsonDataFileName;
        }

        protected void SaveContext()
        {
            string json = JsonConvert.SerializeObject(new
            {
                Workers = Context.Workers,
                Settings = new List<Settings>() { Context.Settings },
                Requests = Context.Requests
            });
            File.WriteAllText(_jsonDataFileName, json);

        }


    }

}
