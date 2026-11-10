using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace AeropuertoSQL.CLL
{
    class Conexion
    {
        string cadena = "Server=localhost;Database=Aeropuerto;Integrated Security=true";
        public SqlConnection conectar()
        {
            SqlConnection cn = new SqlConnection(cadena);
            try
            {
                cn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexion jeje" + ex.Message);
            }
            return cn;
        }
    }
 }
