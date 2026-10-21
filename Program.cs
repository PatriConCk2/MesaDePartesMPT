using System;

namespace MesaDePartesMPT
{
    class Program
    {  
        //Parte de Jhonatan
        static void Main(string[] args)
        {
            int total = 0;
            int CAPACIDAD = 100;
            string opMenu = "0";

            string[] codigos = new string[100];
            string[] solicitantes = new string[100];
            string[] tipos = new string[100];
            string[] asuntos = new string[100];
            string[] fechas = new string[100];
            string[] estados = new string[100];
            string[] tiposTramite = new string[4];

            tiposTramite[0] = "Solicitud administrativa general (FUT)";
            tiposTramite[1] = "Licencia de funcionamiento";
            tiposTramite[2] = "Recurso o impugnacion";
            tiposTramite[3] = "Acceso a la informacion publica";

            while (opMenu != "5")
            {
                MostrarMenu();
                Console.Write("Elija una opcion: ");
                opMenu = Console.ReadLine();

                if (opMenu == "1")
                {
                    Registrar(codigos, solicitantes, tipos, asuntos, fechas, estados, tiposTramite, ref total, CAPACIDAD);
                }
                else if (opMenu == "2")
                {
                    BuscarExpediente(codigos, solicitantes, tipos, asuntos, fechas, estados, total);
                }
                else if (opMenu == "3")
                {
                    Listar(codigos, solicitantes, tipos, asuntos, fechas, estados, total);
                }
                else if (opMenu == "4")
                {
                    Ordenar(codigos, solicitantes, tipos, asuntos, fechas, estados, total);
                }
                else if (opMenu == "5")
                {
                    Console.WriteLine("Fin del programa.");
                }
                else
                {
                    Console.WriteLine("ERROR: opcion invalida. Elija un numero del 1 al 5.");
                }
                Console.WriteLine(); //simplemente un espacio
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("Mesa de Partes de la MPT");
            Console.WriteLine("1. Registrar expediente");
            Console.WriteLine("2. Buscar expediente por codigo");
            Console.WriteLine("3. Listar expedientes");
            Console.WriteLine("4. Ordenar expedientes");
            Console.WriteLine("5. Salir");
        }

        static void MostrarExpediente(string[] codigos, string[] solicitantes, string[] tipos, string[] asuntos, string[] fechas, string[] estados, int pos)
        {
            Console.WriteLine("codigo: " + codigos[pos]);
            Console.WriteLine("solicitante: " + solicitantes[pos]);
            Console.WriteLine("tipo: " + tipos[pos]);
            Console.WriteLine("asunto: " + asuntos[pos]);
            Console.WriteLine("fecha: " + fechas[pos]);
            Console.WriteLine("Estado: " + estados[pos]);
            Console.WriteLine("Posicion: " + (pos + 1)); 
            Console.WriteLine("=================");
        }

        static void Listar(string[] codigos, string[] solicitantes, string[] tipos, string[] asuntos, string[] fechas, string[] estados, int total)
        {
            if (total == 0)
            {
                Console.WriteLine("La lista esta vacia");
            }
            else
            {
                for (int i = 0; i < total; i++)
                {
                    MostrarExpediente(codigos, solicitantes, tipos, asuntos, fechas, estados, i);
                }
            }
        }

        //Parte de Patrick
        static bool ValidarFecha(string fecha)
        {
            if (fecha.Length != 10)
            {
                return false;
            }
            
            if (fecha.Substring(2, 1) != "/" || fecha.Substring(5, 1) != "/")
            {
                return false;
            }

            for (int i = 0; i < 10; i++)
            {
                if (i != 2 && i != 5) 
                {
                    if (!char.IsDigit(fecha[i]))
                    {
                        return false;
                    }
                }
            }

            int dia = int.Parse(fecha.Substring(0, 2));
            int mes = int.Parse(fecha.Substring(3, 2));
            int anio = int.Parse(fecha.Substring(6, 4));

            if (anio < 1900 || anio > 2100 || mes < 1 || mes > 12)
            {
                return false;
            }

            int diasMes;
            switch (mes)
            {
                case 2:
                    diasMes = EsBisiesto(anio) ? 29 : 28;
                    break;
                case 4: case 6: case 9: case 11:
                    diasMes = 30;
                    break;
                default:
                    diasMes = 31;
                    break;
            }

            if (dia < 1 || dia > diasMes)
            {
                return false;
            }

            return true;
        }

        static string PedirFecha()
        {
            string resultado;
            bool valida;
            do
            {
                Console.WriteLine("Fecha de recepcion (DD/MM/AAAA):");
                resultado = Console.ReadLine();
                valida = ValidarFecha(resultado);

                if (!valida)
                {
                    Console.WriteLine("Error: fecha invalida. Use el formato DD/MM/AAAA.");
                }
            } while (!valida);

            return resultado;
        }

        static bool EsBisiesto(int year)
        {
            if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
            {
                return true;
            }
            return false;
        }

        static int FechaANumero(string fecha)
        {
            int dia = int.Parse(fecha.Substring(0, 2));
            int mes = int.Parse(fecha.Substring(3, 2));
            int anio = int.Parse(fecha.Substring(6, 4));
            
            mes = mes * 100;
            anio = anio * 10000;
            return anio + mes + dia;
        }

        static bool ValidarFormatoCodigo(string codigo)
        {
            if (codigo.Length != 7)
            {
                return false;
            }
            if (codigo.Substring(0, 4) != "EXP-")
            {
                return false;
            }

            for (int i = 4; i < 7; i++)
            {
                if (!char.IsDigit(codigo[i]))
                {
                    return false;
                }
            }
            return true;
        }

        static string PedirTextoObligatorio(string mensaje)
        {
            string resultado;
            do
            {
                Console.WriteLine(mensaje);
                resultado = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(resultado))
                {
                    Console.WriteLine("Error: este campo es obligatorio.");
                    Console.WriteLine("Por favor, ingrese un dato.");
                }
            } while (string.IsNullOrWhiteSpace(resultado));

            return resultado;
        }

        //Parte de Gustavo
        static void Registrar(string[] codigos, string[] solicitantes, string[] tipos, string[] asuntos, string[] fechas, string[] estados, string[] tiposTramite, ref int total, int CAPACIDAD) { }
        static void BuscarExpediente(string[] codigos, string[] solicitantes, string[] tipos, string[] asuntos, string[] fechas, string[] estados, int total) { }
        static int BuscarPosicion(string[] codigos, int total, string codeABuscar) { return -1; }
        static string PedirTipoTramite(string[] tiposTramite) { return ""; }

        //Parte de Pedro
        static void Ordenar(string[] codigos, string[] solicitantes, string[] tipos, string[] asuntos, string[] fechas, string[] estados, int total) { }
        static void OrdenarPorCodigo(string[] codigos, string[] solicitantes, string[] tipos, string[] asuntos, string[] fechas, string[] estados, int total) { }
        static void OrdenarPorFecha(string[] codigos, string[] solicitantes, string[] tipos, string[] asuntos, string[] fechas, string[] estados, int total) { }
        static void Intercambiar(string[] codigos, string[] solicitantes, string[] tipos, string[] asuntos, string[] fechas, string[] estados, int i, int menoor) { }
        static void IntercambioDatos(string[] arreglo, int i, int j) { }
    }
}
