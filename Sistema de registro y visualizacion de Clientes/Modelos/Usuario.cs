using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_registro_y_visualizacion_de_Clientes.Modelos
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Rol { get; set; }
        public byte[] ImagenUsuario { get; set; }
    }

}
