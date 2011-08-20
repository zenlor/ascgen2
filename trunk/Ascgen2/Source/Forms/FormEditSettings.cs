//---------------------------------------------------------------------------------------
// <copyright file="FormEditSettings.cs" company="Jonathan Mathews Software">
//     ASCII Generator dotNET - Image to ASCII Art Conversion Program
//     Copyright (C) 2011 Jonathan Mathews Software. All rights reserved.
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
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Form to edit the settings
    /// </summary>
    public partial class FormEditSettings : Form
    {
        #region Fields

        /// <summary>
        /// The default font
        /// </summary>
        private Font defaultFont;

        /// <summary>
        /// The textboxes that contain directory strings
        /// </summary>
        private TextBox[] directories;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FormEditSettings"/> class.
        /// </summary>
        public FormEditSettings()
        {
            this.InitializeComponent();

            this.UpdateUI();

            this.ResetSettings();

            this.directories = new TextBox[] { this.textBoxInputDirectory, this.textBoxOutputDirectory };
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a value indicating whether to check for new versions.
        /// </summary>
        /// <value>
        /// <c>true</c> if checking for new versions; otherwise, <c>false</c>.
        /// </value>
        public bool CheckForNewVersions
        {
            get
            {
                return this.checkBoxConfirmVersionCheck.Checked;
            }
        }

        /// <summary>
        /// Gets a value indicating whether to confirm on close.
        /// </summary>
        /// <value>
        /// <c>true</c> if confirm on close; otherwise, <c>false</c>.
        /// </value>
        public bool ConfirmOnClose
        {
            get
            {
                return this.checkBoxConfirmClose.Checked;
            }
        }

        /// <summary>
        /// Gets or sets the default font.
        /// </summary>
        /// <value>The default font.</value>
        public new Font DefaultFont
        {
            get
            {
                return this.defaultFont;
            }

            set
            {
                this.defaultFont = value;

                this.UpdateFont();
            }
        }

        /// <summary>
        /// Gets the input directory.
        /// </summary>
        /// <value>The input directory.</value>
        public string InputDirectory
        {
            get
            {
                return this.textBoxInputDirectory.Text;
            }
        }

        /// <summary>
        /// Gets the output directory.
        /// </summary>
        /// <value>The output directory.</value>
        public string OutputDirectory
        {
            get
            {
                return this.textBoxOutputDirectory.Text;
            }
        }

        #endregion Properties

        #region Protected methods

        /// <summary>
        /// Raise the System.Windows.Forms.Form.Closing event
        /// </summary>
        /// <param name="e">CancelEventArgs containing the event data</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            // Catch ok button press
            if (this.DialogResult != DialogResult.OK)
            {
                return;
            }

            this.errorProvider1.Clear();

            foreach (TextBox textbox in this.directories)
            {
                // the directory must exist if not empty
                if (textbox.Text.Length > 0 && !System.IO.Directory.Exists(textbox.Text))
                {
                    this.errorProvider1.SetError(textbox, Resource.GetString("Invalid Directory"));

                    e.Cancel = true;
                }
            }
        }

        #endregion Protected methods

        #region Private methods

        /// <summary>
        /// Handles the Click event of the buttonFont control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonFont_Click(object sender, EventArgs e)
        {
            this.fontDialog1.Font = this.DefaultFont;

            if (this.fontDialog1.ShowDialog() == DialogResult.OK)
            {
                this.DefaultFont = this.fontDialog1.Font;
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonInputDirectory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonInputDirectory_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.Description = Resource.GetString("Input Directory");
            this.folderBrowserDialog1.SelectedPath = this.textBoxInputDirectory.Text;

            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBoxInputDirectory.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonOutputDirectory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonOutputDirectory_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.Description = Resource.GetString("Output Directory");
            this.folderBrowserDialog1.SelectedPath = this.textBoxOutputDirectory.Text;

            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBoxOutputDirectory.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        /// <summary>
        /// Resets the settings.
        /// </summary>
        private void ResetSettings()
        {
            this.textBoxInputDirectory.Text = Variables.InitialInputDirectory;

            this.textBoxOutputDirectory.Text = Variables.InitialOutputDirectory;

            this.checkBoxConfirmClose.Checked = Variables.ConfirmOnClose;

            this.checkBoxConfirmVersionCheck.Checked = Variables.CheckForNewVersion;
        }

        /// <summary>
        /// Updates the font.
        /// </summary>
        private void UpdateFont()
        {
            this.textBoxFont.Text = this.defaultFont.Name + String.Format(Variables.Culture, " {0}pt", this.defaultFont.Size) +
                (this.defaultFont.Bold ? ", bold" : String.Empty) + (this.defaultFont.Italic ? ", italic" : String.Empty) +
                (this.defaultFont.Underline ? ", underline" : String.Empty) +
                (this.defaultFont.Strikeout ? ", strikeout" : String.Empty) + ".";
        }

        /// <summary>
        /// Update the form with the text strings for the current language
        /// </summary>
        private void UpdateUI()
        {
            this.Text = Resource.GetString("Edit Settings");

            this.tabPageBasic.Text = Resource.GetString("Basic");

            this.buttonOk.Text = Resource.GetString("&Ok");
            this.buttonSave.Text = Resource.GetString("&Save");
            this.buttonDefault.Text = Resource.GetString("&Default");
            this.buttonCancel.Text = Resource.GetString("&Cancel");

            this.checkBoxConfirmClose.Text = Resource.GetString("Confirm close if unsaved");
            this.checkBoxConfirmVersionCheck.Text = Resource.GetString("New version check");

            this.labelInputDirectory.Text = Resource.GetString("Input Directory") + ":";
            this.labelOutputDirectory.Text = Resource.GetString("Output Directory") + ":";

            this.buttonFont.Text = Resource.GetString("Font") + "...";
        }

        #endregion Private methods
    }
}