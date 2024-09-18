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
    public partial class Login : Form
    {
        bool empleado=true;
        bool gerente;
        public Login()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void uusuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            empleado = true;
            gerente = false;
            labelTitulo2.Text = "Ingresar como empleado";
        }

        private void BTN_ENTRAR_Click(object sender, EventArgs e)
        {
            if (empleado)
            {
                var nuevoForm = new GestionEmpleados();
                nuevoForm.Show();
            }
            else if(gerente)
            {
                var nuevoForm = new GestionPuestos();
                nuevoForm.Show();
            }
            //this.Close();
        }

        private void menuGerente_Click(object sender, EventArgs e)
        {
            gerente = true;
            empleado = false;
            
            labelTitulo2.Text = "Ingresar como gerente";
            
        }

        private void labelTitulo2_Click(object sender, EventArgs e)
        {
           
        }
    }
}
