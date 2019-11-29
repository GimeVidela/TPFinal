using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPFinal
{
    public partial class SimulacionAtencionEDEN : Form
    {
        public SimulacionAtencionEDEN()
        {
            InitializeComponent();
        }

        private void btn_simular_Click(object sender, EventArgs e)
        {
            if (diasASimular.Text != "" && hor2.Text != "" && min2.Text != "" && seg2.Text != "" && iteraciones.Text != "")
            {
                if (diasASimular.Text.Any(x => !char.IsNumber(x)) || hor2.Text.Any(x => !char.IsNumber(x)) || min2.Text.Any(x => !char.IsNumber(x)) || seg2.Text.Any(x => !char.IsNumber(x)) || iteraciones.Text.Any(x => !char.IsNumber(x)))
                {
                    MessageBox.Show("Solo ingrese valores numéricos");
                }
                else
                {
                    //Validacion de texboxs
                    int dias = Convert.ToInt32(diasASimular.Text);
                    int hora2 = Int32.Parse(hor2.Text);
                    int minu2 = Int32.Parse(min2.Text);
                    int segu2 = Int32.Parse(seg2.Text);
                    long iter = long.Parse(iteraciones.Text);

                    if (dias > 0 && (hora2 >= 0 && hora2 <= 24) && (minu2 >= 0 && minu2 <= 60) && (segu2 >= 0 && segu2 <= 60))
                    {
                        if (iter >= 5 && iter <= 500000)
                        {
                            TimeSpan TiempoASimular = TimeSpan.Parse(dias * 24 + ":" + "0" + ":" + "0");
                            TimeSpan TiempoIniciociclos = TimeSpan.Parse(hor2.Text + ":" + min2.Text + ":" + seg2.Text);

                            if (TiempoASimular <= TiempoIniciociclos)
                            {
                                MessageBox.Show(" La hora ingresada que indicara la cantidad de iteraciones a mostrar debe estar dentro del rango del tiempo a simular ingresado.");

                            }
                            else
                            {
                                GestorSimulacion gestor = new GestorSimulacion(Convert.ToInt32(iteraciones.Text), dias, TiempoIniciociclos);
                                grillaEstadisticas.DataSource = gestor.SimularVectorEstado();
                                //ListaCamionesGrilla grillaCamiones = new ListaCamionesGrilla();
                                //grillaCamiones.cargarGrilla(gestor.cargarTablaCamiones(gestor.listaCamionesAtendidos));
                                //grillaCamiones.Show();
                                txtColaMax.Text = Convert.ToString(gestor.colaMax);
                            }
                        }
                        else
                        {
                            MessageBox.Show("La cantidad ingresada de iteraciones no es correcta. Ingrese un valor entre 5 y 500000.");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Ingrese parametros de horas, minutos y segundos válidos. Para hora entre 0 y 24, para minutos y segundos entre 0 y 60.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Ningún campo debe estar vacío");
            }
        }
    }
}
