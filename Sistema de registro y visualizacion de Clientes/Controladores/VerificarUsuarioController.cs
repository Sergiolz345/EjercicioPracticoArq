using Conexiones;
using Sistema_de_registro_y_visualizacion_de_Clientes.Modelos;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Sistema_de_registro_y_visualizacion_de_Clientes.Controladores
{
    public class VerificarUsuarioController
    {
        private dbSqlServer _conexion;

        public VerificarUsuarioController()
        {
            _conexion = new dbSqlServer();
        }

        public Usuario VerificarUsuario(string nombreUsuario, string contrasena)
        {
            Usuario usuario = null;

            try
            {
                _conexion.AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("sp_VerificarUsuario", _conexion.conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                    cmd.Parameters.AddWithValue("@contraseña", contrasena);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(reader["idUsuario"]),
                                NombreUsuario = reader["nombreUsuario"].ToString(),
                                ImagenUsuario = reader["imagenUsuario"] != DBNull.Value ? (byte[])reader["imagenUsuario"] : null
                            };
                        }
                    }
                }

                _conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                _conexion.sLastError = ex.Message;
            }

            return usuario;
        }
    }
}
