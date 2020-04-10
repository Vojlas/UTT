using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalTimerTool
{
    class ReTimeSpan
    {
        public TimeSpan TimeSpan { get; set; }
        public ReTimeSpan(TimeSpan timeSpan)
        {
            this.TimeSpan = timeSpan;
        }
        public ReTimeSpan()
        {
            this.TimeSpan = new TimeSpan();
        }
        public ReTimeSpan(int hours, int minutes, int seconds)
        {
            this.TimeSpan = new TimeSpan(hours, minutes, seconds);
        }
        public ReTimeSpan(int days, int hours, int minutes, int seconds)
        {
            this.TimeSpan = new TimeSpan(days, hours, minutes, seconds);
        }
        public ReTimeSpan(long tics)
        {
            this.TimeSpan = new TimeSpan(tics);
        }
        public ReTimeSpan(int seconds)
        {
            this.TimeSpan = new TimeSpan(0, 0, 0, seconds);
        }
    }
}
