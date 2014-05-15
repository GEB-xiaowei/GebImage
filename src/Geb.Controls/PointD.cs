﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Geb.Controls
{
    public struct PointD
    {
        public double X;
        public double Y;

        public PointD(double x, double y) 
        {
            this.X = x;
            this.Y = y;
        }

        public PointF ToPointF()
        {
            return new PointF((float)X, (float)Y);
        }

        public Point ToPoint()
        {
            return new Point((int)Math.Round(X), (int)Math.Round(Y));
        }
    }
}
