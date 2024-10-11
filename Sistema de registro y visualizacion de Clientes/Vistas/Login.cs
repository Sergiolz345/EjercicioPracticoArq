using Sistema_de_registro_y_visualizacion_de_Clientes.Controladores;
using Sistema_de_registro_y_visualizacion_de_Clientes.Modelos;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sistema_de_registro_y_visualizacion_de_Clientes
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }


        #region Mover Arrastrar Formulario
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void lblform_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);

            if (tbUsuario.Text == "")
            {
                tbUsuario.Text = "Usuario";
                tbUsuario.ForeColor = Color.Silver;
            }

            if (tbPassword.Text == "")
            {
                cbContraseña.Checked = false;
                cbContraseña.Enabled = false;
                tbPassword.UseSystemPasswordChar = false;
                tbPassword.Text = "Contraseña";
                tbPassword.ForeColor = Color.Silver;
            }
        }

        private void Login_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);

            if (tbUsuario.Text == "")
            {
                tbUsuario.Text = "Usuario";
                tbUsuario.ForeColor = Color.Silver;
            }

            if (tbPassword.Text == "")
            {
                cbContraseña.Checked = false;
                cbContraseña.Enabled = false;
                tbPassword.UseSystemPasswordChar = false;
                tbPassword.Text = "Contraseña";
                tbPassword.ForeColor = Color.Silver;
            }
        }

        private void panel1_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);

            if (tbUsuario.Text == "")
            {
                tbUsuario.Text = "Usuario";
                tbUsuario.ForeColor = Color.Silver;
            }

            if (tbPassword.Text == "")
            {
                cbContraseña.Checked = false;
                cbContraseña.Enabled = false;
                tbPassword.UseSystemPasswordChar = false;
                tbPassword.Text = "Contraseña";
                tbPassword.ForeColor = Color.Silver;
            }
        }
        #endregion


        #region Botones de Ventana
        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion


        #region Marca de Agua
        private void tbUsuario_MouseDown(object sender, MouseEventArgs e)
        {
            if (tbPassword.Text == "")
            {
                cbContraseña.Checked = false;
                cbContraseña.Enabled = false;
                tbPassword.UseSystemPasswordChar = false;
                tbPassword.Text = "Contraseña";
                tbPassword.ForeColor = Color.Silver;
            }

            if (tbUsuario.Text == "Usuario")
            {
                tbUsuario.Text = "";
                tbUsuario.ForeColor = Color.Black;
            }
        }

        private void tbPassword_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (tbUsuario.Text == "")
            {
                tbUsuario.Text = "Usuario";
                tbUsuario.ForeColor = Color.Silver;
            }

            if (tbPassword.Text == "Contraseña")
            {
                cbContraseña.Enabled = true;
                tbPassword.UseSystemPasswordChar = true;
                tbPassword.Text = "";
                tbPassword.ForeColor = Color.Black;
            }
        }

        private void cbContraseña_CheckedChanged_1(object sender, EventArgs e)
        {

            if (cbContraseña.Checked)
            {
                tbPassword.UseSystemPasswordChar = false;
                cbContraseña.Text = "Ocultar Contraseña";
            }

            else
            {
                tbPassword.UseSystemPasswordChar = true;

                cbContraseña.Text = "Mostrar Contraseña";
            }
        }
        #endregion


        // Verificar usuario al hacer clic en "Conectar"
        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUsuario.Text) || tbUsuario.Text == "Usuario")
            {
                MessageBox.Show("Por favor, ingrese un nombre de usuario.");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbPassword.Text) || tbPassword.Text == "Contraseña")
            {
                MessageBox.Show("Por favor, ingrese una contraseña.");
                return;
            }

            // Llamar al controlador para verificar al usuario
            VerificarUsuarioController verificarController = new VerificarUsuarioController();
            Usuario usuario = verificarController.VerificarUsuario(tbUsuario.Text, tbPassword.Text);

            if (usuario != null)
            {
                MessageBox.Show("Login exitoso.");

                // Aquí puedes utilizar los datos del usuario, como su imagen o su rol
                // y mostrar el menú principal.
                this.Hide();
                Menu menu = new Menu(usuario);
                menu.ShowDialog();

                this.Close(); // Cierra el formulario de login después de abrir el menú.
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
            }
        }

        // Método de registro para abrir la ventana de "RegistrarUsuarios"
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegistrarUsuarios registrarForm = new RegistrarUsuarios();
            registrarForm.ShowDialog();
        }
    }
}
