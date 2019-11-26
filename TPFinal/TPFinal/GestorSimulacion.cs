using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
namespace TPFinal
{
    class GestorSimulacion
    {

        DataTable tablaClientes = new DataTable();
        public DataTable tablaProximosClientes = new DataTable();
        GeneradorNumAleatorios GeneradorUnico = new GeneradorNumAleatorios();
        
        Cajero caj1 = new Cajero();
        Cajero caj2 = new Cajero();
        Cabina Cab = new Cabina();

        public int colaMax = 0;

        TimeSpan reloj = new TimeSpan();
        //////TimeSpan relojInicio = new TimeSpan(9, 0, 0);
        //////TimeSpan relojFinDia = new TimeSpan(19, 30, 0);
        TimeSpan proximaLlegadaCliente = new TimeSpan(0, 0, 0);
        TimeSpan proximaFinAtencionC1 = new TimeSpan(0, 0, 0);
        TimeSpan proximaFinAtencionC2 = new TimeSpan(0, 0, 0);
        TimeSpan proximaFinCabina = new TimeSpan(0, 0, 0);
        TimeSpan tiempoMinimo = new TimeSpan();
        TimeSpan seteoDeProximos = new TimeSpan(0, 0, 0);

        int tiempoASimular;
        TimeSpan tiempoDeComienzoDeIteraciones;
        int iteraciones;

        Boolean IteracionesCompletadas = false;

        
        DataTable vectorEstado = new DataTable();
        int contadorDeIteraciones;
        TimeSpan tiempoSimulado;
        TimeSpan relojAnterior;
        Cliente ultimoCliente;
        Cliente ningunCliente = new Cliente();

        int contadorDeIteracionesRealizadas = 0;
        int contadorDeClientes = 1;

        string tipoDeClienteUltimo;
        string clienteSiendoAtendidoEnC1 = "";
        string clienteSiendoAtendidoEnC2 = "";
        string clienteEscuchandoCabina = "";

        //List<Camion> listaCamiones = new List<Camion>();
        Boolean servicioRealizado = false;
        Boolean banderaCierreDePuertas = false;
        Boolean banderaAperturaDePuertas = false;

        int ultimoNumeroCliente;

        // colas

        Queue<Cliente> colaCajeros = new Queue<Cliente>();
      

        // bandera de simulacion de un dia
        string estadoSimulacion = "Llegada Cliente";

        //////////lista camiones atendidos
        ////////public List<Camion> listaCamionesAtendidos = new List<Camion>();
        ////////public List<Tuple<int, int>> resultados = new List<Tuple<int, int>>();

        // inicializar servidores
        public GestorSimulacion(int iteraciones, int tiempoASimular, TimeSpan tiempoInicioSimulacion)
        {
            caj1.setGenerador(ref GeneradorUnico);
            caj2.setGenerador(ref GeneradorUnico);

            caj1.setClienteSiendoAtendido(ningunCliente);
            caj2.setClienteSiendoAtendido(ningunCliente);

            this.iteraciones = iteraciones;
            this.tiempoASimular = tiempoASimular;
            this.tiempoDeComienzoDeIteraciones = tiempoInicioSimulacion;

            tablaProximosClientes.Columns.Add("Próximo Cliente");

            //vectorEstado.Columns.Add("Día");
            vectorEstado.Columns.Add("Reloj");
            vectorEstado.Columns.Add("Evento");
            vectorEstado.Columns.Add("Cliente");
            vectorEstado.Columns.Add("Aleatorio Tipo Cliente");
            vectorEstado.Columns.Add("Tipo Cliente");
            vectorEstado.Columns.Add("Próxima Llegada Cliente");
            vectorEstado.Columns.Add("Atención");
            vectorEstado.Columns.Add("Cola Atención");
            vectorEstado.Columns.Add("Cola Máxima");
            vectorEstado.Columns.Add("Cajero 1");
            vectorEstado.Columns.Add("Cliente Siendo Atendido Cajero 1");
            vectorEstado.Columns.Add("Estado Cajero 1");
            vectorEstado.Columns.Add("Próximo Fin AT. Cajero 1");
            vectorEstado.Columns.Add("Cajero 2");
            vectorEstado.Columns.Add("Cliente Siendo Atendido Cajero 2");
            vectorEstado.Columns.Add("Estado Cajero 2");
            vectorEstado.Columns.Add("Próximo Fin AT. Cajero 2");
        }

        public DataTable SimularVectorEstado()
        {
            //while (IteracionesCompletadas == false && tiempoASimular >= dia)
            //{
            //    resultados.Add(SimulacionDia(dia));
            //    int auxiliarContador = resultados.LastOrDefault().Item1;
            //    totalCamionesAtendidosGlobal += auxiliarContador;

            //    int auxiliarContadorNoAtendidos = resultados.LastOrDefault().Item2;
            //    totalCamionesNoAtendidosGlobal += auxiliarContadorNoAtendidos;
            //    dia++;
            //}
            //if (contadorDeIteracionesRealizadas < iteraciones)
            //{
            //    MessageBox.Show("No se completaron las iteraciones deseadas en los días simulados. Cantidad de iteraciones: " + contadorDeIteracionesRealizadas);
            //}
            //sumTiempoPredioCamion = calcularPromedio(listaCamionesAtendidos);

            return vectorEstado;
        }




        private TimeSpan minimo(TimeSpan a, TimeSpan b, TimeSpan c, TimeSpan d, TimeSpan e, TimeSpan f, TimeSpan g)
        {
            TimeSpan min = new TimeSpan(0, 0, 0);
            TimeSpan noGenerado = new TimeSpan(0, 0, 0);
            List<TimeSpan> numerosValidos = new List<TimeSpan>();


            if (a > noGenerado)
            {
                numerosValidos.Add(a);
            }
            if (b > noGenerado)
            {
                numerosValidos.Add(b);
            }
            if (c > noGenerado)
            {
                numerosValidos.Add(c);
            }
            if (d > noGenerado)
            {
                numerosValidos.Add(d);
            }
            if (e > noGenerado)
            {
                numerosValidos.Add(e);
            }
            if (f > noGenerado)
            {
                numerosValidos.Add(f);
            }
            if (g > noGenerado)
            {
                numerosValidos.Add(g);
            }

            for (int i = 0; i < numerosValidos.Count(); i++)
            {
                if (i == 0)
                {
                    min = numerosValidos[i];
                }
                else if (numerosValidos[i] < min)
                {
                    min = numerosValidos[i];

                }
            }

            return min;
        }
    }
}
