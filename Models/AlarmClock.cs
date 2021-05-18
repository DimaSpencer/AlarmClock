using System;

namespace WinForms_AlarmClock
{
    public class AlarmClock
    {
        //Автоматичні властивості:
        public string Message { get; set; }
        public string MusicFile { get; set; }
        public DateTime TimeAlarm { get; set; }

        public AlarmClock()
        {
            Message = string.Empty;
            MusicFile = string.Empty;
        }
        public AlarmClock(string message, string musicFile, DateTime timeAlarm)//конструктор з параметрами
        {
            Message = message;
            MusicFile = musicFile;
            TimeAlarm = timeAlarm;
        }
    }
}