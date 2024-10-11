using Conexiones;
using Sistema_de_registro_y_visualizacion_de_Clientes.Modelos;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Sistema_de_registro_y_visualizacion_de_Clientes.Controladores
{
    public class ProspectoEstatusController
    {
        private dbSqlServer _conexion;

        public ProspectoEstatusController()
        {
            _conexion = new dbSqlServer();
        }

        // Método para actualizar el estado del prospecto
        public bool ActualizarEstatus(string nombreUseuario, string nuevoEstatus)
        {
            try
            {
                _conexion.AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("sp_ActualizarEstatusProspecto", _conexion.conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombreUseuario", nombreUseuario);
                    cmd.Parameters.AddWithValue("@nuevoEstatus", nuevoEstatus);
                    cmd.ExecuteNonQuery();
                }

                _conexion.CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                _conexion.sLastError = ex.Message;
                return false;
            }
        }
    }
}
