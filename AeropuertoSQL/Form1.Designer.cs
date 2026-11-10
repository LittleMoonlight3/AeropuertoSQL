namespace AeropuertoSQL
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnVuelos = new System.Windows.Forms.Button();
            this.btnDatosVuelos = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnVuelos
            // 
            this.btnVuelos.Location = new System.Drawing.Point(337, 135);
            this.btnVuelos.Name = "btnVuelos";
            this.btnVuelos.Size = new System.Drawing.Size(120, 107);
            this.btnVuelos.TabIndex = 0;
            this.btnVuelos.Text = "Vuelos";
            this.btnVuelos.UseVisualStyleBackColor = true;
            this.btnVuelos.Click += new System.EventHandler(this.btnVuelos_Click);
            // 
            // btnDatosVuelos
            // 
            this.btnDatosVuelos.Location = new System.Drawing.Point(452, 135);
            this.btnDatosVuelos.Name = "btnDatosVuelos";
            this.btnDatosVuelos.Size = new System.Drawing.Size(112, 107);
            this.btnDatosVuelos.TabIndex = 1;
            this.btnDatosVuelos.Text = "Datos de los Vuelos";
            this.btnDatosVuelos.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1267, 705);
            this.Controls.Add(this.btnDatosVuelos);
            this.Controls.Add(this.btnVuelos);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnVuelos;
        private System.Windows.Forms.Button btnDatosVuelos;
    }
}

