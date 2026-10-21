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
            string op = Console.ReadLine();

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
