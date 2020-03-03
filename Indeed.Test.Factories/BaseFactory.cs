using Indeed.Test.Models.Requests;
using Indeed.Test.Models.Workers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Indeed.Test.Factories
{
    public class BaseFactory : IBaseFactory
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

        public Request CreateRequest(Request _request)
        {
            _request.Name = string.IsNullOrEmpty(_request.Name) ? "unknown" : _request.Name;
            _request.CreatedBy = string.IsNullOrEmpty(_request.CreatedBy) ? "unknown" : _request.CreatedBy;
            _request.CreatedDate = DateTime.Now.Ticks;
            return _request;
        }

    }
}
