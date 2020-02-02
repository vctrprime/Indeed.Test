using Indeed.Test.DataAccess;
using Indeed.Test.DataAccess.Repositories.Implementation;
using Indeed.Test.Models;
using Indeed.Test.Models.Requests;
using Indeed.Test.Models.Workers;
using Indeed.Test.Web.Controllers;
using Indeed.Test.Web.Infrastructure.Distributors;
using Indeed.Test.Web.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Indeed.Test.Web.Infrastructure.Distributors.Implementations
{
    internal class RequestsDistributor : IDistributor
    {
        private readonly WorkerRepository workerRepository;
        private readonly RequestRepository requestRepository;
        private readonly SettingsRepository settingsRepository;
        private readonly IHubContext<DistributeHub, ITypedHubClient> hubContext;

        private IEnumerable<Request> activeRequests;
        private IEnumerable<Worker> workers;
        private Settings activeSettings;


        public RequestsDistributor(IHubContext<DistributeHub, ITypedHubClient> hub)
        {
            var Context = new UnitOfWork();
            workerRepository = Context.Repository<Worker>() as WorkerRepository;
            requestRepository = Context.Repository<Request>() as RequestRepository;
            settingsRepository = Context.Repository<Settings>() as SettingsRepository;
            hubContext = hub;
        }

        public IEnumerable<Worker> FreeWorkers
        {
            get
            {
                return workers.Where(w => w.WorkingRequestId is null).OrderBy(x => x.Function);
            }
        }
        public IEnumerable<Request> NotTakenRequest { 
            get
            {
                return activeRequests.Where(x => string.IsNullOrEmpty(x.Executor)).OrderBy(x => x.CreatedDate);
            } 
        }
        public IEnumerable<Request> TakenRequest
        {
            get
            {
                return activeRequests.Where(x => !string.IsNullOrEmpty(x.Executor));
            }
        }

        public async void Distribute()
        {
            activeRequests = requestRepository.GetAll().Result.Where(r => !r.IsComplete);
            workers = workerRepository.GetAll().Result;
            activeSettings = settingsRepository.Get(0).Result;

            void CheckTakenRequests() {
                foreach (var request in TakenRequest)
                {
                    if (DateTime.Now.Ticks >= request.ExecutedDate)
                    {
                        request.IsComplete = true;
                        var worker = workers.SingleOrDefault(x => x.WorkingRequestId == request.Id);
                        worker.WorkingRequestId = null;
                        SaveChanges(request, worker);
                        Debug.WriteLine($"{worker.Name} закончил с запросом {request.Id}!");
                    }
                }
            }
            void DistrubuteForFreeWorkers()
            {
                foreach (var worker in FreeWorkers)
                {
                    var request = NotTakenRequest.FirstOrDefault();
                    if (request is null)
                        break;
                    if (!worker.CheckRequest(request, activeSettings))
                        continue;
                    else
                    {
                        Debug.WriteLine($"Запрос {request.Id} подходит {worker.Name}!");
                        Random random = new Random();
                        request.TakenDate = DateTime.Now.Ticks;
                        request.Executor = $"{worker.Function} {worker.Name}";
                        request.ExecutedDate = DateTime.Now.AddSeconds(random.Next(activeSettings.ExecuteTimeLimitLeft, activeSettings.ExecuteTimeLimitRight)).Ticks;
                        worker.WorkingRequestId = request.Id;
                        SaveChanges(request, worker);
                    }
                }
            }


            CheckTakenRequests();
            DistrubuteForFreeWorkers();

            Debug.WriteLine($"Запросов в очереди: {NotTakenRequest.Count()}");
            if (FreeWorkers.Count() == 0)
                Debug.WriteLine($"Нет свободных сотрудников!");

        }

        private async void SaveChanges(Request request, Worker worker)
        {
            await requestRepository.Update(request);
            await workerRepository.Update(worker);
            await hubContext.Clients.All.SendMessageToClient();
        }
    }
}
