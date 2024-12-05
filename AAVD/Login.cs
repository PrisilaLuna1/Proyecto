using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cassandra;



namespace AAVD
{
    public partial class Login : Form
    {
        //private readonly string archivoCredenciales = "credenciales.txt";
        public static int idUsuario { get; set; }

        public static bool empleado { get; set; }
        public static bool gerente { get; set; }
        public static string NombreUsuarioActual { get; set; }

        public Login()
        {
            InitializeComponent();
            EnlaceCassandra enlace = new EnlaceCassandra();
            Gerente gerente = new Gerente();

            // Verificar si ya existen registros en la tabla de Gerentes
            var gerentesExistentes = enlace.Get_One_Gerente();

            if (!gerentesExistentes.Any()) // Si no hay gerentes registrados
            {
                // Si no hay registros, insertar el nuevo gerente
                enlace.InsertaGerente(gerente);
            }
        }
        //Selecciones del menu ------------------------------------------------------------------------------------------
        
        //Botones -------------------------------------------------------------------------------------------------------
        public void BTN_ENTRAR_Click(object sender, EventArgs e)
        {
            EnlaceCassandra enlace = new EnlaceCassandra();

            // Obtener el usuario y la contraseña proporcionados por el usuario
            string usuarioInput = US_Usuario.Text;  // Suponiendo que 'txtUsuario' es el TextBox para el usuario
            string contrasenaInput = US_Contrasena.Text;  // Suponiendo que 'txtContrasena' es el TextBox para la contraseña

            bool esEmpleado = false;
            bool esGerente = false;

            // Verificar si el usuario es un empleado
            var empleados = enlace.Get_All_EMP();
            var gerentes = enlace.Get_One_Gerente();
            foreach (var emp in empleados)
            {
                if (emp.Usuario == usuarioInput && emp.Contrasena == contrasenaInput)
                {
                    esEmpleado = true;
                    idUsuario = emp.ID;  // Guardamos el ID del empleado
                    break;
                }
            }

            // Verificar si el usuario es un gerente
            if (!esEmpleado)  // Solo verificar si no es un empleado
            {
                
                foreach (var gte in gerentes)
                {
                    if (gte.Usuario == usuarioInput && gte.Contrasena == contrasenaInput)
                    {
                        esGerente = true;
                        idUsuario = gte.ID;  // Guardamos el ID del gerente
                        break;
                    }
                }
            }
            // Verificar si el ID es diferente de 0 (empleado) o igual a 0 (gerente)
            if (esEmpleado && idUsuario != 0)
            {
                // Si es empleado y su ID es diferente de 0, abrir la ventana de gestión de empleados
                var nuevoForm = new GestionEmpleados();
                // Asignamos el nombre del usuario actual
                NombreUsuarioActual = empleados.First(emp => emp.ID == idUsuario).Usuario;
                
                empleado = true;
                nuevoForm.Show();
                this.Hide();
            }
            else if (esGerente && idUsuario == 0)
            {
                // Si es gerente y su ID es 0, abrir la ventana de gestión de departamentos
                var nuevoForm = new GestionDepartamentos();
                // Asignamos el nombre del usuario actual
                NombreUsuarioActual = gerentes.First(gte => gte.ID == idUsuario).Usuario;
                empleado = false;
                nuevoForm.Show();
                this.Hide();
            }
            else
            {
                // Si las credenciales no son correctas o no se encuentran, mostrar mensaje de error
                MessageBox.Show("Credenciales incorrectas. Intenta de nuevo.");
            }
        }
        private void labelTitulo1_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            //if (File.Exists(archivoCredenciales))
            //{
            //    try
            //    {
            //        // Leer las credenciales del archivo
            //        string[] credenciales = File.ReadAllLines(archivoCredenciales);
            //        if (credenciales.Length >= 2)
            //        {
            //            US_Usuario.Text = credenciales[0];
            //            US_Contrasena.Text = credenciales[1];
            //            US_RecordarUsuario.Checked = true; 
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show($"Error al cargar las credenciales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }
        // --------------------------------------------------------------------------------------------------------------
    }

}
