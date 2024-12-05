using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;
using Cassandra.Mapping;
using System.Configuration;
using System.Windows.Forms;
using static AAVD.GestionEmpleados;

namespace AAVD
{
    class EnlaceCassandra
    {
        static private string _dbServer { set; get; }
        static private string _dbKeySpace { set; get; }
        static private Cluster _cluster;
        static private ISession _session;

        //Conexion----------------------------------------------------------------------------------------------------------------------------
        private static void conectar()
        {
            _dbServer = ConfigurationManager.AppSettings["node"].ToString();
            _dbKeySpace = ConfigurationManager.AppSettings["database"].ToString();


            _cluster = Cluster.Builder()
                .AddContactPoint(_dbServer)
                .Build();

            _session = _cluster.Connect(_dbKeySpace);
        }

        private static void desconectar()
        {
            _cluster.Dispose();
        }
        //Insercion---------------------------------------------------------------------------------------------------------------------------

        public void InsertaGerente(Gerente param)
        {
            try
            {
                conectar();

                string query = "insert into Gerente(id, nombre_s, apellido_materno, apellido_paterno, departamento, puesto, salario_diario, fecha_nacimiento, curp, nss, rfc, codigo_postal, calle, colonia, estado, municipio, banco, num_cuenta, email, telefono, usuario, contrasena,fecha_alta,salario_base)";
                query += " values({0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6}, '{7}', '{8}', '{9}', '{10}', {11}, '{12}', '{13}', '{14}', '{15}', '{16}', {17}, '{18}',{19},'{20}','{21}','{22}',{23})";
                var qry = string.Format(query, param.ID,
                param.Nombre_s,
                param.Apellido_Materno,
                param.Apellido_Paterno,
                param.Departamento,
                param.Puesto,
                param.Sueldo_diario,
                param.Fecha_Nacimiento,
                param.CURP,
                param.NSS,
                param.RFC,
                param.Codigo_Postal,
                param.Calle,
                param.Colonia,
                param.Estado,
                param.Municipio,
                param.Banco,
                param.Num_Cuenta,
                param.Email,
                param.Telefono,
                param.Usuario,
                param.Contrasena,
                param.Fecha_Alta,
                param.Salario_base);
                _session.Execute(qry);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {

                desconectar();
            }
        }
        public void InsertaEmpleados(Empleado param)
        {
            try
            {
                conectar();

                string query = "insert into Empleados(id, nombre_s, apellido_materno, apellido_paterno, departamento, puesto, salario_diario, fecha_nacimiento, curp, nss, rfc, codigo_postal, calle, colonia, estado, municipio, banco, num_cuenta, email, telefono, usuario, contrasena,fecha_alta,cuota_fija,salario_base)";
                query += " values({0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6}, '{7}', '{8}', '{9}', '{10}', {11}, '{12}', '{13}', '{14}', '{15}', '{16}', {17}, '{18}',{19},'{20}','{21}','{22}',{23},{24})";
                var qry = string.Format(query, param.ID,
                param.Nombre_s,
                param.Apellido_Materno,
                param.Apellido_Paterno,
                param.Departamento,
                param.Puesto,
                param.Salario_Diario,
                param.Fecha_Nacimiento,
                param.CURP,
                param.NSS,
                param.RFC,
                param.Codigo_Postal,
                param.Calle,
                param.Colonia,
                param.Estado,
                param.Municipio,
                param.Banco,
                param.Num_Cuenta,
                param.Email,
                param.Telefono,
                param.Usuario,
                param.Contrasena,
                param.Fecha_Alta,
                param.Cuota_fija,
                param.Salario_base);
                _session.Execute(qry);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {

                desconectar();
            }
        }
        public void InsertaDepartamentos(Departamentos param)
        {
            try
            {
                conectar();

                string query = "insert into Departamentos(Numero, Departamento, Salario_base, fecha_de_alta) ";
                query += "values({0}, '{1}', {2}, '{3}');";
                var qry = string.Format(query, param.Numero, param.Departamento, param.Salario_base, param.Fecha_de_alta);

                _session.Execute(qry);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                desconectar();
            }
        }

        public void InsertaPuestos(Puestos param)
        {
            try
            {
                conectar();

                string query = "insert into puestos(Puesto, Cuota_fija, id, fecha_de_alta) ";
                query += "values('{0}', {1},{2},'{3}');";
                var qry = string.Format(query, param.Puesto, param.Cuota_fija, param.Id, param.Fecha_de_alta);

                _session.Execute(qry);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                desconectar();
            }
        }

        public void InsertaPercepciones(Percepciones param)
        {
            try
            {
                conectar();

                string query = "insert into percepciones(ID, Concepto, Cantidad, Mes, IDapl, Nombre, IDRegistro) ";
                query += "values({0}, '{1}', {2}, '{3}',{4},'{5}',{6});";
                var qry = string.Format(query, param.ID, param.Concepto, param.Cantidad, param.Mes, param.IDapl,param.Nombre,param.IDRegistro);

                _session.Execute(qry);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                desconectar();
            }
        }

        public void InsertaDeducciones(Deducciones param)
        {
            try
            {
                conectar();

                string query = "insert into Deducciones(ID, Concepto, Cantidad, Mes, IDapl, Nombre,IDRegistro) ";
                query += "values({0}, '{1}', {2},'{3}',{4},'{5}',{6});";
                var qry = string.Format(query, param.ID, param.Concepto, param.Cantidad, param.Mes, param.IDapl, param.Nombre,param.IDRegistro);

                _session.Execute(qry);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                desconectar();
            }
        }

        public void InsertaHoras(Horas param)
        {
            try
            {
                conectar();

                string query = "INSERT INTO horas(ID, Retardos, Horas_extras, faltas) ";
                query += "VALUES({0}, {1}, {2}, {3});";
                var qry = string.Format(query, param.ID,
                    param.retardos,
                    param.Horas_extras,
                    param.faltas);

                _session.Execute(qry);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                desconectar();
            }
        }

        public void InsertaNominas(Nominas param)
        {
            try
            {
                conectar();

                // Asegurarse de insertar solo los campos definidos en la tabla 'Nominas'
                string query = "INSERT INTO Nominas (Puesto, ID, Nombre, Apellido_materno, Apellido_paterno, " +
                               "Salario_diario, Salario_bruto, Salario_neto, Cuota_fija, Dias_del_mes, " +
                               "Salario_total_letra, departamento, Folio, fecha_de_alta) " +
                               "VALUES ('{0}', {1}, '{2}', '{3}', '{4}', {5}, {6}, {7}, {8}, {9}, '{10}', '{11}', {12}, '{13}');";

                var qry = string.Format(query,
                    param.Puesto,
                    param.ID,
                    param.Nombre,
                    param.Apellido_materno,
                    param.Apellido_paterno,
                    param.Salario_diario,
                    param.Salario_bruto,
                    param.Salario_neto,
                    param.Cuota_fija,
                    param.Dias_del_mes,
                    param.Salario_total_letra,
                    param.departamento,
                    param.Folio,
                    param.fecha_de_alta.ToString());
                _session.Execute(qry);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                desconectar();
            }
        }
        //Consulta----------------------------------------------------------------------------------------------------------------------------

        public IEnumerable<Gerente> Get_One_Gerente()
        {
            string query = "SELECT id, nombre_s, apellido_materno, apellido_paterno, departamento, puesto, " +
                "salario_diario, fecha_nacimiento, curp, nss, rfc, codigo_postal, calle, colonia, estado, municipio, " +
                "banco, num_cuenta, email, telefono, usuario, contrasena,fecha_alta, cuota_fija, salario_base FROM gerente;";
            conectar();
            IMapper mapper = new Mapper(_session);
            IEnumerable<Gerente> users = mapper.Fetch<Gerente>(query);

            desconectar();
            return users.ToList();
        }

        public List<Empleado> Get_All_EMP()
        {
            string query = "SELECT id, nombre_s, apellido_materno, apellido_paterno, departamento, puesto, " +
                "salario_diario, fecha_nacimiento, curp, nss, rfc, codigo_postal, calle, colonia, estado, municipio, " +
                "banco, num_cuenta, email, telefono, usuario, contrasena,fecha_alta, cuota_fija, salario_base FROM empleados;";
            conectar();
            IMapper mapper = new Mapper(_session);
            IEnumerable<Empleado> users = mapper.Fetch<Empleado>(query);
            desconectar();
            return users.ToList();

        }

        public List<Departamentos> Get_All_DEP()
        {
            string query = "SELECT Numero, Departamento, Salario_base, fecha_de_alta FROM departamentos;";
            conectar();
            IMapper mapper = new Mapper(_session);
            IEnumerable<Departamentos> deps = mapper.Fetch<Departamentos>(query);
            desconectar();
            return deps.ToList();
        }

        public List<Puestos> Get_All_PUE()
        {
            string query = "SELECT Puesto, Cuota_fija, fecha_de_alta FROM puestos;";
            conectar();
            IMapper mapper = new Mapper(_session);
            IEnumerable<Puestos> deps = mapper.Fetch<Puestos>(query);
            desconectar();
            return deps.ToList();
        }

        public List<Percepciones> Get_All_PER()
        {
            string query = "SELECT id, concepto, cantidad, mes, idapl, nombre, idregistro FROM Percepciones;";
            conectar();
            IMapper mapper = new Mapper(_session);
            IEnumerable<Percepciones> deps = mapper.Fetch<Percepciones>(query);
            desconectar();
            return deps.ToList();
        }

        public List<Deducciones> Get_All_DED()
        {
            string query = "SELECT id, concepto, cantidad, mes, idapl, nombre,idregistro FROM Deducciones;";
            conectar();
            IMapper mapper = new Mapper(_session);
            IEnumerable<Deducciones> deps = mapper.Fetch<Deducciones>(query);
            desconectar();
            return deps.ToList();
        }
        
        public List<Nominas> Get_All_NOM()
        {
            try
            {
                // Consulta ajustada para que solo obtenga los campos relevantes de la tabla 'Nominas'
                string query = "SELECT Puesto, ID, Nombre, Apellido_materno, Apellido_paterno, Salario_diario, " +
                               "Salario_bruto, Salario_neto, Cuota_fija, Dias_del_mes, Salario_total_letra, " +
                               "departamento, Folio, fecha_de_alta FROM Nominas;"; // Se eliminan los campos que no están en la tabla

                conectar();
                IMapper mapper = new Mapper(_session);
                IEnumerable<Nominas> nominas = mapper.Fetch<Nominas>(query);
                desconectar();

                return nominas.ToList(); // Convierte el resultado a una lista y la devuelve
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener las nóminas: " + e.Message);
            }
        }
        public List<DepartamentoResumen> Get_DepartmentSummary()
        {
            List<Empleado> employees = Get_All_EMP();

            // Agrupa por departamento y cuenta el número de empleados en cada grupo
            var resumen = employees
                .GroupBy(e => e.Departamento)
                .Select(g => new DepartamentoResumen
                {
                    Departamento = g.Key,
                    Cantidad = g.Count()
                })
                .ToList();

            return resumen;
        }

        public List<PuestoResumen> Get_PuestoSummary()
        {
            // Obtiene la lista de empleados
            List<Empleado> empleados = Get_All_EMP();

            // Agrupa por puesto y cuenta los empleados en cada grupo
            var resumen = empleados
                .GroupBy(e => e.Puesto)
                .Select(g => new PuestoResumen
                {
                    Puesto = g.Key,
                    Cantidad = g.Count()
                })
                .ToList();

            return resumen;
        }
        //Eliminar------------------------------------------------------------------------------------------------------------
        public void EliminarEmpleado(int idEmpleado)
        {
            try
            {
                // Conectar a la base de datos
                conectar();

                // Consulta CQL para eliminar el empleado
                string query = $"DELETE FROM empleados WHERE id = {idEmpleado};";

                // Ejecutar la consulta
                var statement = new SimpleStatement(query);
                _session.Execute(statement);

                // Confirmar que el empleado fue eliminado
                MessageBox.Show($"Empleado con ID {idEmpleado} ha sido eliminado exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Desconectar
                desconectar();
            }
            catch (Exception ex)
            {
                // Manejo de errores en caso de que falle la eliminación
                MessageBox.Show($"Error al eliminar el empleado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void EliminarDepartamento(int idDepartamento)
        {
            try
            {
                // Conectar a la base de datos
                conectar();

                // Consulta CQL para eliminar el departamento
                string query = $"DELETE FROM departamentos WHERE Numero = {idDepartamento};";

                // Ejecutar la consulta
                var statement = new SimpleStatement(query);
                _session.Execute(statement);

                // Confirmar que el departamento fue eliminado
                MessageBox.Show($"El departamento con ID {idDepartamento} ha sido eliminado exitosamente.",
                                "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Desconectar
                desconectar();
            }
            catch (Exception ex)
            {
                // Manejo de errores en caso de que falle la eliminación
                MessageBox.Show($"Error al eliminar el departamento: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void EliminarPercepcion(Guid idRegistro)
        {
            try
            {
                conectar();
                string query = $"DELETE FROM percepciones WHERE IDRegistro = {idRegistro}";
                _session.Execute(query);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la percepción: {ex.Message}");
            }
            finally
            {
                desconectar();
            }
        }



        public void EliminarDeduccion(Guid idRegistro)
        {
            try
            {
                conectar();
                string query = $"DELETE FROM deducciones WHERE IDRegistro = {idRegistro}";
                _session.Execute(query);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la deducción: {ex.Message}");
            }
            finally
            {
                desconectar();
            }
        }

        public void EliminarPuesto(string puesto)
        {
            try
            {
                // Conectar a la base de datos
                conectar();

                // Consulta CQL para eliminar el puesto
                string query = $"DELETE FROM puestos WHERE Puesto = '{puesto}';";

                // Ejecutar la consulta
                var statement = new SimpleStatement(query);
                _session.Execute(statement);

                // Confirmar que el puesto fue eliminado
                MessageBox.Show($"El puesto '{puesto}' ha sido eliminado exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Desconectar
                desconectar();
            }
            catch (Exception ex)
            {
                // Manejo de errores en caso de que falle la eliminación
                MessageBox.Show($"Error al eliminar el puesto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void EliminarPercepcionPorConcepto(string concepto)
        {
            try
            {
                conectar();
                string query = $"DELETE FROM percepciones WHERE Concepto = '{concepto}'";
                _session.Execute(query);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la percepción: {ex.Message}");
            }
            finally
            {
                desconectar();
            }
        }
        public void EliminarDeduccionPorConcepto(string concepto)
        {
            try
            {
                conectar();
                string query = $"DELETE FROM deducciones WHERE Concepto = '{concepto}'";
                _session.Execute(query);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la deducción: {ex.Message}");
            }
            finally
            {
                desconectar();
            }
        }

        //ACTUALIZAR----------------------------------------------------------------------------------------------------------------------------------
        public void ActualizaDatosEmpleado(int empleadoId, string nuevoUsuario, string nuevoEmail, int nuevoCP, string nuevoCalle, string nuevoColonia, string nuevoEstado, string nuevoMunicipio, string nuevoBanco, double nuevoNumCuenta, double nuevoTelefono)
        {
            try
            {
                conectar();


                string query = $"UPDATE Empleados SET Usuario = '{nuevoUsuario}', Email = '{nuevoEmail}', Codigo_Postal = {nuevoCP},Calle = '{nuevoCalle}', Colonia = '{nuevoColonia}', estado = '{nuevoEstado}', municipio = '{nuevoMunicipio}',banco = '{nuevoBanco}',Num_Cuenta = {nuevoNumCuenta}, Telefono = {nuevoTelefono} WHERE id = {empleadoId}";
                _session.Execute(query);

                MessageBox.Show("Datos actualizados exitosamente.");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error al actualizar los datos del empleado: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                desconectar();
            }
        }

    }
}