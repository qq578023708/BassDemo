using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BassDemo
{
    public class FFTDialg:Control
    {
        private int[] _fftData = null;
        private Color drawColor = Color.Red;
        private int bottom1 = 0;
        private int bottom2 = 0;
        private int bottom2Height = 10;
        private int bottom1Width = 0;
        private int bottomCellWidth = 2; //柱状条间隔
        public FFTDialg()
        {
            
        }

        public int[] FFTData
        {
            get
            {
                return _fftData;
            }
            set
            {
                _fftData = value;
                ReSetSize();
                Invalidate();
            }
        }
        
        public Color DrawColor
        {
            get
            {
                return drawColor;
            }
            set
            {
                drawColor = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            
            
            Graphics g = e.Graphics;
            
            if (_fftData != null)
            {

                g.FillRectangle(Brushes.Green, new Rectangle(0, bottom2, Width, bottom2Height));
                for(int i = 0; i < _fftData.Length/2 - 21; i++)
                {
                    //g.DrawLine(, new Point(i*2, bottom1), new Point(i*2, bottom1 - _fftData[i]));
                    g.FillRectangle(new SolidBrush(drawColor), new Rectangle(i * (bottom1Width+ bottomCellWidth), bottom1 - _fftData[i], bottom1Width, _fftData[i]));
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ReSetSize();
        }

        private void ReSetSize()
        {
            this.BackColor = Color.Black;
            bottom2 = this.Height - bottom2Height;
            bottom1 = bottom2-1;
            bottom1Width = _fftData != null ? this.Width /(_fftData.Length/2-21)-bottomCellWidth : 2;
        }
    }
}
