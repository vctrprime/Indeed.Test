using Indeed.Test.Models.Workers;
using System;

namespace Indeed.Test.Factories
{
    public class WorkerFactory
    {
        public Worker CreateWorker(Worker _worker)
        {
            int id = _worker.Id;
            string name = _worker.Name;
            int? workingRequestId = _worker.WorkingRequestId;
            Worker worker;
            switch (_worker.Position)
            {
                case "Operator":
                    worker = new Operator();
                    break;
                case "Manager":
                    worker = new Manager();
                    break;
                case "Director":
                    worker = new Director();
                    break;
                default:
                    worker = _worker;
                    break;

            }
            worker.Id = id;
            worker.Name = name;
            worker.WorkingRequestId = workingRequestId;
            return worker;
        }
    }
}
