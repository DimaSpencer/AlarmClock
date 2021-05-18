using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WindowsFormsApp1.Model;
using WinForms_AlarmClock;
using WMPLib;

namespace WindowsFormsApp1.Controllers
{
    public class AlarmClockController
    {
        public WindowsMediaPlayer MediaPlayer { get; set; }
        public List<AlarmClock> AlarmClocks { get; set; }
        public FileManager<List<AlarmClock>> FileManager { get; set; }

        public AlarmClockController(string path)
        {
            MediaPlayer = new WindowsMediaPlayer();
            FileManager = new FileManager<List<AlarmClock>>(AppSettings.AlarmFilePath);

            AlarmClocks = FileManager.LoadObjectFromFile();
        }

        public void AddAndSaveAlarmClock(AlarmClock alarmClock)
        {
            AlarmClocks.Add(alarmClock);
            FileManager.UpdateFile(AlarmClocks);
        }

        public void RemoveAlarmClock(object alarmClock)
        {
            var alarmC = AlarmClocks.FirstOrDefault(a => a == alarmClock);

            if (alarmC != null)
            {
                AlarmClocks.Remove(alarmC);
                FileManager.UpdateFile(AlarmClocks);
            }
        }

        public void InvokeMessage(AlarmClock alarmClock)
        {
            if (File.Exists(alarmClock.MusicFile))
            {
                MediaPlayer.URL = alarmClock.MusicFile;
                MediaPlayer.settings.volume = 100;
            }

            FormMessage frmMsg = new FormMessage(alarmClock);
            frmMsg.Show();
        }

        public void CheckTime(AlarmClock alarmClock)
        {
            DateTime currentTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            if (alarmClock.TimeAlarm.CompareTo(currentTime) == 0)
            {
                InvokeMessage(alarmClock);
            }
        }
    }
}
