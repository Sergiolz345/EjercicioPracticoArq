using Conexiones;
using Sistema_de_registro_y_visualizacion_de_Clientes.Modelos;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Sistema_de_registro_y_visualizacion_de_Clientes.Controladores
{
    public class ObservacionesController
    {
        private dbSqlServer _conexion;

        public ObservacionesController()
        {
            _conexion = new dbSqlServer();
        }

        // Método para agregar una observación
        public bool AgregarObservacion(int idProspecto, string observacion)
        {
            try
            {
                _conexion.AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("sp_AgregarObservacionProspecto", _conexion.conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idProspecto", idProspecto);
                    cmd.Parameters.AddWithValue("@observacion", observacion);
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
