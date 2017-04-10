using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Knight
{
    public partial class Form3 : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
(
int nLeftRect, // x-coordinate of upper-left corner
int nTopRect, // y-coordinate of upper-left corner
int nRightRect, // x-coordinate of lower-right corner
int nBottomRect, // y-coordinate of lower-right corner
int nWidthEllipse, // height of ellipse
int nHeightEllipse // width of ellipse
);

        public Form3()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, Width, Height));
            BackColor = Color.FromArgb(163, 0, 255);
        }
 

        private void Form3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Opacity = 0;
            Fade(this, 40);
            label1.MouseDown += new MouseEventHandler(Form3_MouseDown);
        }
        

        private async void Fade(Form o, int interval)
        {
            while(true){
                while (o.Opacity < 1.0)
                {
                    await Task.Delay(interval);
                    o.Opacity += 0.1;
                }
                while (o.Opacity > 0.0)
                {
                    await Task.Delay(interval);
                    o.Opacity -= 0.1;
                }
            }
           
        }

      

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
    }
}
