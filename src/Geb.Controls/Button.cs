using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Geb.Controls
{
    public class Button : Container
    {
        public String Text { get; set; }
        public Font Font;
        public TextAlign Align;
        public TextAlign LineAlign;
        public Color FontColor = Color.Black;

        private Label label=new Label();

        public Button(double x, double y, double width, double heith,string text,Font font,TextAlign align,TextAlign lineAlign,Color color)
        {
            IsChildrenMouseEnable = false;
            this.X = x; this.Y = y; this.Width = width; this.Height = heith;

            this.Text = text;
            this.Font = font;
            this.FontColor = color;
            this.Align = align;this.LineAlign = lineAlign;
        }


        public Button(double x,double y,double width,double heith) 
        {
            IsChildrenMouseEnable = false;
            this.X = x; this.Y = y; this.Width = width; this.Height = heith;
        }

        public override void Create()
        {
            label.Width = Width;
            label.Height = Height;
            label.Align = this.Align;
            label.LineAilgn = this.LineAlign;
            label.Font = this.Font;
            label.FontColor = this.FontColor;
            label.Text = this.Text;
            this.Add(label);
        }



    }
}