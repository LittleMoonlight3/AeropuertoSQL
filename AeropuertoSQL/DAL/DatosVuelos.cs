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
    public class DatosVuelos
    {
        Conexion con = new Conexion();

        public DataTable ObtenerTodosLosDatosVuelos()
        {
            using (SqlConnection conexion = con.conectar())
            {
                string query = "SELECT * FROM Aviones";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
        public void AgregarDatosVuelo(string modelo_avion, int capacidad, string estadoOperativo)
        {
            using (SqlConnection conexion = con.conectar())
            {
                string query = "INSERT INTO Aviones (modelo_avion,capacidad,estadoOperativo) VALUES (@modelo_avion,@capacidad,@estadoOperativo)";
                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@modelo_avion", modelo_avion);
                command.Parameters.AddWithValue("@capacidad", capacidad);
                command.Parameters.AddWithValue("@estadoOperativo", estadoOperativo);
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                command.ExecuteNonQuery();
            }
        }
        public void ModificarDatosVuelo(int idAvion, string modelo_avion, int capacidad, string estadoOperativo)
        {
            using (SqlConnection conexion = con.conectar())
            {
                string query = "UPDATE Aviones SET modelo_avion = @modelo_avion, capacidad = @capacidad, estadoOperativo = @estadoOperativo WHERE idAvion = @idAvion";
                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@idAvion", idAvion);
                command.Parameters.AddWithValue("@modelo_avion", modelo_avion);
                command.Parameters.AddWithValue("@capacidad", capacidad);
                command.Parameters.AddWithValue("@estadoOperativo", estadoOperativo);
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                command.ExecuteNonQuery();
            }
        }

        public void EliminarDatosVuelo(int idAvion)
        {
            using (SqlConnection conexion = new Conexion().conectar())
            {
                string query = "DELETE FROM Aviones WHERE idAvion = @idAvion";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@idAvion", idAvion);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
