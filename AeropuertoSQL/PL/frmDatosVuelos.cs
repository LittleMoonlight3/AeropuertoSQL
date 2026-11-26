using AeropuertoSQL.CLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeropuertoSQL.PL
{
    public partial class frmDatosVuelos : Form
    {

        public frmDatosVuelos()
        {
            InitializeComponent();
        }

        private int idAvionSeleccionado = 0;

        private void frmDatosVuelos_Load_1(object sender, EventArgs e)
        {
            DatosVuelos datosDatosVuelos = new DatosVuelos();
            dataGridView2.DataSource = datosDatosVuelos.ObtenerTodosLosDatosVuelos();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dataGridView2.Rows[e.RowIndex];
                idAvionSeleccionado = Convert.ToInt32(fila.Cells["idAvion"].Value);
                txtmodelo.Text = fila.Cells["modelo_avion"].Value.ToString();
                txtcapacidad.Text = fila.Cells["capacidad"].Value.ToString();
                txtestado.Text = fila.Cells["estadoOperativo"].Value.ToString();
            }
        }


        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            DatosVuelos datosVuelos = new DatosVuelos();
            string modelo_avion = txtmodelo.Text;
            int capacidad = Convert.ToInt32(txtcapacidad.Text);
            string estadoOperativo = txtestado.Text;
            datosVuelos.AgregarDatosVuelo(modelo_avion, capacidad, estadoOperativo);
            MessageBox.Show("Datos agregados correctamente");
            dataGridView2.DataSource = datosVuelos.ObtenerTodosLosDatosVuelos();
        }

        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            if (idAvionSeleccionado == 0)
            {
                MessageBox.Show("Por favor, selecciona un dato de la tabla primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DatosVuelos datosVuelos = new DatosVuelos();
            string modelo_avion = txtmodelo.Text;
            int capacidad = Convert.ToInt32(txtcapacidad.Text);
            string estadoOperativo = txtestado.Text;
            datosVuelos.ModificarDatosVuelo(idAvionSeleccionado, modelo_avion, capacidad, estadoOperativo);
            MessageBox.Show("Datos modificados correctamente");
            dataGridView2.DataSource = datosVuelos.ObtenerTodosLosDatosVuelos();
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (idAvionSeleccionado == 0)
            {
                MessageBox.Show("Por favor, selecciona un dato de la tabla primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar este dato?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion == DialogResult.Yes)
            {
                DatosVuelos datosVuelos = new DatosVuelos();
                datosVuelos.EliminarDatosVuelo(idAvionSeleccionado);

                MessageBox.Show("Dato eliminado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                dataGridView2.DataSource = datosVuelos.ObtenerTodosLosDatosVuelos();
                idAvionSeleccionado = 0;
                txtmodelo.Clear();
                txtcapacidad.Clear();
                txtestado.Clear();
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Desea cerrar la ventana?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
