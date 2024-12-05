using Cassandra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AAVD
{
    public partial class GestionEmpleados : Form
    {

        private Empleado empleadoActual;
        private Nominas nominaActual;
        PrintDocument printDocument = new PrintDocument();

        public GestionEmpleados()
        {
            InitializeComponent();
            CargarDatosUsuario();
            puestosToolStripMenuItem.Visible = false;
        }
        //Declaraciones--------------------------------------------------------------------------------------------------
        private void CargarDatosUsuario()
        {
            EnlaceCassandra enlace = new EnlaceCassandra();
            int idUsuario = Login.idUsuario;  // Obtener el ID del usuario desde Login

            if (idUsuario != 0)  // Si el ID del usuario es válido (no es 0)
            {
                var empleados = enlace.Get_All_EMP();  // Obtener todos los empleados
                var empleadoActual = empleados.FirstOrDefault(emp => emp.ID == idUsuario);  // Buscar al empleado por su ID

                if (empleadoActual != null)
                {
                    // Asignar los datos del empleado a los campos de la interfaz
                    EM_NumeroEmpleadoEMP.Text = empleadoActual.ID.ToString();
                    EM_NombreEMP.Text = empleadoActual.Nombre_s;
                    EM_ApellidoM_EMP.Text = empleadoActual.Apellido_Materno;
                    EM_ApellidoP_EMP.Text = empleadoActual.Apellido_Paterno;
                    EM_DepartamentoEMP.Text = empleadoActual.Departamento;
                    EM_PuestoEMP.Text = empleadoActual.Puesto;
                    EM_FechaNacimientoEMP.Text = empleadoActual.Fecha_Nacimiento.ToString();
                    EM_CURPEMP.Text = empleadoActual.CURP;
                    EM_NSSEMP.Text = empleadoActual.NSS;
                    EM_RFCEMP.Text = empleadoActual.RFC;
                    EM_BancoEMP.Text = empleadoActual.Banco;
                    EM_NumeroCuentaEMP.Text = empleadoActual.Num_Cuenta.ToString();
                    EM_TelefonoEMP.Text = empleadoActual.Telefono.ToString();
                    EM_CodigoPostalEMP.Text = empleadoActual.Codigo_Postal.ToString();
                    EM_CalleEMP.Text = empleadoActual.Calle;
                    EM_ColoniaEMP.Text = empleadoActual.Colonia;
                    EM_EstadoEMP.Text = empleadoActual.Estado;
                    EM_MunicipioEMP.Text = empleadoActual.Municipio;
                    EM_UsuarioEMP.Text = empleadoActual.Usuario;
                    EM_EmailEMP.Text = empleadoActual.Email;

                    // Buscar los datos de nómina para este empleado
                    var nomina = enlace.Get_All_NOM();  // Método para obtener los datos de nómina por ID de empleado
                    var nominaActual = nomina.FirstOrDefault(nom => nom.ID == idUsuario);
                    if (nominaActual != null)
                    {
                        // Asignar los valores de la nómina a los campos de la interfaz
                        NE_NumeroEmpleadoEMP.Text = nominaActual.ID.ToString();
                        NE_NombreEMP.Text = nominaActual.Nombre;
                        NE_ApellidoM_EMP.Text = nominaActual.Apellido_materno;
                        NE_ApellidoP_EMP.Text = nominaActual.Apellido_paterno;
                        NE_DepartamentoEMP.Text = nominaActual.departamento;
                        NE_PuestoEMP.Text = nominaActual.Puesto;

                        NE_SalarioDiarioEMP.Text = nominaActual.Salario_diario.ToString("C2");
                        NE_CuotaFijaEMP.Text = nominaActual.Cuota_fija.ToString();
                        NE_DiasTrabajadosEMP.Text = nominaActual.Dias_del_mes.ToString();  // Esto representa los días del mes

                        NE_SalarioBrutoEMP.Text = nominaActual.Salario_bruto.ToString("C2");
                        NE_SalarioNetoEMP.Text = nominaActual.Salario_neto.ToString("C2");
                        NE_SalarioLetraEMP.Text = nominaActual.Salario_total_letra;

                        if (nominaActual.fecha_de_alta != null)
                        {
                            // Si Fecha_Alta es de tipo Date, crea un DateTime solo con la fecha
                            DateTime fechaAlta = new DateTime(nominaActual.fecha_de_alta.Year, nominaActual.fecha_de_alta.Month, nominaActual.fecha_de_alta.Day);
                            FechaAltaEMP.Value = fechaAlta;
                        }
                        else
                        {
                            FechaAltaEMP.Value = DateTime.Now;
                        }

                        // Filtrar percepciones y deducciones según el ID del empleado y su departamento
                        EnlaceCassandra comex = new EnlaceCassandra();

                        // Obtener y filtrar las percepciones
                        var percepciones = comex.Get_All_PER();
                        var percepcionesFiltradas = percepciones.Where(p =>
                            p.ID == idUsuario ||       // ID del empleado actual
                            p.IDapl == 0 ||            // Percepciones con IDapl 0
                            p.Nombre == empleadoActual.Departamento // Percepciones del mismo departamento
                        ).ToList();

                        // Asignar las percepciones filtradas al DataGridView
                        NE_TablaPercepcionEMP.DataSource = percepcionesFiltradas;

                        // Ocultar la columna "ID"
                        if (NE_TablaPercepcionEMP.Columns["ID"] != null)
                        {
                            NE_TablaPercepcionEMP.Columns["ID"].Visible = false;
                        }

                        NE_TablaPercepcionEMP.Columns["IDapl"].Visible = false;
                        NE_TablaPercepcionEMP.Columns["IDRegistro"].Visible = false;
                        NE_TablaPercepcionEMP.Columns["Cantidad"].DefaultCellStyle.Format = "C2";

                        // Obtener y filtrar las deducciones
                        var deducciones = comex.Get_All_DED();
                        var deduccionesFiltradas = deducciones.Where(d =>
                            d.ID == idUsuario ||       // ID del empleado actual
                            d.IDapl == 0 ||            // Deducciones con IDapl 0
                            d.Nombre == empleadoActual.Departamento // Deducciones del mismo departamento
                        ).ToList();

                        // Asignar las deducciones filtradas al DataGridView
                        NE_TablaDeduccionEMP.DataSource = deduccionesFiltradas;

                        // Ocultar la columna "ID"
                        if (NE_TablaDeduccionEMP.Columns["ID"] != null)
                        {
                            NE_TablaDeduccionEMP.Columns["ID"].Visible = false;
                        }

                        NE_TablaDeduccionEMP.Columns["IDapl"].Visible = false;
                        NE_TablaDeduccionEMP.Columns["IDRegistro"].Visible = false;
                        NE_TablaDeduccionEMP.Columns["Cantidad"].DefaultCellStyle.Format = "C2";
                    }
                    else
                    {
                        MessageBox.Show("Aun no se registra su nómina. Espere pacientemente o comuniquese con RH", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else
                {
                    MessageBox.Show("Empleado no encontrado.");
                }
            }
        }

        public string ConvertirNumeroALetras(double numero)
        {
            string[] unidades = { "", "uno", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve", "diez", "once", "doce", "trece", "catorce", "quince", "dieciséis", "diecisiete", "dieciocho", "diecinueve" };
            string[] decenas = { "", "", "veinte", "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa" };
            string[] centenas = { "", "cien", "doscientos", "trescientos", "cuatrocientos", "quinientos", "seiscientos", "setecientos", "ochocientos", "novecientos" };
            string[] miles = { "", "mil", "millón", "mil millones", "billón" }; // Agregar más si se requiere

            if (numero == 0)
                return "cero";

            // Dividir en partes por cada segmento (miles, millones, etc.)
            string textoFinal = "";
            int divisor = 1;
            int segmento = (int)numero;
            int pos = 0;

            while (segmento > 0)
            {
                int parte = segmento % 1000;
                if (parte > 0)
                {
                    string parteTexto = ConvertirMiles(parte, unidades, decenas, centenas);
                    textoFinal = parteTexto + (miles[pos] == "" ? "" : " " + miles[pos]) + " " + textoFinal;
                }

                segmento /= 1000;
                pos++;
            }

            return textoFinal.Trim();
        }

        private string ConvertirMiles(int num, string[] unidades, string[] decenas, string[] centenas)
        {
            string resultado = "";
            if (num >= 100)
            {
                resultado += centenas[num / 100] + " ";
                num %= 100;
            }

            if (num >= 20)
            {
                resultado += decenas[num / 10] + " y ";
                num %= 10;
            }

            if (num > 0)
            {
                resultado += unidades[num];
            }

            return resultado.Trim();
        }


        //Tablas --------------------------------------------------------------------------------------------------------


        //Botones -------------------------------------------------------------------------------------------------------

        private void EM_Regresar_Click(object sender, EventArgs e)
        {

            var nuevoForm = new Login();
            nuevoForm.Show();
            this.Close();


        }

        //Selecciones del menu ------------------------------------------------------------------------------------------

        private void departamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        private void puestosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        private void catalogoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
        private void administrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void GestionEmpleados_Load(object sender, EventArgs e)
        {
            try
            {
                // ID del empleado actual
                int idEmpleado = Login.idUsuario;

                // Enlace a la base de datos
                EnlaceCassandra comex = new EnlaceCassandra();

                // Obtener todas las percepciones
                var percepciones = comex.Get_All_PER();

                

                // Actualizar el nombre del usuario actual en la UI
                US_UsuarioActual.Text = Login.NombreUsuarioActual;
                US_UsuarioActual.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AD_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(EM_NumeroEmpleadoEMP.Text, out int idEmpleado))
                {
                    MessageBox.Show("ID de empleado inválido. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(AD_CodigoPostalEMP.Text, out int nvCP))
                {
                    MessageBox.Show("Código postal inválido. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!double.TryParse(AD_NumeroCuentaEMP.Text, out double nvNumCuenta))
                {
                    MessageBox.Show("Número de cuenta inválido. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!double.TryParse(AD_TelefonoEMP.Text, out double nvTel))
                {
                    MessageBox.Show("Número de cuenta inválido. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!AD_EmailEMP.Text.Contains("@") || !AD_EmailEMP.Text.EndsWith(".com"))
                {
                    MessageBox.Show("Por favor ingrese un correo electrónico válido (debe contener '@' y terminar en '.com').", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nuevoUsuario = AD_UsuarioEMP.Text;
                string nuevoEmail = AD_EmailEMP.Text;
                int nuevoCP = nvCP;
                string nuevoCalle = AD_CalleEMP.Text;
                string nuevoColonia = AD_ColoniaEMP.Text;
                string nuevoEstado = AD_EstadoEMP.Text;
                string nuevoMunicipio = AD_MunicipioEMP.Text;
                string nuevoBanco = AD_BancoEMP.Text;
                double nuevoNumCuenta = nvNumCuenta;
                double nuevoTelefono = nvTel;

                if (string.IsNullOrEmpty(nuevoBanco) || string.IsNullOrEmpty(nuevoUsuario) || string.IsNullOrEmpty(nuevoCalle) || string.IsNullOrEmpty(nuevoColonia) || string.IsNullOrEmpty(nuevoEstado) || string.IsNullOrEmpty(nuevoEmail) || string.IsNullOrEmpty(nuevoMunicipio))
                {
                    MessageBox.Show("Por favor complete todos los campos requeridos.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                EnlaceCassandra enlace = new EnlaceCassandra();
                enlace.ActualizaDatosEmpleado(idEmpleado, nuevoUsuario, nuevoEmail, nuevoCP, nuevoCalle, nuevoColonia, nuevoEstado, nuevoMunicipio, nuevoBanco, nuevoNumCuenta, nuevoTelefono);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar los datos del empleado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button_aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                // Llamamos a la función para generar el recibo de nómina
                //GenerarReciboNomina(RegistroNom);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar la nómina: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GenerarReciboNomina(Nominas registroNomina)
        {
            try
            {
                // Establecer el evento para definir cómo se va a imprimir el documento
                printDocument.PrintPage += new PrintPageEventHandler((sender, e) =>
                {
                    // Definir el área de impresión
                    int posY = 20;
                    Font font = new Font("Arial", 10);
                    Brush brush = Brushes.Black;

                    // Imprimir el título
                    e.Graphics.DrawString($"Recibo de Nómina - Folio: {registroNomina.Folio}", new Font("Arial", 12, FontStyle.Bold), brush, 20, posY);
                    posY += 20;

                    // Imprimir el ID
                    e.Graphics.DrawString($"ID Empleado: {registroNomina.ID}", font, brush, 20, posY);
                    posY += 20;

                    // Imprimir los detalles de la nómina
                    e.Graphics.DrawString($"Empleado: {registroNomina.Nombre} {registroNomina.Apellido_paterno} {registroNomina.Apellido_materno}", font, brush, 20, posY);
                    posY += 20;

                    e.Graphics.DrawString($"Puesto: {registroNomina.Puesto}", font, brush, 20, posY);
                    posY += 20;

                    e.Graphics.DrawString($"Departamento: {registroNomina.departamento}", font, brush, 20, posY);
                    posY += 20;


                    // Formatear los valores en el formato monetario [$0.00]
                    e.Graphics.DrawString($"Salario Diario: {registroNomina.Salario_diario.ToString("C2")}", font, brush, 20, posY);
                    posY += 20;

                    e.Graphics.DrawString($"Salario Bruto: {registroNomina.Salario_bruto.ToString("C2")}", font, brush, 20, posY);
                    posY += 20;

                    e.Graphics.DrawString($"Salario Neto: {registroNomina.Salario_neto.ToString("C2")}", font, brush, 20, posY);
                    posY += 20;

                    e.Graphics.DrawString($"Cuota Fija: {registroNomina.Cuota_fija.ToString("C2")}", font, brush, 20, posY);
                    posY += 20;

                    e.Graphics.DrawString($"Días Trabajados: {registroNomina.Dias_del_mes}", font, brush, 20, posY);
                    posY += 20;

                    e.Graphics.DrawString($"Salario Total (en letra): {registroNomina.Salario_total_letra}", font, brush, 20, posY);
                    posY += 20;

                    e.Graphics.DrawString($"Fecha de alta: {registroNomina.fecha_de_alta.ToString()}", font, brush, 20, posY);
                    posY += 20;
                });

                // Mostrar el cuadro de diálogo para imprimir
                PrintDialog printDialog = new PrintDialog();
                printDialog.Document = printDocument;
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el recibo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // --------------------------------------------------------------------------------------------------------------
    }
}
