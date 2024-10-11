using Conexiones;
using Sistema_de_registro_y_visualizacion_de_Clientes.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Sistema_de_registro_y_visualizacion_de_Clientes.Controladores
{
    public class DetallesProspectoController
    {
        private dbSqlServer _conexion;

        public DetallesProspectoController()
        {
            _conexion = new dbSqlServer();
        }

        public Prospecto ObtenerDetallesProspectoPorNombreYApellidos(string nombre, string primerApellido, string segundoApellido, out List<Documento> documentos, out List<Observacion> observaciones)
        {
            documentos = new List<Documento>();
            observaciones = new List<Observacion>();
            Prospecto prospecto = null;

            try
            {
                _conexion.AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("sp_ObtenerDetallesProspectoPorNombreYApellidos", _conexion.conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@primerApellido", primerApellido);
                    cmd.Parameters.AddWithValue("@segundoApellido", segundoApellido);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Obtener los datos del prospecto
                        if (reader.Read())
                        {
                            prospecto = new Prospecto
                            {
                                IdProspecto = Convert.ToInt32(reader["idProspecto"]),
                                Nombre = reader["nombre"].ToString(),
                                PrimerApellido = reader["primerApellido"].ToString(),
                                SegundoApellido = reader["segundoApellido"].ToString(),
                                Calle = reader["calle"].ToString(),
                                Numero = reader["numero"].ToString(),
                                Colonia = reader["colonia"].ToString(),
                                CodigoPostal = reader["codigoPostal"].ToString(),
                                Telefono = reader["telefono"].ToString(),
                                Rfc = reader["rfc"].ToString(),
                                Estatus = reader["estatus"].ToString(),
                                FechaCreacion = Convert.ToDateTime(reader["fechaCreacion"])
                            };
                        }

                        // Mover al siguiente conjunto de resultados para obtener los documentos
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                Documento documento = new Documento
                                {
                                    NombreArchivo = reader["nombreArchivo"].ToString(),
                                    TipoArchivo = reader["tipoArchivo"].ToString(),
                                    RutaArchivo = reader["rutaArchivo"].ToString()  // Capturar la ruta del archivo
                                };
                                documentos.Add(documento);
                            }
                        }

                        // Mover al siguiente conjunto de resultados para obtener las observaciones
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                Observacion observacion = new Observacion
                                {
                                    ObservacionTexto = reader["observacion"].ToString(),
                                    FechaObservacion = Convert.ToDateTime(reader["fechaObservacion"])
                                };
                                observaciones.Add(observacion);
                            }
                        }
                    }
                }

                _conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                _conexion.sLastError = ex.Message;
            }

            return prospecto;
        }
    }
}
