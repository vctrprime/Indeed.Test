﻿using Indeed.Test.DataAccess.Repositories.Implementation;
using Indeed.Test.Factories;
using Indeed.Test.Models;
using Indeed.Test.Models.Requests;
using Indeed.Test.Models.Workers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        public readonly WorkerFactory workerFactory;

        public UnitOfWork()
        {
            workerFactory = new WorkerFactory();
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

        void FillStaticContext(JObject obj)
        {
            List<T> GetStaticEntities<T>(string jsonKey)
            {
                var jsonArray = JArray.Parse(obj[jsonKey].ToString());
                return JsonConvert.DeserializeObject<List<T>>(jsonArray.ToString());
            }

            var workers = GetStaticEntities<Worker>("Workers");
            Context.Workers = new List<Worker>();
            workers.ForEach(w =>
            {
                var worker = workerFactory.CreateWorker(w);
                Context.Workers.Add(worker);
            });
            Context.Settings = GetStaticEntities<Settings>("Settings").First();
            Context.Requests = GetStaticEntities<Request>("Requests");
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
