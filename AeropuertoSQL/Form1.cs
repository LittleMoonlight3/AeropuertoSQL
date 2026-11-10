using AeropuertoSQL.PL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
namespace AeropuertoSQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnVuelos_Click(object sender, EventArgs e)
        {
            frmVuelos ventanaVuelos = new frmVuelos();
            ventanaVuelos.Show();
        }
    }
}
