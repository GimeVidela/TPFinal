using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPFinal
{
    class Cajero
    {
        GeneradorNumAleatorios generador = new GeneradorNumAleatorios();
        public string estado = "Libre";
        private Cliente clienteSiendoAtendido;

        public void setGenerador(ref GeneradorNumAleatorios generador)
        {
            this.generador = generador;
        }

        public void setClienteSiendoAtendido(Cliente clienteSiendoAtendido)
        {
            this.clienteSiendoAtendido = clienteSiendoAtendido;
        }
        public Cliente getClienteSiendoAtendido()
        {
            return clienteSiendoAtendido;
        }

        public TimeSpan TiempoAtencionCajero()
        {
            TimeSpan atencion = new TimeSpan(0, 2, 0);
            return atencion;
        }
    }
}
