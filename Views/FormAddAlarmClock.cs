using System;
using System.Windows.Forms;

namespace WinForms_AlarmClock
{
    public partial class FormAddAlarmClock : Form
    {
        public string FilePath { get; set; }
        public FormAddAlarmClock()
        {
            InitializeComponent();
            DateTime currentDate = DateTime.Now.AddMinutes(1.0d);

            numericUpDownHour.Value = currentDate.Hour;
            numericUpDownMinute.Value = currentDate.Minute;
            numericUpDownSeconds.Value = 0;
            dateTimePicker.Value = currentDate;
        }

        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            if (openDialog.ShowDialog(this) == DialogResult.OK)
            {
                FilePath = textBoxFilePath.Text = openDialog.FileName;
            }
        }

        public AlarmClock GetAlarmClock()
        {
            string message = richTextBoxMessage.Text;

            DateTime date = new DateTime(
                this.dateTimePicker.Value.Year,
                this.dateTimePicker.Value.Month,
                this.dateTimePicker.Value.Day,
                (int)numericUpDownHour.Value,
                (int)numericUpDownMinute.Value,
                (int)numericUpDownSeconds.Value);

            AlarmClock alarmClock = new AlarmClock(message, FilePath, date);

            return alarmClock;
        }
    }
}