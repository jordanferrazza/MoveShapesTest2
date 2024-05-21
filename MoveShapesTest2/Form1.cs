using Microsoft.VisualBasic.Devices;
using StuffProject.Tools;
using System.Collections.ObjectModel;
using System.Drawing.Text;
using System.Media;

namespace MoveShapesTest2
{
    public partial class Form1 : Form
    {

        Pen pen = new Pen(Brushes.Gray, 1);
        Pen pen2 = new Pen(Color.Gray, 1) { DashPattern = new float[] { 5, 5 } };


        public Form1()
        {
            InitializeComponent();
            Drawings.Add(new Rect(10, 10, 200, 100));
            Drawings.Add(new Ellipse(20, 20, 200, 100));

            foreach (var item in new InstalledFontCollection().Families)
            {
                cbFont.Items.Add(item.Name);
            }
            foreach (var item in new int[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 })
            {
                cbSize.Items.Add(item);
            }
            foreach (var item in new string[] { "Left", "Centre", "Right" })
            {
                cbAlign.Items.Add(item);
            }
            foreach (var item in new string[] { "Top", "Middle", "Bottom" })
            {
                cbVAlign.Items.Add(item);
            }
            foreach (var item in new int[] { 0, 1, 2, 3, 4, 6, 8, 10, 15, 20 })
            {
                cbWeight.Items.Add(item);
            }
            ZoomDrawing(0);
            UpdateInterface();
            pictureBox1.Focus();
            pictureBox1.MouseWheel += PictureBox1_MouseWheel;
            panel1.MouseWheel += PictureBox1_MouseWheel;
            SelectedDrawings.CollectionChanged += SelectedDrawings_CollectionChanged;

        }

        private void SelectedDrawings_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateInterface();
        }

        private void PictureBox1_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (ModifierKeys != Keys.Control) return;

            if (e.Delta < 0)
            {
                ZoomDrawing(0.25f);
            }
            if (e.Delta > 0)
            {
                ZoomDrawing(-0.25f);
            }
        }



        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var item in Drawings)
            {
                item.Draw(e.Graphics, Zoom);
            }
        }

        ObservableCollection<Drawing> SelectedDrawings = new ObservableCollection<Drawing>();
        List<Drawing> Drawings = new List<Drawing>();

        private void pictureBox1_Click(object sender, EventArgs e)
        {



        }

        enum MoveMode
        {
            None, Move, NW, NE, SE, SW, N, E, S, W
        }

        MoveMode mode = MoveMode.None;
        Point mouse = new Point(0, 0);
        Point CanvasBaseLoc = new Point(0, 0);




        List<Drawing> oldSelection;
        List<Drawing> oldDrawing;

        System.Drawing.Rectangle oldBounds;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var pos2 = panel1.PointToClient(MousePosition);

                CanvasBaseLoc = new Point(pos2.X - pictureBox1.Left, pos2.Y - pictureBox1.Top);


                return;
            }
            var sels = Drawings.Where(x => x.Selected);
            var things = Drawings.FindAll(x => x.InBounds(e.Location, Global.SELECTION_ANCHOR_PADDING));
            var things2 = things.Where(x => x.Selected);
            var origin = sels.FirstOrDefault(x => x.Sizing) ?? things.LastOrDefault();

            if (NewDrawing != null)
            {
                Drawings.Add(NewDrawing);
                NewDrawing.Create(MousePosition);
                NewDrawing = null;
                pictureBox1.Invalidate();
                return;
            }

            if (!Drawings.Any(x => things.Contains(x) && x.Selected))
            {
                if (ModifierKeys.HasFlag(Keys.Shift))
                {
                    Drawings.ForEach(x => x.Selected |= x == things.LastOrDefault());

                }
                else
                {
                    Drawings.ForEach(x => x.Selected = x == things.LastOrDefault());

                }

            }
            else
            {
                sels.ToList().ForEach(x =>
                {
                    var p = e.Location;

                    p.Offset(x.X - origin.X, x.Y - origin.Y);

                    x.MouseClick(p);

                });
            }
            pictureBox1.Invalidate();
            if (!SelectedDrawings.SequenceEqual(sels))
            {
                SelectedDrawings.Clear();
                foreach (var item in sels)
                {
                    SelectedDrawings.Add(item);
                }
            }


            var sizing = SelectedDrawings.Any(x => x.Sizing);

            if (sizing)
                foreach (var item in SelectedDrawings)
                {
                    item.SizingOverride = true;
                }

        }

        float Zoom = 1;

        /// <summary>
        /// zoom or hard-refresh the canvas
        /// </summary>
        /// <param name="v"></param>
        private void ZoomDrawing(float v)
        {
            if (v != 0) Zoomed = true;

            Zoom *= 1 + v;

            var oldW = pictureBox1.Width;
            var oldH = pictureBox1.Height;

            pictureBox1.Width = (int)(800 * Zoom);
            pictureBox1.Height = (int)(400 * Zoom);
            pictureBox1.Left -= (int)(pictureBox1.Width - oldW) / 2;
            pictureBox1.Top -= (int)(pictureBox1.Height - oldH) / 2;

            pictureBox1.Invalidate();
        }


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //if panning
            if (e.Button == MouseButtons.Right)
            {
                var pos2 = panel1.PointToClient(MousePosition);
                Zoomed = true;

                pictureBox1.Top = pos2.Y - CanvasBaseLoc.Y;
                pictureBox1.Left = pos2.X - CanvasBaseLoc.X;
                return;
            }

            //get the selected drawings
            var things = Drawings.FindAll(x => x.Selected);
            //get the drawings of which are being resized
            var origin = things.FirstOrDefault(x => x.Sizing) ?? things.LastOrDefault();

            //if the mouse is over the resize anchor
            if (Drawings.Any(x => x.Sizing || Global.InBounds(e.Location, new Rectangle((int)(((x.Width + x.X) * Zoom) - Global.SELECTION_ANCHOR_PADDING), (int)(((x.Y + x.Height) * Zoom) - Global.SELECTION_ANCHOR_PADDING), Global.SELECTION_ANCHOR_PADDING * 2, Global.SELECTION_ANCHOR_PADDING * 2))))
            {
                Cursor = Cursors.SizeNWSE;
            }
            //if the mouse is over a selected object
            else if (things.Any(x => x.InBounds(e.Location, 0)))
            {
                Cursor = Cursors.SizeAll;
            }
            //if the mouse is over AN object
            else if (Drawings.Any(x => x.InBounds(e.Location, 0)))
            {
                Cursor = Cursors.Hand;
            }
            //else
            else
            {
                Cursor = Cursors.Default;

            }

            //if the selection is not empty
            if (things.Count > 0)
            {
                //for each selection
                things.ToList().ForEach(x =>
                {
                    //get the location of the mouse within this object relative to the main selection
                    var p = e.Location;
                    p.Offset(x.X - origin.X, x.Y - origin.Y);

                    //handle mouse move
                    x.MouseMove(p, e, ModifierKeys);

                });

                //refresh canvas
                if (e.Button != MouseButtons.None) pictureBox1.Invalidate();
            }




        }


        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            var things = Drawings.FindAll(x => x.Selected);

            things.ForEach(x => x.MouseUp(e.Location));

        }

        /// <summary>
        /// update the GUI state
        /// </summary>
        private void UpdateInterface()
        {

            updatingFont = true; //begin update

            //check if selection exists
            var sel = SelectedDrawings.Count > 0;
            //if selection exists, enable the GUI
            cbFont.Enabled = sel;
            cbSize.Enabled = sel;
            cbAlign.Enabled = sel;
            cbVAlign.Enabled = sel;
            bBold.Enabled = sel;
            bUnderline.Enabled = sel;
            bItalic.Enabled = sel;
            bFillColour.Enabled = sel;
            bOutlineColour.Enabled = sel;
            bTextColour.Enabled = sel;
            cbWeight.Enabled = sel;

            if (SelectedDrawings.Count == 1) //if selection is singular, change to that
            {
                cbFont.Text = SelectedDrawings[0].Font.Name;
                cbSize.Text = SelectedDrawings[0].Font.Size.ToString();
                cbAlign.Text = SelectedDrawings[0].TextFormat.Get(x =>
                {
                    if ((x & TextFormatFlags.HorizontalCenter) > 0) return "Centre";
                    if ((x & TextFormatFlags.Right) > 0) return "Right";
                    return "Left";
                });
                cbVAlign.Text = SelectedDrawings[0].TextFormat.Get(x =>
                {
                    if ((x & TextFormatFlags.VerticalCenter) > 0) return "Middle";
                    if ((x & TextFormatFlags.Bottom) > 0) return "Bottom";
                    return "Top";
                });
                bBold.Checked = SelectedDrawings[0].Font.Bold;
                bUnderline.Checked = SelectedDrawings[0].Font.Underline;
                bItalic.Checked = SelectedDrawings[0].Font.Italic;
                bTextColour.Tag = new SolidBrush(SelectedDrawings[0].TextColor);
                bFillColour.Tag = SelectedDrawings[0].BackColor;
                bOutlineColour.Tag = SelectedDrawings[0].Stroke.Brush;
                cbWeight.Text = SelectedDrawings[0].Stroke.Width.ToString();
                bTextColour.Invalidate();
                bFillColour.Invalidate();
                bOutlineColour.Invalidate();
            }
            else if (SelectedDrawings.Count > 1) //if selection is ambiguous, change to nothing
            {
                cbFont.Text = "";
                cbSize.Text = "";
                cbAlign.Text = "";
                cbVAlign.Text = "";
                bBold.Checked = false;
                bUnderline.Checked = false;
                bItalic.Checked = false;
            }
            //else do nothing

            updatingFont = false; //end update
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {



            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {


        }
        FormWindowState state;

        private void pictureBox1_Move(object sender, EventArgs e)
        {


            if (WindowState == FormWindowState.Maximized)
            {

                timer1.Start();
            }

        }

        bool updatingFont = false;

        /// <summary>
        /// update the drawing options
        /// </summary>
        private void UpdateDrawing(string updates)
        {
            if (updatingFont) return;

            //arrange (locals based off GUI state)
            FontStyle f = FontStyle.Regular;
            TextFormatFlags t = TextFormatFlags.Default;
            if (bBold.Checked) f |= FontStyle.Bold;
            if (bUnderline.Checked) f |= FontStyle.Underline;
            if (bItalic.Checked) f |= FontStyle.Italic;
            cbAlign.Text.Do(x =>
            {
                if (x == "Left") t |= TextFormatFlags.Left;
                if (x == "Centre") t |= TextFormatFlags.HorizontalCenter;
                if (x == "Right") t |= TextFormatFlags.Right;
            });
            cbVAlign.Text.Do(x =>
            {
                if (x == "Top") t |= TextFormatFlags.Top;
                if (x == "Middle") t |= TextFormatFlags.VerticalCenter;
                if (x == "Bottom") t |= TextFormatFlags.Bottom;
            });
            t |= TextFormatFlags.WordBreak;

            foreach (var item in SelectedDrawings)
            {
                //arrange (temps based off or backed by a selected item)
                if (t == TextFormatFlags.Default) t = item.TextFormat;
                var font = cbFont.Text.MakeNever("", item.Font.Name);
                var size = float.TryParse(cbSize.Text, out float s).Get(x =>
                 {
                     if (x)
                     {
                         return s;
                     }
                     else
                     {
                         return item.Font.Size;
                     }
                 });
                var weight = float.TryParse(cbWeight.Text, out float w).Get(x =>
                {
                    if (x)
                    {
                        return w;
                    }
                    else
                    {
                        return item.Stroke.Width;
                    }
                });

                //act (paste desired temps into selected item)
                try
                {
                    if (updates.Contains("a")) item.TextFormat = t;
                    if (updates.Contains("b")) item.BackColor = (Brush)bFillColour.Tag;
                    if (updates.Contains("l")) item.Stroke = new Pen((Brush)bOutlineColour.Tag, weight);
                    if (updates.Contains("t")) item.TextColor = new Pen((Brush)bTextColour.Tag).Color;
                    if (updates.Contains("f")) item.Font = new Font(font, size, f);
                }
                catch
                {

                }
            }// end of for

            pictureBox1.Invalidate(); //refresh
        }

        private void toolStripComboBox1_Leave(object sender, EventArgs e)
        {
            //  UpdateDrawing();

        }



        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
            {
                SelectedDrawings.Clear();
                pictureBox1.Invalidate();
                UpdateInterface();
            }
            if (e.KeyCode == Keys.Delete)
            {
                foreach (var item in SelectedDrawings)
                {
                    Drawings.Remove(item);
                }
                SelectedDrawings.Clear();
                pictureBox1.Invalidate();
                UpdateInterface();
            }
        }



        private void bFillColour_Click(object sender, EventArgs e)
        {
            var o = sender as ToolStripButton;

            colorDialog1.Color = new Pen((Brush)o.Tag).Color;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                o.Tag = new SolidBrush(colorDialog1.Color);
                UpdateDrawing(o.Get(x =>
                {
                    switch (o.Name)
                    {
                        case "bOutlineColour":
                            return "l";
                        case "bTextColour":
                            return "t";
                        default:
                            return "f";
                    }

                }));
            }
        }



        Drawing? NewDrawing = null;
        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDrawing = new Rect(0, 0, 0, 0);
        }

        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDrawing = new Ellipse(0, 0, 0, 0);

        }
        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDrawing = new Ellipse(0, 0, 0, 0) { Text = "Your Text Here", TextFormat = TextFormatFlags.Left | TextFormatFlags.Top, BackColor = Brushes.White, TextColor = Color.Black };


        }
        private void bTextColour_Paint(object sender, PaintEventArgs e)
        {
            var o = sender as ToolStripButton;
            var c = (Brush?)o?.Tag;
            if (o == null || c == null) return;

            e.Graphics.FillRectangle(c, 3, o.Height - 6, o.Width - 6, 3);
            e.Graphics.DrawRectangle(pen, 3, o.Height - 6, o.Width - 6, 3);
        }

        bool Zoomed = false;
        private void panel1_Resize(object sender, EventArgs e)
        {
            if (!Zoomed)
            {
                pictureBox1.Top = (panel1.Height - pictureBox1.Height) / 2;
                pictureBox1.Left = (panel1.Width - pictureBox1.Width) / 2;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                ZoomDrawing(0.25f);
            }
            if (e.Delta > 0)
            {
                ZoomDrawing(-0.25f);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void cbFont_TextUpdate(object sender, EventArgs e)
        {
            UpdateDrawing("f");
        }

        private void cbSize_TextUpdate(object sender, EventArgs e)
        {
            UpdateDrawing("t");
        }


        private void cbAlign_TextUpdate(object sender, EventArgs e)
        {
            UpdateDrawing("a");

        }

        private void cbWeight_TextUpdate(object sender, EventArgs e)
        {
            UpdateDrawing("l");

        }
    }




}
