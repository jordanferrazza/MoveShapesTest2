using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveShapesTest2
{
    public static class Global
    {
        public static bool InBounds(Point p, Rectangle r)
        {
            return p.X > r.X && p.Y > r.Y && p.X < r.Width + r.X && p.Y < r.Y + r.Height;
        }
        public static bool InBallPark(Point p1, Point p2, int e)
        {
            return InBounds(p1, new Rectangle(p2.X - e, p2.Y - e, e * 2, e * 2));
        }
        public static bool InBallPark(int p1, int p2, int e)
        {
            return p1 < p2 + e && p1 > p2 - e;
        }
        public readonly static Pen SELECTION_RECTANGLE_PEN = new Pen(Brushes.Gray, 1) { DashPattern = new float[] { 5, 5 } };
        public readonly static Pen SELECTION_ANCHOR_PEN = new Pen(Brushes.Gray, 1) { };
        public readonly static int SELECTION_RECTANGLE_PADDING = 2;
        public readonly static int SELECTION_ANCHOR_PADDING = 5;

    }

}
