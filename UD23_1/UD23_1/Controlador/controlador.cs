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
    class controlador
    {
        private vista v;
        private modelo m;

        //constructor y lanzador
        public controlador(vista v, modelo m)
        {
            this.v = v;
            this.m = m;
            Inicio(v,m);
        }

        

        //procedimiento para empezar
        public void Inicio(vista v, modelo m)
        {

            int menu=0;
            while (menu != 5)
            {
                this.v.PintaMenu();
                menu = Convert.ToInt32(Console.ReadLine());
                switch (menu) {
                    case 1:
                        //insertar nuevo cliente
                        this.v.Pinta1();
                        cliente nuevo = NuevoCliente();
 /*porque no this.m*/   modelo.InsertarCliente(@"('"+nuevo.nombre+"','"+nuevo.apellidos+ "','"+nuevo.direccion+"','"+nuevo.dni+ "','"+nuevo.fecha_alta+ "','"+nuevo.fecha_mod+"')");
                        this.v.PintaMenu();
                        break;
                    case 2:
                        //modificar cliente
                        this.v.Pinta2();
                        cliente viejo = modelo.LeerCliente(Convert.ToInt32(Console.ReadLine()));
                        cliente modificado =EditaCliente(viejo);//leer viejo
                        modelo.ModificarCliente(modificado);
                        this.v.PintaMenu();
                        break;
                    case 3:
                        //listado
                        this.v.Pinta3();
                        var Tabla = new ConsoleTable("ID", "NOMBRE", "APELLIDOS", "DIRECCION", "DNI", "FECHA ALTA", "FECHA MOD");
                        Tabla = modelo.CargarListado();
                        Tabla.Write(Format.Alternative);
                        Console.WriteLine("Pulsa cualquier tecla para continuar...");
                        Console.ReadLine();
                        this.v.PintaMenu();
                        break;
                    case 4:
                        this.v.Pinta4();
                        modelo.BorrarCliente(Convert.ToInt32(Console.ReadLine()));
                        this.v.PintaMenu();
                        Console.WriteLine("Pulsa cualquier tecla para continuar...");
                        Console.ReadLine();
                        break;
                    case 5:
                        //salir
                        break;
                    default:
                        Console.WriteLine("Introduce un valor válido");
                        Console.ReadLine();
                        this.v.PintaMenu();
                        break;
                }
            }

        }

        public cliente NuevoCliente()
        {
            cliente nuevo = new cliente();
            Console.WriteLine("Introduce el nombre del cliente:");
            nuevo.nombre = Console.ReadLine();
            Console.WriteLine("Introduce los apellidos del cliente:");
            nuevo.apellidos = Console.ReadLine();
            Console.WriteLine("Introduce la direccion del cliente:");
            nuevo.direccion = Console.ReadLine();
            Console.WriteLine("Introduce el DNI del cliente (12345678A)");
            nuevo.dni = Console.ReadLine();
            nuevo.fecha_alta = DateTime.Now;
            nuevo.fecha_mod = nuevo.fecha_alta;
            return nuevo;
        }
        public cliente EditaCliente(cliente cli)
        {
            
            cliente c = new cliente();
            c.id = cli.id;
            Console.WriteLine("Introduce el nombre del cliente (o deja en blanco para dejar el valor antiguo '" + cli.nombre + "'):");
            c.nombre = Console.ReadLine();
            if (c.nombre == "") {
                c.nombre = cli.nombre;
            }
            Console.WriteLine("Introduce los apellidos del cliente (o deja en blanco para dejar el valor antiguo '" + cli.apellidos + "'):");
            c.apellidos = Console.ReadLine();
            if (c.apellidos == "")
            {
                c.apellidos = cli.apellidos;
            }
            Console.WriteLine("Introduce la direccion del cliente (o deja en blanco para dejar el valor antiguo '" + cli.direccion + "'):");
            c.direccion = Console.ReadLine();
            if (c.direccion == "")
            {
                c.direccion = cli.direccion;
            }
            Console.WriteLine("Introduce el DNI del cliente (o deja en blanco para dejar el valor antiguo '" + cli.dni + "')");
            c.dni = Console.ReadLine();
            if (c.dni == "")
            {
                c.dni = cli.dni;
            }
            //c.fecha_alta = DateTime.Now;
            c.fecha_mod = DateTime.Now;
            return c;
        }
    }
}
