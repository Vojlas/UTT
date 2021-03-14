namespace UniversalTimerTool.Model
{
    public class Time
    {
        public double Hours { get; set; }
        public byte Minutes { get; set; }
        public byte Seconds { get; set; }
        public Time() {
            this.Hours = 0;
            this.Minutes = 0;
            this.Seconds = 0;
        }
        public Time(double _hours, byte _minutes, byte _seconds)
        {
            this.Hours = _hours;
            this.Minutes = _minutes;
            this.Seconds = _seconds;
        }
        public Time(string savedTime)
        {
            this.Hours = 0;
            this.Minutes = 0;
            this.Seconds = 0;
        }
        public Time AddSec()
        {
            if ((this.Seconds += 1) < 60) return this;
            if ((this.Minutes += 1) < 60) { this.Seconds = 0; return this; }
            this.Hours++; this.Minutes = 0; this.Seconds = 0; return this;
        }
        public Time Add(Time time)
        {
            //add times together
            this.Seconds += time.Seconds;
            this.Minutes += time.Minutes;
            this.Hours += time.Hours;

            //Redistribute Hours, minutes, seconds according to time rules
            //max addition is 60+59 = 119 -> so add {0,1}
            if (this.Seconds >= 60) { this.Seconds = (byte)(this.Seconds % 60); this.Minutes++; }
            if (this.Minutes >= 60) { this.Minutes = (byte)(this.Minutes % 60); this.Hours++; }
            return this;
        }
        public string Show()
        {
            return this.Hours + ":" + this.Minutes + ":" + this.Seconds;
        }
    }
}