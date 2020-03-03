using Indeed.Test.Models.Requests;
using Indeed.Test.Models.Workers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Indeed.Test.Factories
{
    public interface IBaseFactory 
    {
        Worker CreateWorker(Worker _worker);
        Request CreateRequest(Request _request);
    }
}
