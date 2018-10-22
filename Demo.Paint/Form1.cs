using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.Paint
{
    public partial class Form1 : Form
    {
        private float[] _sin = new float[360];
        private float[] _cos = new float[360];

        public Form1()
        {
            for(int i=0; i<360; ++i)
            {
                _sin[i] = (float) Math.Sin(i * 2 * Math.PI / 360.0);
                _cos[i] = (float) Math.Cos(i * 2 * Math.PI / 360.0);
            }
            
            InitializeComponent();            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {           
            Graphics gr = e.Graphics;

            Point center = new Point(ClientSize.Width / 2, ClientSize.Height / 2);
            int ArrowLength = (ClientSize.Width > ClientSize.Height ?
                ClientSize.Height / 2 : ClientSize.Width / 2) - 30;
            var f = new Font("Arial", 12);
            Brush b = Brushes.Black;
           

            int angle = DateTime.Now.Second * 6 + 270;
            for (int i = 1; i <= 12; i++)
            {
                gr.DrawString(i.ToString(), f, b, new PointF
                {
                    X = - 12 + center.X + (ArrowLength+20) * _cos[(i*30 + 270) % 360],
                    Y = - 12 + center.Y + (ArrowLength+20) * _sin[(i*30 + 270) % 360],
                });
            }
            //Minute dash
            for (int i = 0; i < 360; i += 6 )
            {
               
                gr.DrawLine(Pens.Black, new PointF
                {
                    X = center.X + (ArrowLength -10) * _cos[i % 360],
                    Y = center.Y + (ArrowLength -10) * _sin[i % 360],
                }
                , 
                    
                    new PointF
                {
                    X = center.X + ArrowLength * _cos[i % 360],
                    Y = center.Y + ArrowLength * _sin[i % 360],
                });
            }
            //Hour dash
            for (int i = 0; i < 360; i += 30)
            {

                gr.DrawLine(Pens.Black, new PointF
                {
                    X = center.X + (ArrowLength - 20) * _cos[i % 360],
                    Y = center.Y + (ArrowLength - 20) * _sin[i % 360],
                }
                ,

                    new PointF
                    {
                        X = center.X + ArrowLength * _cos[i % 360],
                        Y = center.Y + ArrowLength * _sin[i % 360],
                    });
            }
            //Second hand
            gr.DrawLine(Pens.Red,
                center,
                new PointF {
                    X=center.X + ArrowLength * _cos[angle % 360],
                    Y = center.Y + ArrowLength * _sin[angle % 360],
                });
            //Minute hand
            int angMin = DateTime.Now.Minute * 6 + 270;
            gr.DrawLine(Pens.Blue,
               center,
               new PointF
               {
                   X = center.X + ArrowLength*4/5 * _cos[angMin % 360],
                   Y = center.Y + ArrowLength*4/5 * _sin[angMin % 360],
               });
            //Hour hand
            int angHr = DateTime.Now.Hour%12 * 30 + 270;
            gr.DrawLine(Pens.Green,
               center,
               new PointF
               {
                   X = center.X + ArrowLength/2 * _cos[angHr % 360],
                   Y = center.Y + ArrowLength/2 * _sin[angHr % 360],
               });
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
