using System;

namespace cuadros
{
    public class Reloj
    {
        public static int MSxD = 10;
        long inicio;
        public Reloj()
        {
            inicio = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        public long GetMilliseconds()
        {
            long t = DateTimeOffset.Now.ToUnixTimeMilliseconds() - inicio;
            return t;
        }

        public string DayNumberToDate(int dayOfYear)
        {
            int year = DateTime.Now.Year;
            DateTime theDate = new DateTime(year, 1, 1).AddDays(dayOfYear - 1);
            return theDate.ToString("dd/MM/yyyy");
        }
    }
}