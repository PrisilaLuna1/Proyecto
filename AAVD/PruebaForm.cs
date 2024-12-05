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
    public partial class PruebaForm : Form
    {

        public int id_emp { get; set; }
        public string nombre_emp { get; set; }
        public string apellidom_emp { get; set; }
        public string apellidop_emp { get; set; }
        public string curp_emp { get; set; }
        public LocalDate fnac_emp { get; set; }
        public string nss_emp { get; set; }
        public string rfc_emp { get; set; }
        public string banco_emp { get; set; }
        public string numcuenta_emp { get; set; }
        public int telefono_emp { get; set; }
        public int d_cp_emp { get; set; }
        public string d_calle_emp { get; set; }
        public string d_colonia_emp { get; set; }
        public string d_estado_emp { get; set; }
        public string d_municipio_emp { get; set; }
        public string usuario_emp { get; set; }
        public string email_emp { get; set; }
        public string contrasena_emp { get; set; }
        public PruebaForm()
        {
            InitializeComponent();

        }

        public void button1_Click(object sender, EventArgs e)
        {
            
                EnlaceCassandra comex = new EnlaceCassandra();
                dataGridView1.DataSource = comex.Get_All_EMP();
            var data = comex.Get_All_EMP();
            MessageBox.Show($"Registros obtenidos: {data.Count}");
            if (data.Count > 0)
            {
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = data;
            }
            else
            {
                MessageBox.Show("No se encontraron registros en la base de datos.");
            }


        }




        public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    public class Kardex
    {
        public int Id_Emp { get; set; }
        public string Nombre_Emp { get; set; }
        public string ApellidoM_Emp { get; set; }
        public string ApellidoP_Emp { get; set; }
        public string CURP_Emp { get; set; }
        public DateTime FNac_Emp { get; set; }
        public string NSS_Emp { get; set; }
        public string RFC_Emp { get; set; }
        public string Banco_Emp { get; set; }
        public string NumCuenta_Emp { get; set; }
        public int Telefono_Emp { get; set; }
        public int D_CP_Emp { get; set; }
        public string D_Calle_Emp { get; set; }
        public string D_Colonia_Emp { get; set; }
        public string D_Estado_Emp { get; set; }
        public string D_Municipio_Emp { get; set; }
        public string Usuario_Emp { get; set; }
        public string Email_Emp { get; set; }
        public string Contrasena_Emp { get; set; }
    }

}
