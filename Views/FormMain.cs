using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using WindowsFormsApp1.Controllers;

namespace WinForms_AlarmClock
{
    public partial class FormMain : Form
    {
        private readonly string ALARMCLOCKS_PATH;
        private readonly AlarmClockController _alarmClockController;

        public FormMain()
        {
            InitializeComponent();

            ALARMCLOCKS_PATH = AppSettings.AlarmFilePath;
            _alarmClockController = new AlarmClockController(ALARMCLOCKS_PATH);

            AddAlarmClockInListView(_alarmClockController.AlarmClocks);

            timerCheckTime.Tick += CheckAlarmClockTime;
            timer1.Start();
        }

        private void CheckAlarmClockTime(object sender, EventArgs e)
        {
            foreach (var currentAlarmClock in _alarmClockController.AlarmClocks)
                _alarmClockController.CheckTime(currentAlarmClock);
        }

        private void AddAlarmClockInListView(List<AlarmClock> alarmClocks)
        {
            foreach (var alarmClock in alarmClocks)
            {
                int index = listViewAlarm.Items.Add(alarmClock.Message).Index;

                string longTime = alarmClock.TimeAlarm.ToLongTimeString();
                string longDate = alarmClock.TimeAlarm.ToLongDateString();

                listViewAlarm.Items[index].SubItems.Add($"{longTime} - {longDate}");
                listViewAlarm.Items[index].Checked = true;
                listViewAlarm.Items[index].Tag = alarmClock;
            }
        }

        private void InvokeCreationForm(object sender, EventArgs e)
        {
            FormAddAlarmClock formNewAlarm = new FormAddAlarmClock();

            if (formNewAlarm.ShowDialog() == DialogResult.OK)
            {
                AlarmClock alarmClock = formNewAlarm.GetAlarmClock();
                _alarmClockController.AddAndSaveAlarmClock(alarmClock);
                AddAlarmClockInListView(new List<AlarmClock> { alarmClock });

                timerCheckTime.Start();
            }
        }

        private void DeleteAlarm(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ви дійсно хочете видалити даний будильник?", "Увага",
                MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (listViewAlarm.Items.Count < 1)
                {
                    MessageBox.Show("Не обраний будильник, який потрібно видалити", "Увага");
                    timerCheckTime.Stop();
                }
                foreach (ListViewItem currentItem in listViewAlarm.SelectedItems)
                {
                    currentItem.Remove();
                    _alarmClockController.RemoveAlarmClock(currentItem.Tag);
                }
            }
        }
    }
}