//---------------------------------------------------------------------------------------
// <copyright file="WidgetBrightnessContrast.Designer.cs" company="Jonathan Mathews Software">
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
namespace JMSoftware.Widgets
{
    using System.ComponentModel;
    using System.Windows.Forms;
    using JMSoftware.Controls;

    /// <summary>
    /// Widget to provide controls for adjusting Brightness/Contrast
    /// </summary>
    public partial class WidgetBrightnessContrast : BaseWidget
    {
        #region Fields

        /// <summary>Reset toolstrip menu item</summary>
        private ToolStripMenuItem cmenuReset;

        /// <summary>Form components</summary>
        private IContainer components;

        /// <summary>The context menu</summary>
        private ContextMenuStrip contextMenuStrip1;

        /// <summary>Brightness/contrast control</summary>
        private JMBrightnessContrast brightnessContrast;

        #endregion Fields

        #region Private methods

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.brightnessContrast = new JMSoftware.Controls.JMBrightnessContrast();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmenuReset = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // brightnessContrast
            // 
            this.brightnessContrast.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.brightnessContrast.ContextMenuStrip = this.contextMenuStrip1;
            this.brightnessContrast.Location = new System.Drawing.Point(4, 4);
            this.brightnessContrast.MinimumSize = new System.Drawing.Size(120, 86);
            this.brightnessContrast.Name = "brightnessContrast";
            this.brightnessContrast.Size = new System.Drawing.Size(120, 86);
            this.brightnessContrast.Suspended = false;
            this.brightnessContrast.TabIndex = 0;
            this.brightnessContrast.OnContrastChanged += new System.EventHandler(this.JmBrightnessContrast1_ContrastChanged);
            this.brightnessContrast.OnBrightnessChanged += new System.EventHandler(this.JmBrightnessContrast1_BrightnessChanged);
            this.brightnessContrast.OnValueChanged += new System.EventHandler(this.JmBrightnessContrast1_ValueChanged);
            this.brightnessContrast.OnContrastChanging += new System.EventHandler(this.JmBrightnessContrast1_ContrastChanging);
            this.brightnessContrast.OnValueChanging += new System.EventHandler(this.JmBrightnessContrast1_ValueChanging);
            this.brightnessContrast.OnBrightnessChanging += new System.EventHandler(this.JmBrightnessContrast1_BrightnessChanging);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmenuReset});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(134, 26);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1_Opening);
            // 
            // cmenuReset
            // 
            this.cmenuReset.Name = "cmenuReset";
            this.cmenuReset.Size = new System.Drawing.Size(133, 22);
            this.cmenuReset.Text = "cmenuReset";
            this.cmenuReset.Click += new System.EventHandler(this.CmenuReset_Click);
            // 
            // WidgetBrightnessContrast
            // 
            this.ClientSize = new System.Drawing.Size(128, 96);
            this.Controls.Add(this.brightnessContrast);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(136, 122);
            this.Name = "WidgetBrightnessContrast";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion Private methods
    }
}