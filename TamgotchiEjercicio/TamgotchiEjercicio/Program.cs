namespace TamagotchiRealTime
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.WriteLine("¡Bienvenido a tu Tamagotchi en tiempo real!");
            Console.Write("Dale un nombre a tu Tamagotchi: ");
            string nombre = Console.ReadLine();

            Tamagotchi miMascota = new Tamagotchi(nombre);

            int refreshRate = 1000;
            DateTime lastUpdate = DateTime.Now;

            while (miMascota.Vida > 0)
            {
                if ((DateTime.Now - lastUpdate).TotalSeconds >= 10)
                {
                    miMascota.PasarTiempo();
                    lastUpdate = DateTime.Now;
                }

                Console.Clear();
                miMascota.MostrarEstado();
                Console.WriteLine("\nPresiona tecla: C=comida, H=cariño, Q=salir");

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.C:
                            miMascota.Comer();
                            break;
                        case ConsoleKey.H:
                            miMascota.DarCarino();
                            break;
                        case ConsoleKey.Q:
                            Environment.Exit(0);
                            break;
                    }
                }

                Thread.Sleep(refreshRate);
            }

            Console.Clear();
            Console.WriteLine($"\n{miMascota.Nombre} ha muerto :c");
        }
    }

    class Tamagotchi
    {
        public string Nombre { get; private set; }
        public int Vida { get; private set; }
        public int Hambre { get; private set; }

        private int maxVida = 100;
        private int maxHambre = 100;
        private int carinoRestante = 2;
        private int turnosParaRecuperarCarino = 3;
        private int turnosDesdeUltimoCarino = 0;
        private object bloqueo = new object();

        public Tamagotchi(string nombre)
        {
            Nombre = nombre;
            Vida = maxVida;
            Hambre = 0;
        }

        public void MostrarEstado()
        {
            lock (bloqueo)
            {
                Console.WriteLine($"=== {Nombre} ===");
                Console.WriteLine($"Vida: {Vida} / {maxVida}");
                Console.WriteLine($"Hambre: {Hambre} / {maxHambre}");
                Console.WriteLine("Mascota:");
                MostrarSprite();
                Console.WriteLine("Comida disponible: [*]");
            }
        }

        private void MostrarSprite()
        {
            if (Vida > 50)
            {
                Console.WriteLine(@"
    (\_/)
    (•_•)
    / >* ");
            }
            else if (Vida > 20)
            {
                Console.WriteLine(@"
    (\_/)
    (x_x)
    / >* ");
            }
            else
            {
                Console.WriteLine(@"
    (\_/)
    (×_×)
    /   ");
            }
        }

        public void Comer()
        {
            lock (bloqueo)
            {
                if (Hambre == 0)
                {
                    Console.WriteLine($"{Nombre} no tiene hambre.");
                }
                else
                {
                    Hambre -= 20;
                    if (Hambre < 0) Hambre = 0;
                    Console.WriteLine($"{Nombre} comió y su hambre bajó a {Hambre}");
                }
            }
            Thread.Sleep(500);
        }

        public void DarCarino()
        {
            lock (bloqueo)
            {
                if (carinoRestante > 0)
                {
                    AumentarVida(15);
                    carinoRestante--;
                    turnosDesdeUltimoCarino = 0;
                    Console.WriteLine($"{Nombre} recibió cariño. Vida actual: {Vida}");
                }
                else
                {
                    Console.WriteLine($"No puedes darle más cariño ahora. Espera {turnosParaRecuperarCarino - turnosDesdeUltimoCarino} turnos.");
                }
            }
            Thread.Sleep(500);
        }

        public void PasarTiempo()
        {
            lock (bloqueo)
            {
                Hambre += 10;
                if (Hambre > maxHambre) Hambre = maxHambre;

                int decrementoVida = (Hambre >= maxHambre) ? 20 : 5;
                DisminuirVida(decrementoVida);

                turnosDesdeUltimoCarino++;
                if (turnosDesdeUltimoCarino >= turnosParaRecuperarCarino)
                {
                    carinoRestante = 2;
                }
            }
        }

        private void AumentarVida(int cantidad)
        {
            Vida += cantidad;
            if (Vida > maxVida) Vida = maxVida;
        }

        private void DisminuirVida(int cantidad)
        {
            Vida -= cantidad;
            if (Vida < 0) Vida = 0;
        }
    }
}
