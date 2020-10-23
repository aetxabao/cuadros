using System;
using System.Threading;

namespace cuadros
{
    public class Marchante
    {

        Expo expo;
        Reloj reloj;
        Thread thread;
        public Marchante(Reloj reloj, Expo expo)
        {
            this.reloj = reloj;
            this.expo = expo;
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
            while (!expo.IsClosed())
            {
                expo.Remove();
            }
        }
    }
}