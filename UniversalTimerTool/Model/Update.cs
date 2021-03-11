using System.Collections.Generic;

namespace UniversalTimerTool.Model
{
    public class Update
    {
        public Time Work { get; set; }
        public Time Train { get; set; }
        public int LastPricePerHour { get; set; }
        public string UpdateName { get; set; }
        public string UpdateDescription { get; set; }
        public List<ToDo> ToDoList { get; set; }

        public Update(Time _workTime, Time _trainTime, int _lastPricePerHour, string _updateName, List<ToDo> _ToDOList = null, string _updateDescription = "")
        {
            this.Work = _workTime;
            this.Train = _trainTime;
            this.LastPricePerHour = _lastPricePerHour;
            this.UpdateName = _updateName;
            this.ToDoList = _ToDOList;
            this.UpdateDescription = _updateDescription;            
        }

        public string DisplayWorkTime()
        {
            return Work.Hours + ":" + Work.Minutes + ":" + Work.Seconds;
        }
        public string DisplayTrainTime()
        {
            return Train.Hours + ":" + Train.Minutes + ":" + Train.Seconds;
        }
    }
}