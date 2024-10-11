using Sistema_de_registro_y_visualizacion_de_Clientes.Controladores;
using Sistema_de_registro_y_visualizacion_de_Clientes.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sistema_de_registro_y_visualizacion_de_Clientes
{
    public partial class Aceptar_o_Rechazar : Form
    {
        private Prospecto _prospecto;
        private List<Documento> _documentos;
        private string Ruta;
        private string rutaImagenes = @"C:\Users\Sergio Lopez Castro\source\repos\Sistema de registro y visualizacion de Clientes\Sistema de registro y visualizacion de Clientes\Imagenes\";
        public event Action EstadoActualizado;

        public Aceptar_o_Rechazar(Prospecto prospecto, List<Documento> documentos)
        {
            InitializeComponent();
            _prospecto = prospecto;
            _documentos = documentos;

            // Llenar los labels con los datos del prospecto
            label1.Text = label1.Text + _prospecto.Nombre;
            label4.Text = label4.Text + _prospecto.PrimerApellido;
            label5.Text = label5.Text + _prospecto.SegundoApellido;
            label7.Text = label7.Text + _prospecto.Calle;
            label6.Text = label6.Text + _prospecto.Numero;
            label8.Text = label8.Text + _prospecto.Colonia;
            label10.Text = label10.Text + _prospecto.CodigoPostal;
            label2.Text = label2.Text + _prospecto.Telefono;
            label3.Text = label3.Text + _prospecto.Rfc;

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
                // Cargar la imagen basada en el tipo de archivo
                string tipoArchivo = doc.TipoArchivo.ToLower();
                Ruta = doc.RutaArchivo;
                Image icono;

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
                    icono = Image.FromFile(Path.Combine(rutaImagenes, "default.png")); // Imagen genérica
                }

                // Añadir el nombre del archivo, tipo y la imagen al DataGridView
                dgvDocumentos.Rows.Add(doc.NombreArchivo, doc.TipoArchivo, icono);
            }

            // Evento para manejar el clic en la columna de imagen
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

        private void Aceptar_o_Rechazar_MouseDown(object sender, MouseEventArgs e)
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


        // Método para manejar el clic en la columna de imagen
        private void dgvDocumentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si se hizo clic en la columna de la imagen ("Icono")
            if (e.ColumnIndex == dgvDocumentos.Columns["Icono"].Index && e.RowIndex >= 0)
            {
                // Obtener el documento correspondiente de la lista
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

        private void cbAutorizarar_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutorizar.Checked)
            {
                // Desmarcar el checkbox de Rechazar
                cbRechazar.Checked = false;

                // Desactivar el TextBox y Label de observaciones, ya que no son necesarias al aceptar
                label11.Enabled = false;
                tbObservaciones.Enabled = false;
            }
        }

        private void cbRechazar_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRechazar.Checked)
            {
                // Desmarcar el checkbox de Aceptar
                cbAutorizar.Checked = false;

                // Activar el TextBox y Label de observaciones, ya que son obligatorios al rechazar
                label11.Enabled = true;
                tbObservaciones.Enabled = true;
            }
            else
            {
                // Si se desmarca el checkbox de Rechazar, se desactiva el TextBox y Label
                label11.Enabled = false;
                tbObservaciones.Enabled = false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!cbAutorizar.Checked && !cbRechazar.Checked)
            {
                MessageBox.Show("Debe seleccionar Aceptar o Rechazar.");
                return;
            }

            string nuevoEstatus = cbAutorizar.Checked ? "Autorizado" : "Rechazado";
            var prospectoEstatusController = new ProspectoEstatusController();
            var observacionesController = new ObservacionesController();

            bool estatusActualizado = prospectoEstatusController.ActualizarEstatus(_prospecto.Nombre, nuevoEstatus);

            if (estatusActualizado)
            {
                if (cbRechazar.Checked)
                {
                    // Verificar si el campo de observaciones está vacío
                    if (string.IsNullOrEmpty(tbObservaciones.Text))
                    {
                        MessageBox.Show("Debe proporcionar una observación al rechazar el prospecto.");
                        return; // Detener el flujo si no hay observación
                    }

                    // Si la observación está presente, proceder a guardarla
                    bool observacionAgregada = observacionesController.AgregarObservacion(_prospecto.IdProspecto, tbObservaciones.Text);
                    if (!observacionAgregada)
                    {
                        MessageBox.Show("Ocurrió un error al agregar la observación.");
                        return;
                    }
                }


                // Disparar el evento de actualización de estado
                EstadoActualizado?.Invoke();

                MessageBox.Show("El prospecto fue actualizado correctamente.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al actualizar el estado del prospecto.");
            }
        }


    }
}
