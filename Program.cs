using System;

namespace cuadros
{
    class Program
    {
        Reloj reloj;
        Expo expo;
        Pintor[] pintores;
        Marchante[] marchantes;

        public void Run(int stock, int capacidad, int[] tp1, int[] tp2, int[] tm1, int[] tm2)
        {
            Console.WriteLine("Inicio");
            Init(stock, capacidad, tp1, tp2, tm1, tm2);
            Start();
            Finish();
            Console.WriteLine("Metidos: {0} Sacados: {1} Stock: {2} ",
                                expo.Metidos, expo.Sacados, expo.Stock);
            Console.WriteLine("Tiempo pintores parados: {0}\nTiempo marchantes parados: {1}",
                                expo.GetTiempoEsperaMeter(), expo.GetTiempoEsperaSacar());
        }
        void Init(int stock, int capacidad, int[] tp1, int[] tp2, int[] tm1, int[] tm2)
        {
            reloj = new Reloj();
            expo = new Expo(reloj, stock, capacidad);
            int n = tp1.Length;
            int m = tm1.Length;
            pintores = new Pintor[n];
            marchantes = new Marchante[m];
            for (int i = 0; i < n; i++)
            {
                pintores[i] = new Pintor(reloj, expo, tp1[i], tp2[i]);
            }
            for (int i = 0; i < m; i++)
            {
                marchantes[i] = new Marchante(reloj, expo, tm1[i], tm2[i]);
            }
        }
        void Start()
        {
            expo.Start();
            for (int i = 0; i < pintores.Length; i++)
            {
                pintores[i].Start();
            }
            for (int i = 0; i < marchantes.Length; i++)
            {
                marchantes[i].Start();
            }
        }
        void Finish()
        {
            expo.Finish();
            for (int i = 0; i < pintores.Length; i++)
            {
                pintores[i].Finish();
            }
            for (int i = 0; i < marchantes.Length; i++)
            {
                marchantes[i].Finish();
            }
        }
        static void Main(string[] args)
        {
            Program p = new Program();
            int stock = 6;
            int capacidad = 10;
            int[] tm1 = { 1, 1, 7 };
            int[] tm2 = { 60, 30, 15 };
            int[] tp1 = { 20, 7 };
            int[] tp2 = { 40, 7 };
            p.Run(stock, capacidad, tm1, tm2, tp1, tp2);
        }

    }
}
