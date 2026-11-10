using AeropuertoSQL.CLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeropuertoSQL
{
    public class Vuelos
    {
        Conexion con = new Conexion();

        public DataTable ObtenerTodosLosVuelos()
        {
            using (SqlConnection conexion = con.conectar())
            {
                string query = "SELECT * FROM Vuelos";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable; 
            }
        }
        public void AgregarVuelo(int idAvion,string destino, DateTime fechasalida,string estado)
        {
            using (SqlConnection conexion = con.conectar())
            {
                string query = "INSERT INTO Vuelos (idAvion,destino,fechasalida,estado) VALUES (@idAvion,@destino,@fechasalida,@estado)";
                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@idAvion", idAvion);
                command.Parameters.AddWithValue("@destino", destino);
                command.Parameters.AddWithValue("@fechasalida", fechasalida);
                command.Parameters.AddWithValue("@estado", estado);
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                command.ExecuteNonQuery();
            }
        }
        public void ModificarVuelo(int idVuelo, int idAvion, string destino, DateTime fechasalida, string estado)
        {
            using (SqlConnection conexion = con.conectar())
            {
                string query = "UPDATE Vuelos SET idAvion = @idAvion, destino = @destino, fechasalida = @fechasalida, estado = @estado WHERE idVuelo = @idVuelo";
                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@idVuelo", idVuelo);
                command.Parameters.AddWithValue("@idAvion", idAvion);
                command.Parameters.AddWithValue("@destino", destino);
                command.Parameters.AddWithValue("@fechasalida", fechasalida);
                command.Parameters.AddWithValue("@estado", estado);
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                command.ExecuteNonQuery();
            }
        }
    }
}
