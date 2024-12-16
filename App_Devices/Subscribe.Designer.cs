namespace App_Devices
{
    partial class Subscribe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Subscribe));
            this.cb_events = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbNewModule = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_subscriptionEndpoint = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_nameSubscription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lb_SubModName = new System.Windows.Forms.Label();
            this.gb_Modules = new System.Windows.Forms.GroupBox();
            this.btn_delete = new System.Windows.Forms.Button();
            this.lb_subscriptions = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.gbNewModule.SuspendLayout();
            this.gb_Modules.SuspendLayout();
            this.SuspendLayout();
            // 
            // cb_events
            // 
            this.cb_events.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_events.Location = new System.Drawing.Point(7, 180);
            this.cb_events.Name = "cb_events";
            this.cb_events.Size = new System.Drawing.Size(121, 28);
            this.cb_events.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.gbNewModule);
            this.groupBox1.Controls.Add(this.btnAddNew);
            this.groupBox1.Location = new System.Drawing.Point(444, 49);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(283, 339);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // gbNewModule
            // 
            this.gbNewModule.Controls.Add(this.label3);
            this.gbNewModule.Controls.Add(this.tb_subscriptionEndpoint);
            this.gbNewModule.Controls.Add(this.label1);
            this.gbNewModule.Controls.Add(this.cb_events);
            this.gbNewModule.Controls.Add(this.tb_nameSubscription);
            this.gbNewModule.Controls.Add(this.label2);
            this.gbNewModule.Location = new System.Drawing.Point(16, 27);
            this.gbNewModule.Margin = new System.Windows.Forms.Padding(2);
            this.gbNewModule.Name = "gbNewModule";
            this.gbNewModule.Padding = new System.Windows.Forms.Padding(2);
            this.gbNewModule.Size = new System.Drawing.Size(255, 238);
            this.gbNewModule.TabIndex = 0;
            this.gbNewModule.TabStop = false;
            this.gbNewModule.Text = "New Subscription";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10.8F);
            this.label3.Location = new System.Drawing.Point(4, 160);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Subscription event";
            // 
            // tb_subscriptionEndpoint
            // 
            this.tb_subscriptionEndpoint.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_subscriptionEndpoint.Location = new System.Drawing.Point(7, 113);
            this.tb_subscriptionEndpoint.Margin = new System.Windows.Forms.Padding(2);
            this.tb_subscriptionEndpoint.Name = "tb_subscriptionEndpoint";
            this.tb_subscriptionEndpoint.Size = new System.Drawing.Size(234, 26);
            this.tb_subscriptionEndpoint.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10.8F);
            this.label1.Location = new System.Drawing.Point(4, 94);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Subscription endpoint";
            // 
            // tb_nameSubscription
            // 
            this.tb_nameSubscription.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_nameSubscription.Location = new System.Drawing.Point(8, 44);
            this.tb_nameSubscription.Margin = new System.Windows.Forms.Padding(2);
            this.tb_nameSubscription.Name = "tb_nameSubscription";
            this.tb_nameSubscription.Size = new System.Drawing.Size(234, 26);
            this.tb_nameSubscription.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10.8F);
            this.label2.Location = new System.Drawing.Point(5, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Subscription name:";
            // 
            // btnAddNew
            // 
            this.btnAddNew.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddNew.Location = new System.Drawing.Point(16, 282);
            this.btnAddNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(128, 45);
            this.btnAddNew.TabIndex = 3;
            this.btnAddNew.Text = "Add subscription";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tempus Sans ITC", 36F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.IndianRed;
            this.label4.Location = new System.Drawing.Point(245, 275);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 62);
            this.label4.TabIndex = 4;
            this.label4.Text = "MQTT";
            // 
            // lb_SubModName
            // 
            this.lb_SubModName.AutoSize = true;
            this.lb_SubModName.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_SubModName.Location = new System.Drawing.Point(11, 23);
            this.lb_SubModName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_SubModName.Name = "lb_SubModName";
            this.lb_SubModName.Size = new System.Drawing.Size(325, 24);
            this.lb_SubModName.TabIndex = 6;
            this.lb_SubModName.Text = "Subscripions of \"ModuleName\"";
            // 
            // gb_Modules
            // 
            this.gb_Modules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_Modules.Controls.Add(this.label4);
            this.gb_Modules.Controls.Add(this.btn_delete);
            this.gb_Modules.Controls.Add(this.lb_subscriptions);
            this.gb_Modules.ForeColor = System.Drawing.Color.LimeGreen;
            this.gb_Modules.Location = new System.Drawing.Point(15, 49);
            this.gb_Modules.Margin = new System.Windows.Forms.Padding(2);
            this.gb_Modules.Name = "gb_Modules";
            this.gb_Modules.Padding = new System.Windows.Forms.Padding(2);
            this.gb_Modules.Size = new System.Drawing.Size(408, 339);
            this.gb_Modules.TabIndex = 7;
            this.gb_Modules.TabStop = false;
            this.gb_Modules.Text = " Active Subscriptions";
            // 
            // btn_delete
            // 
            this.btn_delete.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_delete.ForeColor = System.Drawing.Color.Black;
            this.btn_delete.Location = new System.Drawing.Point(20, 281);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(131, 45);
            this.btn_delete.TabIndex = 6;
            this.btn_delete.Text = "Delete";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // lb_subscriptions
            // 
            this.lb_subscriptions.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_subscriptions.FormattingEnabled = true;
            this.lb_subscriptions.ItemHeight = 22;
            this.lb_subscriptions.Location = new System.Drawing.Point(20, 27);
            this.lb_subscriptions.Name = "lb_subscriptions";
            this.lb_subscriptions.Size = new System.Drawing.Size(372, 224);
            this.lb_subscriptions.TabIndex = 0;
            // 
            // Subscribe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 399);
            this.Controls.Add(this.gb_Modules);
            this.Controls.Add(this.lb_SubModName);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Subscribe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Subscribe";
            this.groupBox1.ResumeLayout(false);
            this.gbNewModule.ResumeLayout(false);
            this.gbNewModule.PerformLayout();
            this.gb_Modules.ResumeLayout(false);
            this.gb_Modules.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cb_events;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbNewModule;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.TextBox tb_nameSubscription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_SubModName;
        private System.Windows.Forms.GroupBox gb_Modules;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.ListBox lb_subscriptions;
        private System.Windows.Forms.TextBox tb_subscriptionEndpoint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}