using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_registro_y_visualizacion_de_Clientes.Modelos
{
    public class Observacion
    {
        public int IdObservacion { get; set; }
        public int IdProspecto { get; set; }
        public string ObservacionTexto { get; set; }
        public DateTime FechaObservacion { get; set; }
    }

}
