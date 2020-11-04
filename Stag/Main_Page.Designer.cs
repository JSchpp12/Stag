namespace Stag
{
    partial class Main_Page
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
            System.Windows.Forms.Button btn_openFile;
            System.Windows.Forms.Button Save;
            this.process1 = new System.Diagnostics.Process();
            this.txtBx_filePath = new System.Windows.Forms.TextBox();
            this.pic_orgImage = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnl_msg = new System.Windows.Forms.Panel();
            this.lbl_currChars = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_maxMsgSizeVal = new System.Windows.Forms.Label();
            this.lbl_maxMsgSize = new System.Windows.Forms.Label();
            this.lbl_msgEnc = new System.Windows.Forms.Label();
            this.rTxt_imageMsg = new System.Windows.Forms.RichTextBox();
            this.lbl_origImage = new System.Windows.Forms.Label();
            this.pic_modifiedImage = new System.Windows.Forms.PictureBox();
            this.lbl_modifiedImage = new System.Windows.Forms.Label();
            btn_openFile = new System.Windows.Forms.Button();
            Save = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pic_orgImage)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.pnl_msg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_modifiedImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_openFile
            // 
            btn_openFile.Location = new System.Drawing.Point(525, 376);
            btn_openFile.Name = "btn_openFile";
            btn_openFile.Size = new System.Drawing.Size(86, 29);
            btn_openFile.TabIndex = 1;
            btn_openFile.Text = "Open";
            btn_openFile.UseVisualStyleBackColor = true;
            btn_openFile.Click += new System.EventHandler(this.btn_openFile_Click);
            // 
            // Save
            // 
            Save.Location = new System.Drawing.Point(546, 51);
            Save.Name = "Save";
            Save.Size = new System.Drawing.Size(86, 29);
            Save.TabIndex = 11;
            Save.Text = "Save";
            Save.UseVisualStyleBackColor = true;
            Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // process1
            // 
            this.process1.StartInfo.Domain = "";
            this.process1.StartInfo.LoadUserProfile = false;
            this.process1.StartInfo.Password = null;
            this.process1.StartInfo.StandardErrorEncoding = null;
            this.process1.StartInfo.StandardOutputEncoding = null;
            this.process1.StartInfo.UserName = "";
            this.process1.SynchronizingObject = this;
            // 
            // txtBx_filePath
            // 
            this.txtBx_filePath.Location = new System.Drawing.Point(39, 379);
            this.txtBx_filePath.Name = "txtBx_filePath";
            this.txtBx_filePath.ReadOnly = true;
            this.txtBx_filePath.Size = new System.Drawing.Size(480, 22);
            this.txtBx_filePath.TabIndex = 0;
            this.txtBx_filePath.Text = "File Path";
            // 
            // pic_orgImage
            // 
            this.pic_orgImage.Location = new System.Drawing.Point(53, 80);
            this.pic_orgImage.Name = "pic_orgImage";
            this.pic_orgImage.Size = new System.Drawing.Size(569, 275);
            this.pic_orgImage.TabIndex = 2;
            this.pic_orgImage.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1258, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.saveToolStripMenuItem1});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.saveToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(128, 26);
            this.saveToolStripMenuItem1.Text = "Save";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // pnl_msg
            // 
            this.pnl_msg.Controls.Add(this.lbl_currChars);
            this.pnl_msg.Controls.Add(this.label1);
            this.pnl_msg.Controls.Add(this.lbl_maxMsgSizeVal);
            this.pnl_msg.Controls.Add(this.lbl_maxMsgSize);
            this.pnl_msg.Controls.Add(this.lbl_msgEnc);
            this.pnl_msg.Controls.Add(Save);
            this.pnl_msg.Controls.Add(this.rTxt_imageMsg);
            this.pnl_msg.Location = new System.Drawing.Point(30, 411);
            this.pnl_msg.Name = "pnl_msg";
            this.pnl_msg.Size = new System.Drawing.Size(678, 196);
            this.pnl_msg.TabIndex = 5;
            this.pnl_msg.Visible = false;
            // 
            // lbl_currChars
            // 
            this.lbl_currChars.AutoSize = true;
            this.lbl_currChars.Location = new System.Drawing.Point(614, 167);
            this.lbl_currChars.Name = "lbl_currChars";
            this.lbl_currChars.Size = new System.Drawing.Size(16, 17);
            this.lbl_currChars.TabIndex = 16;
            this.lbl_currChars.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(547, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "# Chars:";
            // 
            // lbl_maxMsgSizeVal
            // 
            this.lbl_maxMsgSizeVal.AutoSize = true;
            this.lbl_maxMsgSizeVal.Location = new System.Drawing.Point(168, 13);
            this.lbl_maxMsgSizeVal.Name = "lbl_maxMsgSizeVal";
            this.lbl_maxMsgSizeVal.Size = new System.Drawing.Size(46, 17);
            this.lbl_maxMsgSizeVal.TabIndex = 14;
            this.lbl_maxMsgSizeVal.Text = "label1";
            // 
            // lbl_maxMsgSize
            // 
            this.lbl_maxMsgSize.AutoSize = true;
            this.lbl_maxMsgSize.Location = new System.Drawing.Point(-1, 13);
            this.lbl_maxMsgSize.Name = "lbl_maxMsgSize";
            this.lbl_maxMsgSize.Size = new System.Drawing.Size(162, 17);
            this.lbl_maxMsgSize.TabIndex = 13;
            this.lbl_maxMsgSize.Text = "Maximum Message Size:";
            // 
            // lbl_msgEnc
            // 
            this.lbl_msgEnc.AutoSize = true;
            this.lbl_msgEnc.Location = new System.Drawing.Point(-1, 30);
            this.lbl_msgEnc.Name = "lbl_msgEnc";
            this.lbl_msgEnc.Size = new System.Drawing.Size(178, 17);
            this.lbl_msgEnc.TabIndex = 12;
            this.lbl_msgEnc.Text = "Message Ecoded In Image:";
            // 
            // rTxt_imageMsg
            // 
            this.rTxt_imageMsg.Location = new System.Drawing.Point(2, 51);
            this.rTxt_imageMsg.Name = "rTxt_imageMsg";
            this.rTxt_imageMsg.Size = new System.Drawing.Size(538, 133);
            this.rTxt_imageMsg.TabIndex = 10;
            this.rTxt_imageMsg.Text = "";
            this.rTxt_imageMsg.TextChanged += new System.EventHandler(this.rTxt_imageMsg_TextChanged);
            // 
            // lbl_origImage
            // 
            this.lbl_origImage.AutoSize = true;
            this.lbl_origImage.Location = new System.Drawing.Point(50, 60);
            this.lbl_origImage.Name = "lbl_origImage";
            this.lbl_origImage.Size = new System.Drawing.Size(111, 17);
            this.lbl_origImage.TabIndex = 6;
            this.lbl_origImage.Text = "Origional Image:";
            // 
            // pic_modifiedImage
            // 
            this.pic_modifiedImage.Location = new System.Drawing.Point(677, 80);
            this.pic_modifiedImage.Name = "pic_modifiedImage";
            this.pic_modifiedImage.Size = new System.Drawing.Size(569, 275);
            this.pic_modifiedImage.TabIndex = 7;
            this.pic_modifiedImage.TabStop = false;
            // 
            // lbl_modifiedImage
            // 
            this.lbl_modifiedImage.AutoSize = true;
            this.lbl_modifiedImage.Location = new System.Drawing.Point(674, 60);
            this.lbl_modifiedImage.Name = "lbl_modifiedImage";
            this.lbl_modifiedImage.Size = new System.Drawing.Size(107, 17);
            this.lbl_modifiedImage.TabIndex = 8;
            this.lbl_modifiedImage.Text = "Modified Image:";
            // 
            // Main_Page
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1258, 625);
            this.Controls.Add(this.lbl_modifiedImage);
            this.Controls.Add(this.pic_modifiedImage);
            this.Controls.Add(this.lbl_origImage);
            this.Controls.Add(this.pnl_msg);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pic_orgImage);
            this.Controls.Add(btn_openFile);
            this.Controls.Add(this.txtBx_filePath);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main_Page";
            this.Text = "Image Steganography";
            ((System.ComponentModel.ISupportInitialize)(this.pic_orgImage)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnl_msg.ResumeLayout(false);
            this.pnl_msg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_modifiedImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Diagnostics.Process process1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.PictureBox pic_orgImage;
        private System.Windows.Forms.TextBox txtBx_filePath;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Panel pnl_msg;
        private System.Windows.Forms.Label lbl_maxMsgSizeVal;
        private System.Windows.Forms.Label lbl_maxMsgSize;
        private System.Windows.Forms.Label lbl_msgEnc;
        private System.Windows.Forms.RichTextBox rTxt_imageMsg;
        private System.Windows.Forms.Label lbl_currChars;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_modifiedImage;
        private System.Windows.Forms.PictureBox pic_modifiedImage;
        private System.Windows.Forms.Label lbl_origImage;
    }
}

