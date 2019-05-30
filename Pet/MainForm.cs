using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Pet
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg,int wparam , int lparam);

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (panel1.Width == 50)
            {
                panel1.Width = 150;
            }
           else 
               panel1.Width = 50;
        }

       

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MinMax_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            MinMax.Visible = false;
            normal.Visible = true;
        }

        private void fold_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            
        }

        private void normal_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            normal.Visible = false;
            MinMax.Visible = true;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle,0x112,0xf012,0);
        }

        private void AbrirFormInPanel(object Formhijo)
        {
            if (this.panel3.Controls.Count > 0)
                this.panel3.Controls.RemoveAt(0);
            Form fh = Formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(fh);
            this.panel3.Tag = fh;
            fh.Show();
        }
        private void btn_search_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new SearchForm());
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {

        }


    }
    
}

