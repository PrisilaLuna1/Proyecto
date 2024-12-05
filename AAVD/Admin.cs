using Cassandra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;


namespace AAVD
{
    public partial class Administrador : Form
    {
        public Administrador()
        {
            InitializeComponent();
            puestosToolStripMenuItem.Visible = false;
            // Inicializa la instancia de EnlaceCassandra
            EnlaceCassandra enlaceCassandra = new EnlaceCassandra();

            // Obtiene la lista de departamentos
            List<Departamentos> departamentos = enlaceCassandra.Get_All_DEP();
            List<Puestos> puestos = enlaceCassandra.Get_All_PUE();

            // Configura el ComboBox Registro de empleados
            RE_DepartamentoEMP.DataSource = departamentos; // Carga la lista en el ComboBox
            RE_DepartamentoEMP.DisplayMember = "Departamento"; // Muestra el nombre del departamento
            RE_DepartamentoEMP.ValueMember = "salario_base";

            RE_PuestoEMP.DataSource = puestos; // Carga la lista en el ComboBox
            RE_PuestoEMP.DisplayMember = "Puesto"; // Muestra el nombre del puesto
            RE_PuestoEMP.ValueMember = "cuota_fija";

            // Carga la lista de empleados en el ListBox
            List<Empleado> empleados = enlaceCassandra.Get_All_EMP();

            ListaEmpleados.DataSource = empleados;
            ListaEmpleados.DisplayMember = "NumeroYNombreCompleto";

            GE_ListaEmpleadosEMP.DataSource = empleados;

            // Configura el ListBox para mostrar el número de empleado y nombre completo
            GE_ListaEmpleadosEMP.DisplayMember = "NumeroYNombreCompleto";

            // Suscribe el evento SelectedIndexChanged
            GE_ListaEmpleadosEMP.SelectedIndexChanged += GE_ListaEmpleadosEMP_SelectedIndexChanged;

            RD_ListaDepa.DataSource = departamentos;
            RD_ListaDepa.DisplayMember = "Departamento";
            RD_ListaDepa.ValueMember = "Numero";

            RP_ListaPuesto.SelectedIndexChanged += RP_ListaPuesto_SelectedIndexChanged;

            RP_ListaPuesto.DataSource = puestos;
            RP_ListaPuesto.DisplayMember = "Puesto";
            RP_ListaPuesto.ValueMember = "Id";
        }
        //Funciones------------------------------------------------------------------------------------------------------

        //CAMBIOS... ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void GE_ListaEmpleadosEMP_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtén el empleado seleccionado
            Empleado empleadoSeleccionado = GE_ListaEmpleadosEMP.SelectedItem as Empleado;

            if (empleadoSeleccionado != null)
            {
                // Muestra los datos del empleado seleccionado en los controles
                GE_NumeroEmpleadoEMP.Text = empleadoSeleccionado.ID.ToString();
                GE_NombreEMP.Text = empleadoSeleccionado.Nombre_s;
                GE_ApellidoP_EMP.Text = empleadoSeleccionado.Apellido_Paterno;
                GE_ApellidoM_EMP.Text = empleadoSeleccionado.Apellido_Materno;
                GE_Departamento.Text = empleadoSeleccionado.Departamento;
                GE_Puesto.Text = empleadoSeleccionado.Puesto;
                GE_Curp.Text = empleadoSeleccionado.CURP;
                GE_CodigoPostal.Text = empleadoSeleccionado.Codigo_Postal.ToString();
                GE_Calle.Text = empleadoSeleccionado.Calle;
                GE_Colonia.Text = empleadoSeleccionado.Colonia;
                GE_Municipio.Text = empleadoSeleccionado.Municipio;
                GE_Estado.Text = empleadoSeleccionado.Estado;
                GE_FechaNacimiento.Text = empleadoSeleccionado.Fecha_Nacimiento.ToString();
                GE_NSS.Text = empleadoSeleccionado.NSS.ToString();
                GE_RFC.Text = empleadoSeleccionado.RFC;
                GE_Banco.Text = empleadoSeleccionado.Banco;
                GE_NumCuenta.Text = empleadoSeleccionado.Num_Cuenta.ToString();
                GE_Email.Text = empleadoSeleccionado.Email;
                GE_Telefono.Text = empleadoSeleccionado.Telefono.ToString();

                if (empleadoSeleccionado.Fecha_Alta != null)
                {
                    // Si Fecha_Alta es de tipo Date, crea un DateTime solo con la fecha
                    DateTime fechaAlta = new DateTime(empleadoSeleccionado.Fecha_Alta.Year, empleadoSeleccionado.Fecha_Alta.Month, empleadoSeleccionado.Fecha_Alta.Day);
                    GE_FechaAlta.Value = fechaAlta;
                }
                else
                {
                    GE_FechaAlta.Value = DateTime.Now;
                }

                // Obtener la fecha de alta y convertirla a DateTime
                if (empleadoSeleccionado.Fecha_Alta != null)
                {
                    // Convertir LocalDate a DateTime
                    DateTime fechaAlta = new DateTime(empleadoSeleccionado.Fecha_Alta.Year, empleadoSeleccionado.Fecha_Alta.Month, empleadoSeleccionado.Fecha_Alta.Day);

                    // Determinar el número de días del mes según la fecha de alta
                    int diasDelMes = DateTime.DaysInMonth(fechaAlta.Year, fechaAlta.Month);

                    double SBruto = empleadoSeleccionado.Salario_Diario * diasDelMes;

                    EnlaceCassandra enlaceCassandra = new EnlaceCassandra();
                    var percepciones = enlaceCassandra.Get_All_PER();
                    var deducciones = enlaceCassandra.Get_All_DED();

                    // Filtrar percepciones y deducciones
                    double totalPercepciones = 0;
                    double totalDeducciones = 0;

                    // Variables para almacenar las percepciones y deducciones
                    string percepcionesDetalle = "Percepciones encontradas:\n";
                    string deduccionesDetalle = "Deducciones encontradas:\n";

                    foreach (var percepcion in percepciones)
                    {
                        if (percepcion.IDapl == 0 || percepcion.IDapl == empleadoSeleccionado.ID || percepcion.Nombre == empleadoSeleccionado.Departamento)
                        {
                            double monto;
                            if (percepcion.Cantidad < 5) // Es un porcentaje
                            {
                                monto = empleadoSeleccionado.Salario_base * percepcion.Cantidad;
                            }
                            else // Es una cantidad fija
                            {
                                monto = percepcion.Cantidad;
                            }
                            totalPercepciones += monto;
                            // Agregar detalle al mensaje
                            percepcionesDetalle += $"- Concepto: {percepcion.Concepto}, Monto: {monto:C2}\n";
                        }
                    }

                    foreach (var deduccion in deducciones)
                    {
                        if (deduccion.IDapl == 0 || deduccion.IDapl == empleadoSeleccionado.ID || deduccion.Nombre == empleadoSeleccionado.Departamento)
                        {
                            double monto;
                            if (deduccion.Cantidad < 5) // Es un porcentaje
                            {
                                monto = empleadoSeleccionado.Salario_base * deduccion.Cantidad;
                            }
                            else // Es una cantidad fija
                            {
                                monto = deduccion.Cantidad;
                            }

                            totalDeducciones += monto;
                            // Agregar detalle al mensaje
                            deduccionesDetalle += $"- Concepto: {deduccion.Concepto}, Monto: {monto:C2}\n";
                        }
                    }
                   
                    // Calcular el salario bruto
                    double salarioBruto = SBruto + totalPercepciones;
                    double salarioNeto = SBruto + totalPercepciones - totalDeducciones;

                    GestionEmpleados convertir = new GestionEmpleados();

                    // Mostrar los días del mes de la fecha de alta en el TextBox
                    GE_DiasTrabajados.Text = diasDelMes.ToString();
                    GE_SalarioBruto.Text = salarioBruto.ToString("C2");
                    GE_SalarioNeto.Text = salarioNeto.ToString("C2");
                    GE_SalarioNetoLetra.Text = convertir.ConvertirNumeroALetras(salarioNeto);
                }
                else
                {
                    GE_DiasTrabajados.Text = "N/A"; // Si no hay fecha de alta
                }

                GE_SalarioDiario.Text = empleadoSeleccionado.Salario_Diario.ToString("C2");
                GE_CuotaFija.Text = empleadoSeleccionado.Cuota_fija.ToString();

            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Tablas --------------------------------------------------------------------------------------------------------

        //Botones -------------------------------------------------------------------------------------------------------
        //Bones: Actualizar----------------------------------------------------------------------------------------------
        private void RD_ACTUALIZAR_BTN_Click(object sender, EventArgs e)
        {
            EnlaceCassandra comex = new EnlaceCassandra();

            // Obtener los departamentos
            var departamentos = comex.Get_All_DEP();

            // Mostrar en el MessageBox la cantidad de registros obtenidos
            MessageBox.Show($"Registros obtenidos: {departamentos.Count}");

            // Cargar la data en el DataGridView
            RD_TablaDepartamentos.DataSource = departamentos;

            // Formatear las columnas de salario en formato monetario
            RD_TablaDepartamentos.Columns["Salario_base"].DefaultCellStyle.Format = "C2"; // "C2" es el formato monetario en 2 decimales

            // Eliminar los guiones en los nombres de las columnas
            foreach (DataGridViewColumn column in RD_TablaDepartamentos.Columns)
            {
                column.HeaderText = column.HeaderText.Replace("_", " "); // Reemplazar guiones bajos por espacios
            }
        }

        private void RP_ACTUALIZAR_BTN_Click(object sender, EventArgs e)
        {
            EnlaceCassandra comex = new EnlaceCassandra();

            // Obtener todos los puestos
            var puestos = comex.Get_All_PUE();

            // Mostrar en el MessageBox la cantidad de registros obtenidos
            MessageBox.Show($"Registros obtenidos: {puestos.Count}");

            // Cargar la data en el DataGridView
            PUESTOSDATAGRID.DataSource = puestos;

            // Formatear las columnas de salario en formato monetario
            PUESTOSDATAGRID.Columns["Cuota_fija"].DefaultCellStyle.Format = "P2";
            PUESTOSDATAGRID.Columns["Cuota_fija"].HeaderText = "Nivel Salarial";

            // Ocultar la columna "Id"
            PUESTOSDATAGRID.Columns["Id"].Visible = false;

            // Eliminar los guiones bajos en los nombres de las columnas (si es necesario)
            foreach (DataGridViewColumn column in PUESTOSDATAGRID.Columns)
            {
                column.HeaderText = column.HeaderText.Replace("_", " "); // Reemplazar guiones bajos por espacios
            }
        }

        private void RM_ACTUALIZAR_BTN_Click(object sender, EventArgs e)
        {
            try
            {
                // ID del empleado actual
                int idEmpleado = Login.idUsuario;

                EnlaceCassandra comex = new EnlaceCassandra();

                // Obtener y filtrar las percepciones
                var percepciones = comex.Get_All_PER();

                // Asignar las percepciones filtradas al DataGridView
                RCP_TablaPercepcionCPD.DataSource = percepciones;

                // Ocultar la columna "ID"
                if (RCP_TablaPercepcionCPD.Columns["ID"] != null)
                {
                    RCP_TablaPercepcionCPD.Columns["ID"].Visible = false;
                }

                RCP_TablaPercepcionCPD.Columns["IDapl"].Visible = false;
                RCP_TablaPercepcionCPD.Columns["IDRegistro"].Visible = false;

                RCP_TablaPercepcionCPD.Columns["Cantidad"].DefaultCellStyle.Format = "C2";

                // Obtener y filtrar las deducciones
                var deducciones = comex.Get_All_DED();

                // Asignar las deducciones filtradas al DataGridView
                RCP_TablaDeduccionCPD.DataSource = deducciones;

                // Ocultar la columna "ID"
                if (RCP_TablaDeduccionCPD.Columns["ID"] != null)
                {
                    RCP_TablaDeduccionCPD.Columns["ID"].Visible = false;
                }
                RCP_TablaDeduccionCPD.Columns["IDapl"].Visible = false;
                RCP_TablaDeduccionCPD.Columns["IDRegistro"].Visible = false;
                RCP_TablaDeduccionCPD.Columns["Cantidad"].DefaultCellStyle.Format = "C2";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RD_Generar_Click(object sender, EventArgs e)
        {
            // Crear una instancia de Random
            Random random = new Random();

            // Generar un número aleatorio entre 1000 y 9999 (4 dígitos)
            int numeroAleatorio = random.Next(1000, 10000); // El límite superior es exclusivo, por eso 10000.

            // Asignar el número aleatorio al TextBox RD_NumeroDepartamentoDEP
            RD_NumeroDepartamentoDEP.Text = numeroAleatorio.ToString();
        }
        private void RE_Generar_Click_1(object sender, EventArgs e)
        {
            // Crear una instancia de Random
            Random random = new Random();

            // Generar un número aleatorio entre 1000 y 9999 (4 dígitos)
            int numeroAleatorio = random.Next(1000, 10000); // El límite superior es exclusivo, por eso 10000.

            // Asignar el número aleatorio al TextBox RD_NumeroDepartamentoDEP
            RE_NumeroEmpleadoEMP.Text = numeroAleatorio.ToString();
        }
        //Botones: Registrar-----------------------------------------------------------------------------------------------
        //EMPLEADO
        public void RE_Registrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(RE_NombreEMP.Text) ||
                    string.IsNullOrWhiteSpace(RE_ApellidoM_EMP.Text) ||
                    string.IsNullOrWhiteSpace(RE_ApellidoP_EMP.Text) ||
                    string.IsNullOrWhiteSpace(RE_DepartamentoEMP.Text) ||
                    string.IsNullOrWhiteSpace(RE_PuestoEMP.Text) ||
                    string.IsNullOrWhiteSpace(RE_CURPEMP.Text) ||
                    string.IsNullOrWhiteSpace(RE_NSSEMP.Text) ||
                    string.IsNullOrWhiteSpace(RE_RFCEmp.Text) ||
                    string.IsNullOrWhiteSpace(RE_CalleEMP.Text) ||
                    string.IsNullOrWhiteSpace(RE_ColoniaEMP.Text) ||
                    string.IsNullOrWhiteSpace(RE_EstadoEMP.Text) ||
                    string.IsNullOrWhiteSpace(RE_MunicipioEMP.Text) ||
                    string.IsNullOrWhiteSpace(RE_BancoEMP.Text) ||
                    string.IsNullOrWhiteSpace(RE_EmailEMP.Text) ||
                    string.IsNullOrWhiteSpace(RE_UsuarioEMP.Text) ||
                    string.IsNullOrWhiteSpace(RE_ContrasenaEMP.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!RE_EmailEMP.Text.Contains("@") || !RE_EmailEMP.Text.EndsWith(".com"))
                {
                    MessageBox.Show("Por favor ingrese un correo electrónico válido (debe contener '@' y terminar en '.com').", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(RE_NumeroEmpleadoEMP.Text, out int numeroEmpleado))
                {
                    MessageBox.Show("Número de Empleado inválido. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!double.TryParse(RE_NumeroCuentaEMP.Text, out double numeroCuenta))
                {
                    MessageBox.Show("Número de Cuenta inválido. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                if (!double.TryParse(RE_TelefonoEMP.Text, out double telefono))
                {
                    MessageBox.Show("Teléfono inválido. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(RE_CodigoPostalEMP.Text, out int codigoPostal))
                {
                    MessageBox.Show("Código Postal inválido. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int anio = RE_FechaNacEMP.Value.Year;
                int mes = RE_FechaNacEMP.Value.Month;


                int diasEnMes = DateTime.DaysInMonth(anio, mes);

                if (RE_PuestoEMP.SelectedValue == null || RE_DepartamentoEMP.SelectedValue == null)
                {
                    MessageBox.Show("Por favor seleccione un puesto y un departamento válidos.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!double.TryParse(RE_PuestoEMP.SelectedValue.ToString(), out double cuota))
                {
                    MessageBox.Show("El valor del puesto seleccionado no es válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!double.TryParse(RE_DepartamentoEMP.SelectedValue.ToString(), out double Sbase))
                {
                    MessageBox.Show("El valor del departamento seleccionado no es válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Calcular salario diario
                double SDiario = cuota * Sbase;
                textBox3.Text = SDiario.ToString("F2");

                Empleado RegistroEmp = new Empleado
                {
                    ID = numeroEmpleado,
                    Nombre_s = RE_NombreEMP.Text,
                    Apellido_Materno = RE_ApellidoM_EMP.Text,
                    Apellido_Paterno = RE_ApellidoP_EMP.Text,
                    Departamento = RE_DepartamentoEMP.Text,
                    Puesto = RE_PuestoEMP.Text,
                    Salario_Diario = SDiario,

                    Fecha_Nacimiento = new LocalDate(
                                   RE_FechaNacEMP.Value.Year,
                                   RE_FechaNacEMP.Value.Month,
                                   RE_FechaNacEMP.Value.Day),
                    CURP = RE_CURPEMP.Text,
                    NSS = RE_NSSEMP.Text,
                    RFC = RE_RFCEmp.Text,
                    Codigo_Postal = codigoPostal,
                    Calle = RE_CalleEMP.Text,
                    Colonia = RE_ColoniaEMP.Text,
                    Estado = RE_EstadoEMP.Text,
                    Municipio = RE_MunicipioEMP.Text,
                    Banco = RE_BancoEMP.Text,
                    Num_Cuenta = numeroCuenta,
                    Email = RE_EmailEMP.Text,
                    Telefono = telefono,
                    Usuario = RE_UsuarioEMP.Text,
                    Contrasena = RE_ContrasenaEMP.Text,
                    Fecha_Alta = new LocalDate(
                                  RE_FechaRegEMP.Value.Year,
                                  RE_FechaRegEMP.Value.Month,
                                  RE_FechaRegEMP
                                  .Value.Day),
                    Cuota_fija = cuota,
                    Salario_base = Sbase

                };

                EnlaceCassandra enlace = new EnlaceCassandra();
                enlace.InsertaEmpleados(RegistroEmp);



                MessageBox.Show("Empleado agregado exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el empleado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
        //DEPARTAMENTO
        private void RD_Añadir_Click(object sender, EventArgs e)
        {
            try
            {

                if (!int.TryParse(RD_NumeroDepartamentoDEP.Text, out int numeroDepartamento))
                {
                    MessageBox.Show("Número de Departamento inválido. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                if (!float.TryParse(RD_SalarioDiarioDEP.Text, out float SalarioDep))
                {
                    MessageBox.Show("Teléfono inválido. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }




                Departamentos RegistroDep = new Departamentos
                {
                    Numero = numeroDepartamento,
                    Departamento = RD_NombreDepartamentoDEP.Text,
                    Salario_base = SalarioDep,
                    //Fecha_de_alta = RD_FechaAltaDEP.Value,
                    Fecha_de_alta = new LocalDate(
                                   RD_FechaAltaDEP.Value.Year,
                                   RD_FechaAltaDEP.Value.Month,
                                   RD_FechaAltaDEP.Value.Day)

                };


                EnlaceCassandra enlace = new EnlaceCassandra();
                enlace.InsertaDepartamentos(RegistroDep);
                MessageBox.Show("Departamento agregado exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el departamento: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        //PUESTO
        private void RP_Añadir_Click(object sender, EventArgs e)
        {
            try
            {

                if (!double.TryParse(RP_CuotaFijaPUE.Text, out double cuotaFija))
                {
                    MessageBox.Show("Cuota fija inválida. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                if (Radio_proporcion.Checked)
                {
                    cuotaFija = cuotaFija / 100;
                }


                //if (!double.TryParse(RP_SalarioDiarioPUE.Text, out double SalarioDiario))
                //{
                //    MessageBox.Show("Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}




                Puestos RegistroPue = new Puestos
                {
                    Puesto = RP_NombrePuestoPUE.Text,
                    //Departamento = RP_DEPARTAMENTO_CB.Text,
                    Cuota_fija = cuotaFija,
                    //Salario_diario = SalarioDiario * cuotaFija,
                    Id = Guid.NewGuid(),
                    Fecha_de_alta = new LocalDate(
                                   RD_FechaAltaDEP.Value.Year,
                                   RD_FechaAltaDEP.Value.Month,
                                   RD_FechaAltaDEP.Value.Day)

                };


                EnlaceCassandra enlace = new EnlaceCassandra();
                enlace.InsertaPuestos(RegistroPue);
                MessageBox.Show("Puesto agregado exitosamente.");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el puesto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //PERCEPCIONES y DEDUCCIONES
        private void RCP_Añadir_Click(object sender, EventArgs e)
        {
            string TipoDE = RPD_TipoCPD.Text;

            if (TipoDE == "Percepcion")
            {
                try
                {

                    if (!double.TryParse(RCP_CantidadCPD.Text, out double Cantidad))
                    {
                        MessageBox.Show("Cantidad inválida. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (radioPorcen_CAt.Checked)
                    {
                        Cantidad /= 100;
                    }

                    int opc = 0;
                    string nombre = "";
                    int idApl = 0;

                    if (radio_Todos.Checked)
                    {
                        opc = 1;
                        nombre = "Todos";
                        idApl = 0;
                    }
                    else if (radio_Departamento.Checked)
                    {
                        opc = 2;
                        if (comboBox_info.SelectedItem != null)
                        {
                            nombre = comboBox_info.Text;
                            idApl = (int)comboBox_info.SelectedValue;
                        }
                    }
                    else if (radioEmpleado.Checked)
                    {
                        opc = 3;
                        if (comboBox_info.SelectedItem != null)
                        {
                            nombre = comboBox_info.Text;
                            idApl = (int)comboBox_info.SelectedValue;
                        }
                    }


                    Percepciones RegistroPer = new Percepciones
                    {
                        ID = opc,
                        Concepto = RCP_ConceptoCPD.Text,
                        Cantidad = Cantidad,
                        Mes = dateTimeMES.Text,
                        Nombre = nombre,
                        IDapl = idApl,
                        IDRegistro = Guid.NewGuid()
                    };


                    EnlaceCassandra enlace = new EnlaceCassandra();
                    enlace.InsertaPercepciones(RegistroPer);
                    MessageBox.Show("Percepción agregada exitosamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al registrar la percepción: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (TipoDE == "Deduccion")
            {
                try
                {

                    if (!double.TryParse(RCP_CantidadCPD.Text, out double Cantidad))
                    {
                        MessageBox.Show("Cantidad inválida. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (radioPorcen_CAt.Checked)
                    {
                        Cantidad /= 100;
                    }

                    int opc = 0;
                    string nombre = "";
                    int idApl = 0;

                    if (radio_Todos.Checked)
                    {
                        opc = 1;
                        nombre = "Todos";
                        idApl = 0;
                    }
                    else if (radio_Departamento.Checked)
                    {
                        opc = 2;
                        if (comboBox_info.SelectedItem != null)
                        {
                            nombre = comboBox_info.Text;
                            idApl = (int)comboBox_info.SelectedValue;
                        }
                    }
                    else if (radioEmpleado.Checked)
                    {
                        opc = 3;
                        if (comboBox_info.SelectedItem != null)
                        {
                            nombre = comboBox_info.Text;
                            idApl = (int)comboBox_info.SelectedValue;
                        }
                    }


                    Deducciones RegistroDed = new Deducciones
                    {
                        ID = opc,
                        Concepto = RCP_ConceptoCPD.Text,
                        Cantidad = Cantidad,
                        Mes = dateTimeMES.Text,
                        Nombre = nombre,
                        IDapl = idApl,
                        IDRegistro = Guid.NewGuid()
                    };


                    EnlaceCassandra enlace = new EnlaceCassandra();
                    enlace.InsertaDeducciones(RegistroDed);
                    MessageBox.Show("Deducción agregada exitosamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al registrar la deducción: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un tipo válido (Percepción o Deducción).", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Selecciones del menu ------------------------------------------------------------------------------------------
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

        }

        private void empleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void catalogoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var nuevoForm = new Catalogo();
            nuevoForm.Show();
            this.Close();
        }

        private void Administrador_Load(object sender, EventArgs e)
        {
            // Actualiza el control en la UI
            US_UsuarioActual.Text = Login.NombreUsuarioActual;
            US_UsuarioActual.Refresh();
        }

        private void GE_Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(GE_NumeroEmpleadoEMP.Text, out int NumEmpleado))
                {
                    MessageBox.Show("Numero de empleado inválido. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!double.TryParse(GE_SalarioDiario.Text.Replace("$", "").Replace(",", "").Trim(), out double SaldarioDiario))
                {
                    MessageBox.Show("Salario diario inválido. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!double.TryParse(GE_SalarioBruto.Text.Replace("$", "").Replace(",", "").Trim(), out double SaldarioBruto))
                {
                    MessageBox.Show("Salario bruto inválido. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!double.TryParse(GE_SalarioNeto.Text.Replace("$", "").Replace(",", "").Trim(), out double SaldarioNeto))
                {
                    MessageBox.Show("Salario neto inválido. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!double.TryParse(GE_CuotaFija.Text, out double Cuota))
                {
                    MessageBox.Show("Cuota fija inválido. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!int.TryParse(GE_DiasTrabajados.Text, out int Dias))
                {
                    MessageBox.Show("Dias trabajados inválidos. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Nominas RegistroNominas = new Nominas
                {
                    Puesto = GE_Puesto.Text,
                    ID = NumEmpleado,
                    Nombre = GE_NombreEMP.Text,
                    Apellido_materno = GE_ApellidoM_EMP.Text,
                    Apellido_paterno = GE_ApellidoP_EMP.Text,
                    Salario_diario = SaldarioDiario,
                    Salario_bruto = SaldarioBruto,
                    Salario_neto = SaldarioNeto,
                    Cuota_fija = Cuota,
                    Dias_del_mes = Dias,
                    Salario_total_letra = GE_SalarioNetoLetra.Text,
                    departamento = GE_Departamento.Text,
                    Folio = Guid.NewGuid(),
                    fecha_de_alta = new LocalDate(
                                   GE_FechaAlta.Value.Year,
                                   GE_FechaAlta.Value.Month,
                                   GE_FechaAlta.Value.Day)
                };

                EnlaceCassandra enlace = new EnlaceCassandra();
                enlace.InsertaNominas(RegistroNominas);
                MessageBox.Show($"Nómina generada correctamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar recibo de nómina: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void RD_Borrar_Click(object sender, EventArgs e)
        {
            EnlaceCassandra enlace = new EnlaceCassandra();

            // Obtener el departamento seleccionado
            Departamentos departamentoSeleccionado = RD_ListaDepa.SelectedItem as Departamentos;

            if (departamentoSeleccionado != null)
            {
                // Confirmar si realmente desea eliminar el departamento
                DialogResult dialogResult = MessageBox.Show($"¿Está seguro de que desea eliminar el departamento '{departamentoSeleccionado.Departamento}'?",
                                                            "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    // Llamar a la función de eliminación
                    enlace.EliminarDepartamento(departamentoSeleccionado.Numero);

                    // Actualizar la lista de departamentos después de la eliminación
                    RD_ListaDepa.DataSource = enlace.Get_All_DEP(); // Refrescar la lista de departamentos
                    RD_ListaDepa.DisplayMember = "Departamento"; // Mostrar el nombre del departamento
                    RD_ListaDepa.ValueMember = "Numero"; // Usar el ID como valor
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un departamento para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RP_Borrar_Click(object sender, EventArgs e)
        {
            // Verificar que se ha seleccionado un puesto
            if (RP_ListaPuesto.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un puesto para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el puesto seleccionado
            Puestos puestoSeleccionado = RP_ListaPuesto.SelectedItem as Puestos;

            // Confirmar eliminación
            DialogResult dialogResult = MessageBox.Show($"¿Está seguro de que desea eliminar el puesto '{puestoSeleccionado.Puesto}'?",
                                                        "Confirmación",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    EnlaceCassandra enlace = new EnlaceCassandra();
                    enlace.EliminarPuesto(puestoSeleccionado.Puesto);

                    // Refrescar la lista de puestos
                    RP_ListaPuesto.SelectedIndexChanged -= RP_ListaPuesto_SelectedIndexChanged;

                    RP_ListaPuesto.DataSource = enlace.Get_All_PUE(); // Cargar nueva lista
                    RP_ListaPuesto.DisplayMember = "Puesto";

                    RP_ListaPuesto.SelectedIndexChanged += RP_ListaPuesto_SelectedIndexChanged;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar el puesto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void RP_ListaPuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Puestos puestoSeleccionado = GE_ListaEmpleadosEMP.SelectedItem as Puestos;
        }

        private void groupBox23_Enter(object sender, EventArgs e)
        {

        }

        private void PUESTOSDATAGRID_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Radio_porcentaje_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void BTN_SELECCIONAR_Click(object sender, EventArgs e)
        {
            if (RP_ListaPuesto.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione para ver el puesto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            Puestos puestoSeleccionado = RP_ListaPuesto.SelectedItem as Puestos;

            RP_NombrePuestoPUE.Text = puestoSeleccionado.Puesto;
            //RP_CuotaFijaPUE.Text = puestoSeleccionado.Cuota_fija.ToString("F2"); ;
            double cuotaMultiplicada = puestoSeleccionado.Cuota_fija * 100;
            RP_CuotaFijaPUE.Text = cuotaMultiplicada.ToString("F2");
            Radio_proporcion.Checked = true;
            //RP_FechaAltaPUE.Text = puestoSeleccionado.Fecha_de_alta.ToString();


        }

        private void RE_PuestoEMP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BTN_modificar_Click(object sender, EventArgs e)
        {
            try
            {

                if (!double.TryParse(RP_CuotaFijaPUE.Text, out double cuotaFija))
                {
                    MessageBox.Show("Cuota fija inválida. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                if (Radio_proporcion.Checked)
                {
                    cuotaFija = cuotaFija / 100;
                }

                Puestos RegistroPue = new Puestos
                {
                    Puesto = RP_NombrePuestoPUE.Text,
                    //Departamento = RP_DEPARTAMENTO_CB.Text,
                    Cuota_fija = cuotaFija,
                    //Salario_diario = SalarioDiario * cuotaFija,
                    Id = Guid.NewGuid(),
                    Fecha_de_alta = new LocalDate(
                                   RD_FechaAltaDEP.Value.Year,
                                   RD_FechaAltaDEP.Value.Month,
                                   RD_FechaAltaDEP.Value.Day)

                };


                EnlaceCassandra enlace = new EnlaceCassandra();
                enlace.InsertaPuestos(RegistroPue);
                MessageBox.Show("Puesto modificado exitosamente.");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el puesto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            RP_NombrePuestoPUE.Text = "";
            RP_CuotaFijaPUE.Text = "";

        }

        private void tabPageCatalogo_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (RD_ListaDepa.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione para ver el puesto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el puesto seleccionado
            Departamentos DepartamentoSeleccionado = RD_ListaDepa.SelectedItem as Departamentos;

            RD_NumeroDepartamentoDEP.Text = DepartamentoSeleccionado.Numero.ToString();
            RD_NombreDepartamentoDEP.Text = DepartamentoSeleccionado.Departamento;
            RD_SalarioDiarioDEP.Text = DepartamentoSeleccionado.Salario_base.ToString("F2");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (!int.TryParse(RD_NumeroDepartamentoDEP.Text, out int numeroDepartamento))
                {
                    MessageBox.Show("Número de Departamento inválido. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                if (!float.TryParse(RD_SalarioDiarioDEP.Text, out float SalarioDep))
                {
                    MessageBox.Show("Teléfono inválido. Por favor ingrese un número válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }




                Departamentos RegistroDep = new Departamentos
                {
                    Numero = numeroDepartamento,
                    Departamento = RD_NombreDepartamentoDEP.Text,
                    Salario_base = SalarioDep,
                    Fecha_de_alta = new LocalDate(
                                   RD_FechaAltaDEP.Value.Year,
                                   RD_FechaAltaDEP.Value.Month,
                                   RD_FechaAltaDEP.Value.Day)

                };


                EnlaceCassandra enlace = new EnlaceCassandra();
                enlace.InsertaDepartamentos(RegistroDep);
                MessageBox.Show("Departamento modificado exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el departamento: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            RD_NumeroDepartamentoDEP.Text = "";
            RD_NombreDepartamentoDEP.Text = "";
            RD_SalarioDiarioDEP.Text = "";
        }

        private void radio_Departamento_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_info.Visible = true;
            EnlaceCassandra enlaceCassandra = new EnlaceCassandra();
            List<Departamentos> departamentos = enlaceCassandra.Get_All_DEP();
            comboBox_info.DataSource = departamentos;
            comboBox_info.DisplayMember = "Departamento";
            comboBox_info.ValueMember = "Numero";

        }

        private void radio_Todos_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_info.Visible = false;
        }

        private void radioEmpleado_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_info.Visible = true;
            EnlaceCassandra enlaceCassandra = new EnlaceCassandra();
            List<Empleado> empleados = enlaceCassandra.Get_All_EMP();
            comboBox_info.DataSource = empleados;
            comboBox_info.DisplayMember = "nombre_s";
            comboBox_info.ValueMember = "id";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_Insertar_Click(object sender, EventArgs e)
        {
            try

            {
                if (!double.TryParse(textBox_ISR.Text, out double Cantidad))

                {
                    MessageBox.Show("Cantidad inválida.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!double.TryParse(textBox_IMSS.Text, out double Cantidad1))

                {
                    MessageBox.Show("Cantidad inválida.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Deducciones RegistroPer = new Deducciones
                {
                    ID = 0,
                    Concepto = "ISR",
                    Cantidad = Cantidad,
                    Mes = dateTimeMES.Text,
                    Nombre = "Fija",
                    IDapl = 0,
                    IDRegistro = Guid.NewGuid()
                };
                Deducciones RegistroPer1 = new Deducciones
                {
                    ID = 0,
                    Concepto = "IMSS",
                    Cantidad = Cantidad1,
                    Mes = dateTimeMES.Text,
                    Nombre = "Fija",
                    IDapl = 0,
                    IDRegistro = Guid.NewGuid()
                };

                EnlaceCassandra enlace = new EnlaceCassandra();
                EnlaceCassandra enlace1 = new EnlaceCassandra();
                enlace.InsertaDeducciones(RegistroPer);
                enlace1.InsertaDeducciones(RegistroPer1);
                MessageBox.Show("Deducciones fijas agregadas exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar la percepción: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GE_Eliminar_Click(object sender, EventArgs e)
        {
            EnlaceCassandra enlace = new EnlaceCassandra();
            // Obtén el empleado seleccionado
            Empleado empleadoSeleccionado = ListaEmpleados.SelectedItem as Empleado;

            if (empleadoSeleccionado != null)
            {
                // Confirmar si realmente desea eliminar al empleado
                DialogResult dialogResult = MessageBox.Show("¿Esta seguro de que desea eliminar este empleado?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    // Llamar a la función de eliminación
                    enlace.EliminarEmpleado(empleadoSeleccionado.ID);

                    // Opcional: Actualizar la lista de empleados después de la eliminación
                    ListaEmpleados.DataSource = enlace.Get_All_EMP(); // Refrescar la lista de empleados
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un empleado para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ListaEmpleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Obtener el ID del empleado seleccionado desde la lista
                var empleadoSeleccionado = ListaEmpleados.SelectedItem as Empleado;  // Asumiendo que la lista tiene objetos de tipo Empleado

                if (empleadoSeleccionado != null)
                {
                    // Obtener el ID del empleado seleccionado
                    int idEmpleado = empleadoSeleccionado.ID;

                    EnlaceCassandra enlace = new EnlaceCassandra();

                    // Obtener los datos del empleado seleccionado
                    var empleados = enlace.Get_All_EMP();
                    var empleadoActual = empleados.FirstOrDefault(emp => emp.ID == idEmpleado);

                    if (empleadoActual != null)
                    {
                        // Asignar los datos del empleado a los campos de la interfaz
                        RE_NumeroEmpleadoEMP.Text = empleadoActual.ID.ToString();
                        RE_NombreEMP.Text = empleadoActual.Nombre_s;
                        RE_ApellidoM_EMP.Text = empleadoActual.Apellido_Materno;
                        RE_ApellidoP_EMP.Text = empleadoActual.Apellido_Paterno;
                        RE_CURPEMP.Text = empleadoActual.CURP;
                        RE_NSSEMP.Text = empleadoActual.NSS;
                        RE_RFCEmp.Text = empleadoActual.RFC;
                        RE_BancoEMP.Text = empleadoActual.Banco;
                        RE_NumeroCuentaEMP.Text = empleadoActual.Num_Cuenta.ToString();
                        RE_TelefonoEMP.Text = empleadoActual.Telefono.ToString();
                        RE_CodigoPostalEMP.Text = empleadoActual.Codigo_Postal.ToString();
                        RE_CalleEMP.Text = empleadoActual.Calle;
                        RE_ColoniaEMP.Text = empleadoActual.Colonia;
                        RE_EstadoEMP.Text = empleadoActual.Estado;
                        RE_MunicipioEMP.Text = empleadoActual.Municipio;
                        RE_UsuarioEMP.Text = empleadoActual.Usuario;
                        RE_EmailEMP.Text = empleadoActual.Email;
                        RE_ContrasenaEMP.Text = empleadoActual.Contrasena;

                        // Convertir fechas de LocalDate a DateTime
                        if (empleadoActual.Fecha_Nacimiento != null)
                        {
                            // Asumimos que Fecha_Nacimiento es de tipo LocalDate
                            var fechaNacimiento = empleadoActual.Fecha_Nacimiento;
                            RE_FechaNacEMP.Value = new DateTime(fechaNacimiento.Year, fechaNacimiento.Month, fechaNacimiento.Day);
                        }
                        else
                        {
                            RE_FechaNacEMP.Value = DateTime.Now;
                        }

                        if (empleadoActual.Fecha_Alta != null)
                        {
                            // Asumimos que Fecha_Alta es de tipo LocalDate
                            var fechaAlta = empleadoActual.Fecha_Alta;
                            RE_FechaRegEMP.Value = new DateTime(fechaAlta.Year, fechaAlta.Month, fechaAlta.Day);
                        }
                        else
                        {
                            RE_FechaRegEMP.Value = DateTime.Now;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Empleado no encontrado.");
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un empleado de la lista.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos del empleado: " + ex.Message);
            }
        }

        private void RE_Limpiar_Click(object sender, EventArgs e)
        {
            try
            {
                // Limpiar campos de texto
                RE_NumeroEmpleadoEMP.Text = "";
                RE_NombreEMP.Text = "";
                RE_ApellidoM_EMP.Text = "";
                RE_ApellidoP_EMP.Text = "";
                RE_CURPEMP.Text = "";
                RE_NSSEMP.Text = "";
                RE_RFCEmp.Text = "";
                RE_BancoEMP.Text = "";
                RE_NumeroCuentaEMP.Text = "";
                RE_TelefonoEMP.Text = "";
                RE_CodigoPostalEMP.Text = "";
                RE_CalleEMP.Text = "";
                RE_ColoniaEMP.Text = "";
                RE_EstadoEMP.Text = "";
                RE_MunicipioEMP.Text = "";
                RE_UsuarioEMP.Text = "";
                RE_EmailEMP.Text = "";
                RE_ContrasenaEMP.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar los campos: " + ex.Message);
            }
        }

        private void EM_Regresar_Click(object sender, EventArgs e)
        {
            var nuevoForm = new Login();
            nuevoForm.Show();
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }
    }




    // --------------------------------------------------------------------------------------------------------------
}
//Clases---------------------------------------------------------------------------------------------------------
public class Departamentos
{
    public int Numero { get; set; }
    public string Departamento { get; set; }
    public float Salario_base { get; set; }
    public LocalDate Fecha_de_alta { get; set; }

}
public class DepartamentoResumen
{
    public string Departamento { get; set; }
    public int Cantidad { get; set; }
}
public class Puestos
{
    public string Puesto { get; set; }
    public double Cuota_fija { get; set; }
    public Guid Id { get; set; }
    public LocalDate Fecha_de_alta { get; set; }

}
public class PuestoResumen
{
    public string Puesto { get; set; }
    public int Cantidad { get; set; }
}
public class Percepciones
{
    public int ID { get; set; }
    public string Concepto { get; set; }
    public double Cantidad { get; set; }
    public string Mes { get; set; }
    public int IDapl { get; set; }
    public string Nombre { get; set; }
    public Guid IDRegistro { get; set; }

    // Propiedad calculada para combinar Nombre y Concepto
    public string DisplayName => $"{Nombre} - {Concepto}";

}

public class Deducciones
{
    public int ID { get; set; }
    public string Concepto { get; set; }
    public double Cantidad { get; set; }
    public string Mes { get; set; }
    public int IDapl { get; set; }
    public string Nombre { get; set; }
    public Guid IDRegistro { get; set; }

    // Propiedad calculada para combinar Nombre y Concepto
    public string DisplayName => $"{Nombre} - {Concepto}";

}

public class Horas
{

    public int ID { get; set; }
    public int retardos { get; set; }
    public double Horas_extras { get; set; }
    public int faltas { get; set; }

}

public class Empleado
{
    public int ID { get; set; }
    public string Nombre_s { get; set; }
    public string Apellido_Materno { get; set; }
    public string Apellido_Paterno { get; set; }
    public string Departamento { get; set; }
    public string Puesto { get; set; }
    public double Salario_Diario { get; set; }
    public LocalDate Fecha_Nacimiento { get; set; }
    public string CURP { get; set; }
    public string NSS { get; set; }
    public string RFC { get; set; }
    public int Codigo_Postal { get; set; }
    public string Calle { get; set; }
    public string Colonia { get; set; }
    public string Estado { get; set; }
    public string Municipio { get; set; }
    public string Banco { get; set; }
    public double Num_Cuenta { get; set; }
    public string Email { get; set; }
    public double Telefono { get; set; }
    public string Usuario { get; set; }
    public string Contrasena { get; set; }
    public LocalDate Fecha_Alta { get; set; }

    public double Cuota_fija { get; set; }

    public double Salario_base { get; set; }

    // Propiedad para el nombre completo
    public string NombreCompleto
    {
        get { return $"{Nombre_s} {Apellido_Paterno} {Apellido_Materno}"; }
    }
    // Propiedad para el número y nombre completo
    public string NumeroYNombreCompleto
    {
        get { return $"#{ID} - {Nombre_s} {Apellido_Paterno} {Apellido_Materno}"; }
    }
}
public class Gerente
{
    public int ID { get; set; }
    public string Nombre_s { get; set; }
    public string Apellido_Materno { get; set; }
    public string Apellido_Paterno { get; set; }
    public string Departamento { get; set; }
    public string Puesto { get; set; }
    public double Sueldo_diario { get; set; }
    public LocalDate Fecha_Nacimiento { get; set; }
    public string CURP { get; set; }
    public string NSS { get; set; }
    public string RFC { get; set; }
    public int Codigo_Postal { get; set; }
    public string Calle { get; set; }
    public string Colonia { get; set; }
    public string Estado { get; set; }
    public string Municipio { get; set; }
    public string Banco { get; set; }
    public double Num_Cuenta { get; set; }
    public string Email { get; set; }
    public double Telefono { get; set; }
    public string Usuario { get; set; }
    public string Contrasena { get; set; }
    public LocalDate Fecha_Alta { get; set; }
    public double Cuota_fija { get; set; }
    public double Salario_base { get; set; }

    // Propiedad para el nombre completo
    public string NombreCompleto
    {
        get { return $"{Nombre_s} {Apellido_Paterno} {Apellido_Materno}"; }
    }
    // Propiedad para el número y nombre completo
    public string NumeroYNombreCompleto
    {
        get { return $"#{ID} - {Nombre_s} {Apellido_Paterno} {Apellido_Materno}"; }
    }
    public Gerente()
    {
        // Asignamos valores predeterminados
        ID = 0;
        Nombre_s = "Juan";
        Apellido_Materno = "Pérez";
        Apellido_Paterno = "Gómez";
        Departamento = "Ventas";
        Puesto = "Gerente";
        Sueldo_diario = 5000.0;
        Fecha_Nacimiento = new LocalDate(1980, 1, 1);  // Asegúrate de que el tipo LocalDate se maneje correctamente
        CURP = "CURP1234567890";
        NSS = "NSS1234567890";
        RFC = "RFC1234567890";
        Codigo_Postal = 12345;
        Calle = "Calle Falsa 123";
        Colonia = "Colonia X";
        Estado = "Estado Y";
        Municipio = "Municipio Z";
        Banco = "Banco A";
        Num_Cuenta = 1234567890.0;
        Email = "juan.perez@email.com";
        Telefono = 5551234567.0;
        Usuario = "admin";
        Contrasena = "contra";
        Fecha_Alta = new LocalDate(2024, 11, 11);
        Cuota_fija = 35;
    }
}

public class Nominas
{
    public string Puesto { get; set; }
    public int ID { get; set; }
    public string Nombre { get; set; }
    public string Apellido_materno { get; set; }
    public string Apellido_paterno { get; set; }
    public double Salario_diario { get; set; }
    public double Salario_bruto { get; set; }
    public double Salario_neto { get; set; }
    public double Cuota_fija { get; set; }
    public int Dias_del_mes { get; set; }
    public string Salario_total_letra { get; set; }
    public string departamento { get; set; }
    public Guid Folio { get; set; }
    public LocalDate fecha_de_alta { get; set; }

}

