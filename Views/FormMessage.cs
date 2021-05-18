using System;
using System.Windows.Forms;
using WMPLib;

namespace WinForms_AlarmClock
{
    public partial class FormMessage : Form
    {
        private readonly WindowsMediaPlayer _mediaPlayer;
        public FormMessage(AlarmClock alarmClock)
        {
            _mediaPlayer = new WindowsMediaPlayer() { URL = alarmClock.MusicFile };
            _mediaPlayer.settings.volume = 100;

            InitializeComponent();
            lblTextMsg.Text = alarmClock.Message;

            _mediaPlayer.controls.play();
        }
        private void FormMessage_FormClosing(object sender, FormClosingEventArgs e)
        {
            _mediaPlayer.controls.stop();
        }
        private void CloseForm(object sender, EventArgs e)
        {
            _mediaPlayer.controls.stop();
            Close();
        }
    }
}