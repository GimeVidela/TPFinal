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
        Cliente cl = new Cliente();
        public int colaMax = 0;

        TimeSpan reloj = new TimeSpan(0, 0, 0);

        TimeSpan proximaLlegadaCliente = new TimeSpan(0, 0, 0);
        TimeSpan proximaFinAtencionC1 = new TimeSpan(0, 0, 0);
        TimeSpan proximaFinAtencionC2 = new TimeSpan(0, 0, 0);
        TimeSpan proximaFinCabina = new TimeSpan(0, 0, 0);
        TimeSpan tiempoMinimo = new TimeSpan();
        TimeSpan seteoDeProximos = new TimeSpan(0, 0, 0);

        TimeSpan tiempoASimular;
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

        string tipoDeClienteUltimoA = " ";
        string tipoDeClienteUltimoBC1  = " ";
        string tipoDeClienteUltimoBC2 = " ";
        string tipoDeClienteUltimoC = " ";
        string clienteSiendoAtendidoEnC1 = "";
        string clienteSiendoAtendidoEnC2 = "";
        string clienteEscuchandoCabina = "";

        public List<Cliente> listaClientes = new List<Cliente>();
        Boolean servicioRealizado = false;

        // colas

        Queue<Cliente> colaCajeros = new Queue<Cliente>();
        Queue<Cliente> colaCabina = new Queue<Cliente>();


        // bandera de simulacion de inicio de simulacion
        string estadoSimulacion = "Llegada Cliente";

        ////////public List<Tuple<int, int>> resultados = new List<Tuple<int, int>>();

        // inicializar servidores
        public GestorSimulacion(int iteraciones, TimeSpan tiempoASimular, TimeSpan tiempoInicioSimulacion)
        {
            caj1.setGenerador(ref GeneradorUnico);
            caj2.setGenerador(ref GeneradorUnico);

            caj1.setClienteSiendoAtendido(ningunCliente);
            caj2.setClienteSiendoAtendido(ningunCliente);
            Cab.setClienteSiendoAtendido(ningunCliente);

            this.iteraciones = iteraciones;
            this.tiempoASimular = tiempoASimular;
            this.tiempoDeComienzoDeIteraciones = tiempoInicioSimulacion;

            tablaProximosClientes.Columns.Add("Próximo Cliente");

            vectorEstado.Columns.Add("Reloj");
            vectorEstado.Columns.Add("Evento");
            vectorEstado.Columns.Add("Cliente");
            vectorEstado.Columns.Add("Aleatorio Tipo Cliente");
            vectorEstado.Columns.Add("Tipo Cliente Llegado");
            vectorEstado.Columns.Add("Próxima Llegada Cliente");
            vectorEstado.Columns.Add("Atención");
            vectorEstado.Columns.Add("Cola Atención");
            vectorEstado.Columns.Add("Cola Máxima");
            vectorEstado.Columns.Add("Cajero 1");
            vectorEstado.Columns.Add("Estado Cajero 1");
            vectorEstado.Columns.Add("Cliente Siendo Atendido Cajero 1");
            vectorEstado.Columns.Add("Aleatorio Tipo Cliente despues de Atención 1");
            vectorEstado.Columns.Add("Tipo Cliente despues de Atención 1");
            vectorEstado.Columns.Add("Próximo Fin AT. Cajero 1");
            vectorEstado.Columns.Add("Cajero 2");
            vectorEstado.Columns.Add("Estado Cajero 2");
            vectorEstado.Columns.Add("Cliente Siendo Atendido Cajero 2");
            vectorEstado.Columns.Add("Aleatorio Tipo Cliente despues de Atención 2 ");
            vectorEstado.Columns.Add("Tipo Cliente despues de Atención 2");
            vectorEstado.Columns.Add("Próximo Fin AT. Cajero 2");
            vectorEstado.Columns.Add("Cabinas");
            vectorEstado.Columns.Add("Cliente Escuchando en Cabina");
            vectorEstado.Columns.Add("Próximo Fin Escuchando en Cabina");
            vectorEstado.Columns.Add("Aleatorio Tipo Cliente despues de Cabina");
            vectorEstado.Columns.Add("Tipo Cliente despues de Cabina");
        }

        public DataTable SimularVectorEstado()
        {
            TimeSpan ini = new TimeSpan(0, 0, 0);
            if (tiempoDeComienzoDeIteraciones == ini)
            {
                inicioSim();
            }
            while (IteracionesCompletadas == false && tiempoASimular >= tiempoSimulado)
            {
                Simulacion();
            }
            if (contadorDeIteracionesRealizadas < iteraciones-1)
            {
                MessageBox.Show("No se completaron las iteraciones deseadas en el tiempo simulado. Cantidad de iteraciones: " + contadorDeIteracionesRealizadas);
            }
            //sumTiempoPredioCamion = calcularPromedio(listaCamionesAtendidos);

            return vectorEstado;
        }

        public void inicioSim()
        {

            vectorEstado.Rows.Add(
                                    "00:00:00"
                                    , "Inicio"
                                    , 0
                                    , ""
                                    , ""
                                    , ""
                                    , null
                                    , "-"
                                    , 0
                                    , null
                                    , "Libre"
                                    , ""
                                    , ""
                                    , ""
                                    , ""
                                    , null
                                    , "Libre"
                                    , ""
                                    , ""
                                    , ""
                                    , ""
                                    , null
                                    , ""
                                    , ""
                                    , ""
                                    , ""
                                    );

        }

        public int Simulacion()
        {
            // determinacion de eventos en el sistema
            // Cuando llega un cliente y es para atencion se suma en cola
            servicioRealizado = false;
            relojAnterior = reloj;

            if (estadoSimulacion == "Llegada Cliente")
            {
                proximaLlegadaCliente = reloj + llegadaCliente(2.0);
                //reloj = proximaLlegadaCliente;
            }

            //cuando un cliente esta en cola y un cajero esta Libre lo toma y el cajero pasa a estar ocupado
            if (colaCajeros.Count != 0 && caj1.estado == "Libre")
            {
                proximaFinAtencionC1 = reloj + caj1.TiempoAtencionCajero();
                caj1.setClienteSiendoAtendido(colaCajeros.Dequeue());
                caj1.getClienteSiendoAtendido().agregarEstado("Atención Cajero 1", reloj);
                ultimoCliente = caj1.getClienteSiendoAtendido();
                caj1.estado = "Ocupado";
                if (caj1.getClienteSiendoAtendido().calcularClienteB() == 4)
                {
                    //calcular tipo cliente si es 3 o 4

                    tipoDeClienteUltimoBC1 = "Para Escuchar";
                    //colaCabina.Enqueue(caj1.getClienteSiendoAtendido());
                    //colaCabina.Last().setGenerador(ref GeneradorUnico);
                }
                else
                {
                    tipoDeClienteUltimoBC1 = "Compra Definitiva";
                }
            }
            if (colaCajeros.Count != 0 && caj2.estado == "Libre")
            {
                proximaFinAtencionC2 = reloj + caj2.TiempoAtencionCajero();
                caj2.setClienteSiendoAtendido(colaCajeros.Dequeue());
                caj2.getClienteSiendoAtendido().agregarEstado("Atención Cajero 2", reloj);
                ultimoCliente = caj2.getClienteSiendoAtendido();
                caj2.estado = "Ocupado";
                if (caj2.getClienteSiendoAtendido().calcularClienteB() == 4)
                {
                    //calcular tipo cliente si es 3 o 4

                    tipoDeClienteUltimoBC2 = "Para Escuchar";
                    //colaCabina.Enqueue(caj2.getClienteSiendoAtendido());
                    //colaCabina.Last().setGenerador(ref GeneradorUnico);
                }
                else
                {
                    tipoDeClienteUltimoBC2 = "Compra Definitiva";
                }
            }

            //si hya clientes en cola de cabina se calcula l proximo fin de cabina
            if (colaCabina.Count != 0)
            {
                proximaFinCabina = reloj + Cab.TiempoEscuchandoCabina(4, 1);
                Cab.setClienteSiendoAtendido(colaCabina.Dequeue());
                Cab.getClienteSiendoAtendido().agregarEstado("Escuchando CD en Cabina", reloj);
                ultimoCliente = Cab.getClienteSiendoAtendido();
                if (ultimoCliente.calcularClienteC() == 5)
                {
                    colaCajeros.Enqueue(ultimoCliente);
                    colaCajeros.Last().setGenerador(ref GeneradorUnico);
                    ultimoCliente = colaCajeros.Last();
                    //proximaFinCabina = seteoDeProximos;
                    //servicioRealizado = true;
                    Cab.setClienteSiendoAtendido(ningunCliente);
                    tipoDeClienteUltimoC = "Compra";
                }
                else
                {
                    tipoDeClienteUltimoC = "No Compra";
                }
            }

        
            if (colaMax < colaCajeros.Count)
            {
                colaMax = colaCajeros.Count;
            }

            generarVectorEstado();
            // se calcula tiempo minimo entre los proximos eventos
            tiempoMinimo = minimo(proximaFinAtencionC1, proximaFinAtencionC2, proximaFinCabina, proximaLlegadaCliente);

            if (tiempoMinimo == proximaLlegadaCliente && servicioRealizado == false)
            {
                reloj = proximaLlegadaCliente;
                estadoSimulacion = "Llegada Cliente";
                servicioRealizado = true;
                //estadoSimulacion = "Llegada Cliente";
                ultimoCliente = new Cliente(contadorDeClientes);
                listaClientes.Add(ultimoCliente);

                contadorDeClientes++;
                proximaLlegadaCliente = seteoDeProximos;
                if (ultimoCliente.calcularClienteA() == 2)
                {
                    colaCajeros.Enqueue(ultimoCliente);
                    colaCajeros.Last().setGenerador(ref GeneradorUnico);

                    ultimoCliente = colaCajeros.Last();
                    colaCajeros.Last().agregarEstado("En Cola Atención", reloj);
                    tipoDeClienteUltimoA = "Para Atención";
                }
                else
                {
                    tipoDeClienteUltimoA = "Mirando";
                }
                proximaLlegadaCliente = seteoDeProximos;
            }

            if (tiempoMinimo == proximaFinAtencionC1 && servicioRealizado == false)
            {

                reloj = proximaFinAtencionC1;
                estadoSimulacion = "Fin Atención Cajero 1";
                //caj1.estado = "Libre";
                //proximaFinAtencionC1 = seteoDeProximos;
                caj1.getClienteSiendoAtendido().agregarEstado("Fin Atención Cajero 1", reloj);
                servicioRealizado = true;
                //caj1.setClienteSiendoAtendido(ningunCliente);
                //proximaFinAtencionC1 = reloj + caj1.TiempoAtencionCajero();
                proximaFinAtencionC1 = seteoDeProximos;
                caj1.estado = "Libre";
                if (tipoDeClienteUltimoBC1 == "Para Escuchar")
                {
                    colaCabina.Enqueue(caj1.getClienteSiendoAtendido());
                    colaCabina.Last().setGenerador(ref GeneradorUnico);
                }
            }
            if (tiempoMinimo == proximaFinAtencionC2  && servicioRealizado == false)
            {

                reloj = proximaFinAtencionC2;
                estadoSimulacion = "Fin Atención Cajero 2";
                //caj2.estado = "Libre";
                //proximaFinAtencionC2 = seteoDeProximos;
                caj2.getClienteSiendoAtendido().agregarEstado("Fin Atención Cajero 2", reloj);
                servicioRealizado = true;
                //caj2.setClienteSiendoAtendido(ningunCliente);
                //proximaFinAtencionC2 = reloj + caj2.TiempoAtencionCajero();
                proximaFinAtencionC2 = seteoDeProximos;
                caj2.estado = "Libre";
                if (tipoDeClienteUltimoBC2 == "Para Escuchar")
                {
                    colaCabina.Enqueue(caj2.getClienteSiendoAtendido());
                    colaCabina.Last().setGenerador(ref GeneradorUnico);
                }
            }

            if (tiempoMinimo == proximaFinCabina && servicioRealizado == false)
            {

                reloj = proximaFinCabina;
                estadoSimulacion = "Fin Escuchando en Cabina";
                Cab.getClienteSiendoAtendido().agregarEstado("Fin Escuchando en Cabina", reloj);
                servicioRealizado = true;
                ultimoCliente = Cab.getClienteSiendoAtendido();
                proximaFinCabina = seteoDeProximos;
            }

            tiempoSimulado = tiempoSimulado + reloj - relojAnterior;

            if (contadorDeIteracionesRealizadas == (iteraciones -1))
            {
                IteracionesCompletadas = true;
            }
            return colaMax;
        }

        private TimeSpan llegadaCliente(double lambda)
        {
            //Distribucion Exponencial Negativa
            GeneradorNumAleatorios gen = new GeneradorNumAleatorios();
            double aleatorio = gen.GenerarAleatorio();

            double tiempoLlegada = ((-lambda) * Math.Log(1 - aleatorio));

            return GeneradorUnico.convertirSegundosHorasMinutos(tiempoLlegada);
        }

        //calcula tiempo minimo entre los eventos
        private TimeSpan minimo(TimeSpan a, TimeSpan b, TimeSpan c, TimeSpan d)
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

        //se genera el vectorestado
        private void generarVectorEstado()
        {
            if (tiempoDeComienzoDeIteraciones < tiempoSimulado)
            {

                if (caj1.getClienteSiendoAtendido().numeroCliente != 0)
                {
                    clienteSiendoAtendidoEnC1 = Convert.ToString(caj1.getClienteSiendoAtendido().numeroCliente);
                }
                else
                {
                    clienteSiendoAtendidoEnC1 = "";
                }
                if (caj2.getClienteSiendoAtendido().numeroCliente != 0)
                {
                    clienteSiendoAtendidoEnC2 = Convert.ToString(caj2.getClienteSiendoAtendido().numeroCliente);
                }
                else
                {
                    clienteSiendoAtendidoEnC2 = "";
                }
                if (Cab.getClienteSiendoAtendido().numeroCliente != 0)
                {
                    clienteEscuchandoCabina = Convert.ToString(Cab.getClienteSiendoAtendido().numeroCliente);
                }
                else
                {
                    clienteEscuchandoCabina = "";
                }
                if (estadoSimulacion == "Fin Atención Cajero 1" || estadoSimulacion == "Fin Atención Cajero 2" || estadoSimulacion == "Fin Escuchando en Cabina")
                {
                    
                    vectorEstado.Rows.Add(
                                           reloj
                                           , estadoSimulacion
                                           , ultimoCliente.numeroCliente
                                           , ""
                                           , ""
                                           , proximaLlegadaCliente
                                           , null
                                           , ClientesEnCola(colaCajeros)
                                           , colaMax
                                           , null
                                           , caj1.estado
                                           , clienteSiendoAtendidoEnC1
                                           , caj1.getClienteSiendoAtendido().getTipoAleatorio2()
                                           , tipoDeClienteUltimoBC1                                          
                                           , proximaFinAtencionC1
                                           , null
                                           , caj2.estado
                                           , clienteSiendoAtendidoEnC2
                                           , caj2.getClienteSiendoAtendido().getTipoAleatorio2()
                                           , tipoDeClienteUltimoBC2
                                           , proximaFinAtencionC2
                                           , null
                                           , clienteEscuchandoCabina
                                           , proximaFinCabina
                                           , Cab.getClienteSiendoAtendido().getTipoAleatorio3()
                                           , tipoDeClienteUltimoC
                                           );
                }
                else
                {
                    vectorEstado.Rows.Add(
                                            reloj
                                            , estadoSimulacion
                                            , ultimoCliente.numeroCliente
                                            , ultimoCliente.getTipoAleatorio()
                                            , tipoDeClienteUltimoA
                                            , proximaLlegadaCliente
                                            , null
                                            , ClientesEnCola(colaCajeros)
                                            , colaMax
                                            ,null
                                           , caj1.estado
                                           , clienteSiendoAtendidoEnC1
                                           , caj1.getClienteSiendoAtendido().getTipoAleatorio2()
                                           , tipoDeClienteUltimoBC1
                                           , proximaFinAtencionC1
                                           , null
                                           , caj2.estado
                                           , clienteSiendoAtendidoEnC2
                                           , caj2.getClienteSiendoAtendido().getTipoAleatorio2()
                                           , tipoDeClienteUltimoBC2
                                           , proximaFinAtencionC2
                                           , null
                                           , clienteEscuchandoCabina
                                           , proximaFinCabina
                                           , Cab.getClienteSiendoAtendido().getTipoAleatorio3()
                                           , tipoDeClienteUltimoC
                                            );
                }
                contadorDeIteracionesRealizadas++;
            }
        }

        //muestra lista de clientes en cola
        private string ClientesEnCola(Queue<Cliente> colaClientes)
        {
            string cadena = "";
            if (colaClientes.Count == 0)
            {
                return "-";
            }
            else
            {
                foreach (Cliente i in colaClientes)
                {
                    cadena = cadena + i.numeroCliente + "_";
                }
                return cadena;
            }
        }

        public DataTable cargarTablaClientes(List<Cliente> lista)
        {
            //tablaClientes.Columns.Add("Estado de Cliente");
            //tablaClientes.Columns.Add("Reloj");
            foreach (Cliente i in lista)
            {
                tablaClientes.Columns.Add("Estado de Cliente " + i.numeroCliente);
                tablaClientes.Columns.Add("Reloj "+ i.numeroCliente);
                tablaClientes.Rows.Add("Estado de Cliente " + i.numeroCliente, "Reloj");
                for (int j = 0; j < i.conocerEstados().Item1.Count; j++)
                {
                    tablaClientes.Rows.Add(i.conocerEstados().Item1[j], i.conocerEstados().Item2[j]);
                }
                tablaClientes.Rows.Add("", "");
            }
            return tablaClientes;
        }
    }
}
