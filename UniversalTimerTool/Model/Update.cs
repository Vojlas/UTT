using System;
using System.Collections.Generic;

namespace UniversalTimerTool.Model
{
    public class Update
    {
        public DateTime WorkTime { get; set; }
        public DateTime TrainTime { get; set; }
        public int LastPricePerHour { get; set; }
        public string UpdateName { get; set; }
        public string UpdateDescription { get; set; }
        public List<ToDo> ToDoList { get; set; }

        public Update(DateTime workTime, DateTime trainTime, int lastPricePerHour, string updateName, string updateDescription = "")
        {
            this.WorkTime = workTime;
            this.TrainTime = trainTime;
            this.LastPricePerHour = lastPricePerHour;
            this.UpdateName = updateName;
            this.UpdateDescription = updateDescription;
        }

        public string DisplayWorkTime()
        {
            return (((WorkTime.Day-1)*24) + WorkTime.Hour) + ":" + WorkTime.Minute + ":" + WorkTime.Second; 
        }
        public string DisplayTrainTime()
        {
            return (((TrainTime.Day-1) * 24) + TrainTime.Hour) + ":" + TrainTime.Minute + ":" + TrainTime.Second;
        }
    }
}