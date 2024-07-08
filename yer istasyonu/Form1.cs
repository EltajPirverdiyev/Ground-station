using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;



namespace yer_istasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        FilterInfoCollection fic;
        VideoCaptureDevice vcd;
        VideoCaptureDevice vcd1;

        private void btnCamera1_Click(object sender, EventArgs e)
        {
            vcd = new VideoCaptureDevice(fic[comboBox1.SelectedIndex].MonikerString);
            vcd.NewFrame += VideoCaptureDevice_NewFrame;
            vcd.Start();
        }

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pictureBox1.Image = bitmap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in fic)
            {
                comboBox1.Items.Add(filterInfo.Name);
                comboBox2.Items.Add(filterInfo.Name);
            }
            comboBox1.SelectedIndex = 0;
            vcd = new VideoCaptureDevice();
        }
        private void btnCamera2_Click(object sender, EventArgs e)

        {
            vcd1 = new VideoCaptureDevice(fic[comboBox2.SelectedIndex].MonikerString);
            vcd1.NewFrame += VideoCaptureDevice_NewFrame1;
            vcd1.Start();
        }
        private void VideoCaptureDevice_NewFrame1(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pictureBox2.Image = bitmap;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(vcd.IsRunning == true)
            {
                vcd.Stop();
            }
            if (vcd1.IsRunning == true)
            {
                vcd1.Stop();
            }
        }

        private void btnStop1_Click(object sender, EventArgs e)
        {
            if (vcd != null && vcd.IsRunning)
            {
                vcd.SignalToStop();
                vcd.WaitForStop();
                pictureBox1.Image = null; // pictureBox1'i temizleme (isteğe bağlı)
            }
        }

        private void btnStop2_Click(object sender, EventArgs e)
        {
            if (vcd1 != null && vcd1.IsRunning)
            {
                vcd1.SignalToStop();
                vcd1.WaitForStop();
                pictureBox2.Image = null; // pictureBox1'i temizleme (isteğe bağlı)
            }
        }
    
    }
}
