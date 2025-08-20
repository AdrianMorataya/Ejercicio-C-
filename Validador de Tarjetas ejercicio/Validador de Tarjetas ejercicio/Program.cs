namespace PruebaCSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\nHola!\nElige una opción:\n1. Validador de tarjetas\n2. Operaciones aritméticas\n3. Sistema bancario\n4. Salir");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        ValidarTarjeta();
                        break;

                    case "2":
                        OperacionesAritmeticas();
                        break;

                    case "3":
                        SistemaBancario();
                        break;

                    case "4":
                        salir = true;
                        Console.WriteLine("¡Hasta luego!");
                        break;

                    default:
                        Console.WriteLine("Opción inválida. Intenta de nuevo.");
                        break;
                }
            }
        }

        static void ValidarTarjeta()
        {
            Console.WriteLine("Bienvenido al validador de tarjetas!\n¿Cuál es tu nombre?");
            string name = Console.ReadLine();
            Console.WriteLine($"Mucho gusto {name}!\nEscribe el número de tarjeta a validar:");

            string numberCard = Console.ReadLine();

            while (numberCard.Length != 16 || !numberCard.All(char.IsDigit))
            {
                Console.WriteLine("El número de tarjeta es inválido, debe tener 16 dígitos numéricos.\nVuelve a escribir el número:");
                numberCard = Console.ReadLine();
            }

            string tipoCard = "Desconocido";
            string primerosDosDigitos = numberCard.Substring(0, 2);

            if (numberCard.StartsWith("4"))
                tipoCard = "Visa";
            else if (int.TryParse(primerosDosDigitos, out int dosPrimeros) && dosPrimeros >= 51 && dosPrimeros <= 55)
                tipoCard = "Mastercard";
            else if (numberCard.StartsWith("3") && (numberCard[1] == '4' || numberCard[1] == '7'))
                tipoCard = "American Express";
            else if (numberCard.StartsWith("6"))
                tipoCard = "Discover";

            int suma = 0;
            for (int i = 0; i < numberCard.Length; i++)
            {
                int digito = int.Parse(numberCard[numberCard.Length - 1 - i].ToString());

                if (i % 2 == 1)
                {
                    digito *= 2;
                    if (digito > 9)
                        digito -= 9;
                }

                suma += digito;
            }

            if (suma % 10 == 0)
                Console.WriteLine($"La tarjeta es válida.\nY su tipo de tarjeta es: {tipoCard}");
            else
                Console.WriteLine($"La tarjeta es inválida.\nY su tipo de tarjeta es: {tipoCard}");
        }

        static void OperacionesAritmeticas()
        {
            bool volver = false;

            while (!volver)
            {
                Console.WriteLine("\nElige operación:\n1. Calcular área de figuras\n2. Calcular hipotenusa de un triángulo\n3. Volver al menú principal");
                string opcionOperaciones = Console.ReadLine();

                switch (opcionOperaciones)
                {
                    case "1":
                        CalcularArea();
                        break;

                    case "2":
                        CalcularHipotenusa();
                        break;

                    case "3":
                        volver = true;
                        break;

                    default:
                        Console.WriteLine("Opción inválida. Intenta de nuevo.");
                        break;
                }
            }
        }

        static void CalcularArea()
        {
            Console.WriteLine("Elige la figura para calcular el área:\n1. Cuadrado\n2. Rectángulo\n3. Círculo\n4. Triángulo");
            string figura = Console.ReadLine();
            double area = 0;

            switch (figura)
            {
                case "1":
                    double lado = LeerNumero("Ingresa el lado del cuadrado: ");
                    area = lado * lado;
                    break;

                case "2":
                    double baseRect = LeerNumero("Ingresa la base del rectángulo: ");
                    double alturaRect = LeerNumero("Ingresa la altura del rectángulo: ");
                    area = baseRect * alturaRect;
                    break;

                case "3":
                    double radio = LeerNumero("Ingresa el radio del círculo: ");
                    area = Math.PI * radio * radio;
                    break;

                case "4":
                    double baseTri = LeerNumero("Ingresa la base del triángulo: ");
                    double alturaTri = LeerNumero("Ingresa la altura del triángulo: ");
                    area = (baseTri * alturaTri) / 2;
                    break;

                default:
                    Console.WriteLine("Figura inválida.");
                    break;
            }

            if (area > 0)
                Console.WriteLine($"El área de la figura es: {area}");
        }

        static void CalcularHipotenusa()
        {
            double cateto1 = LeerNumero("Ingresa el primer cateto: ");
            double cateto2 = LeerNumero("Ingresa el segundo cateto: ");

            double hipotenusa = Math.Sqrt(cateto1 * cateto1 + cateto2 * cateto2);
            Console.WriteLine($"La hipotenusa del triángulo es: {hipotenusa}");
        }

        static void SistemaBancario()
        {
            Console.WriteLine("Bienvenido al sistema bancario!\n¿Cuál es tu nombre?");
            string nombre = Console.ReadLine();
            Random rnd = new Random();
            int numeroCuenta = rnd.Next(10000000, 99999999);
            double saldo = 0;

            bool salirBanco = false;

            while (!salirBanco)
            {
                Console.WriteLine($"\nHola {nombre}! Número de cuenta: {numeroCuenta}\nSaldo actual: {saldo}\nElige una opción:\n1. Consultar fondos\n2. Agregar dinero\n3. Gastar dinero\n4. Salir al menú principal");
                string opcionBanco = Console.ReadLine();

                switch (opcionBanco)
                {
                    case "1":
                        Console.WriteLine($"Tu saldo actual es: {saldo}");
                        Console.WriteLine($"Número de cuenta: {numeroCuenta}");
                        break;

                    case "2":
                        double agregar = LeerNumero("Ingresa la cantidad a agregar: ");
                        if (agregar < 0)
                        {
                            Console.WriteLine("No se puede agregar una cantidad negativa.");
                        }
                        else
                        {
                            saldo += agregar;
                            Console.WriteLine($"Se agregaron {agregar} a tu cuenta. Saldo actual: {saldo}");
                        }
                        break;

                    case "3":
                        double gastar = LeerNumero("Ingresa la cantidad a gastar: ");
                        if (gastar < 0)
                        {
                            Console.WriteLine("No se puede gastar una cantidad negativa.");
                        }
                        else if (gastar > saldo)
                        {
                            Console.WriteLine("No tienes suficiente saldo para gastar esa cantidad.");
                        }
                        else
                        {
                            saldo -= gastar;
                            Console.WriteLine($"Se gastaron {gastar}. Saldo restante: {saldo}");
                        }
                        break;

                    case "4":
                        salirBanco = true;
                        break;

                    default:
                        Console.WriteLine("Opción inválida. Intenta de nuevo.");
                        break;
                }
            }
        }

        static double LeerNumero(string mensaje)
        {
            double numero;
            Console.Write(mensaje);
            while (!double.TryParse(Console.ReadLine(), out numero))
            {
                Console.Write("Entrada inválida. " + mensaje);
            }
            return numero;
        }
    }
}