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
            this.US_Usuario = new System.Windows.Forms.TextBox();
            this.labelTitulo1 = new System.Windows.Forms.Label();
            this.BTN_ENTRAR = new System.Windows.Forms.Button();
            this.US_Contrasena = new System.Windows.Forms.TextBox();
            this.US_RecordarUsuario = new System.Windows.Forms.CheckBox();
            this.groupContrasena = new System.Windows.Forms.GroupBox();
            this.pictureContrasena = new System.Windows.Forms.PictureBox();
            this.groupUsuario = new System.Windows.Forms.GroupBox();
            this.pictureUsuario = new System.Windows.Forms.PictureBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.labelTitulo2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            this.groupContrasena.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureContrasena)).BeginInit();
            this.groupUsuario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUsuario)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureLogo
            // 
            this.pictureLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureLogo.Image")));
            this.pictureLogo.Location = new System.Drawing.Point(737, 4);
            this.pictureLogo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureLogo.Name = "pictureLogo";
            this.pictureLogo.Size = new System.Drawing.Size(56, 53);
            this.pictureLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureLogo.TabIndex = 8;
            this.pictureLogo.TabStop = false;
            // 
            // US_Usuario
            // 
            this.US_Usuario.BackColor = System.Drawing.Color.AliceBlue;
            this.US_Usuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.US_Usuario.ForeColor = System.Drawing.Color.MidnightBlue;
            this.US_Usuario.Location = new System.Drawing.Point(13, 23);
            this.US_Usuario.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.US_Usuario.Name = "US_Usuario";
            this.US_Usuario.Size = new System.Drawing.Size(231, 15);
            this.US_Usuario.TabIndex = 2;
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
            this.labelTitulo1.TabIndex = 0;
            this.labelTitulo1.Text = "Bienvenido";
            this.labelTitulo1.Click += new System.EventHandler(this.labelTitulo1_Click);
            // 
            // BTN_ENTRAR
            // 
            this.BTN_ENTRAR.BackColor = System.Drawing.Color.MidnightBlue;
            this.BTN_ENTRAR.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BTN_ENTRAR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_ENTRAR.Font = new System.Drawing.Font("Arial Rounded MT Bold", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_ENTRAR.ForeColor = System.Drawing.Color.SeaShell;
            this.BTN_ENTRAR.Location = new System.Drawing.Point(371, 320);
            this.BTN_ENTRAR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BTN_ENTRAR.Name = "BTN_ENTRAR";
            this.BTN_ENTRAR.Size = new System.Drawing.Size(117, 36);
            this.BTN_ENTRAR.TabIndex = 6;
            this.BTN_ENTRAR.Text = "Ingresar";
            this.BTN_ENTRAR.UseVisualStyleBackColor = false;
            this.BTN_ENTRAR.Click += new System.EventHandler(this.BTN_ENTRAR_Click);
            // 
            // US_Contrasena
            // 
            this.US_Contrasena.BackColor = System.Drawing.Color.AliceBlue;
            this.US_Contrasena.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.US_Contrasena.ForeColor = System.Drawing.Color.MidnightBlue;
            this.US_Contrasena.Location = new System.Drawing.Point(13, 25);
            this.US_Contrasena.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.US_Contrasena.Name = "US_Contrasena";
            this.US_Contrasena.PasswordChar = '•';
            this.US_Contrasena.Size = new System.Drawing.Size(231, 15);
            this.US_Contrasena.TabIndex = 4;
            // 
            // US_RecordarUsuario
            // 
            this.US_RecordarUsuario.AutoSize = true;
            this.US_RecordarUsuario.BackColor = System.Drawing.Color.AliceBlue;
            this.US_RecordarUsuario.Enabled = false;
            this.US_RecordarUsuario.ForeColor = System.Drawing.Color.MidnightBlue;
            this.US_RecordarUsuario.Location = new System.Drawing.Point(363, 274);
            this.US_RecordarUsuario.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.US_RecordarUsuario.Name = "US_RecordarUsuario";
            this.US_RecordarUsuario.Size = new System.Drawing.Size(133, 20);
            this.US_RecordarUsuario.TabIndex = 5;
            this.US_RecordarUsuario.Text = "Recordar usuario";
            this.US_RecordarUsuario.UseVisualStyleBackColor = false;
            this.US_RecordarUsuario.Visible = false;
            // 
            // groupContrasena
            // 
            this.groupContrasena.Controls.Add(this.pictureContrasena);
            this.groupContrasena.Controls.Add(this.US_Contrasena);
            this.groupContrasena.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupContrasena.Location = new System.Drawing.Point(284, 203);
            this.groupContrasena.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupContrasena.Name = "groupContrasena";
            this.groupContrasena.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupContrasena.Size = new System.Drawing.Size(289, 50);
            this.groupContrasena.TabIndex = 3;
            this.groupContrasena.TabStop = false;
            this.groupContrasena.Text = "Contraseña";
            // 
            // pictureContrasena
            // 
            this.pictureContrasena.Image = ((System.Drawing.Image)(resources.GetObject("pictureContrasena.Image")));
            this.pictureContrasena.Location = new System.Drawing.Point(255, 18);
            this.pictureContrasena.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureContrasena.Name = "pictureContrasena";
            this.pictureContrasena.Size = new System.Drawing.Size(23, 20);
            this.pictureContrasena.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureContrasena.TabIndex = 11;
            this.pictureContrasena.TabStop = false;
            // 
            // groupUsuario
            // 
            this.groupUsuario.Controls.Add(this.pictureUsuario);
            this.groupUsuario.Controls.Add(this.US_Usuario);
            this.groupUsuario.Controls.Add(this.textBox3);
            this.groupUsuario.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupUsuario.Location = new System.Drawing.Point(284, 135);
            this.groupUsuario.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupUsuario.Name = "groupUsuario";
            this.groupUsuario.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupUsuario.Size = new System.Drawing.Size(289, 50);
            this.groupUsuario.TabIndex = 2;
            this.groupUsuario.TabStop = false;
            this.groupUsuario.Text = "Usuario";
            // 
            // pictureUsuario
            // 
            this.pictureUsuario.Image = ((System.Drawing.Image)(resources.GetObject("pictureUsuario.Image")));
            this.pictureUsuario.Location = new System.Drawing.Point(255, 17);
            this.pictureUsuario.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(241, 15);
            this.textBox3.TabIndex = 5;
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
            // labelTitulo2
            // 
            this.labelTitulo2.AutoSize = true;
            this.labelTitulo2.Location = new System.Drawing.Point(321, 107);
            this.labelTitulo2.Name = "labelTitulo2";
            this.labelTitulo2.Size = new System.Drawing.Size(217, 16);
            this.labelTitulo2.TabIndex = 1;
            this.labelTitulo2.Text = "Ingres sus datos para iniciar sesion";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelTitulo2);
            this.Controls.Add(this.groupUsuario);
            this.Controls.Add(this.US_RecordarUsuario);
            this.Controls.Add(this.pictureLogo);
            this.Controls.Add(this.labelTitulo1);
            this.Controls.Add(this.BTN_ENTRAR);
            this.Controls.Add(this.groupContrasena);
            this.ForeColor = System.Drawing.Color.MidnightBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureLogo;
        private System.Windows.Forms.TextBox US_Usuario;
        private System.Windows.Forms.Label labelTitulo1;
        private System.Windows.Forms.Button BTN_ENTRAR;
        private System.Windows.Forms.TextBox US_Contrasena;
        private System.Windows.Forms.CheckBox US_RecordarUsuario;
        private System.Windows.Forms.GroupBox groupContrasena;
        private System.Windows.Forms.GroupBox groupUsuario;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.PictureBox pictureUsuario;
        private System.Windows.Forms.PictureBox pictureContrasena;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.Label labelTitulo2;
    }
}

