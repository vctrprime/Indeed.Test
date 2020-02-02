using Indeed.Test.Models.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Indeed.Test.Models.Workers
{
    public class Manager : Worker
    {
        public override bool CheckRequest(Request request, Settings settings)
        {
            return DateTime.Now.Ticks > new DateTime(request.CreatedDate).AddSeconds(settings.TimeManager).Ticks;
        }
    }
}
