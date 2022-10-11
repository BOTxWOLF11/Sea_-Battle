using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace МорскойБой
{
    public partial class Game : Form
    {
        Battle objBattle = new Battle();
        public Game()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int w = panel1.Width/objBattle.ReturnMasSize();
            int h = panel1.Height/objBattle.ReturnMasSize();
            ControlPaint.DrawGrid(e.Graphics, new Rectangle(Point.Empty, panel1.Size), new Size(w, 1), Color.White);
            ControlPaint.DrawGrid(e.Graphics, new Rectangle(Point.Empty, panel1.Size), new Size(1, h), Color.White);
            for (int i = 0; i < objBattle.ReturnMasSize(); i++)
                for (int j = 0; j < objBattle.ReturnMasSize(); j++)
                {
                    if (objBattle[i,j] == 0)
                        e.Graphics.FillRectangle(Brushes.White, j * w + 1, i * h + 1, w - 1, h - 1);
                    if (objBattle[i, j] == 1 || objBattle[i,j] == 2)
                        e.Graphics.FillRectangle(Brushes.Red, j * w + 1, i * h + 1, w - 1, h - 1);
                    if (objBattle[i, j] == 3)
                        e.Graphics.FillRectangle(Brushes.Green, j * w + 1, i * h + 1, w - 1, h - 1);
                    if (objBattle[i, j] == 4)
                        e.Graphics.FillRectangle(Brushes.Black, j * w + 1, i * h + 1, w - 1, h - 1);
                }
        
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            int w = panel1.Width / objBattle.ReturnMasSize();
            int h = panel1.Height / objBattle.ReturnMasSize();
            ControlPaint.DrawGrid(e.Graphics, new Rectangle(Point.Empty, panel1.Size), new Size(w, 1), Color.White);
            ControlPaint.DrawGrid(e.Graphics, new Rectangle(Point.Empty, panel1.Size), new Size(1, h), Color.White);
            for (int i = 0; i < objBattle.ReturnMasSize(); i++)
                for (int j = 0; j < objBattle.ReturnMasSize(); j++)
                {
                    if (objBattle.GetEnemyValue(i, j) < 3)
                        e.Graphics.FillRectangle(Brushes.White, j * w + 1, i * h + 1, w - 1, h - 1);
                    if (objBattle.GetEnemyValue(i, j) == 5)
                       e.Graphics.FillRectangle(Brushes.Red, j * w + 1, i * h + 1, w - 1, h - 1);
                    if (objBattle.GetEnemyValue(i, j) == 3)
                        e.Graphics.FillRectangle(Brushes.Green, j * w + 1, i * h + 1, w - 1, h - 1);
                    if (objBattle.GetEnemyValue(i, j) == 4)
                        e.Graphics.FillRectangle(Brushes.LightCoral, j * w + 1, i * h + 1, w - 1, h - 1);
                }
        }

        private void Game_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!(radioButton1.Enabled == false && radioButton2.Enabled == false && radioButton3.Enabled == false 
                && radioButton4.Enabled == false))
            {
                int w = panel1.Width/objBattle.ReturnMasSize();
                int h = panel1.Height / objBattle.ReturnMasSize();
                int x = e.X / w;
                int y = e.Y / h;
                int size;
                if (radioButton1.Checked) size = 1;
                else if (radioButton2.Checked) size = 2;
                else if (radioButton3.Checked) size = 3;
                else size = 4;
                objBattle.WatchShip(y, x, w, h, size, panel1);
            }                   
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            objBattle.NullMas();
            panel1.Invalidate();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                objBattle.ChangeVerctical();
                objBattle.NullMas();
                panel1.Invalidate();
            }
            else
            {
                int w = panel1.Width / objBattle.ReturnMasSize();
                int h = panel1.Height / objBattle.ReturnMasSize();
                int x = e.X / w;
                int y = e.Y / h;
                int size;
                if (radioButton1.Checked) size = 1;
                else if (radioButton2.Checked) size = 2;
                else if (radioButton3.Checked) size = 3;
                else size = 4;
                objBattle.NullMas();
                if (objBattle.CheckSq(x, y, size - 1, objBattle.GetLink()))
                {
                    objBattle.CreateShip(x, y, 2, size - 1, objBattle.GetLink());
                    if (radioButton1.Checked) objBattle.Count1X++;
                    else if (radioButton2.Checked) objBattle.Count2X++;
                    else if (radioButton3.Checked) objBattle.Count3X++;
                    else objBattle.Count4X++;
                    if (objBattle.Count1X > 3) radioButton1.Enabled = false;
                    if (objBattle.Count2X > 2) radioButton2.Enabled = false;
                    if (objBattle.Count3X > 1) radioButton3.Enabled = false;
                    if (objBattle.Count4X > 0) radioButton4.Enabled = false;
                    objBattle.FindNextRb(radioButton1, radioButton2, radioButton3, radioButton4);
                }
                panel1.Invalidate();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            objBattle.NullMas();
            panel1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            objBattle.Reset();
            radioButton1.Enabled = true;
            radioButton1.Checked = true;
            radioButton2.Enabled = true;
            radioButton3.Enabled = true;
            radioButton4.Enabled = true;
            panel1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    objBattle[i,j] = 0;
            int y, x, count = 0, size = 0;
            while (count < 10)
            {
                Random r = new Random();
                if (count > 3) size = 1;
                if (count > 6) size = 2;
                if (count > 8) size = 3;
                y = r.Next(0, 10);
                x = r.Next(0, 10);
                if (r.Next(0, 2) == 1)
                    objBattle.ChangeVerctical();
                if (objBattle.CheckSq(x, y, size, objBattle.GetLink()))
                {
                    objBattle.CreateShip(x, y, 2, size, objBattle.GetLink());
                    count++;
                }
            }       
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;
            radioButton4.Enabled = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            panel1.Invalidate();
        }       

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = @"Играем против компьютера";
            label1.Location = new Point(label1.Location.X - 75, label1.Location.Y);
            objBattle.Count1X = 0;
            objBattle.Count2X = 0;
            objBattle.Count3X = 0;
            objBattle.Count4X = 0;
            int y, x, count = 10, size = 3;
            while (count > 0)
            {
                Random r = new Random();
                if (count < 10) size = 2;
                if (count < 8) size = 1;
                if (count < 5) size = 0;
                y = r.Next(0, 10);
                x = r.Next(0, 10);
                if (r.Next(0, 2) == 1)
                    objBattle.ChangeVerctical();
                if (objBattle.CheckSq(x, y, size, objBattle.GetEnemyLink()))
                {
                    objBattle.CreateShip(x, y, 2, size, objBattle.GetEnemyLink());
                    count--;
                }      
            }        
            panel1.Enabled = false;
            panel2.Invalidate();
            buttonReady.Enabled = false;
            buttonReset.Enabled = false;
            buttonAutoGen.Enabled = false;
        }

        private void panel2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && buttonAutoGen.Enabled == false)
            {
                int w = panel1.Width / objBattle.ReturnMasSize();
                int h = panel1.Height / objBattle.ReturnMasSize();
                int x = e.X / w;
                int y = e.Y / h;
                if (objBattle.GetEnemyValue(y, x) == 2)
                {
                    objBattle.SetEnemyValue(y, x, 3);
                    objBattle.EnemyShip--;
                    objBattle.Explosion(x, y, objBattle.GetEnemyLink());
                    panel2.Invalidate();
                    if (objBattle.EnemyShip == 0)
                        Victory("Победил человек");
                }
                else if (objBattle.GetEnemyValue(y, x) == 0)
                {
                    EnemyTurn();
                    objBattle.SetEnemyValue(y, x, 4);
                    panel2.Invalidate();
                }
            }
        }

        private void EnemyTurn()
        {
            int x, y;
            Random r = new Random();
            bool flag = true;
            while (flag)
            {
                x = r.Next(0, 10);
                y = r.Next(0, 10);
                if (objBattle[x, y] == 0 || objBattle[x, y] == 2)
                    flag = false;
                if (objBattle[x, y] == 0)
                {
                    objBattle[x, y] = 4;
                    panel1.Invalidate();
                }
                else
                {
                    objBattle[x, y] = 3;
                    objBattle.MyShip--;
                    objBattle.Explosion(x, y, objBattle.GetLink());
                    if (objBattle.MyShip == 0)
                        Victory("Победил компьютер");
                }
            }                 
        }

        private void Victory(string str)
        {
            VictroryGame frmVictroryGame = new VictroryGame(str);
            Hide();
            frmVictroryGame.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }
    }
}
