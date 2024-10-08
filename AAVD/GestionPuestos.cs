﻿using System;
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
    public partial class GestionPuestos : Form
    {
        public GestionPuestos()
        {
            InitializeComponent();
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var nuevoForm = new GestionEmpleados();
            nuevoForm.Show();
            this.Close();
        }

        private void departamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var nuevoForm = new GestionDepartamentos();
            nuevoForm.Show();
            this.Close();
        }

        private void puestosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var nuevoForm = new GestionEmpleados();
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
