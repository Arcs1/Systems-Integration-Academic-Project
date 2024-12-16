namespace App_Devices
{
    partial class Application_view
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Application_view));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCommands = new System.Windows.Forms.Button();
            this.gbCommands = new System.Windows.Forms.GroupBox();
            this.pb_image = new System.Windows.Forms.PictureBox();
            this.lbsomiodnames = new System.Windows.Forms.Label();
            this.lbsomiod = new System.Windows.Forms.Label();
            this.lb_Commands = new System.Windows.Forms.ListBox();
            this.gbNewModule = new System.Windows.Forms.GroupBox();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.tb_nameModule = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelApplication = new System.Windows.Forms.Label();
            this.gb_Modules = new System.Windows.Forms.GroupBox();
            this.btn_connections = new System.Windows.Forms.Button();
            this.btn_subscriptions = new System.Windows.Forms.Button();
            this.tb_search = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_update = new System.Windows.Forms.Button();
            this.lb_modules = new System.Windows.Forms.ListBox();
            this.btn_apagarApp = new System.Windows.Forms.Button();
            this.btn_updateApp = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            this.gbCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_image)).BeginInit();
            this.gbNewModule.SuspendLayout();
            this.gb_Modules.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnCommands);
            this.groupBox1.Controls.Add(this.gbCommands);
            this.groupBox1.Controls.Add(this.gbNewModule);
            this.groupBox1.Location = new System.Drawing.Point(337, 53);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(486, 394);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btnCommands
            // 
            this.btnCommands.Location = new System.Drawing.Point(16, 136);
            this.btnCommands.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCommands.Name = "btnCommands";
            this.btnCommands.Size = new System.Drawing.Size(122, 32);
            this.btnCommands.TabIndex = 5;
            this.btnCommands.Text = "Commands";
            this.btnCommands.UseVisualStyleBackColor = true;
            this.btnCommands.Click += new System.EventHandler(this.btnEnable_Click);
            // 
            // gbCommands
            // 
            this.gbCommands.Controls.Add(this.pb_image);
            this.gbCommands.Controls.Add(this.lbsomiodnames);
            this.gbCommands.Controls.Add(this.lbsomiod);
            this.gbCommands.Controls.Add(this.lb_Commands);
            this.gbCommands.Location = new System.Drawing.Point(16, 174);
            this.gbCommands.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbCommands.Name = "gbCommands";
            this.gbCommands.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbCommands.Size = new System.Drawing.Size(454, 203);
            this.gbCommands.TabIndex = 1;
            this.gbCommands.TabStop = false;
            this.gbCommands.Text = "Commands";
            // 
            // pb_image
            // 
            this.pb_image.Image = ((System.Drawing.Image)(resources.GetObject("pb_image.Image")));
            this.pb_image.InitialImage = ((System.Drawing.Image)(resources.GetObject("pb_image.InitialImage")));
            this.pb_image.Location = new System.Drawing.Point(177, 20);
            this.pb_image.Name = "pb_image";
            this.pb_image.Size = new System.Drawing.Size(98, 98);
            this.pb_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_image.TabIndex = 11;
            this.pb_image.TabStop = false;
            // 
            // lbsomiodnames
            // 
            this.lbsomiodnames.AutoSize = true;
            this.lbsomiodnames.Font = new System.Drawing.Font("Lucida Calligraphy", 5F);
            this.lbsomiodnames.ForeColor = System.Drawing.Color.DimGray;
            this.lbsomiodnames.Location = new System.Drawing.Point(93, 158);
            this.lbsomiodnames.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbsomiodnames.Name = "lbsomiodnames";
            this.lbsomiodnames.Size = new System.Drawing.Size(266, 8);
            this.lbsomiodnames.TabIndex = 6;
            this.lbsomiodnames.Text = "© Made by: Tiago Guardado, Tomás Neves, João Pinto, Célsio Vinagre.";
            // 
            // lbsomiod
            // 
            this.lbsomiod.AutoSize = true;
            this.lbsomiod.Font = new System.Drawing.Font("Lucida Handwriting", 20F, System.Drawing.FontStyle.Bold);
            this.lbsomiod.Location = new System.Drawing.Point(90, 121);
            this.lbsomiod.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbsomiod.Name = "lbsomiod";
            this.lbsomiod.Size = new System.Drawing.Size(286, 36);
            this.lbsomiod.TabIndex = 6;
            this.lbsomiod.Text = "SOMIOD DEVICES";
            // 
            // lb_Commands
            // 
            this.lb_Commands.Font = new System.Drawing.Font("Arial", 12F);
            this.lb_Commands.FormattingEnabled = true;
            this.lb_Commands.ItemHeight = 18;
            this.lb_Commands.Location = new System.Drawing.Point(8, 18);
            this.lb_Commands.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lb_Commands.Name = "lb_Commands";
            this.lb_Commands.Size = new System.Drawing.Size(437, 130);
            this.lb_Commands.TabIndex = 0;
            // 
            // gbNewModule
            // 
            this.gbNewModule.Controls.Add(this.btnAddNew);
            this.gbNewModule.Controls.Add(this.tb_nameModule);
            this.gbNewModule.Controls.Add(this.label1);
            this.gbNewModule.Location = new System.Drawing.Point(16, 27);
            this.gbNewModule.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbNewModule.Name = "gbNewModule";
            this.gbNewModule.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbNewModule.Size = new System.Drawing.Size(454, 103);
            this.gbNewModule.TabIndex = 0;
            this.gbNewModule.TabStop = false;
            this.gbNewModule.Text = "New Module";
            // 
            // btnAddNew
            // 
            this.btnAddNew.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddNew.Location = new System.Drawing.Point(313, 50);
            this.btnAddNew.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(65, 32);
            this.btnAddNew.TabIndex = 3;
            this.btnAddNew.Text = "Add";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // tb_nameModule
            // 
            this.tb_nameModule.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_nameModule.Location = new System.Drawing.Point(8, 53);
            this.tb_nameModule.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tb_nameModule.Name = "tb_nameModule";
            this.tb_nameModule.Size = new System.Drawing.Size(298, 26);
            this.tb_nameModule.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10.8F);
            this.label1.Location = new System.Drawing.Point(5, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Module name:";
            // 
            // labelApplication
            // 
            this.labelApplication.AutoSize = true;
            this.labelApplication.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelApplication.Location = new System.Drawing.Point(9, 20);
            this.labelApplication.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelApplication.Name = "labelApplication";
            this.labelApplication.Size = new System.Drawing.Size(180, 24);
            this.labelApplication.TabIndex = 2;
            this.labelApplication.Text = "ApplicationName";
            // 
            // gb_Modules
            // 
            this.gb_Modules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_Modules.Controls.Add(this.btn_connections);
            this.gb_Modules.Controls.Add(this.btn_subscriptions);
            this.gb_Modules.Controls.Add(this.tb_search);
            this.gb_Modules.Controls.Add(this.button3);
            this.gb_Modules.Controls.Add(this.btn_delete);
            this.gb_Modules.Controls.Add(this.btn_update);
            this.gb_Modules.Controls.Add(this.lb_modules);
            this.gb_Modules.Location = new System.Drawing.Point(13, 53);
            this.gb_Modules.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gb_Modules.Name = "gb_Modules";
            this.gb_Modules.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gb_Modules.Size = new System.Drawing.Size(308, 394);
            this.gb_Modules.TabIndex = 3;
            this.gb_Modules.TabStop = false;
            this.gb_Modules.Text = "Modules";
            // 
            // btn_connections
            // 
            this.btn_connections.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.btn_connections.Location = new System.Drawing.Point(146, 337);
            this.btn_connections.Name = "btn_connections";
            this.btn_connections.Size = new System.Drawing.Size(131, 35);
            this.btn_connections.TabIndex = 10;
            this.btn_connections.Text = "Connections";
            this.btn_connections.UseVisualStyleBackColor = true;
            this.btn_connections.Click += new System.EventHandler(this.btn_connections_Click);
            // 
            // btn_subscriptions
            // 
            this.btn_subscriptions.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.btn_subscriptions.Location = new System.Drawing.Point(20, 337);
            this.btn_subscriptions.Name = "btn_subscriptions";
            this.btn_subscriptions.Size = new System.Drawing.Size(120, 35);
            this.btn_subscriptions.TabIndex = 9;
            this.btn_subscriptions.Text = "Subscriptions";
            this.btn_subscriptions.UseVisualStyleBackColor = true;
            this.btn_subscriptions.Click += new System.EventHandler(this.btn_Subscriptions_Click);
            // 
            // tb_search
            // 
            this.tb_search.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_search.Location = new System.Drawing.Point(20, 252);
            this.tb_search.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tb_search.Name = "tb_search";
            this.tb_search.Size = new System.Drawing.Size(212, 26);
            this.tb_search.TabIndex = 8;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(237, 253);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(40, 26);
            this.button3.TabIndex = 7;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_delete.Location = new System.Drawing.Point(146, 286);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(131, 45);
            this.btn_delete.TabIndex = 6;
            this.btn_delete.Text = "Delete";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_update
            // 
            this.btn_update.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_update.Location = new System.Drawing.Point(20, 286);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(120, 45);
            this.btn_update.TabIndex = 5;
            this.btn_update.Text = "Update";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // lb_modules
            // 
            this.lb_modules.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_modules.FormattingEnabled = true;
            this.lb_modules.ItemHeight = 18;
            this.lb_modules.Location = new System.Drawing.Point(20, 27);
            this.lb_modules.Name = "lb_modules";
            this.lb_modules.Size = new System.Drawing.Size(257, 184);
            this.lb_modules.TabIndex = 0;
            this.lb_modules.SelectedIndexChanged += new System.EventHandler(this.lb_modules_SelectedIndexChanged);
            // 
            // btn_apagarApp
            // 
            this.btn_apagarApp.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_apagarApp.Image = ((System.Drawing.Image)(resources.GetObject("btn_apagarApp.Image")));
            this.btn_apagarApp.Location = new System.Drawing.Point(749, 13);
            this.btn_apagarApp.Name = "btn_apagarApp";
            this.btn_apagarApp.Size = new System.Drawing.Size(74, 31);
            this.btn_apagarApp.TabIndex = 4;
            this.btn_apagarApp.Text = "Delete";
            this.btn_apagarApp.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_apagarApp.UseVisualStyleBackColor = true;
            this.btn_apagarApp.Click += new System.EventHandler(this.btn_apagarApp_Click);
            // 
            // btn_updateApp
            // 
            this.btn_updateApp.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_updateApp.Image = ((System.Drawing.Image)(resources.GetObject("btn_updateApp.Image")));
            this.btn_updateApp.Location = new System.Drawing.Point(666, 13);
            this.btn_updateApp.Name = "btn_updateApp";
            this.btn_updateApp.Size = new System.Drawing.Size(77, 31);
            this.btn_updateApp.TabIndex = 5;
            this.btn_updateApp.Text = "Update";
            this.btn_updateApp.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_updateApp.UseVisualStyleBackColor = true;
            this.btn_updateApp.Click += new System.EventHandler(this.btn_updateApp_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Application_view
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 456);
            this.Controls.Add(this.btn_updateApp);
            this.Controls.Add(this.btn_apagarApp);
            this.Controls.Add(this.gb_Modules);
            this.Controls.Add(this.labelApplication);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "Application_view";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "appName";
            this.groupBox1.ResumeLayout(false);
            this.gbCommands.ResumeLayout(false);
            this.gbCommands.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_image)).EndInit();
            this.gbNewModule.ResumeLayout(false);
            this.gbNewModule.PerformLayout();
            this.gb_Modules.ResumeLayout(false);
            this.gb_Modules.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbNewModule;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_nameModule;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.GroupBox gbCommands;
        private System.Windows.Forms.ListBox lb_Commands;
        private System.Windows.Forms.Button btnCommands;
        private System.Windows.Forms.Label labelApplication;
        private System.Windows.Forms.GroupBox gb_Modules;
        private System.Windows.Forms.ListBox lb_modules;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Button btn_apagarApp;
        private System.Windows.Forms.Button btn_updateApp;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox tb_search;
        private System.Windows.Forms.Label lbsomiodnames;
        private System.Windows.Forms.Label lbsomiod;
        private System.Windows.Forms.Button btn_subscriptions;
        private System.Windows.Forms.Button btn_connections;
        private System.Windows.Forms.PictureBox pb_image;
        private System.Windows.Forms.ImageList imageList1;
    }
}