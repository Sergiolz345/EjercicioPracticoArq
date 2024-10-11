using Sistema_de_registro_y_visualizacion_de_Clientes.Controladores;
using Sistema_de_registro_y_visualizacion_de_Clientes.Modelos;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sistema_de_registro_y_visualizacion_de_Clientes
{
    public partial class RegistrarUsuarios : Form
    {
        public RegistrarUsuarios()
        {
            InitializeComponent();
        }


        #region Mover Arrastrar Formulario
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void lblAProspectos_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void RegistrarUsuarios_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion


        #region Botones de Ventana
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login c = new Login();
            c.ShowDialog();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion



        // Método para manejar el evento Click del PictureBox
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Crear el OpenFileDialog para seleccionar imágenes
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Seleccionar una imagen",
                Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp",  // Filtro para solo permitir imágenes
                InitialDirectory = @"C:\",  // Directorio inicial
                RestoreDirectory = true
            };

            // Mostrar el cuadro de diálogo y comprobar si el usuario selecciona un archivo
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Cargar la imagen seleccionada en el PictureBox
                    pictureBox1.Image = new Bitmap(openFileDialog.FileName);

                    // Ajustar la imagen al tamaño del PictureBox
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen: " + ex.Message);
                }
            }
        }

        // Cargar imagen predeterminada al iniciar
        private void RegistrarUsuarios_Load(object sender, EventArgs e)
        {
            string rutaImagenPredeterminada = @"C:\Users\Sergio Lopez Castro\source\repos\Sistema de registro y visualizacion de Clientes\Sistema de registro y visualizacion de Clientes\Imagenes\AñadirImagen.png";
            pictureBox1.Image = Image.FromFile(rutaImagenPredeterminada);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;  // Ajustar la imagen al tamaño del PictureBox
        }

        // Método para guardar los datos del usuario
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear el modelo Usuario y llenar los campos desde la interfaz
                Usuario nuevoUsuario = new Usuario
                {
                    NombreUsuario = tbNombre.Text,
                    Contraseña = tbPassword.Text,
                    Rol = tbRoll.Text
                };

                // Convertir la imagen a byte[] si hay una imagen seleccionada
                if (pictureBox1.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                        nuevoUsuario.ImagenUsuario = ms.ToArray();
                    }
                }

                // Crear el controlador y agregar el usuario a la base de datos
                AgregarUsuarioController controller = new AgregarUsuarioController();
                bool resultado = controller.AgregarUsuario(nuevoUsuario);

                if (resultado)
                {
                    MessageBox.Show("Usuario agregado exitosamente.");
                    this.Hide();
                    Login c = new Login();
                    c.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Error al agregar el usuario.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
