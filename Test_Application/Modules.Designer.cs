namespace Test_Application
{
    partial class Modules
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Modules));
            this.gb_Modules = new System.Windows.Forms.GroupBox();
            this.tb_search = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.lb_modules = new System.Windows.Forms.ListBox();
            this.labelApplication = new System.Windows.Forms.Label();
            this.gb_Modules.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_Modules
            // 
            this.gb_Modules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_Modules.Controls.Add(this.tb_search);
            this.gb_Modules.Controls.Add(this.button3);
            this.gb_Modules.Controls.Add(this.lb_modules);
            this.gb_Modules.Location = new System.Drawing.Point(13, 49);
            this.gb_Modules.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gb_Modules.Name = "gb_Modules";
            this.gb_Modules.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gb_Modules.Size = new System.Drawing.Size(303, 396);
            this.gb_Modules.TabIndex = 5;
            this.gb_Modules.TabStop = false;
            this.gb_Modules.Text = "Modules";
            // 
            // tb_search
            // 
            this.tb_search.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_search.Location = new System.Drawing.Point(18, 25);
            this.tb_search.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tb_search.Name = "tb_search";
            this.tb_search.Size = new System.Drawing.Size(212, 26);
            this.tb_search.TabIndex = 8;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(245, 25);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(40, 26);
            this.button3.TabIndex = 7;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // lb_modules
            // 
            this.lb_modules.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_modules.FormattingEnabled = true;
            this.lb_modules.ItemHeight = 18;
            this.lb_modules.Location = new System.Drawing.Point(18, 58);
            this.lb_modules.Name = "lb_modules";
            this.lb_modules.Size = new System.Drawing.Size(268, 310);
            this.lb_modules.TabIndex = 0;
            this.lb_modules.SelectedIndexChanged += new System.EventHandler(this.lb_modules_SelectedIndexChanged);
            // 
            // labelApplication
            // 
            this.labelApplication.AutoSize = true;
            this.labelApplication.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelApplication.Location = new System.Drawing.Point(9, 16);
            this.labelApplication.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelApplication.Name = "labelApplication";
            this.labelApplication.Size = new System.Drawing.Size(180, 24);
            this.labelApplication.TabIndex = 4;
            this.labelApplication.Text = "ApplicationName";
            // 
            // Modules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 458);
            this.Controls.Add(this.gb_Modules);
            this.Controls.Add(this.labelApplication);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "Modules";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Application";
            this.gb_Modules.ResumeLayout(false);
            this.gb_Modules.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_Modules;
        private System.Windows.Forms.TextBox tb_search;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox lb_modules;
        private System.Windows.Forms.Label labelApplication;
    }
}