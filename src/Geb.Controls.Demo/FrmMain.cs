using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Geb.Controls.Demo
{
    public partial class FrmMain : Form
    {
        public int X=200;
        public int Y=10;
        public int W=200;
        public int H=70;

        public FrmMain()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            for (int i = 0; i < 5; i++)
            {
                MonitorControl item = new MonitorControl();
                item.X = i * 110;
                item.Id = (i+1).ToString();
                container.Add(item);
            }
            for (int i = 0; i < 1000; i++)
            {
                Button btn = new Button(6*i, 40, 5, 20);
                this.container.Add(btn);                
            }
            this.container.InitEvents();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            foreach (DisplayObject item in container.DisplayObjects)
            {
                item.X += 1;
            }
            container.Invalidate();
        }
    }
}