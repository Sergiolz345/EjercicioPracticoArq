using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_de_registro_y_visualizacion_de_Clientes.Modelos;
using System.IO;
using System.Drawing;

namespace Sistema_de_registro_y_visualizacion_de_Clientes.Controladores
{

   public class DocumentosController
        {
            private List<Documento> listaDocumentos = new List<Documento>();
            private readonly string rutaImagenesPredefinidas = @"C:\Users\Sergio Lopez Castro\source\repos\Sistema de registro y visualizacion de Clientes\Sistema de registro y visualizacion de Clientes\Imagenes\";

        // Obtener la lista de documentos
        public List<Documento> ObtenerDocumentos()
            {
                return listaDocumentos;
            }

            // Método para agregar un documento con la ruta del archivo
            public void AgregarDocumento(string nombre, string tipo, string rutaArchivo)
            {
                Documento nuevoDoc = new Documento
                {
                    NombreArchivo = nombre,
                    TipoArchivo = tipo,
                    RutaArchivo = rutaArchivo  // Guardar la ruta en lugar del contenido binario
                };

                listaDocumentos.Add(nuevoDoc);
            }



            public Image ObtenerImagenPredefinida(string tipoArchivo)
        {
            // Asume que las imágenes predefinidas tienen el mismo nombre que la extensión del archivo
            string tipo = tipoArchivo.TrimStart('.').ToLower();  // Removemos el punto y convertimos a minúsculas
            string rutaImagen = Path.Combine(rutaImagenesPredefinidas, $"{tipo}.png");

            if (File.Exists(rutaImagen))
            {
                return Image.FromFile(rutaImagen);  // Solo se carga la imagen para mostrar en la UI, no para la base de datos
            }
            else
            {
                return Image.FromFile(Path.Combine(rutaImagenesPredefinidas, "pdf.png"));  // Imagen genérica si no encuentra la específica
            }
        }

        public void AbrirArchivo(string rutaArchivo)
        {
            try
            {
                System.Diagnostics.Process.Start(rutaArchivo);  // Abrir el archivo usando la ruta
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"No se pudo abrir el archivo: {ex.Message}");
            }
        }
    }
}