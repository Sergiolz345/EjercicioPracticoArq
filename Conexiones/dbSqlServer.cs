using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration; // Para acceder a App.config

namespace Conexiones
{
    public class dbSqlServer
    {
        public String sDatabase;
        public string sLastError = "";
        public SqlConnection conexion;

        // Constructor sin parámetros
        public dbSqlServer()
        {
            // Obtener la cadena de conexión del archivo App.config
            string connectionString = ConfigurationManager.ConnectionStrings["MiCadenaConexion"].ConnectionString;

            // Crear la conexión con la base de datos
            conexion = new SqlConnection(connectionString);
        }

        public Boolean AbrirConexion()
        {
            Boolean bALLOK = true;

            try
            {
                conexion.Open();
            }
            catch (Exception EX)
            {
                sLastError = EX.Message;
                bALLOK = false;
            }
            return bALLOK;
        }

        public Boolean ConexionAbierta()
        {
            Boolean bALLOK = true;
            try
            {
                bALLOK = conexion.State == System.Data.ConnectionState.Open ? true : false;
            }
            catch (Exception EX)
            {
                sLastError = EX.Message;
                bALLOK = false;
            }
            return bALLOK;
        }

        public Boolean EjecutarCommando(String sCmd)
        {
            Boolean bALLOK = true;

            conexion.Open();

            try
            {
                using (SqlCommand cmd = new SqlCommand(sCmd, conexion))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            catch (Exception EX)
            {
                bALLOK = false;
                sLastError = EX.Message;
            }
            conexion.Close();

            return bALLOK;
        }

        public void CerrarConexion()
        {
            conexion.Close();
        }
    }
}
