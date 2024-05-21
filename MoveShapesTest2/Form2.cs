using Microsoft.VisualBasic;

namespace MoveShapesTest2
{
    public partial class Form2 : Form
    {



        public List<Drawing> Drawings { get; set; } = new List<Drawing>();
        public Form2()
        {
            InitializeComponent();
            Drawings.Add(new Rect(10, 10, 200, 100));
            Drawings.Add(new Ellipse(20, 20, 200, 100));

        }
        public float Zoom = 2;
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var item in Drawings)
            {
                item.Draw(e.Graphics, Zoom);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            var things = Drawings.FindAll(x => x.InBounds(e.Location, Global.SELECTION_ANCHOR_PADDING));
            var things2 = things.Where(x => x.Selected);
            if (!Drawings.Any(x => things.Contains(x) && x.Selected))
            {
                Drawings.ForEach(x => x.Selected = x == things.LastOrDefault());

            }
            else
            {
                things2.ToList().ForEach(x => x.MouseClick(e.Location));
            }
            pictureBox1.Invalidate();

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            var things = Drawings.FindAll(x => x.Selected);

            if (Drawings.Any(x => x.Sizing || Global.InBounds(e.Location, new Rectangle((int)(((x.Width + x.X) * Zoom) - Global.SELECTION_ANCHOR_PADDING), (int)(((x.Y + x.Height) * Zoom) - Global.SELECTION_ANCHOR_PADDING), Global.SELECTION_ANCHOR_PADDING * 2, Global.SELECTION_ANCHOR_PADDING * 2))))
            {
                Cursor = Cursors.SizeNWSE;
            }
            else if (things.Any(x => x.InBounds(e.Location, 0)))
            {
                Cursor = Cursors.SizeAll;
            }
            else if (Drawings.Any(x => x.InBounds(e.Location, 0)))
            {
                Cursor = Cursors.Hand;
            }
            else
            {
                Cursor = Cursors.Default;

            }

            if (things.Count > 0)
            {


                things.ForEach(x => x.MouseMove(e.Location, e, ModifierKeys));
                if (e.Button != MouseButtons.None) pictureBox1.Invalidate();
            }



        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            var things = Drawings.FindAll(x => x.Selected);

            things.ForEach(x => x.MouseUp(e.Location));

        }
    }
}