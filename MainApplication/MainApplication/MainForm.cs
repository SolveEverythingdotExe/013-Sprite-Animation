using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MainApplication
{
    public partial class MainForm : Form
    {
        private List<Image> IdleImages = new List<Image>();
        private List<Image> WalkImages = new List<Image>();

        private int IdleImageMaxIndex = 10;
        private int WalkImageMaxIndex = 10;

        private int CurrentImageIndex = 0;
        private bool IsIdle = true;

        public MainForm()
        {
            InitializeComponent();

            //remove flickerring
            this.DoubleBuffered = true;

            LoadResources();
            animationTimer.Start();
        }

        //lets create a method that will load the resources/images
        private void LoadResources()
        {
            for (int i = 1; i <= IdleImageMaxIndex; i++)
                IdleImages.Add(Image.FromFile(Application.StartupPath + @"\..\..\..\..\Sprites\Idle (" + i.ToString() + ").png"));

            for (int i = 1; i <= WalkImageMaxIndex; i++)
                WalkImages.Add(Image.FromFile(Application.StartupPath + @"\..\..\..\..\Sprites\Walk (" + i.ToString() + ").png"));
        }

        //paint the image
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            //we created a variable IsIdle, which when it sets to false it will change the image
            //that will be painted
            if (IsIdle)
                e.Graphics.DrawImage(IdleImages[CurrentImageIndex], new Point(0, 0));
            else
                e.Graphics.DrawImage(WalkImages[CurrentImageIndex], new Point(0, 0));
        }

        //now lets animate it using Timer
        private void animationTimer_Tick(object sender, EventArgs e)
        {
            int MaxIndex = 0;

            if (IsIdle)
                MaxIndex = IdleImageMaxIndex - 1;
            else
                MaxIndex = WalkImageMaxIndex - 1;

            if (CurrentImageIndex >= MaxIndex)
                CurrentImageIndex = 0;
            else
                CurrentImageIndex++;


            //lets repaint the images
            this.Invalidate();
        }

        //now lets add keyboard event
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            //when right button is pressed lets begin to walk
            if (e.KeyCode == Keys.Right)
                IsIdle = false;
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            //lets stop walking if the key press was released
            if (e.KeyCode == Keys.Right)
                IsIdle = true;
        }
        //It works!
    }
}
