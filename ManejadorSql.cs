using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;

namespace Entidades
{

    internal static class ManejadorSql
    {
        static SqlConnection conexion;
        static SqlCommand comando;

        static ManejadorSql()
        {
            conexion = new SqlConnection(@"Data Source=.;Database=ExamenPrimerFecha2022;Trusted_Connection=True;");

            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.Connection = conexion;

        }

        internal static List<Auto> ObtenerAutos()
        {
            List<Auto> autos = new List<Auto>();

            try
            {
                conexion.Open();
                comando.CommandText = $"SELECT * FROM Vehiculos";

                using (SqlDataReader dataReader = comando.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        autos.Add(new Auto(Convert.ToInt32(dataReader["Presion"]), dataReader["Patente"].ToString()));
                    }

                }
            }
            finally
            {
                conexion.Close();
            }

            return autos;
        }

        internal static bool InsertarVehiculo(Vehiculo vehiculo)
        {
            try
            {
                comando.CommandText = "Insert into Vehiculos values (@Presion, @Patente)";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@Presion", vehiculo.PresionInflado);
                comando.Parameters.AddWithValue("@Patente", vehiculo.Patente);

                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                comando.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }

            finally
            {
                conexion.Close();
            }
        }

    }
}
