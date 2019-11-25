using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPFinal
{
    class GeneradorNumAleatorios
    {
        double primerValor, x1, x2;
        Random aleatorio = new Random();
        public double GenerarAleatorio()
        {

            while (primerValor == x2)
            {
                primerValor = aleatorio.Next(0, 9998);
                x1 = primerValor;
            }

            x2 = (67 * x1 + 71) % 9999;
            x1 = x2;
            return x2 / 10000;


        }

        public TimeSpan convertirSegundosHorasMinutos(double minutos)
        {
            int segundos = Convert.ToInt32(minutos * 60);
            int hor = 0;
            int min = 0;
            int seg = 0;
            hor = (segundos / 3600);
            min = ((segundos - hor * 3600) / 60);
            seg = segundos - (hor * 3600 + min * 60);
            return TimeSpan.Parse(hor + ":" + min + ":" + seg);
        }
        public TimeSpan convertirSegundosHorasMinutosPromedio(double minutos)
        {

            int segundos = Convert.ToInt32(minutos);
            int hor = 0;
            int min = 0;
            int seg = 0;
            hor = (segundos / 3600);
            min = ((segundos - hor * 3600) / 60);
            seg = segundos - (hor * 3600 + min * 60);

            return TimeSpan.Parse(hor + ":" + min + ":" + seg);
        }

        public Double convertirEnDouble(TimeSpan tiempo)
        {
            return tiempo.TotalSeconds;
        }
    }
}
