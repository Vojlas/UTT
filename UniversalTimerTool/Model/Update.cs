using System;

namespace UniversalTimerTool.Model
{
    public class Update
    {
        public DateTime WorkTime { get; set; }
        public DateTime TrainTime { get; set; }
        public int LastPricePerHour { get; set; }
        public string UpdateName { get; set; }
        public string UpdateDescription { get; set; }

        public Update(DateTime workTime, DateTime trainTime, int lastPricePerHour, string updateName, string updateDescription = "")
        {
            
            this.WorkTime = workTime;
            this.TrainTime = trainTime;
            this.LastPricePerHour = lastPricePerHour;
            this.UpdateName = updateName;
            this.UpdateDescription = updateDescription;
        }
    }
}