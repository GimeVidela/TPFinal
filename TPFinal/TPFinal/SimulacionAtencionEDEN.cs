﻿using System;
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
            if (hor1.Text != "" && min1.Text != "" && seg1.Text != "" && iteraciones.Text != "" && hor2.Text != "" && min2.Text != "" && seg2.Text != "" && iteraciones.Text != "")
            {
                if (hor1.Text.Any(x => !char.IsNumber(x)) || min1.Text.Any(x => !char.IsNumber(x)) || seg1.Text.Any(x => !char.IsNumber(x)) || hor2.Text.Any(x => !char.IsNumber(x)) || min2.Text.Any(x => !char.IsNumber(x)) || seg2.Text.Any(x => !char.IsNumber(x)) || iteraciones.Text.Any(x => !char.IsNumber(x)))
                {
                    MessageBox.Show("Solo ingrese valores numéricos");
                }
                else
                {
                    //Validacion de texboxs
                    int hora1 = Int32.Parse(hor2.Text);
                    int minu1 = Int32.Parse(min2.Text);
                    int segu1 = Int32.Parse(seg2.Text);
                    int hora2 = Int32.Parse(hor2.Text);
                    int minu2 = Int32.Parse(min2.Text);
                    int segu2 = Int32.Parse(seg2.Text);
                    long iter = long.Parse(iteraciones.Text);

                    if ((minu1 >= 0 && minu1 <= 60) && (segu1 >= 0 && segu1 <= 60) && (hora2 >= 0 && hora2 <= 24) && (minu2 >= 0 && minu2 <= 60) && (segu2 >= 0 && segu2 <= 60))
                    {
                        if (iter >= 5 && iter <= 8000)
                        {
                            TimeSpan TiempoASimular = TimeSpan.Parse(hor1.Text + ":" + min1.Text + ":" + seg1.Text);
                            TimeSpan TiempoIniciociclos = TimeSpan.Parse(hor2.Text + ":" + min2.Text + ":" + seg2.Text);

                            if (TiempoASimular <= TiempoIniciociclos)
                            {
                                MessageBox.Show(" La hora ingresada que indicara la cantidad de iteraciones a mostrar debe estar dentro del rango del tiempo a simular ingresado.");

                            }
                            else
                            {
                                GestorSimulacion gestor = new GestorSimulacion(Convert.ToInt32(iteraciones.Text), TiempoASimular, TiempoIniciociclos);
                                grillaEstadisticas.DataSource = gestor.SimularVectorEstado();
                               // ListaClienteEstados grillaClientes = new ListaClienteEstados();
                                GrillaLista.DataSource = gestor.cargarTablaClientes(gestor.listaClientes);
                                //grillaClientes.Show();
                                txtColaMax.Text = Convert.ToString(gestor.colaMax);
                                txtPromCola.Text = Convert.ToString(gestor.promTiempoEnCola);
                                TimeSpan valor = new TimeSpan(0, 2, 0);
                               
                                if (gestor.promTiempoEnCola > valor)
                                {
                                    tbxRTACajero.Text = "Si, el Tiempo Promedio de Espera en Cola " + gestor.promTiempoEnCola + " es mayor al tiempo de atención (2min).";
                                }
                                else
                                {
                                    tbxRTACajero.Text = "No, el Tiempo Promedio de Espera en Cola " + gestor.promTiempoEnCola + " es menor al tiempo de atención (2min).";
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("La cantidad ingresada de iteraciones no es correcta. Ingrese un valor entre 5 y 8000.");
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
        private void GrillaLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string hora = "";
            string reloj = ""; 
            if (GrillaLista.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                GrillaLista.CurrentRow.Selected = true;
                hora = GrillaLista.Rows[e.RowIndex].Cells["Reloj"].FormattedValue.ToString();
                for (int i = 0; i < grillaEstadisticas.RowCount; i++)
                {
                    reloj = grillaEstadisticas.Rows[i].Cells["Reloj"].FormattedValue.ToString();
                    if (hora == reloj)
                    {
                        grillaEstadisticas.Rows[i].Selected = true;
                    }
                }
            }
            

        }
    }
}
