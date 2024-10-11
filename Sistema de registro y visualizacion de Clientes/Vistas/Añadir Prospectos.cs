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

namespace Sistema_de_registro_y_visualizacion_de_Clientes
{
    public partial class Añadir_Prospectos : Form
    {
        public dbSqlServer conexion = null;
        private DocumentosController controladorDocumentos = new DocumentosController();
        public String sData = "";
        string sUsuario, sPasword;
        private Usuario _usuario;

        public Añadir_Prospectos(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;

            conexion = new dbSqlServer();

            // Deshabilitar la capacidad de redimensionar columnas
            dgvDocumentos.AllowUserToResizeColumns = false;

            // Crear y añadir la columna de imagen al DataGridView si no está creada
            if (dgvDocumentos.Columns["Archivos"] == null)
            {
                DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
                imageColumn.Name = "Archivos";
                imageColumn.HeaderText = "Archivos";
                imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;  // Ajustar imagen dentro de la celda
                imageColumn.Width = 80;  // Establecer el tamaño de la columna a 80 píxeles
                dgvDocumentos.Columns.Add(imageColumn);
            }

            // Establecer la altura fija de las filas a 40 píxeles
            dgvDocumentos.RowTemplate.Height = 40;

            // Evento para manejar el clic en la columna de la imagen
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

        private void Añadir_Prospectos_MouseDown(object sender, MouseEventArgs e)
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Abre la ventana de documentos y le pasa el controlador
            Documentos formDocumentos = new Documentos(controladorDocumentos);
            formDocumentos.ShowDialog();

            // Actualiza el DataGridView con los documentos seleccionados
            dgvDocumentos.Rows.Clear();  // Limpiar filas anteriores
            foreach (var doc in controladorDocumentos.ObtenerDocumentos())
            {
                Image imagenArchivo = controladorDocumentos.ObtenerImagenPredefinida(doc.TipoArchivo);

                // Añadir una fila al DataGridView con Nombre, Tipo y la Imagen
                dgvDocumentos.Rows.Add(doc.RutaArchivo, doc.TipoArchivo, imagenArchivo);
            }
        }

        // Método para manejar el clic en las celdas del DataGridView
        private void dgvDocumentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si la celda clicada es de la columna de la imagen
            if (e.ColumnIndex == dgvDocumentos.Columns["Archivos"].Index && e.RowIndex >= 0)
            {
                // Obtener el documento correspondiente de la lista
                var documento = controladorDocumentos.ObtenerDocumentos()[e.RowIndex];

                // Llamar al método para abrir el archivo
                controladorDocumentos.AbrirArchivo(documento.RutaArchivo);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones para asegurarse de que todos los campos necesarios estén completos
            if (string.IsNullOrWhiteSpace(tbNombre.Text) || 
                string.IsNullOrWhiteSpace(tbApellido1.Text) || 
                string.IsNullOrWhiteSpace(tbCalle.Text) || 
                string.IsNullOrWhiteSpace(tbNumero.Text) || 
                string.IsNullOrWhiteSpace(tbColonia.Text) || 
                string.IsNullOrWhiteSpace(tbCodigoPostal.Text) || 
                string.IsNullOrWhiteSpace(tbTelefono.Text) || 
                string.IsNullOrWhiteSpace(tbRfc.Text))
            {
                MessageBox.Show("Todos los campos deben estar completos.");
                return;
            }

            // Capturar los datos del prospecto desde los campos del formulario
            Prospecto prospecto = new Prospecto
            {
                Nombre = tbNombre.Text,
                PrimerApellido = tbApellido1.Text,
                SegundoApellido = tbApellido2.Text,
                Calle = tbCalle.Text,
                Numero = tbNumero.Text,
                Colonia = tbColonia.Text,
                CodigoPostal = tbCodigoPostal.Text,
                Telefono = tbTelefono.Text,
                Rfc = tbRfc.Text,
                Estatus = "Enviado",  // Establecer el estatus inicial del prospecto
                FechaCreacion = DateTime.Now  // Guardar la fecha actual como fecha de creación
            };

            // Obtener la lista de documentos que fueron agregados desde el DocumentosController
            var listaDocumentos = controladorDocumentos.ObtenerDocumentos();

            // Verificar si se han ingresado documentos
            if (listaDocumentos.Count == 0)
            {
                MessageBox.Show("Debes agregar al menos un documento.");
                return;
            }

            // Crear una instancia del controlador de prospectos para manejar el guardado
            ProspectosController prospectosController = new ProspectosController();

            // Intentar guardar el prospecto junto con los documentos en la base de datos
            bool guardadoExitoso = prospectosController.AgregarProspectoConDocumentos(prospecto, listaDocumentos);

            // Verificar si la operación fue exitosa
            if (guardadoExitoso)
            {
                MessageBox.Show("El prospecto y los documentos se guardaron correctamente.");
                this.Hide();  // Cerrar la ventana después de guardar
                Menu c = new Menu(_usuario);
                c.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al guardar el prospecto y los documentos.");
            }
        }
    }
}
