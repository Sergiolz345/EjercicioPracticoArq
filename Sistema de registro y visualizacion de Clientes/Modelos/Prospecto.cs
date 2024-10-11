using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_registro_y_visualizacion_de_Clientes.Modelos
{
    public class Prospecto
    {
        public int IdProspecto { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Colonia { get; set; }
        public string CodigoPostal { get; set; }
        public string Telefono { get; set; }
        public string Rfc { get; set; }
        public string Estatus { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
