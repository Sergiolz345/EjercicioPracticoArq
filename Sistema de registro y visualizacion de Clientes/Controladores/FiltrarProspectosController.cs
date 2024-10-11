using Conexiones;
using Sistema_de_registro_y_visualizacion_de_Clientes.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Sistema_de_registro_y_visualizacion_de_Clientes.Controladores
{
    public class FiltrarProspectosController
    {
        private dbSqlServer _conexion;

        public FiltrarProspectosController()
        {
            _conexion = new dbSqlServer();
        }

        // Método para obtener los prospectos filtrados por estado
        public List<Prospecto> ObtenerProspectosPorEstado(string estado)
        {
            List<Prospecto> listaProspectos = new List<Prospecto>();

            try
            {
                _conexion.AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("sp_ObtenerProspectosPorEstado", _conexion.conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@estado", estado);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Prospecto prospecto = new Prospecto
                            {
                                IdProspecto = Convert.ToInt32(reader["idProspecto"]),
                                Nombre = reader["nombre"].ToString(),
                                PrimerApellido = reader["primerApellido"].ToString(),
                                SegundoApellido = reader["segundoApellido"].ToString(),
                                Estatus = reader["estatus"].ToString()
                            };

                            listaProspectos.Add(prospecto);
                        }
                    }
                }

                _conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                _conexion.sLastError = ex.Message;
            }

            return listaProspectos;
        }
    }
}
