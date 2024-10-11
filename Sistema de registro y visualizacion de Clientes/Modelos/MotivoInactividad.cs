using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_registro_y_visualizacion_de_Clientes.Modelos
{
    public class MotivoInactividad
    {
        public int IdMotivo { get; set; }
        public int IdProspecto { get; set; }
        public string Motivo { get; set; }
        public string DecididoPor { get; set; }
        public DateTime FechaInactividad { get; set; }
    }

}
