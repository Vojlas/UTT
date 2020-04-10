using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalTimerTool.Model
{
    public class Project
    {
        public string ProjektName { get; set; }
        public DateTime Created { get; private set; }
        public string FileName { get; private set; }
        public string Description { get; set; }
        public List<Update> Updates { get; set; }

        public Project(string name, DateTime createdTime, List<Update> updates, string description = "")
        {
            this.ProjektName = name;
            this.Created = createdTime;
            this.Description = description;
            this.FileName = getSafeFilename(name) + ".xml";
            this.Updates = updates;
        }
        private string getSafeFilename(string filename) { return string.Join("", filename.Split(Path.GetInvalidFileNameChars())); }

        public TimeSpan TotalTimeFromUpdates()
        {
            TimeSpan time = new TimeSpan();
            foreach (Update update in this.Updates)
            {
                TimeSpan train = new TimeSpan(update.TrainTime.Ticks);
                TimeSpan work = new TimeSpan(update.WorkTime.Ticks);
                time = time.Add(train);
                time = time.Add(work);
            }
            return time;
        }

        public TimeSpan WorkTrainUpdateTime(int updateNumber)
        {
            TimeSpan time = new TimeSpan();
            try
            {
                TimeSpan train = new TimeSpan(this.Updates.ElementAt(updateNumber).TrainTime.Ticks);
                TimeSpan work = new TimeSpan(this.Updates.ElementAt(updateNumber).WorkTime.Ticks);
                time = time.Add(train);
                time = time.Add(work);
            }
            catch (Exception)
            {
                throw new NonExstingUpdateException();
            }
            return time;
        }

        public void AddSeconds(int UpdateNumber, int seconds, bool work)
        {
            try
            {
                if (work) this.Updates.ElementAt(UpdateNumber).WorkTime = this.Updates.ElementAt(UpdateNumber).WorkTime.AddSeconds(seconds);
                else
                {
                    this.Updates.ElementAt(UpdateNumber).TrainTime = this.Updates.ElementAt(UpdateNumber).TrainTime.AddSeconds(seconds);
                }
            }
            catch (Exception)
            {
                throw new NonExstingUpdateException();
            }
        }
    }
}
