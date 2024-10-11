namespace Sistema_de_registro_y_visualizacion_de_Clientes
{
    partial class Lista_Prospectos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lista_Prospectos));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblAProspectos = new System.Windows.Forms.Label();
            this.btnMinimizar = new System.Windows.Forms.PictureBox();
            this.btnVolverMenu = new System.Windows.Forms.PictureBox();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFiltro = new System.Windows.Forms.ComboBox();
            this.dgvProspecto = new System.Windows.Forms.DataGridView();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Primer_Apellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Segundo_Apellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnVolverMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProspecto)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(60)))), ((int)(((byte)(95)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblAProspectos);
            this.panel1.Controls.Add(this.btnMinimizar);
            this.panel1.Controls.Add(this.btnVolverMenu);
            this.panel1.Controls.Add(this.btnCerrar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1278, 60);
            this.panel1.TabIndex = 47;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // lblAProspectos
            // 
            this.lblAProspectos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAProspectos.AutoSize = true;
            this.lblAProspectos.Font = new System.Drawing.Font("Century Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAProspectos.ForeColor = System.Drawing.Color.White;
            this.lblAProspectos.Location = new System.Drawing.Point(472, 8);
            this.lblAProspectos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAProspectos.Name = "lblAProspectos";
            this.lblAProspectos.Size = new System.Drawing.Size(317, 44);
            this.lblAProspectos.TabIndex = 50;
            this.lblAProspectos.Text = "Listas Prospectos";
            this.lblAProspectos.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblAProspectos_MouseDown);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimizar.Image = ((System.Drawing.Image)(resources.GetObject("btnMinimizar.Image")));
            this.btnMinimizar.Location = new System.Drawing.Point(1215, 20);
            this.btnMinimizar.Margin = new System.Windows.Forms.Padding(4);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(19, 18);
            this.btnMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMinimizar.TabIndex = 49;
            this.btnMinimizar.TabStop = false;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
            // 
            // btnVolverMenu
            // 
            this.btnVolverMenu.AccessibleDescription = "";
            this.btnVolverMenu.AccessibleName = "";
            this.btnVolverMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnVolverMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVolverMenu.Image = ((System.Drawing.Image)(resources.GetObject("btnVolverMenu.Image")));
            this.btnVolverMenu.Location = new System.Drawing.Point(16, 10);
            this.btnVolverMenu.Margin = new System.Windows.Forms.Padding(4);
            this.btnVolverMenu.Name = "btnVolverMenu";
            this.btnVolverMenu.Size = new System.Drawing.Size(28, 37);
            this.btnVolverMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnVolverMenu.TabIndex = 48;
            this.btnVolverMenu.TabStop = false;
            this.btnVolverMenu.Click += new System.EventHandler(this.btnVolverMenu_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.AccessibleDescription = "";
            this.btnCerrar.AccessibleName = "";
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.Location = new System.Drawing.Point(1242, 20);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(17, 16);
            this.btnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnCerrar.TabIndex = 10;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 110);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 24);
            this.label1.TabIndex = 75;
            this.label1.Text = "Filtrar por Estados:";
            // 
            // cbFiltro
            // 
            this.cbFiltro.FormattingEnabled = true;
            this.cbFiltro.Location = new System.Drawing.Point(217, 113);
            this.cbFiltro.Name = "cbFiltro";
            this.cbFiltro.Size = new System.Drawing.Size(288, 24);
            this.cbFiltro.TabIndex = 76;
            this.cbFiltro.Text = "Enviado";
            this.cbFiltro.SelectedIndexChanged += new System.EventHandler(this.cbFiltro_SelectedIndexChanged_1);
            // 
            // dgvProspecto
            // 
            this.dgvProspecto.AllowUserToAddRows = false;
            this.dgvProspecto.AllowUserToDeleteRows = false;
            this.dgvProspecto.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProspecto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProspecto.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nombre,
            this.Primer_Apellido,
            this.Segundo_Apellido,
            this.Estatus});
            this.dgvProspecto.Location = new System.Drawing.Point(17, 168);
            this.dgvProspecto.Margin = new System.Windows.Forms.Padding(4);
            this.dgvProspecto.Name = "dgvProspecto";
            this.dgvProspecto.ReadOnly = true;
            this.dgvProspecto.RowHeadersVisible = false;
            this.dgvProspecto.RowHeadersWidth = 51;
            this.dgvProspecto.Size = new System.Drawing.Size(1243, 553);
            this.dgvProspecto.TabIndex = 90;
            // 
            // Nombre
            // 
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.MinimumWidth = 6;
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            this.Nombre.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Primer_Apellido
            // 
            this.Primer_Apellido.HeaderText = "Primer Apellido";
            this.Primer_Apellido.MinimumWidth = 6;
            this.Primer_Apellido.Name = "Primer_Apellido";
            this.Primer_Apellido.ReadOnly = true;
            // 
            // Segundo_Apellido
            // 
            this.Segundo_Apellido.HeaderText = "Segundo Apellido";
            this.Segundo_Apellido.MinimumWidth = 6;
            this.Segundo_Apellido.Name = "Segundo_Apellido";
            this.Segundo_Apellido.ReadOnly = true;
            // 
            // Estatus
            // 
            this.Estatus.HeaderText = "Estatus";
            this.Estatus.MinimumWidth = 6;
            this.Estatus.Name = "Estatus";
            this.Estatus.ReadOnly = true;
            // 
            // Lista_Prospectos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.ClientSize = new System.Drawing.Size(1278, 739);
            this.Controls.Add(this.dgvProspecto);
            this.Controls.Add(this.cbFiltro);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Lista_Prospectos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista_Prospectos";
            this.Load += new System.EventHandler(this.Lista_Prospectos_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Lista_Prospectos_MouseDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnVolverMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProspecto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblAProspectos;
        private System.Windows.Forms.PictureBox btnMinimizar;
        private System.Windows.Forms.PictureBox btnVolverMenu;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFiltro;
        private System.Windows.Forms.DataGridView dgvProspecto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Primer_Apellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn Segundo_Apellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estatus;
    }
}