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
        private int[] _topData = null;
        private Color drawColor = Color.Red;
        private int bottom1 = 0;
        private int bottom2 = 0;
        private int bottom2Height = 10;
        private int bottom1Width = 0;
        private int bottomCellWidth = 2; //柱状条间隔
        const int cells = 42; //柱状数量
        const int topRectSpeed = 4; //顶部下落速度
        private int topRectHeight = 1;
        private int currentPlayValue = 0;
        private Random rd = null;
        public FFTDialg()
        {
            rd = new Random();
            _topData = new int[cells];
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            
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
                bottom1Width = _fftData != null ? this.Width / cells - bottomCellWidth : 2;
                g.FillRectangle(Brushes.Green, new Rectangle(0, bottom2, Width, bottom2Height));
                for(int i = 0; i < cells; i++)
                {
                    if (_fftData[i] > bottom1) _fftData[i] = bottom1-topRectHeight;
                    if(_topData[i]<= bottom1) _topData[i] += topRectSpeed;
                    
                    if (bottom1 - _fftData[i]- topRectHeight <= _topData[i]) _topData[i] = bottom1 - _fftData[i]- topRectHeight;

                    Rectangle zhuRect = new Rectangle(i * (bottom1Width + bottomCellWidth), bottom1 - _fftData[i], bottom1Width, _fftData[i]);
                    Rectangle topRect = new Rectangle(i * (bottom1Width + bottomCellWidth),  _topData[i], bottom1Width, topRectHeight);
                    g.FillRectangle(new SolidBrush(drawColor), zhuRect);
                    g.FillRectangle(Brushes.Yellow, topRect);
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
            
            for (int i = 0; i < cells; i++)
            {
                _topData[i] = bottom1;
            }
        }
    }
}
