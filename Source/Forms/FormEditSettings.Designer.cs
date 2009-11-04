//---------------------------------------------------------------------------------------
// <copyright file="FormEditSettings.Designer.cs" company="Jonathan Mathews Software">
//     ASCII Generator dotNET - Image to ASCII Art Conversion Program
//     Copyright (C) 2009 Jonathan Mathews Software. All rights reserved.
// </copyright>
// <author>Jonathan Mathews</author>
// <email>info@jmsoftware.co.uk</email>
// <email>jmsoftware@gmail.com</email>
// <website>http://www.jmsoftware.co.uk/</website>
// <website>http://ascgen2.sourceforge.net/</website>
// <license>
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the license, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/.
// </license>
//---------------------------------------------------------------------------------------
namespace JMSoftware.AsciiGeneratorDotNet
{
    /// <summary>
    /// Form to edit the settings
    /// </summary>
    public partial class FormEditSettings
    {
        #region Fields

        private System.Windows.Forms.Button btnCancel;

        private System.Windows.Forms.Button btnFont;

        private System.Windows.Forms.Button btnInputDirectory;

        private System.Windows.Forms.Button btnOk;

        private System.Windows.Forms.Button btnOutputDirectory;

        private System.Windows.Forms.Button btnSave;

        private System.Windows.Forms.CheckBox cbxConfirmClose;

        private System.Windows.Forms.CheckBox cbxConfirmVersionCheck;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ErrorProvider errorProvider1;

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;

        private System.Windows.Forms.FontDialog fontDialog1;

        private System.Windows.Forms.Label lblInputDirectory;

        private System.Windows.Forms.Label lblOutputDirectory;

        private System.Windows.Forms.TabPage pageBasic;

        private System.Windows.Forms.TabControl tabSettings;

        private System.Windows.Forms.TextBox tbxFont;

        private System.Windows.Forms.TextBox tbxInputDirectory;

        private System.Windows.Forms.TextBox tbxOutputDirectory;

        #endregion Fields

        #region Protected methods

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

        #endregion Protected methods

        #region Private methods

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.pageBasic = new System.Windows.Forms.TabPage();
            this.tbxFont = new System.Windows.Forms.TextBox();
            this.cbxConfirmVersionCheck = new System.Windows.Forms.CheckBox();
            this.cbxConfirmClose = new System.Windows.Forms.CheckBox();
            this.btnFont = new System.Windows.Forms.Button();
            this.tbxOutputDirectory = new System.Windows.Forms.TextBox();
            this.tbxInputDirectory = new System.Windows.Forms.TextBox();
            this.btnOutputDirectory = new System.Windows.Forms.Button();
            this.lblOutputDirectory = new System.Windows.Forms.Label();
            this.btnInputDirectory = new System.Windows.Forms.Button();
            this.lblInputDirectory = new System.Windows.Forms.Label();
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.pageBasic.SuspendLayout();
            this.tabSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(12, 189);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(266, 189);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(139, 189);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Visible = false;
            // 
            // pageBasic
            // 
            this.pageBasic.Controls.Add(this.tbxFont);
            this.pageBasic.Controls.Add(this.cbxConfirmVersionCheck);
            this.pageBasic.Controls.Add(this.cbxConfirmClose);
            this.pageBasic.Controls.Add(this.btnFont);
            this.pageBasic.Controls.Add(this.tbxOutputDirectory);
            this.pageBasic.Controls.Add(this.tbxInputDirectory);
            this.pageBasic.Controls.Add(this.btnOutputDirectory);
            this.pageBasic.Controls.Add(this.lblOutputDirectory);
            this.pageBasic.Controls.Add(this.btnInputDirectory);
            this.pageBasic.Controls.Add(this.lblInputDirectory);
            this.pageBasic.Location = new System.Drawing.Point(4, 22);
            this.pageBasic.Name = "pageBasic";
            this.pageBasic.Padding = new System.Windows.Forms.Padding(3);
            this.pageBasic.Size = new System.Drawing.Size(321, 145);
            this.pageBasic.TabIndex = 4;
            this.pageBasic.Text = "pageBasic";
            this.pageBasic.UseVisualStyleBackColor = true;
            // 
            // tbxFont
            // 
            this.tbxFont.Location = new System.Drawing.Point(99, 80);
            this.tbxFont.Name = "tbxFont";
            this.tbxFont.ReadOnly = true;
            this.tbxFont.Size = new System.Drawing.Size(216, 20);
            this.tbxFont.TabIndex = 7;
            // 
            // cbxConfirmVersionCheck
            // 
            this.cbxConfirmVersionCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxConfirmVersionCheck.AutoSize = true;
            this.cbxConfirmVersionCheck.Location = new System.Drawing.Point(168, 122);
            this.cbxConfirmVersionCheck.Name = "cbxConfirmVersionCheck";
            this.cbxConfirmVersionCheck.Size = new System.Drawing.Size(144, 17);
            this.cbxConfirmVersionCheck.TabIndex = 9;
            this.cbxConfirmVersionCheck.Text = "cbxConfirmVersionCheck";
            this.cbxConfirmVersionCheck.UseVisualStyleBackColor = true;
            // 
            // cbxConfirmClose
            // 
            this.cbxConfirmClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxConfirmClose.AutoSize = true;
            this.cbxConfirmClose.Location = new System.Drawing.Point(6, 122);
            this.cbxConfirmClose.Name = "cbxConfirmClose";
            this.cbxConfirmClose.Size = new System.Drawing.Size(104, 17);
            this.cbxConfirmClose.TabIndex = 8;
            this.cbxConfirmClose.Text = "cbxConfirmClose";
            this.cbxConfirmClose.UseVisualStyleBackColor = true;
            // 
            // btnFont
            // 
            this.btnFont.Location = new System.Drawing.Point(6, 78);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(75, 23);
            this.btnFont.TabIndex = 6;
            this.btnFont.Text = "btnFont";
            this.btnFont.UseVisualStyleBackColor = true;
            this.btnFont.Click += new System.EventHandler(this.BtnFont_Click);
            // 
            // tbxOutputDirectory
            // 
            this.tbxOutputDirectory.Location = new System.Drawing.Point(99, 38);
            this.tbxOutputDirectory.Name = "tbxOutputDirectory";
            this.tbxOutputDirectory.Size = new System.Drawing.Size(180, 20);
            this.tbxOutputDirectory.TabIndex = 4;
            // 
            // tbxInputDirectory
            // 
            this.tbxInputDirectory.Location = new System.Drawing.Point(99, 12);
            this.tbxInputDirectory.Name = "tbxInputDirectory";
            this.tbxInputDirectory.Size = new System.Drawing.Size(180, 20);
            this.tbxInputDirectory.TabIndex = 1;
            // 
            // btnOutputDirectory
            // 
            this.btnOutputDirectory.Image = global::AscGenDotNet.Properties.Resources.folder;
            this.btnOutputDirectory.Location = new System.Drawing.Point(285, 36);
            this.btnOutputDirectory.Name = "btnOutputDirectory";
            this.btnOutputDirectory.Size = new System.Drawing.Size(30, 23);
            this.btnOutputDirectory.TabIndex = 5;
            this.btnOutputDirectory.UseVisualStyleBackColor = true;
            this.btnOutputDirectory.Click += new System.EventHandler(this.BtnOutputDirectory_Click);
            // 
            // lblOutputDirectory
            // 
            this.lblOutputDirectory.AutoSize = true;
            this.lblOutputDirectory.Location = new System.Drawing.Point(6, 41);
            this.lblOutputDirectory.Name = "lblOutputDirectory";
            this.lblOutputDirectory.Size = new System.Drawing.Size(91, 13);
            this.lblOutputDirectory.TabIndex = 3;
            this.lblOutputDirectory.Text = "lblOutputDirectory";
            // 
            // btnInputDirectory
            // 
            this.btnInputDirectory.Image = global::AscGenDotNet.Properties.Resources.folder;
            this.btnInputDirectory.Location = new System.Drawing.Point(285, 10);
            this.btnInputDirectory.Name = "btnInputDirectory";
            this.btnInputDirectory.Size = new System.Drawing.Size(30, 23);
            this.btnInputDirectory.TabIndex = 2;
            this.btnInputDirectory.UseVisualStyleBackColor = true;
            this.btnInputDirectory.Click += new System.EventHandler(this.BtnInputDirectory_Click);
            // 
            // lblInputDirectory
            // 
            this.lblInputDirectory.AutoSize = true;
            this.lblInputDirectory.Location = new System.Drawing.Point(6, 15);
            this.lblInputDirectory.Name = "lblInputDirectory";
            this.lblInputDirectory.Size = new System.Drawing.Size(83, 13);
            this.lblInputDirectory.TabIndex = 0;
            this.lblInputDirectory.Text = "lblInputDirectory";
            // 
            // tabSettings
            // 
            this.tabSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSettings.Controls.Add(this.pageBasic);
            this.tabSettings.HotTrack = true;
            this.tabSettings.Location = new System.Drawing.Point(12, 12);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(329, 171);
            this.tabSettings.TabIndex = 0;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FormEditSettings
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(353, 224);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabSettings);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormEditSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormEditSettings";
            this.pageBasic.ResumeLayout(false);
            this.pageBasic.PerformLayout();
            this.tabSettings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion Private methods
    }
}