using Cassandra.Mapping;
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
            puestosToolStripMenuItem.Visible = false;
            // Inicializa la instancia de EnlaceCassandra
            EnlaceCassandra enlaceCassandra = new EnlaceCassandra();

            // Obtiene la lista de departamentos
            List<Departamentos> departamentos = enlaceCassandra.Get_All_DEP();
            List<Puestos> puestos = enlaceCassandra.Get_All_PUE();

            // Configura el ComboBox Registro de puestos
            RG_PorDepa.DataSource = departamentos; // Carga la lista en el ComboBox
            RG_PorDepa.DisplayMember = "Departamento"; // Muestra el nombre del departamento

            // Configura el ComboBox Registro de puestos
            HC_DeptoPuestoDEP.DataSource = departamentos; // Carga la lista en el ComboBox
            HC_DeptoPuestoDEP.DisplayMember = "Departamento"; // Muestra el nombre del departamento

            // Limpia el DataGridView
            HC_TablaPorDeptoDEP.DataSource = null;
            // Limpia el DataGridView
            HC_TablaPorPuestoDEP.DataSource = null;

            // Obtiene el resumen por departamento y lo carga en el DataGridView
            List<DepartamentoResumen> resumen = enlaceCassandra.Get_DepartmentSummary();
            List<PuestoResumen> resumenp = enlaceCassandra.Get_PuestoSummary();

            HC_TablaPorDeptoDEP.DataSource = resumen;
            HC_TablaPorPuestoDEP.DataSource = resumenp;

            // Cambia los encabezados de las columnas de HC_TablaPorDeptoDEP
            HC_TablaPorDeptoDEP.Columns["Departamento"].HeaderText = "Departamento";
            HC_TablaPorDeptoDEP.Columns["Cantidad"].HeaderText = "# de Empleados";
            // Cambia los encabezados de las columnas de HC_TablaPorPuestoDEP
            HC_TablaPorPuestoDEP.Columns["Puesto"].HeaderText = "Puesto";
            HC_TablaPorPuestoDEP.Columns["Cantidad"].HeaderText = "# de Empleados";
        }
        //Declaraciones--------------------------------------------------------------------------------------------------
        private void HC_DeptoPuestoDEP_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verifica si hay una selección válida
            if (HC_DeptoPuestoDEP.SelectedItem != null)
            {
                // Obtén el departamento seleccionado desde el ComboBox (asegurándote de acceder a la propiedad Departamento)
                string departamentoSeleccionado = ((Departamentos)HC_DeptoPuestoDEP.SelectedItem).Departamento.Trim().ToLower();
                
                EnlaceCassandra enlaceCassandra = new EnlaceCassandra();
                // Obtiene todos los empleados
                List<Empleado> empleados = enlaceCassandra.Get_All_EMP();

                // Filtra los empleados por el departamento seleccionado
                var empleadosPorDepto = empleados
                    .Where(emp => !string.IsNullOrEmpty(emp.Departamento) && emp.Departamento.Trim().ToLower() == departamentoSeleccionado)
                    .ToList();

                // Si se encontraron empleados, agrupa por puesto
                if (empleadosPorDepto.Count > 0)
                {
                    var resumenPorPuesto = empleadosPorDepto
                        .GroupBy(emp => emp.Puesto)
                        .Select(g => new PuestoResumen
                        {
                            Puesto = g.Key,
                            Cantidad = g.Count()
                        })
                        .ToList();

                    // Actualiza el DataGridView con los datos filtrados por puesto
                    HC_TablaPorPuestoDEP.DataSource = resumenPorPuesto;
                }
                else
                {
                    HC_TablaPorPuestoDEP.DataSource = null; // Limpia el DataGridView si no hay datos
                }
            }
        }

        //Tablas --------------------------------------------------------------------------------------------------------
        private void OCTL()
        {
            string[] columnasOcultas = { "NumeroYNombreCompleto", "NombreCompleto", "Fecha_Nacimiento", "CURP", "NSS", "RFC", "Codigo_Postal", "Calle", "Colonia", "Estado", "Municipio", "Banco", "Num_Cuenta", "Email", "Telefono", "Usuario", "Contrasena" };

            foreach (string columna in columnasOcultas)
            {
                RG_ReporteGeneralDEP.Columns[columna].Visible = false;
            }
            // Cambia el orden de las columnas visibles
            RG_ReporteGeneralDEP.Columns["Fecha_Alta"].DisplayIndex = 1;
            RG_ReporteGeneralDEP.Columns["ID"].DisplayIndex = 2;
            RG_ReporteGeneralDEP.Columns["Nombre_s"].DisplayIndex = 3;
            RG_ReporteGeneralDEP.Columns["Apellido_Paterno"].DisplayIndex = 4;
            RG_ReporteGeneralDEP.Columns["Apellido_Materno"].DisplayIndex = 5;
            RG_ReporteGeneralDEP.Columns["Puesto"].DisplayIndex = 6;
            RG_ReporteGeneralDEP.Columns["Cuota_fija"].DisplayIndex = 7;
            RG_ReporteGeneralDEP.Columns["Departamento"].DisplayIndex = 8;
            RG_ReporteGeneralDEP.Columns["Salario_base"].DisplayIndex = 9;
            RG_ReporteGeneralDEP.Columns["Salario_Diario"].DisplayIndex = 10;

            // Cambia los nombres de los encabezados
            RG_ReporteGeneralDEP.Columns["Fecha_Alta"].HeaderText = "Fecha de Alta";
            RG_ReporteGeneralDEP.Columns["ID"].HeaderText = "ID Empleado";
            RG_ReporteGeneralDEP.Columns["Nombre_s"].HeaderText = "Nombre";
            RG_ReporteGeneralDEP.Columns["Apellido_Paterno"].HeaderText = "Apellido Paterno";
            RG_ReporteGeneralDEP.Columns["Apellido_Materno"].HeaderText = "Apellido Materno";
            RG_ReporteGeneralDEP.Columns["Puesto"].HeaderText = "Puesto";
            RG_ReporteGeneralDEP.Columns["Cuota_fija"].HeaderText = "Cuota fija";
            RG_ReporteGeneralDEP.Columns["Departamento"].HeaderText = "Departamento";
            RG_ReporteGeneralDEP.Columns["Salario_base"].HeaderText = "Sueldo Base";
            RG_ReporteGeneralDEP.Columns["Salario_Diario"].HeaderText = "Sueldo Diario";
        }
        //Botones -------------------------------------------------------------------------------------------------------

        //Selecciones del menu ------------------------------------------------------------------------------------------
        private void administrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var nuevoForm = new Administrador();
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


        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void catalogoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var nuevoForm = new Catalogo();
            nuevoForm.Show();
            this.Close();
        }

        private void GestionDepartamentos_Load(object sender, EventArgs e)
        {
            EnlaceCassandra comex = new EnlaceCassandra();
            RG_ReporteGeneralDEP.DataSource = comex.Get_All_EMP();
            OCTL();
            var data = comex.Get_All_EMP();

            // Actualiza el control en la UI
            US_UsuarioActual.Text = Login.NombreUsuarioActual;
            US_UsuarioActual.Refresh();
        }

        private void RG_Actualizar_Click(object sender, EventArgs e)
        {
            EnlaceCassandra comex = new EnlaceCassandra();
            RG_ReporteGeneralDEP.DataSource = comex.Get_All_EMP();
            OCTL();
            var data = comex.Get_All_EMP();

        }

        private void RG_Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores seleccionados del DateTimePicker
                int mesSeleccionado = RG_FiltroMesDEP.Value.Month;
                int añoSeleccionado = RG_FiltroAnoDEP.Value.Year;

                // Obtener todos los empleados de la base de datos
                EnlaceCassandra comex = new EnlaceCassandra();
                var empleados = comex.Get_All_EMP();

                // Filtrar empleados por mes y año de la Fecha_Alta
                var empleadosFiltrados = empleados.Where(emp =>
                    emp.Fecha_Alta.Month == mesSeleccionado &&
                    emp.Fecha_Alta.Year == añoSeleccionado).ToList();

                // Asignar los datos filtrados al DataGridView
                RG_ReporteGeneralDEP.DataSource = empleadosFiltrados;

                // Formatear la columna "Salario_Diario" en formato monetario
                RG_ReporteGeneralDEP.Columns["Salario_Diario"].DefaultCellStyle.Format = "C2"; // Formato monetario para salario diario

                // Ocultar las columnas no necesarias
                OCTL();

                // Actualizar el control en la UI
                US_UsuarioActual.Text = Login.NombreUsuarioActual;
                US_UsuarioActual.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aplicar el filtro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RG_PorDepa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Obtener el departamento seleccionado
                string departamentoSeleccionado = RG_PorDepa.SelectedItem?.ToString();

                // Obtener todos los empleados de la base de datos
                EnlaceCassandra comex = new EnlaceCassandra();
                var empleados = comex.Get_All_EMP();

                // Filtrar empleados por departamento
                var empleadosFiltrados = empleados.Where(emp =>
                    !string.IsNullOrEmpty(departamentoSeleccionado) && emp.Departamento == departamentoSeleccionado).ToList();

                // Asignar los datos filtrados al DataGridView
                RG_ReporteGeneralDEP.DataSource = empleadosFiltrados;

                // Formatear la columna "Salario_Diario" en formato monetario
                RG_ReporteGeneralDEP.Columns["Salario_Diario"].DefaultCellStyle.Format = "C2";

                // Ocultar las columnas no necesarias
                OCTL();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aplicar el filtro por departamento: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // --------------------------------------------------------------------------------------------------------------
    }
}
