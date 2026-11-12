using AeropuertoSQL.CLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeropuertoSQL.PL
{
    public partial class frmVuelos : Form
    {
        public frmVuelos()
        {
            InitializeComponent();
        }
        private int idVueloSeleccionado = 0;
        private void frmVuelos_Load(object sender, EventArgs e)
        {
            Vuelos datosVuelos = new Vuelos();
            dataGridView1.DataSource = datosVuelos.ObtenerTodosLosVuelos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Vuelos vuelos = new Vuelos();
            int idAvion = Convert.ToInt32(txtidAvion.Text);
            string destino = txtdestino.Text;
            DateTime fecha = dtpfechasalida.Value;
            string estado = txtestado.Text;
            vuelos.AgregarVuelo(idAvion, destino, fecha, estado);
            MessageBox.Show("Vuelo agregado correctamente");
            dataGridView1.DataSource = vuelos.ObtenerTodosLosVuelos();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
                idVueloSeleccionado = Convert.ToInt32(fila.Cells["idVuelo"].Value);
                txtidAvion.Text = fila.Cells["idAvion"].Value.ToString();
                txtdestino.Text = fila.Cells["destino"].Value.ToString();
                dtpfechasalida.Value = Convert.ToDateTime(fila.Cells["fechasalida"].Value);
                txtestado.Text = fila.Cells["estado"].Value.ToString();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idVueloSeleccionado == 0)
            {
                MessageBox.Show("Por favor, selecciona un vuelo de la tabla primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Vuelos vuelos = new Vuelos();
            int idAvion = Convert.ToInt32(txtidAvion.Text);
            string destino = txtdestino.Text;
            DateTime fecha = dtpfechasalida.Value;
            string estado = txtestado.Text;
            vuelos.ModificarVuelo(idVueloSeleccionado, idAvion, destino, fecha, estado);
            MessageBox.Show("Vuelo modificado correctamente");
            dataGridView1.DataSource = vuelos.ObtenerTodosLosVuelos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result=MessageBox.Show("¿Desea cerrar la ventana?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
           
        }

        public void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
           using(SqlConnection conexion = new Conexion().conectar())
            {
                string query = "SELECT * FROM Vuelos WHERE idVuelo LIKE @idVuelo";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
                adapter.SelectCommand.Parameters.AddWithValue("@idVuelo", "%" + txtBusqueda.Text + "%");
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }
    }
}
