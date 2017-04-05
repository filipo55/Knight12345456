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
        private int[,] board;
        private Point Knight;
        private Random rand = new Random();
        private List<PictureBox> Boxes;


        public Form1()
        {
            InitializeComponent();
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
                tableLayoutPanel1.ColumnStyles.Add(new RowStyle(SizeType.Percent, (float)100.0 / board.GetLength(1)));
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


        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Settings = new Form2();
            DialogResult res = Settings.ShowDialog(this);


            if (Settings.comboBox1.SelectedIndex == 0)
            {
                NewGame(8);
            }
            if (Settings.comboBox1.SelectedIndex == 1)
            {
                NewGame(10);
            }
            if (Settings.comboBox1.SelectedIndex == 2)
            {
                NewGame(12);
            }
        }


    }
}
