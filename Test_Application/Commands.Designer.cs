namespace Test_Application
{
    partial class Commands
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Commands));
            this.labelModule = new System.Windows.Forms.Label();
            this.tb_search = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.lb_commands = new System.Windows.Forms.ListBox();
            this.gb_Modules = new System.Windows.Forms.GroupBox();
            this.gb_images = new System.Windows.Forms.GroupBox();
            this.pb_image = new System.Windows.Forms.PictureBox();
            this.lbsomiodnames = new System.Windows.Forms.Label();
            this.lbsomiod = new System.Windows.Forms.Label();
            this.gbcommand = new System.Windows.Forms.GroupBox();
            this.btnSaveEdit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.textCommand = new System.Windows.Forms.RichTextBox();
            this.textNameCommand = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btn_Execute = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.gb_Modules.SuspendLayout();
            this.gb_images.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_image)).BeginInit();
            this.gbcommand.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelModule
            // 
            this.labelModule.AutoSize = true;
            this.labelModule.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelModule.Location = new System.Drawing.Point(9, 15);
            this.labelModule.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelModule.Name = "labelModule";
            this.labelModule.Size = new System.Drawing.Size(140, 24);
            this.labelModule.TabIndex = 6;
            this.labelModule.Text = "ModuleName";
            // 
            // tb_search
            // 
            this.tb_search.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_search.Location = new System.Drawing.Point(18, 25);
            this.tb_search.Margin = new System.Windows.Forms.Padding(2);
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
            // lb_commands
            // 
            this.lb_commands.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_commands.FormattingEnabled = true;
            this.lb_commands.ItemHeight = 22;
            this.lb_commands.Location = new System.Drawing.Point(18, 58);
            this.lb_commands.Name = "lb_commands";
            this.lb_commands.Size = new System.Drawing.Size(268, 224);
            this.lb_commands.TabIndex = 0;
            this.lb_commands.SelectedIndexChanged += new System.EventHandler(this.lb_commands_SelectedIndexChanged);
            // 
            // gb_Modules
            // 
            this.gb_Modules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_Modules.Controls.Add(this.gb_images);
            this.gb_Modules.Controls.Add(this.gbcommand);
            this.gb_Modules.Controls.Add(this.btnCreate);
            this.gb_Modules.Controls.Add(this.btnEdit);
            this.gb_Modules.Controls.Add(this.btn_Execute);
            this.gb_Modules.Controls.Add(this.tb_search);
            this.gb_Modules.Controls.Add(this.btnDelete);
            this.gb_Modules.Controls.Add(this.button3);
            this.gb_Modules.Controls.Add(this.lb_commands);
            this.gb_Modules.Location = new System.Drawing.Point(13, 48);
            this.gb_Modules.Margin = new System.Windows.Forms.Padding(2);
            this.gb_Modules.Name = "gb_Modules";
            this.gb_Modules.Padding = new System.Windows.Forms.Padding(2);
            this.gb_Modules.Size = new System.Drawing.Size(614, 404);
            this.gb_Modules.TabIndex = 7;
            this.gb_Modules.TabStop = false;
            this.gb_Modules.Text = "Commands";
            // 
            // gb_images
            // 
            this.gb_images.Controls.Add(this.pb_image);
            this.gb_images.Controls.Add(this.lbsomiodnames);
            this.gb_images.Controls.Add(this.lbsomiod);
            this.gb_images.Location = new System.Drawing.Point(302, 18);
            this.gb_images.Name = "gb_images";
            this.gb_images.Size = new System.Drawing.Size(307, 373);
            this.gb_images.TabIndex = 14;
            this.gb_images.TabStop = false;
            // 
            // pb_image
            // 
            this.pb_image.Image = ((System.Drawing.Image)(resources.GetObject("pb_image.Image")));
            this.pb_image.InitialImage = ((System.Drawing.Image)(resources.GetObject("pb_image.InitialImage")));
            this.pb_image.Location = new System.Drawing.Point(14, 24);
            this.pb_image.Name = "pb_image";
            this.pb_image.Size = new System.Drawing.Size(286, 252);
            this.pb_image.TabIndex = 10;
            this.pb_image.TabStop = false;
            // 
            // lbsomiodnames
            // 
            this.lbsomiodnames.AutoSize = true;
            this.lbsomiodnames.Font = new System.Drawing.Font("Lucida Calligraphy", 5F);
            this.lbsomiodnames.ForeColor = System.Drawing.Color.DimGray;
            this.lbsomiodnames.Location = new System.Drawing.Point(21, 329);
            this.lbsomiodnames.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbsomiodnames.Name = "lbsomiodnames";
            this.lbsomiodnames.Size = new System.Drawing.Size(266, 8);
            this.lbsomiodnames.TabIndex = 8;
            this.lbsomiodnames.Text = "© Made by: Tiago Guardado, Tomás Neves, João Pinto, Célsio Vinagre.";
            // 
            // lbsomiod
            // 
            this.lbsomiod.AutoSize = true;
            this.lbsomiod.Font = new System.Drawing.Font("Lucida Handwriting", 20F, System.Drawing.FontStyle.Bold);
            this.lbsomiod.Location = new System.Drawing.Point(32, 293);
            this.lbsomiod.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbsomiod.Name = "lbsomiod";
            this.lbsomiod.Size = new System.Drawing.Size(243, 36);
            this.lbsomiod.TabIndex = 9;
            this.lbsomiod.Text = "SOMIOD TESTS";
            // 
            // gbcommand
            // 
            this.gbcommand.Controls.Add(this.btnSaveEdit);
            this.gbcommand.Controls.Add(this.btnCancel);
            this.gbcommand.Controls.Add(this.btnSave);
            this.gbcommand.Controls.Add(this.textCommand);
            this.gbcommand.Controls.Add(this.textNameCommand);
            this.gbcommand.Controls.Add(this.label3);
            this.gbcommand.Controls.Add(this.label2);
            this.gbcommand.Location = new System.Drawing.Point(311, 25);
            this.gbcommand.Margin = new System.Windows.Forms.Padding(2);
            this.gbcommand.Name = "gbcommand";
            this.gbcommand.Padding = new System.Windows.Forms.Padding(2);
            this.gbcommand.Size = new System.Drawing.Size(288, 345);
            this.gbcommand.TabIndex = 13;
            this.gbcommand.TabStop = false;
            // 
            // btnSaveEdit
            // 
            this.btnSaveEdit.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSaveEdit.Location = new System.Drawing.Point(149, 284);
            this.btnSaveEdit.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveEdit.Name = "btnSaveEdit";
            this.btnSaveEdit.Size = new System.Drawing.Size(88, 40);
            this.btnSaveEdit.TabIndex = 6;
            this.btnSaveEdit.Text = "Guardar";
            this.btnSaveEdit.UseVisualStyleBackColor = true;
            this.btnSaveEdit.Click += new System.EventHandler(this.btnSaveEdit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(17, 283);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 40);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(149, 284);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 40);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Guardar";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // textCommand
            // 
            this.textCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textCommand.Location = new System.Drawing.Point(17, 100);
            this.textCommand.Margin = new System.Windows.Forms.Padding(2);
            this.textCommand.Name = "textCommand";
            this.textCommand.Size = new System.Drawing.Size(220, 171);
            this.textCommand.TabIndex = 3;
            this.textCommand.Text = "";
            // 
            // textNameCommand
            // 
            this.textNameCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNameCommand.Location = new System.Drawing.Point(72, 36);
            this.textNameCommand.Margin = new System.Windows.Forms.Padding(2);
            this.textNameCommand.Name = "textNameCommand";
            this.textNameCommand.Size = new System.Drawing.Size(137, 24);
            this.textNameCommand.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 80);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 18);
            this.label3.TabIndex = 1;
            this.label3.Text = "Comando:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 35);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Name:";
            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Location = new System.Drawing.Point(200, 352);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(2);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(86, 40);
            this.btnCreate.TabIndex = 12;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(18, 353);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(86, 39);
            this.btnEdit.TabIndex = 11;
            this.btnEdit.Text = "Edit/View";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btn_Execute
            // 
            this.btn_Execute.BackColor = System.Drawing.Color.OliveDrab;
            this.btn_Execute.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Execute.Location = new System.Drawing.Point(18, 308);
            this.btn_Execute.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Execute.Name = "btn_Execute";
            this.btn_Execute.Size = new System.Drawing.Size(267, 41);
            this.btn_Execute.TabIndex = 9;
            this.btn_Execute.Text = "Execute";
            this.btn_Execute.UseVisualStyleBackColor = false;
            this.btn_Execute.Click += new System.EventHandler(this.btn_Execute_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(108, 352);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(86, 40);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // Commands
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 461);
            this.Controls.Add(this.labelModule);
            this.Controls.Add(this.gb_Modules);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Commands";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Commands";
            this.gb_Modules.ResumeLayout(false);
            this.gb_Modules.PerformLayout();
            this.gb_images.ResumeLayout(false);
            this.gb_images.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_image)).EndInit();
            this.gbcommand.ResumeLayout(false);
            this.gbcommand.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelModule;
        private System.Windows.Forms.TextBox tb_search;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox lb_commands;
        private System.Windows.Forms.GroupBox gb_Modules;
        private System.Windows.Forms.Button btn_Execute;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.GroupBox gbcommand;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.RichTextBox textCommand;
        private System.Windows.Forms.TextBox textNameCommand;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSaveEdit;
        private System.Windows.Forms.PictureBox pb_image;
        private System.Windows.Forms.Label lbsomiodnames;
        private System.Windows.Forms.Label lbsomiod;
        private System.Windows.Forms.GroupBox gb_images;
    }
}