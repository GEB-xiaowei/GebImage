using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace Geb.Controls
{
    public class GebContainer : Control
    {
        public GebContainer()
        {
            this.DoubleBuffered = true;
        }

        public List<DisplayObject> DisplayObjects = new List<DisplayObject>();
        public Point _lastMouseLocation = new Point(-1,-1);

        public DisplayObject HitTest(double x, double y)
        {
            foreach (DisplayObject item in DisplayObjects)
            {
                DisplayObject m = item.HitTest(x, y);
                if (m != null) return m;
            }
            return null; 
        }

        public void Add(DisplayObject element)
        {
            if (element == null) throw new ArgumentNullException("element");
            if (Controls == null) DisplayObjects = new List<DisplayObject>();
            element.IsRootDisplayObject = true;
            element.OnInvalidate = (DisplayObject item) => { this.Invalidate(); };
            element.Create();
            DisplayObjects.Add(element);
        }

        public void Remove(DisplayObject element)
        {
            if (element == null) throw new ArgumentNullException("element");
            if (Controls != null && this.DisplayObjects.Contains(element) == true)
            {
                element.Parent = null;
                DisplayObjects.Remove(element);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach (DisplayObject item in DisplayObjects)
            {
                item.SetInvalidated(false);
                item.Draw(g);
            }
        }

        public void InitEvents()
        {
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GebContainer_MouseDown);
            this.MouseUp += GebContainer_MouseUp;
            this.MouseMove += GebContainer_MouseMove;
        }


        void GebContainer_MouseMove(object sender, MouseEventArgs e)
        {
            //DisplayObject find = HitTest(e.Location.X, e.Location.Y);
            //if (find != null)
            //{
            //    find.OnMouseEvent("MouseMove", e);
            //    this._lastMouseLocation = e;
            //}
            //else { 
            //    if(_lastMouseLocation!=null)
            //    {
            //        DisplayObject find2 = HitTest(_lastMouseLocation.Location.X, _lastMouseLocation.Location.Y);
            //        find2.OnMouseEvent("MouseLeave", _lastMouseLocation);
            //        this._lastMouseLocation=null;
            //    }
            //    this._lastMouseLocation = null;
            //}
        }

       private  void GebContainer_MouseUp(object sender, MouseEventArgs e)
        {
            DisplayObject find = HitTest(e.Location.X, e.Location.Y);
            if (find != null)
            {
                find.OnMouseEvent("MouseUp", e);
            }
        }

        private void GebContainer_MouseDown(object sender, MouseEventArgs e)
        {
            DisplayObject find = HitTest(e.Location.X, e.Location.Y);
            if (find != null)
            {
                find.OnMouseEvent("MouseDown", e);
            }
        }


        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }
    }
}
