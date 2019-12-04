using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPFinal
{
    class Cliente
    {

        int tipoCliente; 
        double aleatorio;
        double aleatorio2;
        double aleatorio3;
        GeneradorNumAleatorios generador = new GeneradorNumAleatorios();

        TimeSpan horaLlegada = new TimeSpan();
        TimeSpan horaSalida = new TimeSpan();
        //private GeneradorNumAleatorios generador;
        public string estadoActual = "";
        //public TimeSpan horaDelEstado;
        public int numeroCliente;


        private List<string> estados = new List<string>();
        private List<string> tiempos = new List<string>();

        public Tuple<List<string>, List<string>> conocerEstados()
        {
            return Tuple.Create(this.estados, this.tiempos);
        }

        public void agregarEstado(string estado, TimeSpan reloj)
        {
            //if (estado == "Cola Dársena" && estados[estados.Count - 1] == "Fin Atención Recepción")
            //{
            //    this.estados.Add("");
            //    this.tiempos.Add("");

            //    this.estados.Add("");
            //    this.tiempos.Add("");

            //}
            this.estados.Add(estado);
            this.tiempos.Add(Convert.ToString(reloj));
        }

        public void setGenerador(ref GeneradorNumAleatorios generador)
        {
            this.generador = generador;
           
        }
        public void setHoraLlegada(TimeSpan horaLlegadaCliente)
        {
            this.horaLlegada = horaLlegadaCliente;
        }
        public void setHoraSalida(TimeSpan horaSalidaCliente)
        {
            this.horaSalida = horaSalidaCliente;
        }

        public TimeSpan getHoraLlegada()
        {
            return horaLlegada;
        }
        public TimeSpan getHoraSalida()
        {
            return horaSalida;
        }

        public Cliente(int numeroCliente = 0)
        {
            this.numeroCliente = numeroCliente;

        }

        //public TimeSpan TioempoAdentro()
        //{
        //    TimeSpan tiempoAdentroDelComplejo;
        //    tiempoAdentroDelComplejo = horaSalida - horaLlegada;
        //    return tiempoAdentroDelComplejo;
        //}

        public int getTipoCliente()
        {
            return tipoCliente;
        }

        public double getTipoAleatorio()
        {
            return aleatorio;
        }

        public double getTipoAleatorio2()
        {
            return aleatorio2;
        }

        public double getTipoAleatorio3()
        {
            return aleatorio3;
        }

        public int calcularClienteA()
        {
            Thread.Sleep(15);

            this.aleatorio = Math.Round(generador.GenerarAleatorio(), 3);
            
            if (aleatorio < 0.2)
            {
                tipoCliente = 1; // CLiente que entra a mirar y no compra
            }
            else
            {
                tipoCliente = 2; //Cliente que que van a  atencion
            }
            return tipoCliente;
        }

        public int calcularClienteB()
        {
            this.aleatorio2 = Math.Round(generador.GenerarAleatorio(), 3);
            if (aleatorio2 < 0.40)
            {
                tipoCliente = 3; //Cliente que va directo a comprar
            }
            else
            {
                tipoCliente = 4; //Cliente que pasa a escuchar en Cabina
            }
            return tipoCliente;
        }

        public int calcularClienteC()
        {
            this.aleatorio3 = Math.Round(generador.GenerarAleatorio(), 3);
            if (aleatorio3 < 0.30)
            {
                tipoCliente = 5; //Cliente que vuelve a atencion para comprar
            }
            else
            {
                tipoCliente = 6;// Cliente que no compra
            }
            return tipoCliente;
        }
        public TimeSpan TiempoMirando()
        {
            TimeSpan mirando = new TimeSpan(0, 3, 0);
            return mirando;
        }
        public TimeSpan CalcularTiempoEnCola(TimeSpan inicioCola, TimeSpan finCola)
        {
            TimeSpan tiempoCola = new TimeSpan();
            tiempoCola = finCola - inicioCola;
            return tiempoCola;
        }
    }
}
