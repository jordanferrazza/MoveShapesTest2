using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveShapesTest2
{
    public abstract class Drawing
    {
        //(C) 2024 Jordan Ferrazza


        /// <summary>
        /// subclasses handle draw
        /// </summary>
        /// <param name="g"></param>
        /// <param name="zoom"></param>
        protected abstract void _Draw(Graphics g, float zoom);


        /// <summary>
        /// handle draw
        /// </summary>
        /// <param name="g"></param>
        /// <param name="zoom"></param>
        public virtual void Draw(Graphics g, float zoom)
        {
            Zoom = zoom; //set the zoom field

            //scale the properties to it
            ActualHeight = (int)(Height * zoom);
            ActualWidth = (int)(Width * zoom);
            if (ActualStroke.Width * Zoom != Stroke.Width || ActualStroke.Brush != Stroke.Brush) ActualStroke = new Pen(Stroke.Color, Stroke.Width*Zoom);
            ActualX = (int)(X * zoom);
            ActualY = (int)(Y * zoom);

            //pass down handle to subclass
            _Draw(g, zoom);

            //draw text
            TextRenderer.DrawText(g, Text, Font, new System.Drawing.Rectangle(X, Y, ActualWidth, ActualHeight), TextColor, TextFormat);

            //draw selection rectangle and handle
            if (Selected)
            {
                g.DrawRectangle(Global.SELECTION_RECTANGLE_PEN, ActualX - Global.SELECTION_RECTANGLE_PADDING, ActualY - Global.SELECTION_RECTANGLE_PADDING, ActualWidth + (Global.SELECTION_RECTANGLE_PADDING * 2), ActualHeight + (Global.SELECTION_RECTANGLE_PADDING * 2));
                g.DrawRectangle(Global.SELECTION_ANCHOR_PEN, (ActualX + ActualWidth - Global.SELECTION_ANCHOR_PADDING), (ActualY + ActualHeight - Global.SELECTION_ANCHOR_PADDING), Global.SELECTION_ANCHOR_PADDING * 2, Global.SELECTION_ANCHOR_PADDING * 2);
            }
        }


        /// <summary>
        /// handle being drawn
        /// </summary>
        /// <param name="point"></param>
        public virtual void Create(Point point)
        {
            X = point.X; Y = point.Y;
            Selected = true;
            SizingOverride = true;
        }

        /// <summary>
        /// check if bounds of object, specifying whether to be to-scale or actual
        /// </summary>
        /// <param name="point"></param>
        /// <param name="e"></param>
        /// <param name="visual"></param>
        /// <returns></returns>
        public virtual bool InBounds(Point point, int e, bool visual = true)
        {
            return visual ?
            Global.InBounds(point, new Rectangle(ActualX - e, ActualY - e, ActualWidth + e + e, ActualHeight + e + e))
            :
            Global.InBounds(point, new Rectangle(X - e, Y - e, Width + e + e, Height + e + e));
        }

        //handle mouse click
        public virtual void MouseClick(Point mouse)
        {
            //get the mouse position relative to the object
            var p = PointToClient(mouse);

            //if flagged selected
            if (Selected)
            {
                //if anchor is clicked
                if (Global.InBounds(p, new Rectangle(ActualWidth - Global.SELECTION_ANCHOR_PADDING, ActualHeight - Global.SELECTION_ANCHOR_PADDING, Global.SELECTION_ANCHOR_PADDING * 2, Global.SELECTION_ANCHOR_PADDING * 2)))
                {
                    //flag resizing
                    Sizing = true;
                }
                else
                {

                }
            }
            else //else
            {
                //flag selected
                Selected = true;
            }
        }

        /// <summary>
        /// handle mouse move
        /// </summary>
        /// <param name="mouse"></param>
        /// <param name="e"></param>
        /// <param name="modifierKeys"></param>
        public virtual void MouseMove(Point mouse, MouseEventArgs e, Keys modifierKeys)
        {
            var p = PointToClient(mouse); //get the mouse position relative to the object
            var p2 = new Point((int)(p.X / Zoom), (int)(p.Y / Zoom)); //get the to-scale mouse position
            var max = Math.Max(p.X, p.Y); //turn the mouse position 1D by getting the maximum co-ordination
            var shift = modifierKeys.HasFlag(Keys.Shift); //get whether shift is held down
            var control = modifierKeys.HasFlag(Keys.Control); //get whether ctrl is held down
            if (Selected && (Sizing || SizingOverride)) //if being resized or resized along
            {

                if (shift & !control) // if shift is held down: resize but keep aspect ratio
                {
                    //tick the original size if cleared
                    if (BaseSize == null) BaseSize = new Point(Width, Height);

                    //get the non-null original size
                    var pnt = (Point)BaseSize;

                    if (max == p2.X) //if the max co-ordinate is X
                    {
                        Width = p.X; //change width to X
                        Height = (int)(pnt.Y * ((double)p2.X / pnt.X)); //scale height by X
                    }
                    else //if the max co-ordinate is Y
                    {
                        Width = (int)(pnt.X * ((double)p2.Y / pnt.Y)); //scale height by Y
                        Height = p2.Y; //change width to Y
                    }
                }
                else
                {
                    BaseSize = null; //clear original size
                    if (!control | (control & !shift)) Width = p2.X; //change the width, change only the width if ctrl is down
                    if (!control | (control & shift)) Height = p2.Y; //change the height, change only the height if ctrl+shift is down


                    //if the shape would be equilateral, snap to it
                    if (Global.InBallPark(p.X, p.Y, Global.SELECTION_ANCHOR_PADDING * 2)) //if X == Y +- 10
                    {
                        Width = Height;
                    }
                    else if (Global.InBallPark(p.Y, p.X, Global.SELECTION_ANCHOR_PADDING * 2))
                    {
                        Height = Width;
                    }

                }


            }
            else if (Selected && e.Button == MouseButtons.Left) //if not being resized
            {

                //tick the original location if cleared
                if (BaseLoc == null) BaseLoc = p;

                {
                    if (!control | (control & !shift)) X = (int)((mouse.X - ((Point)BaseLoc).X) / Zoom);//change X, change only X if ctrl is down
                    if (!control | (control & shift)) Y = (int)((mouse.Y - ((Point)BaseLoc).Y) / Zoom);//change Y, change only Y if ctrl+shift is down
                }

            }
        }

        /// <summary>
        /// handle mouse up
        /// </summary>
        /// <param name="mouse"></param>
        public virtual void MouseUp(Point mouse)
        {

            //reset all flags
            Sizing = false;
            SizingOverride = false;
            BaseLoc = null;
            BaseSize = null;
        }

        /// <summary>
        /// set the Point to one relative to this shape, specifying whether to be to-scale or actual
        /// </summary>
        /// <param name="point"></param>
        /// <param name="visual"></param>
        /// <returns></returns>
        public virtual Point PointToClient(Point point, bool visual = true)
        {
            return visual ?
                new Point(point.X - ActualX, point.Y - ActualY)
                :
                new Point(point.X - X, point.Y - Y);
        }
        private Point? BaseLoc;
        private Point? BaseSize;
        public bool Sizing
        {
            get;
            set;
        } = false;
        public bool SizingOverride
        {
            get;
            set;
        } = false;
        public bool Selected
        {
            get;
            set;
        } = false;

        protected float Zoom;

        public Brush BackColor { get; set; } = Brushes.Blue;
        public Pen Stroke { get; set; } = new Pen(Brushes.Black, 2);
        public Pen ActualStroke { get; set; } = new Pen(Brushes.Black, 2);
        public Color TextColor { get; set; }
        public Font Font { get; set; } = new Font("Arial", 10);
        public TextFormatFlags TextFormat { get; set; } = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
        public string Text = "";
        public int Width { get; protected set; }
        protected int ActualWidth;
        public int Height { get; protected set; }
        protected int ActualHeight;
        public int X { get; protected set; }
        protected int ActualX;
        public int Y { get; protected set; }
        protected int ActualY;
        public Drawing(int x, int y, int w, int h)
        {
            X = x;
            Y = y;
            Width = w; Height = h;
        }
    }

    public class Rect : Drawing
    {
        public Rect(int x, int y, int w, int h) : base(x, y, w, h)
        {
        }

        protected override void _Draw(Graphics g, float zoom)
        {
            g.FillRectangle(BackColor, ActualX, ActualY, ActualWidth, ActualHeight);
            g.DrawRectangle(ActualStroke, ActualX, ActualY, ActualWidth, ActualHeight);

        }
    }

    public class Ellipse : Drawing
    {
        public Ellipse(int x, int y, int w, int h) : base(x, y, w, h)
        {
            BackColor = Brushes.Red;
        }

        protected override void _Draw(Graphics g, float zoom)
        {
            g.FillEllipse(BackColor, ActualX, ActualY, ActualWidth, ActualHeight);
            g.DrawEllipse(ActualStroke, ActualX, ActualY, ActualWidth, ActualHeight);

        }
    }
}
