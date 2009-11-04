//---------------------------------------------------------------------------------------
// <copyright file="WidgetBrightnessContrast.cs" company="Jonathan Mathews Software">
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
    using System;
    using System.ComponentModel;
    using JMSoftware.AsciiGeneratorDotNet;
    using JMSoftware.Interfaces;

    /// <summary>
    /// Widget to provide controls for adjusting Brightness/Contrast
    /// </summary>
    public partial class WidgetBrightnessContrast : BaseWidget, IBrightnessContrast
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetBrightnessContrast"/> class.
        /// </summary>
        public WidgetBrightnessContrast()
        {
            this.InitializeComponent();
        }

        #endregion Constructors

        #region Events / Delegates

        /// <summary>Event raised when the brightness has changed</summary>
        [Browsable(true), Description("Event raised when the brightness has changed")]
        public event EventHandler BrightnessChanged;

        /// <summary>Event raised while the brightness is changing</summary>
        [Browsable(true), Description("Event raised while the brightness is changing")]
        public event EventHandler BrightnessChanging;

        /// <summary>Event raised when the contrast has changed</summary>
        [Browsable(true), Description("Event raised when the contrast has changed")]
        public event EventHandler ContrastChanged;

        /// <summary>Event raised while the contrast is changing</summary>
        [Browsable(true), Description("Event raised while the contrast is changing")]
        public event EventHandler ContrastChanging;

        /// <summary>Event raised when a value has changed</summary>
        [Browsable(true), Description("Event raised when a value has changed")]
        public event EventHandler ValueChanged;

        /// <summary>Event raised while a value is changing</summary>
        [Browsable(true), Description("Event raised while a value is changing")]
        public event EventHandler ValueChanging;

        #endregion Events / Delegates

        #region Properties

        /// <summary>
        /// Gets or sets the Brightness.
        /// </summary>
        /// <value></value>
        public int Brightness
        {
            get { return this.brightnessContrast.Brightness; }
            set { this.brightnessContrast.Brightness = value; }
        }

        /// <summary>
        /// Gets or sets the Contrast
        /// </summary>
        /// <value>The contrast.</value>
        public int Contrast
        {
            get { return this.brightnessContrast.Contrast; }
            set { this.brightnessContrast.Contrast = value; }
        }

        /// <summary>
        /// Gets or sets the maximum brightness.
        /// </summary>
        /// <value>The maximum brightness.</value>
        public int MaximumBrightness
        {
            get { return this.brightnessContrast.MaximumBrightness; }
            set { this.brightnessContrast.MaximumBrightness = value; }
        }

        /// <summary>
        /// Gets or sets the maximum contrast.
        /// </summary>
        /// <value>The maximum contrast.</value>
        public int MaximumContrast
        {
            get { return this.brightnessContrast.MaximumContrast; }
            set { this.brightnessContrast.MaximumContrast = value; }
        }

        /// <summary>
        /// Gets or sets the minimum brightness.
        /// </summary>
        /// <value>The minimum brightness.</value>
        public int MinimumBrightness
        {
            get { return this.brightnessContrast.MinimumBrightness; }
            set { this.brightnessContrast.MinimumBrightness = value; }
        }

        /// <summary>
        /// Gets or sets the minimum contrast.
        /// </summary>
        /// <value>The minimum contrast.</value>
        public int MinimumContrast
        {
            get { return this.brightnessContrast.MinimumContrast; }
            set { this.brightnessContrast.MinimumContrast = value; }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Update the controls with the current cultures resource strings
        /// </summary>
        public void UpdateUI()
        {
            this.brightnessContrast.BrightnessLabel = Resource.GetString("Brightness") + ":";
            this.brightnessContrast.ContrastLabel = Resource.GetString("Contrast") + ":";

            this.cmenuReset.Text = Resource.GetString("Reset");
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Handles the Click event of the cmenuReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuReset_Click(object sender, EventArgs e)
        {
            this.brightnessContrast.Reset();
        }

        /// <summary>
        /// Handles the Opening event of the contextMenuStrip1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            this.cmenuReset.Enabled =
                this.brightnessContrast.Brightness != 0 || this.brightnessContrast.Contrast != 0;
        }

        /// <summary>
        /// Handles the BrightnessChanged event of the jmBrightnessContrast1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JmBrightnessContrast1_BrightnessChanged(object sender, EventArgs e)
        {
            if (this.BrightnessChanged != null)
            {
                this.BrightnessChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles the BrightnessChanging event of the jmBrightnessContrast1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JmBrightnessContrast1_BrightnessChanging(object sender, EventArgs e)
        {
            if (this.BrightnessChanging != null)
            {
                this.BrightnessChanging(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles the ContrastChanged event of the jmBrightnessContrast1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JmBrightnessContrast1_ContrastChanged(object sender, EventArgs e)
        {
            if (this.ContrastChanged != null)
            {
                this.ContrastChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles the ContrastChanging event of the jmBrightnessContrast1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JmBrightnessContrast1_ContrastChanging(object sender, EventArgs e)
        {
            if (this.ContrastChanging != null)
            {
                this.ContrastChanging(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles the ValueChanged event of the jmBrightnessContrast1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JmBrightnessContrast1_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Handles the ValueChanging event of the jmBrightnessContrast1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JmBrightnessContrast1_ValueChanging(object sender, EventArgs e)
        {
            if (this.ValueChanging != null)
            {
                this.ValueChanging(this, new EventArgs());
            }
        }

        #endregion Private methods
    }
}