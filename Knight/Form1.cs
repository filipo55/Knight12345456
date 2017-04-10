using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Knight
{
    public partial class Form1 : Form
    {
        private int[,] board = new int [8,8];
        private int[,] board_elem = new int[8, 8];
        private Point Knight, Doors, Key;
        private Random rand = new Random();
        private List<PictureBox> Boxes;
        private int bsize = 8;
        private int knight_direction = 1;
        private bool pressed_key = false;
        private bool pressed_space = false;

        public Form1()
        {
            InitializeComponent();
            NewGame(8);
        }

      
        private void MakingBoard()
        {
            Boxes = new List<PictureBox>();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.ColumnCount = board.GetLength(0);
            tableLayoutPanel1.RowCount = board.GetLength(1);

            for(int i =0; i<tableLayoutPanel1.ColumnCount;i++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, (float) 100.0 / board.GetLength(0)));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (float)100.0 / board.GetLength(1)));
                for(int j = 0; j<tableLayoutPanel1.RowCount;j++)
                {
                    PictureBox box = new PictureBox
                    {
                        Dock = DockStyle.Fill,
                        Tag = new Point(i, j),
                        BorderStyle = BorderStyle.None,

                    };

                    Margin(box);
                    int randomm = rand.Next(0, 2);
                    Boxes.Add(box);
                    if (randomm == 0)
                        box.BackColor = Color.Maroon;
                    else
                        box.BackColor = Color.ForestGreen;
                    tableLayoutPanel1.Controls.Add(box);
                }

            }
            putKnight();
            putDoors();
            putKey();
        }

        private void Margin(Control contr)
        {
            var Margin = contr.Margin;
            Margin.Top = 0;
            Margin.Bottom = 0;
            Margin.Left = 0;
            Margin.Right = 0;
            contr.Margin = Margin;
        }

        private void NewGame(int size)
        {
            board = new int[size, size];
            MakingBoard();
            tableLayoutPanel1.Enabled = true;


        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (close(false) == true)
                this.Dispose(true);
            else
                e.Cancel = true;
        }

        private bool close(bool can)
        {
            if (MessageBox.Show("Do you want to close?", "Cancel game", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.Yes)
                can = true;
            else
                can = false;
            return can;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame(bsize);
        }

        private void LoadKnight(PictureBox box, int position)
        {
            Bitmap src;
            if (position == 1)
                src = Properties.Resources.knight;
            else
                src = Properties.Resources.knight2;
            box.Image = src;
            box.SizeMode = PictureBoxSizeMode.StretchImage;
            src.MakeTransparent();
           

        }

        private void putKnight()
        {
            int pos = 0;
            bool knightPlaced = false;
            while (knightPlaced == false)
            {
                if (Boxes.ElementAt(pos).BackColor == Color.ForestGreen)
                {
                    LoadKnight(Boxes.ElementAt(pos), knight_direction);
                    Knight = (Point)Boxes.ElementAt(pos).Tag;
                    knightPlaced = true;
                }
                else
                    pos += 1;
            }
        }

        private void putKey()
        {
            int pos = rand.Next(0, Boxes.Count);
            bool keyPlaced = false;
            Key = (Point)Boxes.ElementAt(pos).Tag;
            while (!keyPlaced)
            {
                if (Boxes.ElementAt(pos).BackColor == Color.ForestGreen && Key != Knight && Key!= Doors)
                {
                    LoadKey(Boxes.ElementAt(pos));
                    keyPlaced = true;
                }
                else
                {
                    pos = rand.Next(0, Boxes.Count);
                    Key = (Point)Boxes.ElementAt(pos).Tag;
                }

            }
        }
        private void LoadKey(PictureBox box)
        {
            Bitmap src;
            src = Properties.Resources.key2;
            box.Image = src;
            box.SizeMode = PictureBoxSizeMode.StretchImage;
            src.MakeTransparent();

        }

        private void LoadDoors(PictureBox box)
        {
            Bitmap src;
            src = Properties.Resources.closed_door;
            box.Image = src;
            box.SizeMode = PictureBoxSizeMode.StretchImage;
            src.MakeTransparent();
        }


        private void putDoors()
        {
            int pos = rand.Next(0, Boxes.Count);
            bool doorPlaced = false;
            Doors = (Point)Boxes.ElementAt(pos).Tag;
            while(!doorPlaced)
            {
                if (Boxes.ElementAt(pos).BackColor == Color.ForestGreen && Doors!=Knight && Doors!=Key)
                {
                    LoadDoors(Boxes.ElementAt(pos));
                    doorPlaced = true;
                }
                else
                {
                    pos = rand.Next(0, Boxes.Count);
                    Doors = (Point)Boxes.ElementAt(pos).Tag;
                }

            }




        }


        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 Settings = new Form2();
            DialogResult res = Settings.ShowDialog(this);
            if (res == DialogResult.Abort)
                Settings.Close();
            else
            {
                if (Settings.comboBox1.SelectedIndex == 0)
                {
                    NewGame(8);
                    bsize = 8;
                }
                if (Settings.comboBox1.SelectedIndex == 1)
                {
                    NewGame(10);
                    bsize = 10;
                }
                if (Settings.comboBox1.SelectedIndex == 2)
                {
                    NewGame(12);
                    bsize = 12;
                }
            }

        }

        private void MovingKnight(int direction)
        {
            PictureBox knight = (PictureBox)tableLayoutPanel1.GetControlFromPosition(Knight.Y, Knight.X);
            //Down
            if (direction==0)
            {
                
                PictureBox knight_new = (PictureBox)tableLayoutPanel1.GetControlFromPosition(Knight.Y, Knight.X+1);
                if(knight_new.BackColor == Color.ForestGreen)
                {
                    Knight.X++;
                    knight.Image = null;
                    LoadKnight(knight_new, knight_direction);
                }
            }
            //Up
            if(direction==1)
            {
                PictureBox knight_new = (PictureBox)tableLayoutPanel1.GetControlFromPosition(Knight.Y, Knight.X - 1);
                if (knight_new.BackColor == Color.ForestGreen)
                {
                    Knight.X--;
                    knight.Image = null;
                    LoadKnight(knight_new, knight_direction);
                }
            }
            //Left
            if(direction==2)
            {
                PictureBox knight_new = (PictureBox)tableLayoutPanel1.GetControlFromPosition(Knight.Y-1, Knight.X);
                if (knight_new.BackColor == Color.ForestGreen)
                {
                    Knight.Y--;
                    knight.Image = null;
                    knight_direction = 0;
                    LoadKnight(knight_new, knight_direction);
                    
                }
            }
            //Right
            if(direction==3)
            {
                PictureBox knight_new = (PictureBox)tableLayoutPanel1.GetControlFromPosition(Knight.Y + 1, Knight.X);
                if (knight_new.BackColor == Color.ForestGreen)
                {
                    Knight.Y++;
                    knight.Image = null;
                    knight_direction = 1;
                    LoadKnight(knight_new, knight_direction);
                    
                }
            }
        }

  

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Control | Keys.N):
                    NewGame(bsize);
                    return true;
                case (Keys.Control | Keys.M):
                    {
                        Form2 Settings = new Form2();
                        DialogResult res = Settings.ShowDialog(this);
                        if (res == DialogResult.Abort)
                            Settings.Close();
                        else
                        {
                            if (Settings.comboBox1.SelectedIndex == 0)
                            {
                                NewGame(8);
                                bsize = 8;
                            }
                            if (Settings.comboBox1.SelectedIndex == 1)
                            {
                                NewGame(10);
                                bsize = 10;
                            }
                            if (Settings.comboBox1.SelectedIndex == 2)
                            {
                                NewGame(12);
                                bsize = 12;
                            }
                        }
                    }
                
                    return true;


            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void Attack()
        {
            if (Knight.Y > 0)
            {
                PictureBox left = (PictureBox)tableLayoutPanel1.GetControlFromPosition(Knight.Y-1, Knight.X);
                left.BackColor = Color.ForestGreen;
            }
            if(Knight.Y < bsize-1)
            {
                PictureBox right = (PictureBox)tableLayoutPanel1.GetControlFromPosition(Knight.Y + 1, Knight.X);
                right.BackColor = Color.ForestGreen;
            }
            if(Knight.X > 0)
            {
                PictureBox up = (PictureBox)tableLayoutPanel1.GetControlFromPosition(Knight.Y, Knight.X -1);
                up.BackColor = Color.ForestGreen;
            }
             if(Knight.X<bsize-1)
            {
                PictureBox down = (PictureBox)tableLayoutPanel1.GetControlFromPosition(Knight.Y, Knight.X + 1);
                down.BackColor = Color.ForestGreen;
            }   
            



        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            PictureBox knight = (PictureBox)tableLayoutPanel1.GetControlFromPosition(Knight.Y, Knight.X);
            if (e.KeyCode == Keys.Down)
            {
                if (Knight.X < bsize-1 && !pressed_key)
                    MovingKnight(0);
                pressed_key = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                if (Knight.X > 0 && !pressed_key)
                    MovingKnight(1);
                pressed_key = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                knight_direction = 0;
                LoadKnight(knight, knight_direction);
                if (Knight.Y > 0 && !pressed_key)
                    MovingKnight(2);
                pressed_key = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                knight_direction = 1;
                LoadKnight(knight, knight_direction);
                if (Knight.Y < bsize-1 && !pressed_key)
                    MovingKnight(3);
                pressed_key = true;
            }
            if(e.KeyCode == Keys.Space && !pressed_space)
            {
                Attack();
                pressed_space = true;
            }


            

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            pressed_key = false;
            if (e.KeyCode == Keys.Space)
                pressed_space = false;
            
                
        }
    }
}
