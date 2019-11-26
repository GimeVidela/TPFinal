using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPFinal
{
    class Cliente
    {

        int tipoCliente; // 1 - Mira y se va 2- Compra cd directamente 3 Escucha CD y Compra 4 Escucha CD y se va
        double aleatorio;
        double aleatorio2;
        double aleatorio3;

        TimeSpan horaLlegada;
        TimeSpan horaSalida = new TimeSpan();
        private GeneradorNumAleatorios generador;
        public string estadoActual = "";
        //public TimeSpan horaDelEstado;
        public int numeroCliente;
        //////private List<string> estados = new List<string>();
        //////private List<string> tiempos = new List<string>();

        //////public Tuple<List<string>, List<string>> conocerEstados()
        //////{
        //////    return Tuple.Create(this.estados, this.tiempos);
        //////}

        //////public void agregarEstado(string estado, TimeSpan reloj)
        //////{
        //////    if (estado == "Cola Dársena" && estados[estados.Count - 1] == "Fin Atención Recepción")
        //////    {
        //////        this.estados.Add("");
        //////        this.tiempos.Add("");

        //////        this.estados.Add("");
        //////        this.tiempos.Add("");

        //////    }
        //////    this.estados.Add(estado);
        //////    this.tiempos.Add(Convert.ToString(reloj));
        //////}

        public void setGenerador(ref GeneradorNumAleatorios generador)
        {
            this.generador = generador;
            this.aleatorio = calcularTipoCliente();
           
        }
        public void setHoraLlegada(TimeSpan horaLlegadaCamion)
        {
            this.horaLlegada = horaLlegadaCamion;
        }


        public Cliente(int numeroCliente = 0)
        {
            this.numeroCliente = numeroCliente;

        }


        public void setHoraSalida(TimeSpan hora)
        {
            horaSalida = hora;
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

        private int calcularTipoCliente()
        {
            // 1 - Mira y se va 2- Compra cd directamente 3 Escucha CD y Compra 4 Escucha CD y se va

            Double aleatorio = Math.Round(generador.GenerarAleatorio(), 3);
            
            if (aleatorio < 0.20)
            {
                tipoCliente = 1; // CLiente que entra a mirar y no compra
            }
            else
            {
                tipoCliente = 2; //Cliente que que van a  atencion
                Double aleatorio2 = Math.Round(generador.GenerarAleatorio(), 3);
                if (aleatorio2 < 0.40)
                {
                    tipoCliente = 3; //Cliente que va directo a comprar
                }
                else
                {
                    tipoCliente = 4; //Cliente que pasa a escuchar en Cabina
                    Double aleatorio3 = Math.Round(generador.GenerarAleatorio(), 3);
                    if (aleatorio3 < 0.30)
                    {
                        tipoCliente = 5; //Cliente que vuelve a atencion para comprar
                    }
                    else
                    {
                        tipoCliente = 6;// Cliente que no compra
                    }
                }
            }
            return tipoCliente;
        }
    }
}
