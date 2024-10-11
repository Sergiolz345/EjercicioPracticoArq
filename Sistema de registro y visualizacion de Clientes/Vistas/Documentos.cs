using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Sistema_de_registro_y_visualizacion_de_Clientes.Controladores;
using Sistema_de_registro_y_visualizacion_de_Clientes.Modelos;
using System.Runtime.InteropServices;

namespace Sistema_de_registro_y_visualizacion_de_Clientes
{
    public partial class Documentos : Form
    {
        private DocumentosController controladorDocumentos;
        private string rutaArchivo;  // Guardar la ruta del archivo en lugar del contenido en bytes
        private string tipoArchivo;

        public Documentos(DocumentosController controller)
        {
            InitializeComponent();
            controladorDocumentos = controller;
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

        private void Documentos_MouseDown(object sender, MouseEventArgs e)
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
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion


        private void btnAñadir_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "All Files|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    rutaArchivo = ofd.FileName;  // Guardar la ruta del archivo

                    // Llenar los TextBox con el nombre y tipo del archivo
                    tbNombre.Text = Path.GetFileNameWithoutExtension(rutaArchivo);
                    tbTipo.Text = Path.GetExtension(rutaArchivo);
                    tipoArchivo = Path.GetExtension(rutaArchivo).ToLower();

                    // Mostrar imagen si es un archivo JPG o PNG
                    if (tipoArchivo.Equals(".jpg") || tipoArchivo.Equals(".png"))
                    {
                        pbDoc.Image = Image.FromFile(rutaArchivo);
                    }
                    else
                    {
                        pbDoc.Image = controladorDocumentos.ObtenerImagenPredefinida(tipoArchivo);
                    }
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Guardar el documento con la ruta del archivo
            controladorDocumentos.AgregarDocumento(tbNombre.Text, tbTipo.Text, rutaArchivo);
            this.Close();  // Cerrar la ventana
        }

        private void pbDoc_Click_1(object sender, EventArgs e)
        {
            // Abrir el archivo cuando se haga clic en el PictureBox
            if (!string.IsNullOrEmpty(rutaArchivo))
            {
                controladorDocumentos.AbrirArchivo(rutaArchivo);
            }
        }
    }
}
