namespace AAVD
{
    partial class Login
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.pictureLogo = new System.Windows.Forms.PictureBox();
            this.TextUsuario = new System.Windows.Forms.TextBox();
            this.labelTitulo1 = new System.Windows.Forms.Label();
            this.BTN_ENTRAR = new System.Windows.Forms.Button();
            this.textContrasena = new System.Windows.Forms.TextBox();
            this.checkRecordar = new System.Windows.Forms.CheckBox();
            this.groupContrasena = new System.Windows.Forms.GroupBox();
            this.pictureContrasena = new System.Windows.Forms.PictureBox();
            this.groupUsuario = new System.Windows.Forms.GroupBox();
            this.pictureUsuario = new System.Windows.Forms.PictureBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.labelTitulo2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuEmpleado = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGerente = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            this.groupContrasena.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureContrasena)).BeginInit();
            this.groupUsuario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUsuario)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureLogo
            // 
            this.pictureLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureLogo.Image")));
            this.pictureLogo.Location = new System.Drawing.Point(737, 4);
            this.pictureLogo.Name = "pictureLogo";
            this.pictureLogo.Size = new System.Drawing.Size(56, 53);
            this.pictureLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureLogo.TabIndex = 8;
            this.pictureLogo.TabStop = false;
            // 
            // TextUsuario
            // 
            this.TextUsuario.BackColor = System.Drawing.Color.AliceBlue;
            this.TextUsuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextUsuario.ForeColor = System.Drawing.Color.MidnightBlue;
            this.TextUsuario.Location = new System.Drawing.Point(14, 23);
            this.TextUsuario.Name = "TextUsuario";
            this.TextUsuario.Size = new System.Drawing.Size(231, 15);
            this.TextUsuario.TabIndex = 0;
            this.TextUsuario.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // labelTitulo1
            // 
            this.labelTitulo1.AutoSize = true;
            this.labelTitulo1.BackColor = System.Drawing.Color.AliceBlue;
            this.labelTitulo1.Font = new System.Drawing.Font("Impact", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitulo1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.labelTitulo1.Location = new System.Drawing.Point(321, 52);
            this.labelTitulo1.Name = "labelTitulo1";
            this.labelTitulo1.Size = new System.Drawing.Size(214, 48);
            this.labelTitulo1.TabIndex = 10;
            this.labelTitulo1.Text = "Bienvenido";
            // 
            // BTN_ENTRAR
            // 
            this.BTN_ENTRAR.BackColor = System.Drawing.Color.MidnightBlue;
            this.BTN_ENTRAR.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BTN_ENTRAR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_ENTRAR.Font = new System.Drawing.Font("Arial Rounded MT Bold", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_ENTRAR.ForeColor = System.Drawing.Color.SeaShell;
            this.BTN_ENTRAR.Location = new System.Drawing.Point(370, 320);
            this.BTN_ENTRAR.Name = "BTN_ENTRAR";
            this.BTN_ENTRAR.Size = new System.Drawing.Size(117, 36);
            this.BTN_ENTRAR.TabIndex = 3;
            this.BTN_ENTRAR.Text = "Ingresar";
            this.BTN_ENTRAR.UseVisualStyleBackColor = false;
            this.BTN_ENTRAR.Click += new System.EventHandler(this.BTN_ENTRAR_Click);
            // 
            // textContrasena
            // 
            this.textContrasena.BackColor = System.Drawing.Color.AliceBlue;
            this.textContrasena.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textContrasena.ForeColor = System.Drawing.Color.MidnightBlue;
            this.textContrasena.Location = new System.Drawing.Point(14, 24);
            this.textContrasena.Name = "textContrasena";
            this.textContrasena.PasswordChar = '•';
            this.textContrasena.Size = new System.Drawing.Size(231, 15);
            this.textContrasena.TabIndex = 1;
            this.textContrasena.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // checkRecordar
            // 
            this.checkRecordar.AutoSize = true;
            this.checkRecordar.BackColor = System.Drawing.Color.AliceBlue;
            this.checkRecordar.ForeColor = System.Drawing.Color.MidnightBlue;
            this.checkRecordar.Location = new System.Drawing.Point(362, 274);
            this.checkRecordar.Name = "checkRecordar";
            this.checkRecordar.Size = new System.Drawing.Size(133, 20);
            this.checkRecordar.TabIndex = 2;
            this.checkRecordar.Text = "Recordar usuario";
            this.checkRecordar.UseVisualStyleBackColor = false;
            // 
            // groupContrasena
            // 
            this.groupContrasena.Controls.Add(this.pictureContrasena);
            this.groupContrasena.Controls.Add(this.textContrasena);
            this.groupContrasena.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupContrasena.Location = new System.Drawing.Point(284, 203);
            this.groupContrasena.Name = "groupContrasena";
            this.groupContrasena.Size = new System.Drawing.Size(289, 51);
            this.groupContrasena.TabIndex = 1;
            this.groupContrasena.TabStop = false;
            this.groupContrasena.Text = "Contraseña";
            // 
            // pictureContrasena
            // 
            this.pictureContrasena.Image = ((System.Drawing.Image)(resources.GetObject("pictureContrasena.Image")));
            this.pictureContrasena.Location = new System.Drawing.Point(255, 19);
            this.pictureContrasena.Name = "pictureContrasena";
            this.pictureContrasena.Size = new System.Drawing.Size(23, 20);
            this.pictureContrasena.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureContrasena.TabIndex = 11;
            this.pictureContrasena.TabStop = false;
            // 
            // groupUsuario
            // 
            this.groupUsuario.Controls.Add(this.pictureUsuario);
            this.groupUsuario.Controls.Add(this.TextUsuario);
            this.groupUsuario.Controls.Add(this.textBox3);
            this.groupUsuario.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupUsuario.Location = new System.Drawing.Point(284, 136);
            this.groupUsuario.Name = "groupUsuario";
            this.groupUsuario.Size = new System.Drawing.Size(289, 51);
            this.groupUsuario.TabIndex = 0;
            this.groupUsuario.TabStop = false;
            this.groupUsuario.Text = "Usuario";
            // 
            // pictureUsuario
            // 
            this.pictureUsuario.Image = ((System.Drawing.Image)(resources.GetObject("pictureUsuario.Image")));
            this.pictureUsuario.Location = new System.Drawing.Point(255, 17);
            this.pictureUsuario.Name = "pictureUsuario";
            this.pictureUsuario.Size = new System.Drawing.Size(23, 21);
            this.pictureUsuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureUsuario.TabIndex = 11;
            this.pictureUsuario.TabStop = false;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.AliceBlue;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(37, 23);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(241, 15);
            this.textBox3.TabIndex = 5;
            // 
            // labelTitulo2
            // 
            this.labelTitulo2.AutoSize = true;
            this.labelTitulo2.Location = new System.Drawing.Point(349, 100);
            this.labelTitulo2.Name = "labelTitulo2";
            this.labelTitulo2.Size = new System.Drawing.Size(158, 16);
            this.labelTitulo2.TabIndex = 13;
            this.labelTitulo2.Text = "Ingresar como empleado";
            this.labelTitulo2.Click += new System.EventHandler(this.labelTitulo2_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.MidnightBlue;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEmpleado,
            this.menuGerente});
            this.menuStrip1.Location = new System.Drawing.Point(0, 422);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuEmpleado
            // 
            this.menuEmpleado.BackColor = System.Drawing.Color.DodgerBlue;
            this.menuEmpleado.ForeColor = System.Drawing.Color.AliceBlue;
            this.menuEmpleado.Name = "menuEmpleado";
            this.menuEmpleado.Size = new System.Drawing.Size(91, 24);
            this.menuEmpleado.Text = "Empleado";
            this.menuEmpleado.Click += new System.EventHandler(this.uusuarioToolStripMenuItem_Click);
            // 
            // menuGerente
            // 
            this.menuGerente.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuGerente.ForeColor = System.Drawing.Color.AliceBlue;
            this.menuGerente.Name = "menuGerente";
            this.menuGerente.Size = new System.Drawing.Size(75, 24);
            this.menuGerente.Text = "Gerente";
            this.menuGerente.Click += new System.EventHandler(this.menuGerente_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelTitulo2);
            this.Controls.Add(this.groupUsuario);
            this.Controls.Add(this.checkRecordar);
            this.Controls.Add(this.pictureLogo);
            this.Controls.Add(this.labelTitulo1);
            this.Controls.Add(this.BTN_ENTRAR);
            this.Controls.Add(this.groupContrasena);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.MidnightBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            this.groupContrasena.ResumeLayout(false);
            this.groupContrasena.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureContrasena)).EndInit();
            this.groupUsuario.ResumeLayout(false);
            this.groupUsuario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUsuario)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureLogo;
        private System.Windows.Forms.TextBox TextUsuario;
        private System.Windows.Forms.Label labelTitulo1;
        private System.Windows.Forms.Button BTN_ENTRAR;
        private System.Windows.Forms.TextBox textContrasena;
        private System.Windows.Forms.CheckBox checkRecordar;
        private System.Windows.Forms.GroupBox groupContrasena;
        private System.Windows.Forms.GroupBox groupUsuario;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.PictureBox pictureUsuario;
        private System.Windows.Forms.PictureBox pictureContrasena;
        private System.Windows.Forms.Label labelTitulo2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuEmpleado;
        private System.Windows.Forms.ToolStripMenuItem menuGerente;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
    }
}

