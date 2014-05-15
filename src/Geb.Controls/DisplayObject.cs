using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Geb.Controls
{
    public class DisplayObject
    {
        public double X, Y;
        public double Width, Height;
        public Corners Corners { get; set; }
        public Color BorderColor { get; set; }
        public double BorderThickness = 1;
        public double BorderAlpha { get; set; }

        public Color BackgroundColor = Color.White;
        public double BackgroundAlpha = 1;
        public Bitmap BackgroundImage;

        public Color Color = Color.FromArgb(0);
        public Boolean IsRootDisplayObject;
        protected bool _invalidated = false;

        public Action<DisplayObject> OnInvalidate;

        public virtual void SetInvalidated(Boolean value)
        {
            this._invalidated = value;
        }

        public void Invalidate()
        {
            if (_invalidated == true)
            {
                _invalidated = false;
                if (Parent != null) Parent.Invalidate();
                if (OnInvalidate != null) OnInvalidate(this);
            }
        }

        public Rectangle Rect
        {
            get { return new Rectangle(0, 0, (int)Math.Round(Width), (int)Math.Round(Height)); }
        }

        public Rectangle GlobalRect
        {
            get
            {
                Point pos = this.GetDrawContextPostion().ToPoint();
                Rectangle r = Rect;
                r.X = pos.X;
                r.Y = pos.Y;
                return r;
            }
        }

        public event EventHandler<MouseEventArgs> MouseDown;
        public event EventHandler<MouseEventArgs> MouseUp;
        public event EventHandler<MouseEventArgs> MouseMove;
        public event EventHandler<MouseEventArgs> MouseLeave;
        public event EventHandler<MouseEventArgs> Click;
        public event EventHandler<MouseEventArgs> DoubleClick;

        public virtual void OnMouseEvent(String eventName, MouseEventArgs e)
        {
            switch (eventName)
            {
                case "MouseDown":
                    if(MouseDown != null) MouseDown(this, new MouseEventArgs(e.Button, 0, e.Location.X, e.Location.Y, 0));
                    break;
                case "MouseUp":
                    if (MouseUp != null) MouseUp(this, new MouseEventArgs(e.Button, 0, e.Location.X, e.Location.Y, 0));
                    break;
                case "MouseMove":
                    if (MouseMove != null) MouseMove(this, new MouseEventArgs(e.Button, 0, e.Location.X, e.Location.Y, 0));
                    break;
                case "MouseLeave":
                    if (MouseLeave != null) MouseLeave(this, new MouseEventArgs(e.Button, 0, e.Location.X, e.Location.Y, 0));
                    break;
                case "Click":
                    if (MouseLeave != null) Click(this, new MouseEventArgs(e.Button, 0, e.Location.X, e.Location.Y, 0));
                    break;
                case "DoubleClick":
                    if (MouseLeave != null) Click(this, new MouseEventArgs(e.Button, 0, e.Location.X, e.Location.Y, 0));
                    break;
            }
        }

        public DisplayObject Parent;

        public virtual DisplayObject HitTest(double x, double y)
        {
            PointD pos = GetDrawContextPostion();
            if (x > pos.X && x < pos.X + this.Width && y > pos.Y && y < pos.Y + this.Height) return this;
            else return null;
        }

        public virtual void Create()
        {
        }

        protected virtual PointD GetDrawContextPostion(DisplayObject child)
        {
            PointD pos = GetDrawContextPostion();
            return new PointD { X = pos.X + child.X, Y = pos.Y + child.Y } ;
        }

        protected virtual PointD GetDrawContextPostion()
        {
            if (Parent == null) return IsRootDisplayObject ? new PointD { X = X, Y = Y } : new PointD();
            else return Parent.GetDrawContextPostion(this);
        }

        protected void DrawRectBackground(Graphics g, PointD pos)
        {
            RectangleF r = new RectangleF(pos.ToPointF(), new SizeF((float)Width, (float)Height));
            GraphicsPath borderPath = BuildPath(pos.ToPoint(), Width, Height, Corners);

            // 绘制背景
            if (BackgroundImage == null)
            {
                if (BackgroundAlpha > 0)
                {
                    Byte alpha = (Byte)Math.Min(255, BackgroundAlpha * 255);
                    g.FillPath(new SolidBrush(Color.FromArgb(alpha, BackgroundColor)), borderPath);
                }
            }
            else
            {
                if (Corners.IsAllZeros == true)
                {
                    g.DrawImage(BackgroundImage, r);
                }
                else
                {
                    g.FillPath(new TextureBrush(BackgroundImage), borderPath);
                }
            }

            // 绘制border
            if (BorderAlpha > 0 && BorderThickness > 0)
            {
                g.DrawPath(new Pen(new SolidBrush(BorderColor), (float)BorderThickness), borderPath);
            }
        }

        public virtual void Draw(Graphics g)
        {
            if (_invalidated == true) return;
            _invalidated = true;
        }

        protected GraphicsPath BuildPath(Point point, double width, double height, Corners corners)
        {
            GraphicsPath gp = new GraphicsPath();
            int w = (int)Math.Round(width);
            int h = (int)Math.Round(height);
            if (corners.TopLeftF > 0)
            {
                gp.AddArc(point.X, point.Y, corners.TopLeftF, corners.TopLeftF, 180, 90);
            }
            gp.AddLine(point.X, point.Y, point.X + w, point.Y);
            if (corners.TopRightF > 0)
            {
                gp.AddArc(point.X + w - corners.TopRightF, point.Y, corners.TopRightF, corners.TopRightF, 270, 90);
            }
            gp.AddLine(point.X + w, point.Y, point.X + w, point.Y + h);

            if (corners.BottomRightF > 0)
            {
                gp.AddArc(point.X + w - corners.BottomRightF, point.Y + h - corners.BottomRightF, corners.BottomRightF, corners.BottomRightF, 0, 90);
            }
            gp.AddLine(point.X + w, point.Y + h, point.X, point.Y + h);

            if (corners.BottomLeftF > 0)
            {
                gp.AddArc(point.X, point.Y + h - corners.BottomLeftF, corners.BottomLeftF, corners.BottomLeftF, 90, 90);
            }
            gp.AddLine(point.X, point.Y + h, point.X, point.Y);
            return gp;
        }
    }
}
