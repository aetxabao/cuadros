using System;
using System.Threading;

namespace cuadros
{
    public class Expo
    {
        readonly object token = new object();
        Reloj reloj;
        Thread thread;
        public int Stock { get; set; }
        public int Capacidad { get; set; }

        public int Metidos { get; set; }
        public int Sacados { get; set; }

        bool isClosed = false;
        int tEsperaMeter = 0;
        int tEsperaSacar = 0;
        public Expo(Reloj reloj, int stock, int capacidad)
        {
            this.reloj = reloj;
            this.Stock = stock;
            this.Capacidad = capacidad;
            this.Metidos = 0;
            this.Sacados = 0;
        }

        public void Start()
        {
            //Console.WriteLine("E inicio.");
            this.thread = new Thread(() => this.Agenda());
            this.thread.Start();
        }

        public void Finish()
        {
            thread.Join();
            this.Close();
            //Console.WriteLine("E fin.");
        }

        void Agenda()
        {
            Thread.Sleep(365 * Reloj.MSxD);
        }
        public void Insert()
        {
            long t1, t2;
            lock (token)
            {
                while (!isClosed)
                {
                    if (Stock < Capacidad)
                    {
                        Stock++;
                        Metidos++;
                        int j = (int)(reloj.GetMilliseconds() / Reloj.MSxD);
                        Console.WriteLine("{0} --> metido {1}.", reloj.DayNumberToDate(j), Metidos);
                        Monitor.Pulse(token);
                        return;
                    }
                    else
                    {
                        t1 = reloj.GetMilliseconds();
                        int j = (int)(t1 / Reloj.MSxD);
                        Console.WriteLine("{0} ../ espera P.", reloj.DayNumberToDate(j));
                        Monitor.Wait(token);
                        t2 = reloj.GetMilliseconds();
                        tEsperaMeter += (int)((t2 - t1) / Reloj.MSxD);
                    }
                }
            }
        }

        public void Remove()
        {
            long t1, t2;
            lock (token)
            {
                while (!isClosed)
                {
                    if (Stock > 0)
                    {
                        Stock--;
                        Sacados++;
                        int j = (int)(reloj.GetMilliseconds() / Reloj.MSxD);
                        Console.WriteLine("{0} <-- sacado {1}.", reloj.DayNumberToDate(j), Sacados);
                        Monitor.Pulse(token);
                        return;
                    }
                    else
                    {
                        t1 = reloj.GetMilliseconds();
                        int j = (int)(t1 / Reloj.MSxD);
                        Console.WriteLine("{0} /.. espera M.", reloj.DayNumberToDate(j));
                        Monitor.Wait(token);
                        t2 = reloj.GetMilliseconds();
                        tEsperaSacar += (int)((t2 - t1) / Reloj.MSxD);
                    }
                }
            }
        }

        void Close()
        {
            lock (token)
            {
                isClosed = true;
                int j = (int)(reloj.GetMilliseconds() / Reloj.MSxD);
                Console.WriteLine("{0} cierre.", reloj.DayNumberToDate(j));
                // AVISO A TODOS LOS HILOS EN LA COLA DE ESPERA !!!
                Monitor.PulseAll(token);
            }
        }
        public bool IsClosed()
        {
            lock (token)
            {
                return isClosed;
            }
        }

        public int GetTiempoEsperaMeter()
        {
            lock (token)
            {
                return tEsperaMeter;
            }
        }

        public int GetTiempoEsperaSacar()
        {
            lock (token)
            {
                return tEsperaSacar;
            }
        }
    }
}