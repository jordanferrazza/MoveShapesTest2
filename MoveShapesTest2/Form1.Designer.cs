namespace MoveShapesTest2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pictureBox1 = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            toolStrip1 = new ToolStrip();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            rectangleToolStripMenuItem = new ToolStripMenuItem();
            elipseToolStripMenuItem = new ToolStripMenuItem();
            textBoxToolStripMenuItem = new ToolStripMenuItem();
            lineToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            cbFont = new ToolStripComboBox();
            cbSize = new ToolStripComboBox();
            bBold = new ToolStripButton();
            bUnderline = new ToolStripButton();
            bItalic = new ToolStripButton();
            cbAlign = new ToolStripComboBox();
            cbVAlign = new ToolStripComboBox();
            bTextColour = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            bFillColour = new ToolStripButton();
            bOutlineColour = new ToolStripButton();
            cbWeight = new ToolStripComboBox();
            colorDialog1 = new ColorDialog();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Location = new Point(0, 13);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(800, 400);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            pictureBox1.Paint += pictureBox1_Paint;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            pictureBox1.Move += pictureBox1_Move;
            pictureBox1.PreviewKeyDown += pictureBox1_PreviewKeyDown;
            pictureBox1.Resize += pictureBox1_Resize;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripDropDownButton1, toolStripSeparator2, cbFont, cbSize, bBold, bUnderline, bItalic, cbAlign, cbVAlign, bTextColour, toolStripSeparator1, bFillColour, bOutlineColour, cbWeight });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(800, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { rectangleToolStripMenuItem, elipseToolStripMenuItem, textBoxToolStripMenuItem, lineToolStripMenuItem });
            toolStripDropDownButton1.Image = Properties.Resources.add;
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(60, 22);
            toolStripDropDownButton1.Text = "New";
            // 
            // rectangleToolStripMenuItem
            // 
            rectangleToolStripMenuItem.Name = "rectangleToolStripMenuItem";
            rectangleToolStripMenuItem.Size = new Size(126, 22);
            rectangleToolStripMenuItem.Text = "Rectangle";
            rectangleToolStripMenuItem.Click += rectangleToolStripMenuItem_Click;
            // 
            // elipseToolStripMenuItem
            // 
            elipseToolStripMenuItem.Name = "elipseToolStripMenuItem";
            elipseToolStripMenuItem.Size = new Size(126, 22);
            elipseToolStripMenuItem.Text = "Ellipse";
            elipseToolStripMenuItem.Click += ellipseToolStripMenuItem_Click;
            // 
            // textBoxToolStripMenuItem
            // 
            textBoxToolStripMenuItem.Name = "textBoxToolStripMenuItem";
            textBoxToolStripMenuItem.Size = new Size(126, 22);
            textBoxToolStripMenuItem.Text = "Text Box";
            textBoxToolStripMenuItem.Click += textBoxToolStripMenuItem_Click;
            // 
            // lineToolStripMenuItem
            // 
            lineToolStripMenuItem.Name = "lineToolStripMenuItem";
            lineToolStripMenuItem.Size = new Size(126, 22);
            lineToolStripMenuItem.Text = "Line";
            lineToolStripMenuItem.Click += lineToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // cbFont
            // 
            cbFont.Name = "cbFont";
            cbFont.Size = new Size(121, 25);
            cbFont.TextUpdate += cbFont_TextUpdate;
            cbFont.Leave += toolStripComboBox1_Leave;
            // 
            // cbSize
            // 
            cbSize.DropDownWidth = 10;
            cbSize.Name = "cbSize";
            cbSize.Size = new Size(75, 25);
            cbSize.TextUpdate += cbSize_TextUpdate;
            cbSize.Leave += toolStripComboBox1_Leave;
            // 
            // bBold
            // 
            bBold.CheckOnClick = true;
            bBold.DisplayStyle = ToolStripItemDisplayStyle.Text;
            bBold.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            bBold.ImageTransparentColor = Color.Magenta;
            bBold.Name = "bBold";
            bBold.Size = new Size(23, 22);
            bBold.Text = "B";
            bBold.Click += cbFont_TextUpdate;
            // 
            // bUnderline
            // 
            bUnderline.CheckOnClick = true;
            bUnderline.DisplayStyle = ToolStripItemDisplayStyle.Text;
            bUnderline.Font = new Font("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point);
            bUnderline.ImageTransparentColor = Color.Magenta;
            bUnderline.Name = "bUnderline";
            bUnderline.Size = new Size(23, 22);
            bUnderline.Text = "U";
            bUnderline.Click += cbFont_TextUpdate;
            // 
            // bItalic
            // 
            bItalic.CheckOnClick = true;
            bItalic.DisplayStyle = ToolStripItemDisplayStyle.Text;
            bItalic.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            bItalic.ImageTransparentColor = Color.Magenta;
            bItalic.Name = "bItalic";
            bItalic.Size = new Size(23, 22);
            bItalic.Text = "I";
            bItalic.Click += cbFont_TextUpdate;
            // 
            // cbAlign
            // 
            cbAlign.DropDownStyle = ComboBoxStyle.DropDownList;
            cbAlign.Name = "cbAlign";
            cbAlign.Size = new Size(121, 25);
            cbAlign.TextUpdate += cbAlign_TextUpdate;
            // 
            // cbVAlign
            // 
            cbVAlign.DropDownStyle = ComboBoxStyle.DropDownList;
            cbVAlign.Name = "cbVAlign";
            cbVAlign.Size = new Size(121, 25);
            cbVAlign.TextUpdate += cbAlign_TextUpdate;
            // 
            // bTextColour
            // 
            bTextColour.DisplayStyle = ToolStripItemDisplayStyle.Image;
            bTextColour.Image = Properties.Resources.font_colour;
            bTextColour.ImageTransparentColor = Color.Magenta;
            bTextColour.Name = "bTextColour";
            bTextColour.Size = new Size(23, 22);
            bTextColour.Text = "F";
            bTextColour.Click += bFillColour_Click;
            bTextColour.Paint += bTextColour_Paint;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // bFillColour
            // 
            bFillColour.DisplayStyle = ToolStripItemDisplayStyle.Image;
            bFillColour.Image = Properties.Resources.fill;
            bFillColour.ImageTransparentColor = Color.Magenta;
            bFillColour.Name = "bFillColour";
            bFillColour.Size = new Size(23, 22);
            bFillColour.Text = "toolStripButton1";
            bFillColour.Click += bFillColour_Click;
            bFillColour.Paint += bTextColour_Paint;
            // 
            // bOutlineColour
            // 
            bOutlineColour.DisplayStyle = ToolStripItemDisplayStyle.Text;
            bOutlineColour.Image = Properties.Resources.fill;
            bOutlineColour.ImageTransparentColor = Color.Magenta;
            bOutlineColour.Name = "bOutlineColour";
            bOutlineColour.Size = new Size(23, 22);
            bOutlineColour.Text = "L";
            bOutlineColour.Click += bFillColour_Click;
            bOutlineColour.Paint += bTextColour_Paint;
            // 
            // cbWeight
            // 
            cbWeight.DropDownWidth = 10;
            cbWeight.Name = "cbWeight";
            cbWeight.Size = new Size(75, 25);
            cbWeight.TextUpdate += cbWeight_TextUpdate;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.BackColor = SystemColors.AppWorkspace;
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 25);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 425);
            panel1.TabIndex = 2;
            panel1.MouseDown += panel1_MouseDown;
            panel1.Resize += panel1_Resize;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(toolStrip1);
            DoubleBuffered = true;
            Name = "Form1";
            Text = "Form1";
            MouseDown += Form1_MouseDown;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private ToolStrip toolStrip1;
        private ToolStripComboBox cbFont;
        private ToolStripComboBox cbSize;
        private ToolStripButton bBold;
        private ToolStripButton bUnderline;
        private ToolStripButton bItalic;
        private ToolStripComboBox cbAlign;
        private ToolStripComboBox cbVAlign;
        private ToolStripButton bTextColour;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton bFillColour;
        private ToolStripButton bOutlineColour;
        private ColorDialog colorDialog1;
        private ToolStripComboBox cbWeight;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem rectangleToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem elipseToolStripMenuItem;
        private ToolStripMenuItem textBoxToolStripMenuItem;
        private Panel panel1;
        private ToolStripMenuItem lineToolStripMenuItem;
    }
}