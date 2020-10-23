using System;

namespace cuadros
{
    class Program
    {
        Reloj reloj;
        Expo expo;
        Pintor pintor;
        Marchante marchante;

        public void Run()
        {
            Console.WriteLine("Inicio");
            Init();
            Start();
            Finish();
            Console.WriteLine("Metidos: {0} Sacados: {1} Stock: {2} ",
                                expo.Metidos, expo.Sacados, expo.Stock);
        }

        void Init()
        {
            reloj = new Reloj();
            expo = new Expo(reloj);
            pintor = new Pintor(reloj, expo);
            marchante = new Marchante(reloj, expo);
        }
        void Start()
        {
            pintor.Start();
            marchante.Start();
        }
        void Finish()
        {
            pintor.Finish();
            expo.Close();
            marchante.Finish();
        }
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

    }
}
