using Indeed.Test.DataAccess.Repositories.Implementation;
using Indeed.Test.Factories;
using Indeed.Test.Models;
using Indeed.Test.Models.Requests;
using Indeed.Test.Models.Workers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Indeed.Test.DataAccess
{
    public class UnitOfWork : IDisposable
    {

        private Dictionary<string, object> repositories;
        private readonly string jsonDataFileName;
        private readonly IBaseFactory _factory;

        public UnitOfWork()
        {
            _factory = new BaseFactory();
            jsonDataFileName = string.Format(@"{0}\data.json", Environment.CurrentDirectory);
            if (!Context.isActive)
            {
                using (StreamReader r = new StreamReader(jsonDataFileName))
                {

                    string jsonstring = r.ReadToEnd();
                    JObject obj = JObject.Parse(jsonstring);
                    FillStaticContext(obj);
                    Context.isActive = true;
                }

            }
        }

        private void FillStaticContext(JObject obj)
        {
            var workers = GetStaticEntities<Worker>(obj, "Workers");
            Context.Workers = new List<Worker>();
            foreach(var w in workers)
            {
                var worker = _factory.CreateWorker(w);
                Context.Workers.Add(worker);
            }
            Context.Requests = GetStaticEntities<Request>(obj, "Requests");
            Context.Settings = GetStaticEntity<Settings>(obj, "Settings");
        }
        
        public Repository<T> Repository<T>() where T : class, new()
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var assembly = Assembly.GetExecutingAssembly();
                var repositoryType = assembly.GetTypes().ToList().FirstOrDefault(c => c.Name == type + "Repository");
                if (repositoryType != null)
                {
                    var repositoryInstance = Activator.CreateInstance(repositoryType, new object[] { jsonDataFileName });
                    repositories.Add(type, repositoryInstance);
                }
            }
            return (Repository<T>)repositories[type];
        }

        private List<T> GetStaticEntities<T>(JObject obj,string jsonKey)
        {
            var jsonArray = JArray.Parse(obj[jsonKey].ToString());
            return JsonConvert.DeserializeObject<List<T>>(jsonArray.ToString());
        }
        private T GetStaticEntity<T>(JObject obj, string jsonKey)
        {
            var jsonObject = JObject.Parse(obj[jsonKey].ToString());
            return JsonConvert.DeserializeObject<T>(jsonObject.ToString());
            
        }
    #region IDisposable

    private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {

                }
            }
            _disposed = true;
        }

        #endregion

    }
}
