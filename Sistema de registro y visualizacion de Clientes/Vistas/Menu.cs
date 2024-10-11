using System;
using Conexiones;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Sistema_de_registro_y_visualizacion_de_Clientes.Modelos;
using System.IO;

namespace Sistema_de_registro_y_visualizacion_de_Clientes
{
    public partial class Menu : Form
    {
        public dbSqlServer conexion = null;

        public String sData = "";
        string sUsuario, sPasword;

        private Usuario _usuario;  // Variable para almacenar los datos del usuario

        public Menu(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;  // Asignar el objeto usuario al campo
            CargarDatosUsuario();  // Llamar al método para cargar los datos del usuario
        }

        private void CargarDatosUsuario()
        {
            // Cargar el nombre del usuario en el label
            lblUser.Text = _usuario.NombreUsuario;

            // Si el usuario tiene una imagen asociada, cargarla en el PictureBox
            if (_usuario.ImagenUsuario != null)
            {
                using (MemoryStream ms = new MemoryStream(_usuario.ImagenUsuario))
                {
                    pbUser.Image = Image.FromStream(ms);
                    pbUser.SizeMode = PictureBoxSizeMode.Zoom;  // Ajustar la imagen al PictureBox
                }
            }
            else
            {
                // Si no tiene imagen, cargar una imagen predeterminada
                pbUser.Image = Image.FromFile(@"C:\Users\Sergio Lopez Castro\source\repos\Sistema de registro y visualizacion de Clientes\Sistema de registro y visualizacion de Clientes\Imagenes\AñadirImagen.png");
                pbUser.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }



        #region Mover Arrastrar Formulario
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void lblMenu_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pbUser_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void lblUser_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void Menu_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion


        #region Botones de Ventana
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        private void btnProspectos_Click(object sender, EventArgs e)
        {
            this.Hide();
            Añadir_Prospectos c = new Añadir_Prospectos(_usuario);
            c.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login c = new Login();
            c.ShowDialog();
        }

        private void btnLProspectos_Click(object sender, EventArgs e)
        {
            this.Hide();
            Lista_Prospectos c = new Lista_Prospectos(_usuario);
            c.ShowDialog();
        }
    }
}
