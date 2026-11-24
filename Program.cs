using System;
using System.IO;

namespace MesaDePartesMPT
{
    class Program
    {
        const string ARCHIVO = "expedientes.txt";

        // Parte de Jhonatan
        static void Main(string[] args)
        {
            int total = 0;
            const int CAPACIDAD = 100;
            string opMenu = "0";

            string[] codigos = new string[CAPACIDAD];
            string[] solicitantes = new string[CAPACIDAD];
            string[] tipos = new string[CAPACIDAD];
            string[] asuntos = new string[CAPACIDAD];
            string[] fechas = new string[CAPACIDAD];
            string[] estados = new string[CAPACIDAD];
            string[] tiposTramite = new string[4];

            tiposTramite[0] = "Solicitud administrativa general (FUT)";
            tiposTramite[1] = "Licencia de funcionamiento";
            tiposTramite[2] = "Recurso o impugnacion";
            tiposTramite[3] = "Acceso a la informacion publica";

            CargarArchivo(codigos, solicitantes, tipos, asuntos, fechas, estados, ref total, CAPACIDAD);

            while (opMenu != "5")
            {
                MostrarMenu();
                Console.Write("Elija una opcion: ");
                opMenu = LeerTexto();

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
                    GuardarArchivo(codigos, solicitantes, tipos, asuntos, fechas, estados, total);
                    Console.WriteLine("Fin del programa.");
                }
                else
                {
                    Console.WriteLine("ERROR: opcion invalida. Elija un numero del 1 al 5.");
                }
                Console.WriteLine();
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

        //parte de Patrick
        // Lee una linea del teclado, evita null y quita espacios al inicio y al final
        static string LeerTexto()
        {
            string texto = Console.ReadLine() ?? "";
            return texto.Trim();
        }

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
                resultado = LeerTexto();
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
            bool valido;
            do
            {
                Console.WriteLine(mensaje);
                resultado = LeerTexto();
                valido = !string.IsNullOrWhiteSpace(resultado);

                if (!valido)
                {
                    Console.WriteLine("Error: este campo es obligatorio.");
                    Console.WriteLine("Por favor, ingrese un dato.");
                }
                else if (resultado.Contains('|'))
                {
                    Console.WriteLine("Error: no se permite el caracter |.");
                    valido = false;
                }
            } while (!valido);

            return resultado;
        }
        
        //Parte de Gustavo
        static void Registrar(string[] codigos, string[] solicitantes, string[] tipos, string[] asuntos, string[] fechas, string[] estados, string[] tiposTramite, ref int total, int CAPACIDAD)
        {
            if (total >= CAPACIDAD)
            {
                Console.WriteLine("ERROR: capacidad completa.");
                return; 
            }

            string codigo = PedirTextoObligatorio("Codigo (ej. EXP-001):").ToUpper();

            if (!ValidarFormatoCodigo(codigo))
            {
                Console.WriteLine("ERROR: el codigo debe tener el formato EXP-001. No se registro.");
                return;
            }

            if (BuscarPosicion(codigos, total, codigo) != -1)
            {
                Console.WriteLine("ERROR: el codigo ya existe. No se registro.");
                return;
            }

            string solicitante = PedirTextoObligatorio("Solicitante (ficticio):");
            string tipo = PedirTipoTramite(tiposTramite);
            string asunto = PedirTextoObligatorio("Asunto:");
            string fecha = PedirFecha();

            codigos[total] = codigo;
            solicitantes[total] = solicitante;
            tipos[total] = tipo;
            asuntos[total] = asunto;
            fechas[total] = fecha;
            estados[total] = "Recibido";
            
            total++; 

            Console.WriteLine("Expediente " + codigo + " registrado correctamente.");
        }

        static void BuscarExpediente(string[] codigos, string[] solicitantes, string[] tipos, string[] asuntos, string[] fechas, string[] estados, int total)
        {
            string cod = PedirTextoObligatorio("¿Cual es su codigo?").ToUpper();
            int pos = BuscarPosicion(codigos, total, cod);

            if (pos == -1)
            {
                Console.WriteLine("No se encontro el codigo");
            }
            else
            {
                MostrarExpediente(codigos, solicitantes, tipos, asuntos, fechas, estados, pos);
            }
        }

        static int BuscarPosicion(string[] codigos, int total, string codeABuscar)
        {
            return BuscarRecursivo(codigos, codeABuscar, 0, total);
        }

        //Parte de Patrick
        // Busqueda recursiva: revisa la posicion inicio y, si no es, se llama con inicio + 1
        static int BuscarRecursivo(string[] codigos, string codigo, int inicio, int total)
        {
            if (inicio >= total)
            {
                return -1;
            }

            if (codigos[inicio] == codigo)
            {
                return inicio;
            }

            return BuscarRecursivo(codigos, codigo, inicio + 1, total);
        }

        static string PedirTipoTramite(string[] tiposTramite)
        {
            int opcn;
            bool esNumero;
            do
            {
                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine((i + 1) + ". " + tiposTramite[i]);
                }

                Console.WriteLine("Elija el tipo de tramite (1-4):");
                string input = LeerTexto();
                esNumero = int.TryParse(input, out opcn);

                if (!esNumero || opcn < 1 || opcn > 4)
                {
                    Console.WriteLine("Error: opcion invalida. Elija un numero del 1 al 4.");
                }

            } while (!esNumero || opcn < 1 || opcn > 4);

            return tiposTramite[opcn - 1]; 
        }

        //Parte de Gustavo (archivos)
        // Escribe un expediente por linea, con los seis datos separados por |
        static void GuardarArchivo(string[] codigos, string[] solicitantes, string[] tipos, string[] asuntos, string[] fechas, string[] estados, int total)
        {
            using (StreamWriter archivo = new StreamWriter(ARCHIVO))
            {
                for (int i = 0; i < total; i++)
                {
                    string linea = string.Join("|", codigos[i], solicitantes[i], tipos[i], asuntos[i], fechas[i], estados[i]);
                    archivo.WriteLine(linea);
                }
            }
            Console.WriteLine("Expedientes guardados en " + ARCHIVO + ": " + total);
        }

        // Lee el archivo al iniciar; si no existe o esta vacio, el programa sigue con la lista vacia
        static void CargarArchivo(string[] codigos, string[] solicitantes, string[] tipos, string[] asuntos, string[] fechas, string[] estados, ref int total, int CAPACIDAD)
        {
            total = 0;

            if (!File.Exists(ARCHIVO))
            {
                Console.WriteLine("No hay archivo previo. Se inicia con la lista vacia.");
                return;
            }

            string[] lineas = File.ReadAllLines(ARCHIVO);
            int ignoradas = 0;

            foreach (string linea in lineas)
            {
                if (string.IsNullOrWhiteSpace(linea))
                {
                    continue;
                }

                string[] datos = linea.Split('|');

                if (datos.Length == 6 && total < CAPACIDAD)
                {
                    codigos[total] = datos[0];
                    solicitantes[total] = datos[1];
                    tipos[total] = datos[2];
                    asuntos[total] = datos[3];
                    fechas[total] = datos[4];
                    estados[total] = datos[5];
                    total++;
                }
                else
                {
                    ignoradas++;
                }
            }

            Console.WriteLine("Expedientes cargados: " + total);
            if (ignoradas > 0)
            {
                Console.WriteLine("Lineas ignoradas por formato invalido o por capacidad: " + ignoradas);
            }
        }

        //Parte de Pedro
        static void Ordenar(string[] codigos, string[] solicitantes, string[] tipos, string[] asuntos, string[] fechas, string[] estados, int total)
        {
            if (total == 0)
            {
                Console.WriteLine("No hay expedientes para ordenar.");
                return;
            }

            Console.WriteLine("1. Ordenar por codigo");
            Console.WriteLine("2. Ordenar por fecha");
            Console.Write("Elija una opcion (1-2): ");
            string op = LeerTexto();

            if (op == "1")
            {
                OrdenarPorCodigo(codigos, solicitantes, tipos, asuntos, fechas, estados, total);
            }
            else if (op == "2")
            {
                OrdenarPorFecha(codigos, solicitantes, tipos, asuntos, fechas, estados, total);
            }
            else
            {
                Console.WriteLine("Error: Opcion invalida.");
            }
        }

        static void OrdenarPorCodigo(string[] codigos, string[] solicitantes, string[] tipos, string[] asuntos, string[] fechas, string[] estados, int total)
        {
            for (int i = 0; i < total - 1; i++)
            {
                int menor = i;
                for (int j = i + 1; j < total; j++)
                {
                    if (string.Compare(codigos[j], codigos[menor]) < 0)
                    {
                        menor = j;
                    }
                }
                if (menor != i)
                {
                    Intercambiar(codigos, solicitantes, tipos, asuntos, fechas, estados, i, menor);
                }
            }
            Console.WriteLine("Expedientes ordenados por codigo correctamente.");
        }

        static void OrdenarPorFecha(string[] codigos, string[] solicitantes, string[] tipos, string[] asuntos, string[] fechas, string[] estados, int total)
        {
            for (int i = 0; i < total - 1; i++)
            {
                int menor = i;
                for (int j = i + 1; j < total; j++)
                {
                    if (FechaANumero(fechas[j]) < FechaANumero(fechas[menor]))
                    {
                        menor = j;
                    }
                }
                if (menor != i)
                {
                    Intercambiar(codigos, solicitantes, tipos, asuntos, fechas, estados, i, menor);
                }
            }
            Console.WriteLine("Expedientes ordenados por fecha correctamente.");
        }

        static void Intercambiar(string[] codigos, string[] solicitantes, string[] tipos, string[] asuntos, string[] fechas, string[] estados, int i, int menor)
        {
            IntercambioDatos(codigos, i, menor);
            IntercambioDatos(solicitantes, i, menor);
            IntercambioDatos(tipos, i, menor);
            IntercambioDatos(asuntos, i, menor);
            IntercambioDatos(fechas, i, menor);
            IntercambioDatos(estados, i, menor);
        }

        static void IntercambioDatos(string[] arreglo, int i, int j)
        {
            string aux = arreglo[i];
            arreglo[i] = arreglo[j];
            arreglo[j] = aux;
        }
    }
}
