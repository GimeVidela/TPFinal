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
            GrillaLista.DataSource = tablaClientes;
        }

        private void GrillaLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string hora = "";
            //SimulacionAtencionEDEN sim = new SimulacionAtencionEDEN();
            if (GrillaLista.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                GrillaLista.CurrentRow.Selected = true;
                hora = GrillaLista.Rows[e.RowIndex].Cells["Reloj"].FormattedValue.ToString();
            }
        }
    }
}
