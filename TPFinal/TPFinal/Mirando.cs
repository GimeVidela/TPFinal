using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPFinal
{
    class Mirando
    {
        private GeneradorNumAleatorios generador = new GeneradorNumAleatorios();
        public string estado = "Libre";
        private Cliente clienteSiendoAtendido;

        public void setClienteMirando(Cliente clienteSiendoAtendido)
        {
            this.clienteSiendoAtendido = clienteSiendoAtendido;
        }
        public Cliente getClienteMirando()
        {
            return clienteSiendoAtendido;
        }
    }
}
