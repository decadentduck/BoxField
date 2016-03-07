using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxField
{
    public partial class GameScreen : UserControl
    {
        //player1 button control keys - DO NOT CHANGE
        Boolean leftArrowDown, downArrowDown, rightArrowDown, upArrowDown, bDown, nDown, mDown, spaceDown;

        //used to draw boxes on screen
        SolidBrush boxBrush = new SolidBrush(Color.White);
        
        List<Box> boxes = new List<Box>();
        List<Box> boxesRight = new List<Box>();
        List<Color> lColor = new List<Color>();
        List<Color> rColor = new List<Color>();

        Random ran = new Random();
        int pattern = 1000;

        int waitTime = 18;

        public GameScreen()
        {
            InitializeComponent();
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {
            //TODO - create initial box object and add it to list of Boxes
            Box b = new Box(300, 0);
            Box bb = new Box(600, 0);
            Color c = Color.FromArgb(ran.Next(0, 250), ran.Next(0, 250), ran.Next(0, 250));
            Color cc = Color.FromArgb(ran.Next(0, 250), ran.Next(0, 250), ran.Next(0, 250));
            boxes.Add(b);
            boxesRight.Add(bb);
            lColor.Add(c);
            rColor.Add(cc);
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.B:
                    bDown = true;
                    break;
                case Keys.N:
                    nDown = true;
                    break;
                case Keys.M:
                    mDown = true;
                    break;
                case Keys.Space:
                    spaceDown = true;
                    break;
                default:
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.B:
                    bDown = false;
                    break;
                case Keys.N:
                    nDown = false;
                    break;
                case Keys.M:
                    mDown = false;
                    break;
                case Keys.Space:
                    spaceDown = false;
                    break;
                default:
                    break;
            }
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            waitTime--;
            pattern--;
            int x, xx, y;
            x = 0;
            xx = 0;
            y = 0;

            if(waitTime == 0)
            {

                if (pattern <= -1000) { pattern = 1000; }
                if (pattern > 0)
                {
                    if (x > 0)
                    {
                        x = x - 10;
                        xx = xx - 10;
                    }
                    else { x = 450; xx = 750; }
                }

                else
                {
                    x= ran.Next(0, 400);
                    xx = ran.Next(450, 800);
                }

                Box b = new Box(x, y);
                Box bb = new Box(xx, y);
                boxes.Add(b);
                boxesRight.Add(bb);

                Color c = Color.FromArgb(ran.Next(0, 250), ran.Next(0, 250), ran.Next(0, 250));
                Color cc = Color.FromArgb(ran.Next(0, 250), ran.Next(0, 250), ran.Next(0, 250));
                lColor.Add(c);
                rColor.Add(cc);

                waitTime = 20;
            }

            //update position of ALLL OF THE BOXESSS
            for (int i = 0; i < boxes.Count; i++)
            {
                boxes[i].y += 2;
                boxesRight[i].y += 2;
            }
            //remove THE BOX from list if it is off screen
            if (boxes[0].y > this.Height)
            {
                boxes.RemoveAt(0);
                boxesRight.RemoveAt(0);
                lColor.RemoveAt(0);
                rColor.RemoveAt(0);
            }

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //draw ALL OF THE BOXES
            for (int i = 0; i < boxes.Count; i++)
            {
                boxBrush.Color = lColor[i];
                e.Graphics.FillRectangle(boxBrush, boxes[i].x, boxes[i].y, 30, 30);

                boxBrush.Color = rColor[i];
                e.Graphics.FillRectangle(boxBrush, boxesRight[i].x, boxesRight[i].y, 30, 30);
            }
        }
    }
}
