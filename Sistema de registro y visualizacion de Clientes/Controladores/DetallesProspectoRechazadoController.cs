using Conexiones;
using Sistema_de_registro_y_visualizacion_de_Clientes.Modelos;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

public class DetallesProspectoRechazadoController
{
    private dbSqlServer _conexion;

    public DetallesProspectoRechazadoController()
    {
        _conexion = new dbSqlServer();
    }

    public Prospecto ObtenerDetallesProspectoRechazado(string nombre, string primerApellido, string segundoApellido, 
                                                   out List<Documento> documentos, out string observaciones)
{
    documentos = new List<Documento>();
    observaciones = string.Empty;
    Prospecto prospecto = null;

    try
    {
        _conexion.AbrirConexion();

        using (SqlCommand cmd = new SqlCommand("sp_ObtenerDetallesProspectoRechazado", _conexion.conexion))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@primerApellido", primerApellido);
            cmd.Parameters.AddWithValue("@segundoApellido", segundoApellido);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                // Leer los datos del prospecto rechazado
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

                // Mover al siguiente conjunto de resultados para los documentos
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        Documento documento = new Documento
                        {
                            NombreArchivo = reader["nombreArchivo"].ToString(),
                            TipoArchivo = reader["tipoArchivo"].ToString(),
                            RutaArchivo = reader["rutaArchivo"].ToString()
                        };
                        documentos.Add(documento);
                    }
                }

                // Mover al siguiente conjunto de resultados para las observaciones
                if (reader.NextResult())
                {
                    if (reader.Read())
                    {
                        observaciones = reader["observacion"].ToString();
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
