using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_registro_y_visualizacion_de_Clientes.Modelos
{
    public class Documento
    {
        public int IdDocumento { get; set; }
        public int IdProspecto { get; set; }
        public string NombreArchivo { get; set; }
        public string TipoArchivo { get; set; }
        public string RutaArchivo { get; set; }  // Ahora es la ruta del archivo en lugar del contenido binario
    }
}
