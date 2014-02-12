using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace CalarSaat
{
    public partial class Index : Form
    {
        int SAAT = 60 * 60;
        int DAKIKA = 60;
        int hour, minute, total, alarmsound;

        string mesaj;

        public Index()
        {
            InitializeComponent();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            hour = Convert.ToInt32(cmbHour.SelectedIndex);
            minute = Convert.ToInt32(cmbMinute.SelectedIndex);
            alarmsound = Convert.ToInt32(cmbAlarmSound.SelectedIndex);
            total = (DAKIKA * minute) + (SAAT * hour);
            if ((hour == 0 || hour == -1) && (minute == 0 || minute == -1))
            {
                MessageBox.Show("Bu işlemi gerçekleştiremezsiniz.");
            }
            else
            {
                mesaj = "Alarm ayarlandı...";
                
                lblStatus.Text = mesaj;
                timer1.Start();
                btnSet.Enabled = false;
                btnCancel.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                timer1.Stop();
                lblTime.Text = "00:00";
                lblStatus.Text = "İşleminiz iptal edildi";
                btnSet.Enabled = true;
                btnCancel.Enabled = false;
            }
            catch (Exception genelHata)
            {
                MessageBox.Show("Hata Oluştu.\n" + genelHata.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int hour_now, minute_now, second_now;
            hour_now = (total) / SAAT;
            minute_now = (total - (hour_now * SAAT)) / DAKIKA;
            second_now = (total - (hour_now * SAAT) - (minute_now * DAKIKA));

            if (total < 300)
            {
                lblTime.ForeColor = Color.DarkRed;
            }
            else
            {
                lblTime.ForeColor = Color.Black;
            }

            lblTime.Text = hour_now.ToString("D2") + ":" + minute_now.ToString("D2") + ":" + second_now.ToString("D2");
            total--;
            if (total == 0)
            {
                timer1.Stop();
                try
                {
                    Stream str = str = Properties.Resources.alarm01;

                    if (alarmsound == 0) { str = Properties.Resources.alarm01; }
                    else if (alarmsound == 1) { str = Properties.Resources.alarm02; }
                    else if (alarmsound == 2) { str = Properties.Resources.alarm03; }

                    SoundPlayer snd = new SoundPlayer(str);
                    snd.Play();

                    btnSet.Enabled = true;
                    btnCancel.Enabled = false;
                }
                catch (Exception genelHata)
                {
                    MessageBox.Show("Hata Oluştu.\n" + genelHata.Message);
                }
            }
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void hakkımızdaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About abt = new About();
            abt.Show();
        }
    }
}
