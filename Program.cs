using System;

namespace cuadros
{
    class Program
    {
        Reloj reloj;
        Expo expo;
        Pintor pintor;
        Marchante marchante;

        public void Run(int stock, int capacidad, int tp1, int tp2, int tm1, int tm2)
        {
            Console.WriteLine("Inicio");
            Init(stock, capacidad, tp1, tp2, tm1, tm2);
            Start();
            Finish();
            Console.WriteLine("Metidos: {0} Sacados: {1} Stock: {2} ",
                                expo.Metidos, expo.Sacados, expo.Stock);
            Console.WriteLine("Tiempo pintor parado: {0}\nTiempo marchante parado: {1}",
                                expo.GetTiempoEsperaMeter(), expo.GetTiempoEsperaSacar());
        }
        void Init(int stock, int capacidad, int tp1, int tp2, int tm1, int tm2)
        {
            reloj = new Reloj();
            expo = new Expo(reloj, stock, capacidad);
            pintor = new Pintor(reloj, expo, tp1, tp2);
            marchante = new Marchante(reloj, expo, tm1, tm2);
        }
        void Start()
        {
            expo.Start();
            pintor.Start();
            marchante.Start();
        }
        void Finish()
        {
            expo.Finish();
            pintor.Finish();
            marchante.Finish();
        }
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run(9, 10, 1, 60, 15, 15);
            p.Run(9, 10, 1, 60, 30, 60);
            p.Run(9, 10, 1, 60, 60, 60);
        }

    }
}
