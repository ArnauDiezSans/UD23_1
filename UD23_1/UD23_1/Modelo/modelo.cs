using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using ConsoleTables;

namespace UD23_1
{
    class modelo
    {
        public static SqlConnection AbrirCliente()
        {
            string nombre = "UD23_1";
            SqlConnection conexion = new SqlConnection(@"Data Source=DESKTOP-8C4SS5C\SQLEXPRESS;Initial Catalog=" + nombre + ";Persist Security Info=True;User ID=arnau;Password=arnau1234");
            try
            {
                conexion.Open();
                //   Console.WriteLine("Se abrió la conexión con el servidor 8C4SS5C y la base de datos " + nombre);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return conexion;

        }
        public static void Cerrar(SqlConnection conexion)
        {
            try
            {
                conexion.Close();
                Console.WriteLine("Se cerró la conexión con el servidor");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public static void InsertarCliente(string codigoValor)
        {
            // método para insertar un string pasado por parámetro en una taba

            // conectamos a la base de datos
            SqlConnection conexion = modelo.AbrirCliente();

            // codigoSQL
            string cadena = "INSERT INTO cliente VALUES " + codigoValor;

            try
            {
                // creamos el objeto con el codigo sql y la conexion
                SqlCommand comando = new SqlCommand(cadena, conexion);

                // ejecutamos el codigo sql en el objeto comando
                comando.ExecuteNonQuery();
                Console.WriteLine("Registro insertado con éxito");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                // cerramos la conexión con la base de datos creada
                conexion.Close();
            }
        }
        public static void ModificarCliente(cliente c)
        {
            // método para insertar un string pasado por parámetro en una taba

            // conectamos a la base de datos
            SqlConnection conexion = modelo.AbrirCliente();

            // codigoSQL
            string cadena = "USING UD23_1; UPDATE cliente SET nombre='" + c.nombre + "', apellidos='" + c.apellidos + "', direccion='" + c.direccion + "', dni='" + c.dni + "', fecha_alta='" + c.fecha_alta + "', fecha_mod='" + c.fecha_mod + "' WHERE id=" + c.id + ";";

            try
            {
                // creamos el objeto con el codigo sql y la conexion
                SqlCommand comando = new SqlCommand(cadena, conexion);

                // ejecutamos el codigo sql en el objeto comando
                comando.ExecuteNonQuery();
                Console.WriteLine("Registro modificado con éxito");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                // cerramos la conexión con la base de datos creada
                conexion.Close();
            }
        }
        public static cliente LeerCliente(int codigoCliente)
        {
            // método para insertar un string pasado por parámetro en una taba
            cliente cli = new cliente();

            // conectamos a la base de datos
            SqlConnection conexion = modelo.AbrirCliente();

            // codigoSQL
            string cadena = "USE UD23_1; SELECT * FROM cliente WHERE id=" + codigoCliente + ";";

            try
            {
                // creamos el objeto con el codigo sql y la conexion
                SqlCommand comando = new SqlCommand(cadena, conexion);
                SqlDataReader LectorSql;
                // ejecutamos el codigo sql en el objeto comando
                LectorSql = comando.ExecuteReader();
                //    while (LectorSql.Read())
                //{
                //    cli.nombre = Convert.ToString(LectorSql["nombre"]);
                //    cli.apellidos = Convert.ToString(LectorSql["apellidos"]);
                //    cli.direccion = Convert.ToString(LectorSql["direccion"]);
                //    cli.dni = Convert.ToString(LectorSql["dni"]);
                //    cli.fecha_alta = Convert.ToDateTime(LectorSql["fecha_alta"]);
                //    cli.fecha_mod = Convert.ToDateTime(LectorSql["fecha_mod"]);
                //}
                LectorSql.Read();
                cli.id = codigoCliente;
                cli.nombre = LectorSql.GetString(1);
                cli.apellidos = LectorSql.GetString(2);
                cli.direccion = LectorSql.GetString(3);
                cli.dni = LectorSql.GetString(4);
                cli.fecha_alta = LectorSql.GetDateTime(5);
                cli.fecha_mod = LectorSql.GetDateTime(6);
                //Console.WriteLine("Registro leído con éxito");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                // cerramos la conexión con la base de datos creada
                conexion.Close();
            }
            return cli;
        }
        public static ConsoleTable CargarListado()
        {
            // método para insertar un string pasado por parámetro en una taba
            cliente cli = new cliente();
            ConsoleTable Lista = new ConsoleTable("ID", "NOMBRE", "APELLIDOS", "DIRECCION", "DNI", "FECHA ALTA", "FECHA MOD");

            // conectamos a la base de datos
            SqlConnection conexion = modelo.AbrirCliente();

            // codigoSQL
            string cadena = "USE UD23_1; SELECT * FROM cliente;";

            try
            {
                // creamos el objeto con el codigo sql y la conexion
                SqlCommand comando = new SqlCommand(cadena, conexion);
                SqlDataReader LectorSql;
                // ejecutamos el codigo sql en el objeto comando
                LectorSql = comando.ExecuteReader();
                while (LectorSql.Read())
                {
                    cli.id = LectorSql.GetInt32(0);
                    cli.nombre = LectorSql.GetString(1);
                    cli.apellidos = LectorSql.GetString(2);
                    cli.direccion = LectorSql.GetString(3);
                    cli.dni = LectorSql.GetString(4);
                    cli.fecha_alta = LectorSql.GetDateTime(5);
                    cli.fecha_mod = LectorSql.GetDateTime(6);
                    Lista.AddRow(cli.id, cli.nombre, cli.apellidos, cli.direccion, cli.dni, cli.fecha_alta, cli.fecha_mod);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                // cerramos la conexión con la base de datos creada
                conexion.Close();
            }
            return Lista;
        }
        public static void BorrarCliente(int codigoCliente)
        {
            // método para insertar un string pasado por parámetro en una taba

            // conectamos a la base de datos
            SqlConnection conexion = modelo.AbrirCliente();

            // codigoSQL
            string cadena = "DELETE FROM cliente WHERE id=" + codigoCliente;

            try
            {
                // creamos el objeto con el codigo sql y la conexion
                SqlCommand comando = new SqlCommand(cadena, conexion);

                // ejecutamos el codigo sql en el objeto comando
                comando.ExecuteNonQuery();
                Console.WriteLine("Registro borrado con éxito");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                // cerramos la conexión con la base de datos creada
                conexion.Close();
            }
        }
    }
}
