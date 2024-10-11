using Conexiones;
using Sistema_de_registro_y_visualizacion_de_Clientes.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Sistema_de_registro_y_visualizacion_de_Clientes.Controladores
{
    public class ProspectosController
    {
        private dbSqlServer _conexion;

        public ProspectosController()
        {
            _conexion = new dbSqlServer();
        }

        public bool AgregarProspectoConDocumentos(Prospecto prospecto, List<Documento> documentos)
        {
            try
            {
                _conexion.AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("sp_AgregarProspectoConDocumentos", _conexion.conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros del prospecto
                    cmd.Parameters.AddWithValue("@nombre", prospecto.Nombre);
                    cmd.Parameters.AddWithValue("@primerApellido", prospecto.PrimerApellido);
                    cmd.Parameters.AddWithValue("@segundoApellido", prospecto.SegundoApellido ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@calle", prospecto.Calle);
                    cmd.Parameters.AddWithValue("@numero", prospecto.Numero);
                    cmd.Parameters.AddWithValue("@colonia", prospecto.Colonia);
                    cmd.Parameters.AddWithValue("@codigoPostal", prospecto.CodigoPostal);
                    cmd.Parameters.AddWithValue("@telefono", prospecto.Telefono);
                    cmd.Parameters.AddWithValue("@rfc", prospecto.Rfc);
                    cmd.Parameters.AddWithValue("@estatus", prospecto.Estatus);

                    // Crear la tabla para los documentos
                    DataTable documentosTable = new DataTable();
                    documentosTable.Columns.Add("nombreArchivo", typeof(string));
                    documentosTable.Columns.Add("tipoArchivo", typeof(string));
                    documentosTable.Columns.Add("rutaArchivo", typeof(string));  // Guardar la ruta del archivo

                    foreach (var doc in documentos)
                    {
                        documentosTable.Rows.Add(doc.NombreArchivo, doc.TipoArchivo, doc.RutaArchivo);
                    }

                    // Parámetro de la tabla de documentos
                    SqlParameter documentosParam = cmd.Parameters.AddWithValue("@documentos", documentosTable);
                    documentosParam.SqlDbType = SqlDbType.Structured;

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
