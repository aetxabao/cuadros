using System;
using System.Threading;

namespace cuadros
{
    public class Marchante
    {

        Expo expo;
        Reloj reloj;
        Thread thread;
        int tm1, tm2;
        public Marchante(Reloj reloj, Expo expo, int tm1, int tm2)
        {
            this.reloj = reloj;
            this.expo = expo;
            this.tm1 = tm1;
            this.tm2 = tm2;
        }
        public void Start()
        {
            //Console.WriteLine("M inicio.");
            this.thread = new Thread(() => this.Agenda());
            this.thread.Start();
        }

        public void Finish()
        {
            thread.Join();
            //Console.WriteLine("M fin.");
        }

        void Agenda()
        {
            Random rnd = new Random();
            int i;
            while (!expo.IsClosed())
            {
                i = rnd.Next(tm1, tm2 + 1);
                if (reloj.GetMilliseconds() + i * Reloj.MSxD < 365 * Reloj.MSxD)
                {
                    Thread.Sleep(i * Reloj.MSxD);
                    expo.Remove();
                }
                else
                {
                    return;
                }
            }
        }
    }
}