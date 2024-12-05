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
    public partial class GestionPuestos : Form
    {
        public GestionPuestos()
        {
            InitializeComponent();

            // Inicializa la instancia de EnlaceCassandra
            EnlaceCassandra enlaceCassandra = new EnlaceCassandra();
            // Carga la lista de empleados en el ListBox
            List<Puestos> puestos = enlaceCassandra.Get_All_PUE();

            //// Establece el DataSource del ListBox con la lista de puestos
            //PP_ListaPuestosPUE.DataSource = puestos;

            //// Configura el DisplayMember para mostrar la propiedad 'Puesto' del objeto Puestos
            //PP_ListaPuestosPUE.DisplayMember = "Puesto";

            // Carga los puestos en la tabla PP_TablaPuestosPUE
            PP_TablaPuestosPUE.DataSource = puestos;

            // Cambia los encabezados de las columnas
            PP_TablaPuestosPUE.Columns["Puesto"].HeaderText = "Puesto";
            //PP_TablaPuestosPUE.Columns["Departamento"].HeaderText = "Departamento";
            PP_TablaPuestosPUE.Columns["Cuota_fija"].HeaderText = "Cuota Fija";
           /* PP_TablaPuestosPUE.Columns["Salario_diario"].HeaderText = "Salario Diario"*/;
            PP_TablaPuestosPUE.Columns["fecha_de_alta"].HeaderText = "Fecha de Alta";
            // Oculta la columna Id (si existe)
            if (PP_TablaPuestosPUE.Columns.Contains("Id"))
            {
                PP_TablaPuestosPUE.Columns["Id"].Visible = false;
            }

            // Obtiene la lista de empleados
            List<Empleado> empleados = enlaceCassandra.Get_All_EMP();

            //// Asigna la lista de empleados al DataGridView
            //PN_TablaSalariosPUE.DataSource = empleados;
        }
        //Declaraciones--------------------------------------------------------------------------------------------------
        
        //Tablas --------------------------------------------------------------------------------------------------------


        //Botones -------------------------------------------------------------------------------------------------------

        //Selecciones del menu ------------------------------------------------------------------------------------------
        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void departamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var nuevoForm = new GestionDepartamentos();
            nuevoForm.Show();
            this.Close();
        }

        private void puestosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
        }

        private void administrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var nuevoForm = new Administrador();
            nuevoForm.Show();
            this.Close();
        }

        private void catalogoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var nuevoForm = new Catalogo();
            nuevoForm.Show();
            this.Close();
        }
        
        private void GestionPuestos_Load_1(object sender, EventArgs e)
        {
            // Actualiza el control en la UI
            US_UsuarioActual.Text = Login.NombreUsuarioActual;
            US_UsuarioActual.Refresh();

            EnlaceCassandra comex = new EnlaceCassandra();
            PP_TablaPuestosPUE.DataSource = comex.Get_All_NOM();
            //OCTL();
            var data = comex.Get_All_EMP();

            //try
            //{

            //    EnlaceCassandra comex1 = new EnlaceCassandra();
            //    //PP_ListaPuestosPUE.DataSource = comex1.Get_All_PUE();
            //    var data1 = comex1.Get_All_PUE();
            //    //PP_ListaPuestosPUE.DisplayMember = "Puesto";
            //    //PP_ListaPuestosPUE.ValueMember = "Puesto";
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error al cargar los puestos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            //EnlaceCassandra comex = new EnlaceCassandra();
            ////PN_TablaSalariosPUE.DataSource = comex.Get_All_NOM();
            ////ocultanom();
            //var data = comex.Get_All_NOM();
        }
        private void ocultanom()
        {
            string[] columnasOcultas = { "Salario_diario", "Cuota_fija", "Dias_trabajados", "Horas_productivas", "Horas_extras", "faltas", "Salario_total_letra", "departamento", "Folio", "fecha_de_alta", "fecha_de_final"};

            foreach (string columna in columnasOcultas)
            {
                //PN_TablaSalariosPUE.Columns[columna].Visible = false;
            }
        }

        private void PP_ListaPuestosPUE_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string puestoSeleccionado = PP_ListaPuestosPUE.SelectedValue.ToString();
            
           
        }


        // --------------------------------------------------------------------------------------------------------------
    }

    

}
