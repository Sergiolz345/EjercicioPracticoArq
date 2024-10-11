using Sistema_de_registro_y_visualizacion_de_Clientes.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;  // Para manejar archivos y rutas
using System.Drawing;  // Para manejar imágenes
using Sistema_de_registro_y_visualizacion_de_Clientes.Controladores;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;

namespace Sistema_de_registro_y_visualizacion_de_Clientes
{
    public partial class Rechazado : Form
    {
        private Prospecto _prospecto;
        private List<Documento> _documentos;
        private List<Observacion> _observaciones;
        private string rutaImagenes = @"C:\Users\Sergio Lopez Castro\source\repos\Sistema de registro y visualizacion de Clientes\Sistema de registro y visualizacion de Clientes\Imagenes\";

        public Rechazado(Prospecto prospecto, List<Documento> documentos, List<Observacion> observaciones)
        {
            InitializeComponent();
            _prospecto = prospecto;
            _documentos = documentos;
            _observaciones = observaciones;

            // Llenar los labels con los datos del prospecto
            label1.Text = "Nombre: " + _prospecto.Nombre;
            label4.Text = "Primer Apellido: " + _prospecto.PrimerApellido;
            label5.Text = "Segundo Apellido: " + _prospecto.SegundoApellido;
            label7.Text = "Calle: " + _prospecto.Calle;
            label6.Text = "Número: " + _prospecto.Numero;
            label8.Text = "Colonia: " + _prospecto.Colonia;
            label10.Text = "Código Postal: " + _prospecto.CodigoPostal;
            label2.Text = "Teléfono: " + _prospecto.Telefono;
            label3.Text = "RFC: " + _prospecto.Rfc;

            // Crear y añadir la columna de imagen al DataGridView si no existe
            if (dgvDocumentos.Columns["Icono"] == null)
            {
                DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();
                iconColumn.Name = "Icono";
                iconColumn.HeaderText = "Tipo";
                iconColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Ajustar la imagen en la celda
                iconColumn.Width = 50; // Tamaño de la columna
                dgvDocumentos.Columns.Add(iconColumn);
            }

            // Establecer la altura fija de las filas
            dgvDocumentos.RowTemplate.Height = 40;

            // Llenar el DataGridView con los documentos y la imagen
            dgvDocumentos.Rows.Clear();
            foreach (var doc in _documentos)
            {
                string tipoArchivo = doc.TipoArchivo.ToLower();
                Image icono;

                // Seleccionar el icono basado en el tipo de archivo
                if (tipoArchivo == ".pdf")
                {
                    icono = Image.FromFile(Path.Combine(rutaImagenes, "pdf.png"));
                }
                else if (tipoArchivo == ".jpg" || tipoArchivo == ".jpeg" || tipoArchivo == ".png")
                {
                    icono = Image.FromFile(Path.Combine(rutaImagenes, "image.png"));
                }
                else
                {
                    icono = Image.FromFile(Path.Combine(rutaImagenes, "default.png")); // Imagen genérica para otros tipos de archivo
                }

                // Añadir el nombre del archivo, tipo y la imagen al DataGridView
                dgvDocumentos.Rows.Add(doc.NombreArchivo, doc.TipoArchivo, icono);
            }

            // Llenar el TextBox de observaciones
            tbObservaciones.Text = string.Join(Environment.NewLine, _observaciones.Select(o => o.ObservacionTexto));

            // Desactivar la edición del TextBox (solo lectura)
            tbObservaciones.ReadOnly = true;

            // Evento para manejar el clic en la columna de la imagen y abrir el archivo
            dgvDocumentos.CellClick += dgvDocumentos_CellClick;
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

        private void Rechazado_MouseDown(object sender, MouseEventArgs e)
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


        // Método para manejar el clic en la columna de imagen o el documento
        private void dgvDocumentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si se hizo clic en la columna de la imagen
            if (e.ColumnIndex == dgvDocumentos.Columns["Icono"].Index && e.RowIndex >= 0)
            {
                // Obtener el documento correspondiente
                var documento = _documentos[e.RowIndex];

                // Verificar si la ruta del archivo no es nula o vacía
                if (!string.IsNullOrEmpty(documento.RutaArchivo))
                {
                    try
                    {
                        // Abrir el archivo directamente desde su ruta
                        System.Diagnostics.Process.Start(documento.RutaArchivo);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al abrir el archivo: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("La ruta del archivo es nula o está vacía. No se puede abrir el archivo.");
                }
            }
        }
    }
}
