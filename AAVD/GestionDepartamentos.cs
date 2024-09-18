using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AAVD
{
    public partial class GestionDepartamentos : Form
    {
        public GestionDepartamentos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var nuevoForm = new GestionEmpleados();
            nuevoForm.Show();
            this.Close();
        }

        private void departamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void puestosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var nuevoForm = new GestionPuestos();
            nuevoForm.Show();
            this.Close();
        }

        private void administrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var nuevoForm = new Administrador();
            nuevoForm.Show();
            this.Close();
        }
    }
}
