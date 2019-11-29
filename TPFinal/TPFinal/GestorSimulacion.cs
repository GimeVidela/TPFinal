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
        string tipoDeClienteUltimoB = " ";
        string tipoDeClienteUltimoC = " ";
        string clienteSiendoAtendidoEnC1 = "";
        string clienteSiendoAtendidoEnC2 = "";
        string clienteEscuchandoCabina = "";

        //List<Camion> listaCamiones = new List<Camion>();
        Boolean servicioRealizado = false;

        int ultimoNumeroCliente;

        // colas

        Queue<Cliente> colaCajeros = new Queue<Cliente>();


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
            vectorEstado.Columns.Add("Próximo Cliente");
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
                //resultados.Add(SimulacionDia(dia));
                //int auxiliarContador = resultados.LastOrDefault().Item1;
                //totalCamionesAtendidosGlobal += auxiliarContador;

                //int auxiliarContadorNoAtendidos = resultados.LastOrDefault().Item2;
                //totalCamionesNoAtendidosGlobal += auxiliarContadorNoAtendidos;
                //dia++;


            }
            if (contadorDeIteracionesRealizadas < iteraciones)
            {
                MessageBox.Show("No se completaron las iteraciones deseadas en los días simulados. Cantidad de iteraciones: " + contadorDeIteracionesRealizadas);
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
                                    , 1
                                    , ""
                                    , ""
                                    , ""
                                    , null
                                    , 0
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
           
                proximaLlegadaCliente = seteoDeProximos;

                servicioRealizado = false;
                relojAnterior = reloj;

                if (estadoSimulacion == "Llegada Cliente")
                {
                    proximaLlegadaCliente = reloj + llegadaCliente(2.0);
                }

                if (estadoSimulacion == "Fin Atención Cajero 1")
                {
                    proximaFinAtencionC1 = reloj + caj1.TiempoAtencionCajero();
                    caj1.estado = "Ocupado";
                }

                if (estadoSimulacion == "Fin Atención Cajero 2")
                {
                    proximaFinAtencionC2 = reloj + caj2.TiempoAtencionCajero();
                    caj2.estado = "Ocupado";
                }

                if (estadoSimulacion == "Fin Escuchando en Cabina")
                {
                    proximaFinCabina = reloj + Cab.TiempoEscuchandoCabina(4, 1);
                }

                //generarVectorEstado();

                tiempoMinimo = minimo(proximaFinAtencionC1, proximaFinAtencionC2, proximaFinCabina, proximaLlegadaCliente);

                if (tiempoMinimo == seteoDeProximos)
                {
                    //servicioRealizado = true;
                    estadoSimulacion = "Llegada Cliente";

                }

                if (tiempoMinimo == proximaLlegadaCliente && servicioRealizado == false)
                {
                    reloj = proximaLlegadaCliente;
                    estadoSimulacion = "Llegada Cliente";
                    cl = new Cliente(contadorDeClientes);
                    if (cl.calcularClienteA() == 2)
                    {
                        colaCajeros.Enqueue(cl);
                        colaCajeros.Last().setGenerador(ref GeneradorUnico);

                        contadorDeClientes++;

                        ultimoCliente = colaCajeros.Last();

                        proximaLlegadaCliente = seteoDeProximos;

                        //servicioRealizado = true;

                        colaCajeros.Last().agregarEstado("En Cola Atención", reloj);
                        tipoDeClienteUltimoA = "Para Atención";

                        if (colaCajeros.Count() > 0)
                        {
                            if (caj1.estado == "Libre")
                            {
                                caj1.estado = "Ocupado";
                                caj1.setClienteSiendoAtendido(colaCajeros.Dequeue());
                                proximaFinAtencionC1 = reloj + caj1.TiempoAtencionCajero();

                            }
                            else
                            {
                                if (caj2.estado == "Libre")
                                {
                                    caj2.estado = "Ocupado";
                                    caj2.setClienteSiendoAtendido(colaCajeros.Dequeue());
                                    proximaFinAtencionC2 = reloj + caj2.TiempoAtencionCajero();
                                }
                            }
                        }
                    }
                    else
                    {
                        tipoDeClienteUltimoA = "Mirando";
                    }

                }
                if (tiempoMinimo == proximaFinAtencionC1 && servicioRealizado == false)
                {

                    reloj = proximaFinAtencionC1;
                    estadoSimulacion = "Fin Atención Cajero 1";
                    caj1.estado = "Libre";
                    proximaFinAtencionC1 = seteoDeProximos;
                    caj1.getClienteSiendoAtendido().agregarEstado("Fin Atención Cajero 1", reloj);
                    servicioRealizado = true;
                    if (caj1.getClienteSiendoAtendido().calcularClienteB() == 4)
                    {
                        //calcular tipo cliente si es 3 o 4

                        tipoDeClienteUltimoB = "Para Escuchar";
                        proximaFinCabina = reloj + Cab.TiempoEscuchandoCabina(4, 1);
                    }
                    else
                    {
                        tipoDeClienteUltimoB = "Compra Definitiva";
                    }
                    caj1.setClienteSiendoAtendido(ningunCliente);
                }

                if (tiempoMinimo == proximaFinAtencionC2 && servicioRealizado == false)
                {

                    reloj = proximaFinAtencionC2;
                    estadoSimulacion = "Fin Atención Cajero 1";
                    caj2.estado = "Libre";
                    proximaFinAtencionC2 = seteoDeProximos;
                    caj2.getClienteSiendoAtendido().agregarEstado("Fin Atención Cajero 2", reloj);
                    servicioRealizado = true;
                    if (caj2.getClienteSiendoAtendido().calcularClienteB() == 4)
                    {
                        //calcular tipo cliente si es 3 o 4
                        tipoDeClienteUltimoB = "Para Escuchar";
                        proximaFinCabina = reloj + Cab.TiempoEscuchandoCabina(4, 1);
                    }
                    else
                    {
                        tipoDeClienteUltimoB = "Compra Definitiva";
                    }
                    caj2.setClienteSiendoAtendido(ningunCliente);
                }

                if (tiempoMinimo == proximaFinCabina && servicioRealizado == false)
                {

                    reloj = proximaFinCabina;

                    estadoSimulacion = "Fin Escuchando en Cabina";
                    Cab.getClienteSiendoAtendido().agregarEstado("Fin Escuchando en Cabina", reloj);

                    if (ultimoCliente.calcularClienteC() == 5)
                    {
                        colaCajeros.Enqueue(ultimoCliente);
                        colaCajeros.Last().setGenerador(ref GeneradorUnico);
                        ultimoCliente = colaCajeros.Last();
                        proximaFinCabina = seteoDeProximos;
                        servicioRealizado = true;
                        Cab.setClienteSiendoAtendido(ningunCliente);
                        tipoDeClienteUltimoC = "Escucha y Compra";
                    }
                    else
                    {
                        tipoDeClienteUltimoC = "Escucha y No Compra";
                    }

                }

                tiempoSimulado = tiempoSimulado + reloj - relojAnterior;

                if (contadorDeIteracionesRealizadas == iteraciones)
                {

                    IteracionesCompletadas = true;

                }

                if (colaMax < colaCajeros.Count)
                {
                    colaMax = colaCajeros.Count;
                }

                generarVectorEstado();
            
            return colaMax;
        }


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

                if (estadoSimulacion == "Fin Atención Cajero 1" || estadoSimulacion == "Fin Atención Cajero 2" || estadoSimulacion == "Fin Escuchando en Cabina")
                {
                    if (estadoSimulacion == "Fin Atención Cajero 1")
                    {
                        vectorEstado.Rows.Add(
                                           reloj
                                           , estadoSimulacion
                                           , caj1.getClienteSiendoAtendido().numeroCliente
                                           , ""
                                           , ""
                                           , ""
                                           , proximaLlegadaCliente
                                           , null
                                           , ClientesEnCola(colaCajeros)
                                           , colaMax
                                           , null
                                           , caj1.estado
                                           , clienteSiendoAtendidoEnC1
                                           , ultimoCliente.getTipoAleatorio2()
                                           , tipoDeClienteUltimoB
                                           , proximaFinAtencionC1
                                           , null
                                           , caj2.estado
                                           , clienteSiendoAtendidoEnC2
                                           , ultimoCliente.getTipoAleatorio2()
                                           , tipoDeClienteUltimoB
                                           , proximaFinAtencionC2
                                           , null
                                           , clienteEscuchandoCabina
                                           , proximaFinCabina
                                           , ultimoCliente.getTipoAleatorio3()
                                           , tipoDeClienteUltimoC
                                           );
                    }
                    if (estadoSimulacion == "Fin Escuchando en Cabina")
                    {
                        vectorEstado.Rows.Add(
                                           reloj
                                           , estadoSimulacion
                                           , Cab.getClienteSiendoAtendido().numeroCliente
                                           , ""
                                           , ""
                                           , ""
                                           , proximaLlegadaCliente
                                           , null
                                           , ClientesEnCola(colaCajeros)
                                           , colaMax
                                           , null
                                           , caj1.estado
                                           , clienteSiendoAtendidoEnC1
                                           , ultimoCliente.getTipoAleatorio2()
                                           , tipoDeClienteUltimoB
                                           , proximaFinAtencionC1
                                           , null
                                           , caj2.estado
                                           , clienteSiendoAtendidoEnC2
                                           , ultimoCliente.getTipoAleatorio2()
                                           , tipoDeClienteUltimoB
                                           , proximaFinAtencionC2
                                           , null
                                           , clienteEscuchandoCabina
                                           , proximaFinCabina
                                           , ultimoCliente.getTipoAleatorio3()
                                           , tipoDeClienteUltimoC
                                           );
                    }
                    if (estadoSimulacion == "Fin Atención Cajero 2")
                    {
                        vectorEstado.Rows.Add(
                                           reloj
                                           , estadoSimulacion
                                           , caj2.getClienteSiendoAtendido().numeroCliente
                                           , ""
                                           , ""
                                           , ""
                                           , proximaLlegadaCliente
                                           , null
                                           , ClientesEnCola(colaCajeros)
                                           , colaMax
                                           , null
                                           , caj1.estado
                                           , clienteSiendoAtendidoEnC1
                                           , ultimoCliente.getTipoAleatorio2()
                                           , tipoDeClienteUltimoB
                                           , proximaFinAtencionC1
                                           , null
                                           , caj2.estado
                                           , clienteSiendoAtendidoEnC2
                                           , ultimoCliente.getTipoAleatorio2()
                                           , tipoDeClienteUltimoB
                                           , proximaFinAtencionC2
                                           , null
                                           , clienteEscuchandoCabina
                                           , proximaFinCabina
                                           , ultimoCliente.getTipoAleatorio3()
                                           , tipoDeClienteUltimoC
                                           );
                    }

                }
                else
                {
                    vectorEstado.Rows.Add(
                                            reloj
                                            , estadoSimulacion
                                            , ultimoCliente.numeroCliente
                                            , ultimoCliente.numeroCliente + 1
                                            , ultimoCliente.getTipoAleatorio()
                                            , tipoDeClienteUltimoA
                                            , proximaLlegadaCliente
                                            , null
                                            , ClientesEnCola(colaCajeros)
                                            , colaMax
                                            , null
                                            , caj1.estado
                                            , clienteSiendoAtendidoEnC1
                                            , ultimoCliente.getTipoAleatorio2()
                                            , tipoDeClienteUltimoB
                                            , proximaFinAtencionC1
                                            , null
                                            , caj2.estado
                                            , clienteSiendoAtendidoEnC2
                                            , ultimoCliente.getTipoAleatorio2()
                                            , tipoDeClienteUltimoB
                                            , proximaFinAtencionC2
                                            , null
                                            , clienteEscuchandoCabina
                                            , proximaFinCabina
                                            , ultimoCliente.getTipoAleatorio3()
                                            , tipoDeClienteUltimoC
                                            );
                }
                contadorDeIteracionesRealizadas++;
                //if (tablaProximosCamiones.Rows.Count != 0)
                //{
                //    if (proximaLlegadaCamion != ultimoValorLLegadaCamion)
                //    {
                //        tablaProximosCamiones.Rows.Add(valorValorLLegadaCamion);
                //        ultimoValorLLegadaCamion = proximaLlegadaCamion;
                //    }
                //}
                //else
                //{
                //    tablaProximosCamiones.Rows.Add(valorValorLLegadaCamion);
                //    ultimoValorLLegadaCamion = proximaLlegadaCamion;
                //}
            }
        }

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

        private TimeSpan llegadaCliente(double lambda)
        {
            //Distribucion Exponencial Negativa

            double aleatorio = GeneradorUnico.GenerarAleatorio();

            double tiempoLlegada = ((-lambda) * Math.Log(1 - aleatorio));

            return GeneradorUnico.convertirSegundosHorasMinutos(tiempoLlegada);
        }
    }
}
