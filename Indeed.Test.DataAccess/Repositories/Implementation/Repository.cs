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
        public abstract IEnumerable<T> GetAll();
        public abstract T Get(int id);
        public abstract Task<T> CreateAsync(T item);
        public abstract Task<T> UpdateAsync(T item);
        public abstract Task<int> RemoveAsync(int id);

        protected Repository(string jsonDataFileName)
        {
            _jsonDataFileName = jsonDataFileName;
        }

        protected async Task SaveContext()
        {
            string json = JsonConvert.SerializeObject(new
            {
                Workers = Context.Workers,
                Settings = Context.Settings,
                Requests = Context.Requests
            });

            var buffer = Encoding.UTF8.GetBytes(json);
            while (true)
            {
                try
                {
                    using (var fs = new FileStream(_jsonDataFileName, FileMode.OpenOrCreate,
                FileAccess.Write, FileShare.None, buffer.Length, true))
                    {
                        await fs.WriteAsync(buffer, 0, buffer.Length);
                        fs.Close();
                        break;
                    }
                }
                catch { }
            }
            
        }


    }

}
