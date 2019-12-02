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
            this.GrillaListaCamiones = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.GrillaListaCamiones)).BeginInit();
            this.SuspendLayout();
            // 
            // GrillaListaCamiones
            // 
            this.GrillaListaCamiones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GrillaListaCamiones.Location = new System.Drawing.Point(11, 11);
            this.GrillaListaCamiones.Margin = new System.Windows.Forms.Padding(2);
            this.GrillaListaCamiones.Name = "GrillaListaCamiones";
            this.GrillaListaCamiones.RowTemplate.Height = 24;
            this.GrillaListaCamiones.Size = new System.Drawing.Size(262, 567);
            this.GrillaListaCamiones.TabIndex = 1;
            // 
            // ListaClienteEstados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 585);
            this.Controls.Add(this.GrillaListaCamiones);
            this.Name = "ListaClienteEstados";
            this.Text = "Lista de Cliente con sus Estados";
            ((System.ComponentModel.ISupportInitialize)(this.GrillaListaCamiones)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GrillaListaCamiones;
    }
}