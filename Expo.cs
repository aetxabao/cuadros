using System;
using System.Threading;

namespace cuadros
{
    public class Expo
    {
        readonly object token = new object();
        Reloj reloj;
        public int Stock { get; set; }

        public int Metidos { get; set; }
        public int Sacados { get; set; }

        bool isClosed = false;
        public Expo(Reloj reloj)
        {
            this.reloj = reloj;
            this.Stock = 0;
            this.Metidos = 0;
            this.Sacados = 0;
        }

        public void Insert()
        {
            lock (token)
            {
                if (!isClosed)
                {
                    Stock++;
                    Metidos++;
                    int j = (int)(reloj.GetMilliseconds() / Reloj.MSxD);
                    Console.WriteLine("{0} --> metido {1}.", reloj.DayNumberToDate(j), Metidos);
                    Monitor.Pulse(token);
                }
            }
        }

        public void Remove()
        {
            lock (token)
            {
                Monitor.Wait(token);
                if (!isClosed)
                {
                    if (Stock > 0)
                    {
                        Stock--;
                        Sacados++;
                        int j = (int)(reloj.GetMilliseconds() / Reloj.MSxD);
                        Console.WriteLine("{0} <-- sacado {1}.", reloj.DayNumberToDate(j), Sacados);
                    }
                }
            }
        }
        public void Close()
        {
            lock (token)
            {
                isClosed = true;
                int j = (int)(reloj.GetMilliseconds() / Reloj.MSxD);
                Console.WriteLine("{0} cierre.", reloj.DayNumberToDate(j));
                Monitor.Pulse(token);
            }
        }
        public bool IsClosed()
        {
            lock (token)
            {
                return isClosed;
            }
        }
    }
}