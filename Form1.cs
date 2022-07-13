using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;


namespace Checkin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        VideoCaptureDevice Vdodevice;
        FilterInfoCollection FiltersInfo;

        void StartCamera()
        {
            try
            {
                FiltersInfo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                Vdodevice = new VideoCaptureDevice(FiltersInfo[0].MonikerString);
                Vdodevice.NewFrame += new NewFrameEventHandler(Camera_On);
                Vdodevice.Start();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void Camera_On(object sender, NewFrameEventArgs eventArgs)
        {
            pic1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Vdodevice.Stop();
            }
            catch
            {
                return;
            }
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            StartCamera();
        }

        private void Capture_btn_Click(object sender, EventArgs e)
        {
            pic2.Image = pic1.Image;
            string filename = @"C:\Image\" + System.DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss_tt") + ".jpg";

            var bitmap = new Bitmap(pic2.Width, pic2.Height);
            pic2.DrawToBitmap(bitmap, pic2.ClientRectangle);
            System.Drawing.Imaging.ImageFormat imageFormat = null;
            imageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
            bitmap.Save(filename, imageFormat);
        }
    }
}
