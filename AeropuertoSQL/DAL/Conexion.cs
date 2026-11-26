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
<<<<<<< HEAD
        string cadena = @"Server=(localdb)\MSSQLLocalDB;Database=Aeropuerto;Integrated Security=true";
=======
        string cadena = @"Server=localhost\SQLEXPRESS;Database=Aeropuerto;Integrated Security=true";
>>>>>>> 0023ad08749aa07fe4f1823a5f68fb9643a86c14
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
