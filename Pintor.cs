using System;
using System.Threading;

namespace cuadros
{
    public class Pintor
    {
        Expo expo;
        Reloj reloj;
        Thread thread;
        int tp1, tp2;

        public Pintor(Reloj reloj, Expo expo, int tp1, int tp2)
        {
            this.reloj = reloj;
            this.expo = expo;
            this.tp1 = tp1;
            this.tp2 = tp2;
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
            while (!expo.IsClosed())
            {
                i = rnd.Next(tp1, tp2+1);
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