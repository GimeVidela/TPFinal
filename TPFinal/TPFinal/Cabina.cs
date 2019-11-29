using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPFinal
{
    class Cabina
    {
        private GeneradorNumAleatorios generador = new GeneradorNumAleatorios();
        public string estado = "Libre";
        private Cliente clienteSiendoAtendido;

        public void setClienteSiendoAtendido(Cliente clienteSiendoAtendido)
        {
            this.clienteSiendoAtendido = clienteSiendoAtendido;
        }
        public Cliente getClienteSiendoAtendido()
        {
            return clienteSiendoAtendido;
        }

        public TimeSpan TiempoEscuchandoCabina(double media, double varianza)
        {
            //Distribucion Normal
            double aleatorio1 = generador.GenerarAleatorio();
            double aleatorio2 = generador.GenerarAleatorio();
            while (aleatorio1 == 0 || aleatorio2 == 0)
            {
                aleatorio1 = generador.GenerarAleatorio();
                aleatorio2 = generador.GenerarAleatorio();
            }

            double tiempo = media + varianza * Math.Sqrt(-2 * Math.Log(aleatorio1)) * (Math.Sin(2 * Math.PI * aleatorio2));

            return generador.convertirSegundosHorasMinutos(tiempo);
        }
    }
}
