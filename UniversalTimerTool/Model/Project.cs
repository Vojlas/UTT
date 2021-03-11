using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public Time TotalProjectTime()
        {
            Time time = new Time();
            foreach (Update update in this.Updates)
            {
                time = time.Add(update.Train);
                time = time.Add(update.Work);
            }
            return time;
        }

        public Time TotalUpdateTime(int updateNumber)
        {
            if (updateNumber >= this.Updates.Count) throw new NonExstingUpdateException();
            Time time = new Time();
            time = time.Add(this.Updates.ElementAt(updateNumber).Train);
            time = time.Add(this.Updates.ElementAt(updateNumber).Work);
            return time;
        }

        public void AddSeconds(int updateNumber, int seconds, bool work)
        {
            if (updateNumber >= this.Updates.Count) throw new NonExstingUpdateException();
            if (work) { this.Updates.ElementAt(updateNumber).Work.AddSec(); return; }
            this.Updates.ElementAt(updateNumber).Train.AddSec();
        }
    }
}
