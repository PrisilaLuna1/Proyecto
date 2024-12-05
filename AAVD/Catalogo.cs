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
    public partial class Catalogo : Form
    {
        public Catalogo()
        {
            InitializeComponent();
            puestosToolStripMenuItem.Visible = false;
            try
            {
                // ID del empleado actual
                int idEmpleado = Login.idUsuario;

                // Enlace a la base de datos
                EnlaceCassandra enlace = new EnlaceCassandra();

                // Obtener y filtrar las percepciones
                List<Percepciones> percepcion = enlace.Get_All_PER();

                // Obtener y filtrar las deducciones
                List<Deducciones> deduccion = enlace.Get_All_DED();

                // Asignar las percepciones al ComboBox de percepciones
                CA_ListaP.DataSource = percepcion;
                CA_ListaP.DisplayMember = "DisplayName";
                CA_ListaP.ValueMember = "Concepto";

                // Asignar las deducciones al ComboBox de deducciones
                CA_ListaD.DataSource = deduccion;
                CA_ListaD.DisplayMember = "DisplayName";
                CA_ListaD.ValueMember = "Concepto";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las listas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Declaraciones--------------------------------------------------------------------------------------------------

        //Tablas --------------------------------------------------------------------------------------------------------
        private void CPD_TablaPercepcionCPD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
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

        private void catalogoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        private void Catalogo_Load(object sender, EventArgs e)
        {
            try
            {
                EnlaceCassandra comex = new EnlaceCassandra();

                // Obtener todas las percepciones
                var percepciones = comex.Get_All_PER().ToList();
                CPD_TablaPercepcionCPD.DataSource = percepciones;

                // Formatear las columnas visibles para percepciones
                OCTL(CPD_TablaPercepcionCPD);

                // Obtener todas las deducciones
                var deducciones = comex.Get_All_DED().ToList();
                CPD_TablaDeduccionCPD.DataSource = deducciones;

                // Formatear las columnas visibles para deducciones
                OCTL(CPD_TablaDeduccionCPD);

                // Actualizar el nombre del usuario actual en la UI
                US_UsuarioActual.Text = Login.NombreUsuarioActual;
                US_UsuarioActual.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OCTL(DataGridView grid)
        {
            try
            {
                // Ocultar todas las columnas excepto las relevantes
                foreach (DataGridViewColumn column in grid.Columns)
                {
                    column.Visible = false;
                }

                // Mostrar y configurar columnas específicas
                if (grid.Columns.Contains("Nombre"))
                {
                    grid.Columns["Nombre"].Visible = true;
                    grid.Columns["Nombre"].HeaderText = "Nombre";
                    grid.Columns["Nombre"].DisplayIndex = 0;
                }

                if (grid.Columns.Contains("Concepto"))
                {
                    grid.Columns["Concepto"].Visible = true;
                    grid.Columns["Concepto"].HeaderText = "Concepto";
                    grid.Columns["Concepto"].DisplayIndex = 1;
                }

                if (grid.Columns.Contains("Cantidad"))
                {
                    grid.Columns["Cantidad"].Visible = true;
                    grid.Columns["Cantidad"].HeaderText = "Cantidad";
                    grid.Columns["Cantidad"].DefaultCellStyle.Format = "C2"; // Formato monetario
                    grid.Columns["Cantidad"].DisplayIndex = 2;
                }

                if (grid.Columns.Contains("Mes"))
                {
                    grid.Columns["Mes"].Visible = true;
                    grid.Columns["Mes"].HeaderText = "Mes";
                    grid.Columns["Mes"].DisplayIndex = 3;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al configurar las columnas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CPD_TablaDeduccionCPD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CA_BorrarP_Click(object sender, EventArgs e)
        {
            try
            {
                EnlaceCassandra enlace = new EnlaceCassandra();

                // Obtener la percepción seleccionada
                if (CA_ListaP.SelectedItem is Percepciones percepcionSeleccionada)
                {
                    // Confirmar eliminación con el concepto real
                    DialogResult dialogResult = MessageBox.Show(
                        $"¿Está seguro de que desea eliminar la percepción con concepto '{percepcionSeleccionada.Concepto}'?",
                        "Confirmación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (dialogResult == DialogResult.Yes)
                    {
                        // Eliminar usando la clave primaria
                        enlace.EliminarPercepcion(percepcionSeleccionada.IDRegistro);

                        // Actualizar la lista de percepciones
                        var percepcionesActualizadas = enlace.Get_All_PER()
                                                            .Where(per => per.ID == 0 || per.ID == Login.idUsuario)
                                                            .ToList();
                        CA_ListaP.DataSource = percepcionesActualizadas;
                        CA_ListaP.DisplayMember = "DisplayName";
                        CA_ListaP.ValueMember = "IDRegistro";

                        // Refrescar el DataGridView
                        CPD_TablaPercepcionCPD.DataSource = percepcionesActualizadas;

                        MessageBox.Show("Percepción eliminada exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione una percepción para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar la percepción: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CA_BorrarD_Click(object sender, EventArgs e)
        {
            try
            {
                EnlaceCassandra enlace = new EnlaceCassandra();

                // Obtener la deducción seleccionada
                if (CA_ListaD.SelectedItem is Deducciones deduccionSeleccionada)
                {
                    // Confirmar eliminación con el concepto real
                    DialogResult dialogResult = MessageBox.Show(
                        $"¿Está seguro de que desea eliminar la deducción con concepto '{deduccionSeleccionada.Concepto}'?",
                        "Confirmación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (dialogResult == DialogResult.Yes)
                    {
                        // Eliminar usando la clave primaria
                        enlace.EliminarDeduccion(deduccionSeleccionada.IDRegistro);

                        // Actualizar la lista de deducciones
                        var deduccionesActualizadas = enlace.Get_All_DED()
                                                           .Where(ded => ded.ID == 0 || ded.ID == Login.idUsuario)
                                                           .ToList();
                        CA_ListaD.DataSource = deduccionesActualizadas;
                        CA_ListaD.DisplayMember = "DisplayName";
                        CA_ListaD.ValueMember = "IDRegistro";

                        // Refrescar el DataGridView
                        CPD_TablaDeduccionCPD.DataSource = deduccionesActualizadas;

                        MessageBox.Show("Deducción eliminada exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione una deducción para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar la deducción: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // --------------------------------------------------------------------------------------------------------------
    }
}
