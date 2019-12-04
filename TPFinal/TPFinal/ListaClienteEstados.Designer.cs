namespace TPFinal
{
    partial class ListaClienteEstados
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GrillaLista = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.GrillaLista)).BeginInit();
            this.SuspendLayout();
            // 
            // GrillaLista
            // 
            this.GrillaLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GrillaLista.Location = new System.Drawing.Point(11, 11);
            this.GrillaLista.Margin = new System.Windows.Forms.Padding(2);
            this.GrillaLista.Name = "GrillaLista";
            this.GrillaLista.RowTemplate.Height = 24;
            this.GrillaLista.Size = new System.Drawing.Size(262, 567);
            this.GrillaLista.TabIndex = 1;
            this.GrillaLista.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrillaLista_CellClick);
            // 
            // ListaClienteEstados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 585);
            this.Controls.Add(this.GrillaLista);
            this.Name = "ListaClienteEstados";
            this.Text = "Lista de Cliente con sus Estados";
            ((System.ComponentModel.ISupportInitialize)(this.GrillaLista)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GrillaLista;
    }
}