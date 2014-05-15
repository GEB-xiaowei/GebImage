using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Geb.Controls
{
    public class Label : DisplayObject
    {
        public String Text { get; set; }
        public TextAlign Align;
        public TextAlign LineAilgn;
        public Color FontColor = Color.Black;
        public Font Font = new Font("微软雅黑", 10);

        public Label() 
        { 
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            if (Text == null) return;
            if (Width > 0 && Height > 0)
            {
                PointD p0 = this.GetDrawContextPostion();
                g.DrawString(Text, Font, new SolidBrush(FontColor), GlobalRect, GetStringFormat());
            }
        }

        StringFormat GetStringFormat() 
        {
            StringFormat sf = new StringFormat();
            switch (Align)
            {
                case TextAlign.Left:
                    sf.Alignment = StringAlignment.Near;
                    break;
                case TextAlign.Right:
                    sf.Alignment = StringAlignment.Far;
                    break;
                case TextAlign.Center:
                    sf.Alignment = StringAlignment.Center;
                    break;
                default:
                    break;
            }
            switch (LineAilgn)
            {
                case TextAlign.Left:
                    sf.LineAlignment = StringAlignment.Near;
                    break;
                case TextAlign.Right:
                    sf.LineAlignment = StringAlignment.Far;
                    break;
                case TextAlign.Center:
                    sf.LineAlignment = StringAlignment.Center;
                    break;
                default:
                    break;
            }

            return sf;
        }

    }
}