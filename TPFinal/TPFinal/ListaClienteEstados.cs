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
    public partial class ListaClienteEstados : Form
    {
        public ListaClienteEstados()
        {
            InitializeComponent();
        }
        public void cargarGrilla(DataTable tablaClientes)
        {
            GrillaListaCamiones.DataSource = tablaClientes;
        }
    }
}
