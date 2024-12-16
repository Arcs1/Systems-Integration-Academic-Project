namespace Test_Application
{
    partial class SubscribeApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubscribeApp));
            this.lb_apps = new System.Windows.Forms.ListBox();
            this.btn_sub_app = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_apps
            // 
            this.lb_apps.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_apps.FormattingEnabled = true;
            this.lb_apps.ItemHeight = 18;
            this.lb_apps.Location = new System.Drawing.Point(8, 10);
            this.lb_apps.Margin = new System.Windows.Forms.Padding(2);
            this.lb_apps.Name = "lb_apps";
            this.lb_apps.Size = new System.Drawing.Size(271, 292);
            this.lb_apps.TabIndex = 0;
            this.lb_apps.SelectedIndexChanged += new System.EventHandler(this.lb_apps_SelectedIndexChanged);
            // 
            // btn_sub_app
            // 
            this.btn_sub_app.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_sub_app.Location = new System.Drawing.Point(8, 317);
            this.btn_sub_app.Margin = new System.Windows.Forms.Padding(2);
            this.btn_sub_app.Name = "btn_sub_app";
            this.btn_sub_app.Size = new System.Drawing.Size(270, 41);
            this.btn_sub_app.TabIndex = 1;
            this.btn_sub_app.Text = "Subscribe Application";
            this.btn_sub_app.UseVisualStyleBackColor = true;
            this.btn_sub_app.Click += new System.EventHandler(this.btn_sub_app_Click);
            // 
            // SubscribeApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 368);
            this.Controls.Add(this.btn_sub_app);
            this.Controls.Add(this.lb_apps);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "SubscribeApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Subscribe";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lb_apps;
        private System.Windows.Forms.Button btn_sub_app;
    }
}