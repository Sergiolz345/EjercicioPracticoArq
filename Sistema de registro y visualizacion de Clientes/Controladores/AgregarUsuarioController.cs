using Conexiones;
using Sistema_de_registro_y_visualizacion_de_Clientes.Modelos;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Sistema_de_registro_y_visualizacion_de_Clientes.Controladores
{
    public class AgregarUsuarioController
    {
        private dbSqlServer _conexion;

        public AgregarUsuarioController()
        {
            _conexion = new dbSqlServer();
        }

        public bool AgregarUsuario(Usuario usuario)
        {
            try
            {
                _conexion.AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("sp_AgregarUsuario", _conexion.conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nombreUsuario", usuario.NombreUsuario);
                    cmd.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
                    cmd.Parameters.AddWithValue("@rol", usuario.Rol ?? "Usuario");

                    if (usuario.ImagenUsuario != null)
                    {
                        cmd.Parameters.AddWithValue("@imagenUsuario", usuario.ImagenUsuario);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@imagenUsuario", DBNull.Value);
                    }

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
