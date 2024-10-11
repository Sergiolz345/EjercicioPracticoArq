using Conexiones;
using Sistema_de_registro_y_visualizacion_de_Clientes.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Sistema_de_registro_y_visualizacion_de_Clientes.Controladores
{
    public class ListadoProspectosController
    {
        private dbSqlServer _conexion;

        public ListadoProspectosController()
        {
            _conexion = new dbSqlServer();
        }

        public List<Prospecto> ObtenerProspectos()
        {
            List<Prospecto> prospectos = new List<Prospecto>();
            try
            {
                _conexion.AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("sp_ObtenerProspectos", _conexion.conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Prospecto prospecto = new Prospecto
                            {
                                Nombre = reader["nombre"].ToString(),
                                PrimerApellido = reader["primerApellido"].ToString(),
                                SegundoApellido = reader["segundoApellido"].ToString(),
                                Estatus = reader["estatus"].ToString()
                            };
                            prospectos.Add(prospecto);
                        }
                    }
                }
                _conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                _conexion.sLastError = ex.Message;
            }
            return prospectos;
        }
    }
}
