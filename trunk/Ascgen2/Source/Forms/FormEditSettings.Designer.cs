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

        private System.Windows.Forms.Button buttonCancel;

        private System.Windows.Forms.Button buttonFont;

        private System.Windows.Forms.Button buttonInputDirectory;

        private System.Windows.Forms.Button buttonOk;

        private System.Windows.Forms.Button buttonOutputDirectory;

        private System.Windows.Forms.Button buttonSave;

        private System.Windows.Forms.CheckBox checkBoxConfirmClose;

        private System.Windows.Forms.CheckBox checkBoxConfirmVersionCheck;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ErrorProvider errorProvider1;

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;

        private System.Windows.Forms.FontDialog fontDialog1;

        private System.Windows.Forms.Label labelInputDirectory;

        private System.Windows.Forms.Label labelOutputDirectory;

        private System.Windows.Forms.TabPage pageBasic;

        private System.Windows.Forms.TabControl tabSettings;

        private System.Windows.Forms.TextBox textBoxFont;

        private System.Windows.Forms.TextBox textBoxInputDirectory;

        private System.Windows.Forms.TextBox textBoxOutputDirectory;

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
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.pageBasic = new System.Windows.Forms.TabPage();
            this.textBoxFont = new System.Windows.Forms.TextBox();
            this.checkBoxConfirmVersionCheck = new System.Windows.Forms.CheckBox();
            this.checkBoxConfirmClose = new System.Windows.Forms.CheckBox();
            this.buttonFont = new System.Windows.Forms.Button();
            this.textBoxOutputDirectory = new System.Windows.Forms.TextBox();
            this.textBoxInputDirectory = new System.Windows.Forms.TextBox();
            this.buttonOutputDirectory = new System.Windows.Forms.Button();
            this.labelOutputDirectory = new System.Windows.Forms.Label();
            this.buttonInputDirectory = new System.Windows.Forms.Button();
            this.labelInputDirectory = new System.Windows.Forms.Label();
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.buttonDefault = new System.Windows.Forms.Button();
            this.pageBasic.SuspendLayout();
            this.tabSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(12, 189);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(266, 189);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonSave.Location = new System.Drawing.Point(96, 189);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // pageBasic
            // 
            this.pageBasic.Controls.Add(this.textBoxFont);
            this.pageBasic.Controls.Add(this.checkBoxConfirmVersionCheck);
            this.pageBasic.Controls.Add(this.checkBoxConfirmClose);
            this.pageBasic.Controls.Add(this.buttonFont);
            this.pageBasic.Controls.Add(this.textBoxOutputDirectory);
            this.pageBasic.Controls.Add(this.textBoxInputDirectory);
            this.pageBasic.Controls.Add(this.buttonOutputDirectory);
            this.pageBasic.Controls.Add(this.labelOutputDirectory);
            this.pageBasic.Controls.Add(this.buttonInputDirectory);
            this.pageBasic.Controls.Add(this.labelInputDirectory);
            this.pageBasic.Location = new System.Drawing.Point(4, 22);
            this.pageBasic.Name = "pageBasic";
            this.pageBasic.Padding = new System.Windows.Forms.Padding(3);
            this.pageBasic.Size = new System.Drawing.Size(321, 145);
            this.pageBasic.TabIndex = 4;
            this.pageBasic.Text = "pageBasic";
            this.pageBasic.UseVisualStyleBackColor = true;
            // 
            // textBoxFont
            // 
            this.textBoxFont.Location = new System.Drawing.Point(99, 80);
            this.textBoxFont.Name = "textBoxFont";
            this.textBoxFont.ReadOnly = true;
            this.textBoxFont.Size = new System.Drawing.Size(216, 20);
            this.textBoxFont.TabIndex = 7;
            // 
            // checkBoxConfirmVersionCheck
            // 
            this.checkBoxConfirmVersionCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxConfirmVersionCheck.AutoSize = true;
            this.checkBoxConfirmVersionCheck.Location = new System.Drawing.Point(137, 122);
            this.checkBoxConfirmVersionCheck.Name = "checkBoxConfirmVersionCheck";
            this.checkBoxConfirmVersionCheck.Size = new System.Drawing.Size(175, 17);
            this.checkBoxConfirmVersionCheck.TabIndex = 9;
            this.checkBoxConfirmVersionCheck.Text = "checkBoxConfirmVersionCheck";
            this.checkBoxConfirmVersionCheck.UseVisualStyleBackColor = true;
            // 
            // checkBoxConfirmClose
            // 
            this.checkBoxConfirmClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxConfirmClose.AutoSize = true;
            this.checkBoxConfirmClose.Location = new System.Drawing.Point(6, 122);
            this.checkBoxConfirmClose.Name = "checkBoxConfirmClose";
            this.checkBoxConfirmClose.Size = new System.Drawing.Size(135, 17);
            this.checkBoxConfirmClose.TabIndex = 8;
            this.checkBoxConfirmClose.Text = "checkBoxConfirmClose";
            this.checkBoxConfirmClose.UseVisualStyleBackColor = true;
            // 
            // buttonFont
            // 
            this.buttonFont.Location = new System.Drawing.Point(6, 78);
            this.buttonFont.Name = "buttonFont";
            this.buttonFont.Size = new System.Drawing.Size(75, 23);
            this.buttonFont.TabIndex = 6;
            this.buttonFont.Text = "buttonFont";
            this.buttonFont.UseVisualStyleBackColor = true;
            this.buttonFont.Click += new System.EventHandler(this.ButtonFont_Click);
            // 
            // textBoxOutputDirectory
            // 
            this.textBoxOutputDirectory.Location = new System.Drawing.Point(99, 38);
            this.textBoxOutputDirectory.Name = "textBoxOutputDirectory";
            this.textBoxOutputDirectory.Size = new System.Drawing.Size(180, 20);
            this.textBoxOutputDirectory.TabIndex = 4;
            // 
            // textBoxInputDirectory
            // 
            this.textBoxInputDirectory.Location = new System.Drawing.Point(99, 12);
            this.textBoxInputDirectory.Name = "textBoxInputDirectory";
            this.textBoxInputDirectory.Size = new System.Drawing.Size(180, 20);
            this.textBoxInputDirectory.TabIndex = 1;
            // 
            // buttonOutputDirectory
            // 
            this.buttonOutputDirectory.Image = global::AscGenDotNet.Properties.Resources.folder;
            this.buttonOutputDirectory.Location = new System.Drawing.Point(285, 36);
            this.buttonOutputDirectory.Name = "buttonOutputDirectory";
            this.buttonOutputDirectory.Size = new System.Drawing.Size(30, 23);
            this.buttonOutputDirectory.TabIndex = 5;
            this.buttonOutputDirectory.UseVisualStyleBackColor = true;
            this.buttonOutputDirectory.Click += new System.EventHandler(this.ButtonOutputDirectory_Click);
            // 
            // labelOutputDirectory
            // 
            this.labelOutputDirectory.AutoSize = true;
            this.labelOutputDirectory.Location = new System.Drawing.Point(6, 41);
            this.labelOutputDirectory.Name = "labelOutputDirectory";
            this.labelOutputDirectory.Size = new System.Drawing.Size(103, 13);
            this.labelOutputDirectory.TabIndex = 3;
            this.labelOutputDirectory.Text = "labelOutputDirectory";
            // 
            // buttonInputDirectory
            // 
            this.buttonInputDirectory.Image = global::AscGenDotNet.Properties.Resources.folder;
            this.buttonInputDirectory.Location = new System.Drawing.Point(285, 10);
            this.buttonInputDirectory.Name = "buttonInputDirectory";
            this.buttonInputDirectory.Size = new System.Drawing.Size(30, 23);
            this.buttonInputDirectory.TabIndex = 2;
            this.buttonInputDirectory.UseVisualStyleBackColor = true;
            this.buttonInputDirectory.Click += new System.EventHandler(this.ButtonInputDirectory_Click);
            // 
            // labelInputDirectory
            // 
            this.labelInputDirectory.AutoSize = true;
            this.labelInputDirectory.Location = new System.Drawing.Point(6, 15);
            this.labelInputDirectory.Name = "labelInputDirectory";
            this.labelInputDirectory.Size = new System.Drawing.Size(95, 13);
            this.labelInputDirectory.TabIndex = 0;
            this.labelInputDirectory.Text = "labelInputDirectory";
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
            // buttonDefault
            // 
            this.buttonDefault.Location = new System.Drawing.Point(180, 189);
            this.buttonDefault.Name = "buttonDefault";
            this.buttonDefault.Size = new System.Drawing.Size(75, 23);
            this.buttonDefault.TabIndex = 4;
            this.buttonDefault.Text = "buttonDefault";
            this.buttonDefault.UseVisualStyleBackColor = true;
            // 
            // FormEditSettings
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(353, 224);
            this.Controls.Add(this.buttonDefault);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.tabSettings);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
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

        private System.Windows.Forms.Button buttonDefault;
    }
}