using Conexiones;
using Sistema_de_registro_y_visualizacion_de_Clientes.Controladores;
using Sistema_de_registro_y_visualizacion_de_Clientes.Modelos;
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
using System.IO;

namespace Sistema_de_registro_y_visualizacion_de_Clientes
{
    public partial class Lista_Prospectos : Form
    {
        private Usuario _usuario;
        private FiltrarProspectosController filtrarProspectosController;
        private string rutaImagen = @"C:\Users\Sergio Lopez Castro\source\repos\Sistema de registro y visualizacion de Clientes\Sistema de registro y visualizacion de Clientes\Imagenes\CargarDatos.png";

        public Lista_Prospectos(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            filtrarProspectosController = new FiltrarProspectosController();

            // Agregar la columna de imagen si no existe
            if (dgvProspecto.Columns["CargarDatos"] == null)
            {
                DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
                imageColumn.Name = "CargarDatos";
                imageColumn.HeaderText = "Acciones";
                imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Ajustar la imagen dentro de la celda
                dgvProspecto.Columns.Add(imageColumn);
            }

            // Asignar el evento CellClick para manejar los clics en la columna de imagen
            dgvProspecto.CellClick += dgvProspecto_CellClick;
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

        private void Lista_Prospectos_MouseDown(object sender, MouseEventArgs e)
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
        private void btnVolverMenu_Click(object sender, EventArgs e)
        {
            this.Hide();

            Menu c = new Menu(_usuario);
            c.ShowDialog();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        private void Lista_Prospectos_Load(object sender, EventArgs e)
        {
            // Llenar el ComboBox con los estados disponibles
            cbFiltro.Items.Add("Enviado");
            cbFiltro.Items.Add("Autorizado");
            cbFiltro.Items.Add("Rechazado");
            cbFiltro.SelectedIndex = 0; // Seleccionar el primer estado por defecto

            // Cargar los prospectos con el estado seleccionado
            CargarProspectosPorEstado(cbFiltro.SelectedItem.ToString());
        }

        private void cbFiltro_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // Recargar el DataGridView cuando cambie el filtro
            CargarProspectosPorEstado(cbFiltro.SelectedItem.ToString());
        }

        private void CargarProspectosPorEstado(string estadoSeleccionado)
        {
            // Llamar al controlador para obtener la lista de prospectos por estado
            List<Prospecto> listaProspectos = filtrarProspectosController.ObtenerProspectosPorEstado(estadoSeleccionado);

            // Limpiar el DataGridView antes de añadir los nuevos datos
            dgvProspecto.Rows.Clear();

            // Verificar si la imagen existe
            if (!File.Exists(rutaImagen))
            {
                MessageBox.Show("No se encontró la imagen CargarDatos.png en la ruta especificada.");
                return;
            }

            // Cargar la imagen desde la ruta
            Image cargarImagen = Image.FromFile(rutaImagen);

            // Añadir los prospectos al DataGridView con la columna de imagen
            foreach (var prospecto in listaProspectos)
            {
                dgvProspecto.Rows.Add(prospecto.Nombre, prospecto.PrimerApellido, prospecto.SegundoApellido, prospecto.Estatus, cargarImagen);
            }
        }

        // Método para manejar el clic en las celdas del DataGridView
        private void dgvProspecto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvProspecto.Columns["CargarDatos"].Index && e.RowIndex >= 0)
            {
                string nombre = dgvProspecto.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                string primerApellido = dgvProspecto.Rows[e.RowIndex].Cells["Primer_Apellido"].Value.ToString();
                string segundoApellido = dgvProspecto.Rows[e.RowIndex].Cells["Segundo_Apellido"].Value.ToString();

                // Llamar al controlador para obtener los detalles del prospecto
                DetallesProspectoController detallesProspectoController = new DetallesProspectoController();
                List<Documento> documentos;
                List<Observacion> observaciones;
                Prospecto prospecto = detallesProspectoController.ObtenerDetallesProspectoPorNombreYApellidos(nombre, primerApellido, segundoApellido, out documentos, out observaciones);

                if (prospecto != null)
                {
                    string estado = dgvProspecto.Rows[e.RowIndex].Cells["Estatus"].Value.ToString();

                    if (estado == "Enviado")
                    {
                        Aceptar_o_Rechazar formAceptarRechazar = new Aceptar_o_Rechazar(prospecto, documentos);

                        // Suscribir al evento EstadoActualizado para actualizar la lista cuando cambie el estado
                        formAceptarRechazar.EstadoActualizado += () =>
                        {
                            // Recargar el DataGridView con los nuevos datos
                            CargarProspectosPorEstado(cbFiltro.SelectedItem.ToString());
                        };

                        formAceptarRechazar.ShowDialog();
                    }

                    else if (estado == "Rechazado")
                    {
                        Rechazado formRechazado = new Rechazado(prospecto, documentos, observaciones);
                        formRechazado.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron detalles para el prospecto seleccionado.");
                }
            }
        }

    }
}

