namespace Test_Application
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.fl = new System.Windows.Forms.FlowLayoutPanel();
            this.gpAcoes = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lbsomiodnames = new System.Windows.Forms.Label();
            this.lbsomiod = new System.Windows.Forms.Label();
            this.cb_App_Mod = new System.Windows.Forms.ComboBox();
            this.gpAcoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // fl
            // 
            this.fl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fl.Location = new System.Drawing.Point(65, 217);
            this.fl.Name = "fl";
            this.fl.Padding = new System.Windows.Forms.Padding(15);
            this.fl.Size = new System.Drawing.Size(1238, 496);
            this.fl.TabIndex = 1;
            // 
            // gpAcoes
            // 
            this.gpAcoes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpAcoes.Controls.Add(this.button2);
            this.gpAcoes.Controls.Add(this.button1);
            this.gpAcoes.Controls.Add(this.lbsomiodnames);
            this.gpAcoes.Controls.Add(this.lbsomiod);
            this.gpAcoes.Location = new System.Drawing.Point(13, 12);
            this.gpAcoes.Name = "gpAcoes";
            this.gpAcoes.Size = new System.Drawing.Size(1336, 158);
            this.gpAcoes.TabIndex = 2;
            this.gpAcoes.TabStop = false;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(396, 34);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(303, 99);
            this.button2.TabIndex = 5;
            this.button2.Text = "Unsubscribe";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(52, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(303, 99);
            this.button1.TabIndex = 4;
            this.button1.Text = "Subscribe";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbsomiodnames
            // 
            this.lbsomiodnames.AutoSize = true;
            this.lbsomiodnames.Font = new System.Drawing.Font("Lucida Calligraphy", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbsomiodnames.ForeColor = System.Drawing.Color.DimGray;
            this.lbsomiodnames.Location = new System.Drawing.Point(769, 99);
            this.lbsomiodnames.Name = "lbsomiodnames";
            this.lbsomiodnames.Size = new System.Drawing.Size(647, 23);
            this.lbsomiodnames.TabIndex = 3;
            this.lbsomiodnames.Text = "© Made by: Tiago Guardado, Tomás Neves, João Pinto, Célsio Vinagre.";
            // 
            // lbsomiod
            // 
            this.lbsomiod.AutoSize = true;
            this.lbsomiod.Font = new System.Drawing.Font("Lucida Handwriting", 40.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbsomiod.Location = new System.Drawing.Point(790, 40);
            this.lbsomiod.Name = "lbsomiod";
            this.lbsomiod.Size = new System.Drawing.Size(590, 87);
            this.lbsomiod.TabIndex = 2;
            this.lbsomiod.Text = "SOMIOD TESTS";
            // 
            // cb_App_Mod
            // 
            this.cb_App_Mod.FormattingEnabled = true;
            this.cb_App_Mod.Location = new System.Drawing.Point(65, 197);
            this.cb_App_Mod.Name = "cb_App_Mod";
            this.cb_App_Mod.Size = new System.Drawing.Size(155, 24);
            this.cb_App_Mod.TabIndex = 3;
            this.cb_App_Mod.SelectedIndexChanged += new System.EventHandler(this.cb_App_Mod_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.cb_App_Mod);
            this.Controls.Add(this.gpAcoes);
            this.Controls.Add(this.fl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SOMIOD TESTS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.gpAcoes.ResumeLayout(false);
            this.gpAcoes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel fl;
        private System.Windows.Forms.GroupBox gpAcoes;
        private System.Windows.Forms.Label lbsomiod;
        private System.Windows.Forms.Label lbsomiodnames;
        private System.Windows.Forms.ComboBox cb_App_Mod;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

