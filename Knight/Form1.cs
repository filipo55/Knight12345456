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
        private Point Knight;
        private Random rand = new Random();
        private List<PictureBox> Boxes;
        private int bsize = 8;

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

        private void LoadKnight(PictureBox box)
        {
            Bitmap src = Properties.Resources.knight;
            box.Image = src;
            box.SizeMode = PictureBoxSizeMode.StretchImage;
            src.MakeTransparent();
            box.Image = src;
            box.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void putKnight()
        {
            int pos = 0;
            bool knightPlaced = false;
            while (knightPlaced == false)
            {
                if (Boxes.ElementAt(pos).BackColor == Color.ForestGreen)
                {
                    LoadKnight(Boxes.ElementAt(pos));
                    Knight = (Point)Boxes.ElementAt(pos).Tag;
                    knightPlaced = true;
                }
                else
                    pos += 1;
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
            if(direction==0)
            {
                



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
                    //case (Keys.Left):


            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
