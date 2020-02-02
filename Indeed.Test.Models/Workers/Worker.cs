using Indeed.Test.Models.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Indeed.Test.Models.Workers
{
    public class Worker : BaseEntity
    {
        public string Function { get; set; }
        public int? WorkingRequestId { get; set; }

        public string Status { 
            get
            {
                return WorkingRequestId.HasValue ? $"Исполняет запрос {WorkingRequestId}" : "Свободен";
            } 
        }

        public string StatusColor
        {
            get
            {
                return WorkingRequestId.HasValue ? "#FFD700" : "#00FF7F";
            }
        }

        public Worker()
        {
            Function = this.GetType().Name;
        }        

        public virtual bool CheckRequest(Request request, Settings settings)
        {
            return true;
        }
    }
}
