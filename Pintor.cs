using System;
using System.Threading;

namespace cuadros
{
    public class Pintor
    {
        Expo expo;
        Reloj reloj;
        Thread thread;

        public Pintor(Reloj reloj, Expo expo)
        {
            this.reloj = reloj;
            this.expo = expo;
        }
        public void Start()
        {
            //Console.WriteLine("P inicio.");
            this.thread = new Thread(() => this.Agenda());
            this.thread.Start();
        }

        public void Finish()
        {
            thread.Join();
            //Console.WriteLine("P fin.");
        }

        void Agenda()
        {
            Random rnd = new Random();
            int i;
            while (true)
            {
                i = rnd.Next(1, 60);
                if (reloj.GetMilliseconds() + i * Reloj.MSxD < 365 * Reloj.MSxD)
                {
                    Thread.Sleep(i * Reloj.MSxD);
                    expo.Insert();
                }
                else
                {
                    return;
                }
            }
        }
    }
}