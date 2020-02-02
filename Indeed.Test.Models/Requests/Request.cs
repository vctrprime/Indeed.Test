using System;
using System.Collections.Generic;
using System.Text;

namespace Indeed.Test.Models.Requests
{
    public class Request : BaseEntity
    {
        public string CreatedBy { get; set; }
        public long CreatedDate { get; set; }
        public long? TakenDate { get; set; }
        public long? ExecutedDate { get; set; }
        public string Executor { get; set; }
        public bool IsComplete { get; set; }


        public int WatingTime
        {
            get
            {
                return (int)TimeSpan.FromTicks((TakenDate ?? DateTime.Now.Ticks) - CreatedDate).TotalSeconds;
            }
        }
        public int ExecutedTime
        {
            get
            {
                return (int)TimeSpan.FromTicks((ExecutedDate ?? 0) - (TakenDate ?? 0)).TotalSeconds;
            }
        }
        public int SummaryTime
        {
            get
            {
                return (int)TimeSpan.FromTicks((ExecutedDate ?? DateTime.Now.Ticks) - CreatedDate).TotalSeconds;
            }
        }
        public string Status
        {
            get
            {
                return IsComplete ? "Запрос выполнен" : 
                                  !string.IsNullOrEmpty(Executor) ? "Запрос выполняется" :  "Запрос в очереди";
            }
        }

        public string StatusColor
        {
            get
            {
                return IsComplete ? "#00FF7F" :
                                  !string.IsNullOrEmpty(Executor) ? "#FFD700" : "#FA8072";
            }
        }
    }
}
