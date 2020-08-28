using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Un4seen.Bass;

namespace BassDemo
{
    public partial class Form1 : Form
    {
        int streamHandle = 0;
        int BandsCount = 128;
        int Rate = 1000;
        Timer t = new Timer();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if( Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_LATENCY, this.Handle))
            {
                BASS_INFO info = new BASS_INFO();
                Bass.BASS_GetInfo(info);
            }
            else
            {
                MessageBox.Show("BASS初始化失败!");
                return;
            }
            
            t.Interval = 50;
            t.Tick += T_Tick;
            t.Start();
            streamHandle= Bass.BASS_StreamCreateFile("1.mp3", 0, 0, BASSFlag.BASS_SAMPLE_FLOAT | BASSFlag.BASS_STREAM_PRESCAN);
            
        }

        private void T_Tick(object sender, EventArgs e)
        {
            if (Bass.BASS_ChannelIsActive(streamHandle) != BASSActive.BASS_ACTIVE_PLAYING) return;
            int[] nfftData = Array.ConvertAll<float, int>(GetFFTData(), delegate (float f)
               {
                   return Convert.ToInt32(Math.Round(f*Rate));
               });
            fftDialg1.FFTData = nfftData;

        }

        public float[] GetFFTData()
        {
            float[] fft = new float[BandsCount];
            Bass.BASS_ChannelGetData(streamHandle, fft, (int)BASSData.BASS_DATA_FFT256);
            return fft;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            t.Stop();
            Bass.BASS_Stop();
            Bass.BASS_Free();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (streamHandle != 0)
            {

                Bass.BASS_ChannelPlay(streamHandle, true);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (streamHandle != 0)
            {

                Bass.BASS_ChannelStop(streamHandle);
            }
        }
    }
}
