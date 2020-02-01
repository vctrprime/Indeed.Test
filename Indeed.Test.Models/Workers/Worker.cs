using System;
using System.Collections.Generic;
using System.Text;

namespace Indeed.Test.Models.Workers
{
    public class Worker : BaseEntity
    {
        public string Function { get; set; }

        public Worker()
        {
            Function = this.GetType().FullName;
        }
    }
}
