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

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ErrorProvider errorProvider1;

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;

        private System.Windows.Forms.FontDialog fontDialog1;

        private System.Windows.Forms.Label labelInputDirectory;

        private System.Windows.Forms.Label labelOutputDirectory;

        private System.Windows.Forms.TabPage tabPageBasic;

        private System.Windows.Forms.TabControl tabControlSettings;

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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabPageBasic = new System.Windows.Forms.TabPage();
            this.checkBoxLockRatio = new System.Windows.Forms.CheckBox();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.labelOutputSize = new System.Windows.Forms.Label();
            this.textBoxFont = new System.Windows.Forms.TextBox();
            this.buttonFont = new System.Windows.Forms.Button();
            this.textBoxOutputDirectory = new System.Windows.Forms.TextBox();
            this.textBoxInputDirectory = new System.Windows.Forms.TextBox();
            this.buttonOutputDirectory = new System.Windows.Forms.Button();
            this.labelOutputDirectory = new System.Windows.Forms.Label();
            this.buttonInputDirectory = new System.Windows.Forms.Button();
            this.labelInputDirectory = new System.Windows.Forms.Label();
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.buttonDefault = new System.Windows.Forms.Button();
            this.checkBoxConfirmVersionCheck = new System.Windows.Forms.CheckBox();
            this.checkBoxConfirmClose = new System.Windows.Forms.CheckBox();
            this.tabPageBasic.SuspendLayout();
            this.tabControlSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(12, 217);
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
            this.buttonCancel.Location = new System.Drawing.Point(266, 217);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // tabPageBasic
            // 
            this.tabPageBasic.Controls.Add(this.checkBoxConfirmVersionCheck);
            this.tabPageBasic.Controls.Add(this.checkBoxConfirmClose);
            this.tabPageBasic.Controls.Add(this.checkBoxLockRatio);
            this.tabPageBasic.Controls.Add(this.textBoxHeight);
            this.tabPageBasic.Controls.Add(this.textBoxWidth);
            this.tabPageBasic.Controls.Add(this.labelOutputSize);
            this.tabPageBasic.Controls.Add(this.textBoxFont);
            this.tabPageBasic.Controls.Add(this.buttonFont);
            this.tabPageBasic.Controls.Add(this.textBoxOutputDirectory);
            this.tabPageBasic.Controls.Add(this.textBoxInputDirectory);
            this.tabPageBasic.Controls.Add(this.buttonOutputDirectory);
            this.tabPageBasic.Controls.Add(this.labelOutputDirectory);
            this.tabPageBasic.Controls.Add(this.buttonInputDirectory);
            this.tabPageBasic.Controls.Add(this.labelInputDirectory);
            this.tabPageBasic.Location = new System.Drawing.Point(4, 22);
            this.tabPageBasic.Name = "tabPageBasic";
            this.tabPageBasic.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBasic.Size = new System.Drawing.Size(321, 173);
            this.tabPageBasic.TabIndex = 4;
            this.tabPageBasic.Text = "tabPageBasic";
            this.tabPageBasic.UseVisualStyleBackColor = true;
            // 
            // checkBoxLockRatio
            // 
            this.checkBoxLockRatio.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxLockRatio.Location = new System.Drawing.Point(141, 73);
            this.checkBoxLockRatio.Name = "checkBoxLockRatio";
            this.checkBoxLockRatio.Size = new System.Drawing.Size(24, 24);
            this.checkBoxLockRatio.TabIndex = 10;
            this.checkBoxLockRatio.Text = "X";
            this.checkBoxLockRatio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxLockRatio.UseVisualStyleBackColor = true;
            this.checkBoxLockRatio.CheckedChanged += new System.EventHandler(this.CheckBoxLockRatio_CheckedChanged);
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Location = new System.Drawing.Point(171, 75);
            this.textBoxHeight.MaxLength = 4;
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(36, 20);
            this.textBoxHeight.TabIndex = 11;
            this.textBoxHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxHeight.Leave += new System.EventHandler(this.TextBoxHeight_Leave);
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(99, 75);
            this.textBoxWidth.MaxLength = 4;
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(36, 20);
            this.textBoxWidth.TabIndex = 9;
            this.textBoxWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxWidth.Leave += new System.EventHandler(this.TextBoxWidth_Leave);
            // 
            // labelOutputSize
            // 
            this.labelOutputSize.AutoSize = true;
            this.labelOutputSize.Location = new System.Drawing.Point(6, 78);
            this.labelOutputSize.Name = "labelOutputSize";
            this.labelOutputSize.Size = new System.Drawing.Size(81, 13);
            this.labelOutputSize.TabIndex = 8;
            this.labelOutputSize.Text = "labelOutputSize";
            // 
            // textBoxFont
            // 
            this.textBoxFont.Location = new System.Drawing.Point(99, 111);
            this.textBoxFont.Name = "textBoxFont";
            this.textBoxFont.ReadOnly = true;
            this.textBoxFont.Size = new System.Drawing.Size(216, 20);
            this.textBoxFont.TabIndex = 7;
            // 
            // buttonFont
            // 
            this.buttonFont.Location = new System.Drawing.Point(6, 109);
            this.buttonFont.Name = "buttonFont";
            this.buttonFont.Size = new System.Drawing.Size(75, 23);
            this.buttonFont.TabIndex = 6;
            this.buttonFont.Text = "buttonFont";
            this.buttonFont.UseVisualStyleBackColor = true;
            this.buttonFont.Click += new System.EventHandler(this.ButtonFont_Click);
            // 
            // textBoxOutputDirectory
            // 
            this.textBoxOutputDirectory.Location = new System.Drawing.Point(99, 40);
            this.textBoxOutputDirectory.Name = "textBoxOutputDirectory";
            this.textBoxOutputDirectory.Size = new System.Drawing.Size(180, 20);
            this.textBoxOutputDirectory.TabIndex = 4;
            // 
            // textBoxInputDirectory
            // 
            this.textBoxInputDirectory.Location = new System.Drawing.Point(99, 14);
            this.textBoxInputDirectory.Name = "textBoxInputDirectory";
            this.textBoxInputDirectory.Size = new System.Drawing.Size(180, 20);
            this.textBoxInputDirectory.TabIndex = 1;
            // 
            // buttonOutputDirectory
            // 
            this.buttonOutputDirectory.Image = global::AscGenDotNet.Properties.Resources.folder;
            this.buttonOutputDirectory.Location = new System.Drawing.Point(285, 38);
            this.buttonOutputDirectory.Name = "buttonOutputDirectory";
            this.buttonOutputDirectory.Size = new System.Drawing.Size(30, 23);
            this.buttonOutputDirectory.TabIndex = 5;
            this.buttonOutputDirectory.UseVisualStyleBackColor = true;
            this.buttonOutputDirectory.Click += new System.EventHandler(this.ButtonOutputDirectory_Click);
            // 
            // labelOutputDirectory
            // 
            this.labelOutputDirectory.AutoSize = true;
            this.labelOutputDirectory.Location = new System.Drawing.Point(6, 43);
            this.labelOutputDirectory.Name = "labelOutputDirectory";
            this.labelOutputDirectory.Size = new System.Drawing.Size(103, 13);
            this.labelOutputDirectory.TabIndex = 3;
            this.labelOutputDirectory.Text = "labelOutputDirectory";
            // 
            // buttonInputDirectory
            // 
            this.buttonInputDirectory.Image = global::AscGenDotNet.Properties.Resources.folder;
            this.buttonInputDirectory.Location = new System.Drawing.Point(285, 12);
            this.buttonInputDirectory.Name = "buttonInputDirectory";
            this.buttonInputDirectory.Size = new System.Drawing.Size(30, 23);
            this.buttonInputDirectory.TabIndex = 2;
            this.buttonInputDirectory.UseVisualStyleBackColor = true;
            this.buttonInputDirectory.Click += new System.EventHandler(this.ButtonInputDirectory_Click);
            // 
            // labelInputDirectory
            // 
            this.labelInputDirectory.AutoSize = true;
            this.labelInputDirectory.Location = new System.Drawing.Point(6, 17);
            this.labelInputDirectory.Name = "labelInputDirectory";
            this.labelInputDirectory.Size = new System.Drawing.Size(95, 13);
            this.labelInputDirectory.TabIndex = 0;
            this.labelInputDirectory.Text = "labelInputDirectory";
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlSettings.Controls.Add(this.tabPageBasic);
            this.tabControlSettings.HotTrack = true;
            this.tabControlSettings.Location = new System.Drawing.Point(12, 12);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(329, 199);
            this.tabControlSettings.TabIndex = 0;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // buttonDefault
            // 
            this.buttonDefault.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonDefault.Location = new System.Drawing.Point(139, 217);
            this.buttonDefault.Name = "buttonDefault";
            this.buttonDefault.Size = new System.Drawing.Size(75, 23);
            this.buttonDefault.TabIndex = 4;
            this.buttonDefault.Text = "buttonDefault";
            this.buttonDefault.UseVisualStyleBackColor = true;
            this.buttonDefault.Click += new System.EventHandler(this.ButtonDefault_Click);
            // 
            // checkBoxConfirmVersionCheck
            // 
            this.checkBoxConfirmVersionCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxConfirmVersionCheck.AutoSize = true;
            this.checkBoxConfirmVersionCheck.Location = new System.Drawing.Point(143, 146);
            this.checkBoxConfirmVersionCheck.Name = "checkBoxConfirmVersionCheck";
            this.checkBoxConfirmVersionCheck.Size = new System.Drawing.Size(175, 17);
            this.checkBoxConfirmVersionCheck.TabIndex = 13;
            this.checkBoxConfirmVersionCheck.Text = "checkBoxConfirmVersionCheck";
            this.checkBoxConfirmVersionCheck.UseVisualStyleBackColor = true;
            // 
            // checkBoxConfirmClose
            // 
            this.checkBoxConfirmClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxConfirmClose.AutoSize = true;
            this.checkBoxConfirmClose.Location = new System.Drawing.Point(9, 146);
            this.checkBoxConfirmClose.Name = "checkBoxConfirmClose";
            this.checkBoxConfirmClose.Size = new System.Drawing.Size(135, 17);
            this.checkBoxConfirmClose.TabIndex = 12;
            this.checkBoxConfirmClose.Text = "checkBoxConfirmClose";
            this.checkBoxConfirmClose.UseVisualStyleBackColor = true;
            // 
            // FormEditSettings
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(353, 248);
            this.Controls.Add(this.buttonDefault);
            this.Controls.Add(this.tabControlSettings);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormEditSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormEditSettings";
            this.Load += new System.EventHandler(this.FormEditSettings_Load);
            this.tabPageBasic.ResumeLayout(false);
            this.tabPageBasic.PerformLayout();
            this.tabControlSettings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion Private methods

        private System.Windows.Forms.Button buttonDefault;
        private System.Windows.Forms.CheckBox checkBoxLockRatio;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.Label labelOutputSize;
        private System.Windows.Forms.CheckBox checkBoxConfirmVersionCheck;
        private System.Windows.Forms.CheckBox checkBoxConfirmClose;
    }
}