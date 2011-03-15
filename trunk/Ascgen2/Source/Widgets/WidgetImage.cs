//---------------------------------------------------------------------------------------
// <copyright file="WidgetImage.cs" company="Jonathan Mathews Software">
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
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Widget to display a jmSelectablePictureBox
    /// </summary>
    public partial class WidgetImage : BaseWidget
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetImage"/> class.
        /// </summary>
        public WidgetImage()
        {
            this.InitializeComponent();
        }

        #endregion Constructors

        #region Events / Delegates

        /// <summary>
        /// Event fired when the selected area has changed
        /// </summary>
        [Browsable(true), Description("Event raised when the image has been double clicked")]
        public new event EventHandler DoubleClick;

        /// <summary>
        /// Occurs when a drag-and-drop operation is completed.
        /// </summary>
        [Browsable(true), Description("Occurs when a drag-and-drop operation is completed.")]
        public new event DragEventHandler OnDragDrop;

        /// <summary>
        /// Event fired when the selected area has changed
        /// </summary>
        [Browsable(true), Description("Event raised when the selected area has changed")]
        public event EventHandler SelectionChanged;

        /// <summary>
        /// Event fired when the selected area is changing
        /// </summary>
        [Browsable(true), Description("Event raised when the selected area is changing")]
        public event EventHandler SelectionChanging;

        #endregion Events / Delegates

        #region Properties

        /// <summary>
        /// Gets or sets the text displayed when no image is shown.
        /// </summary>
        /// <value>The display text.</value>
        public string DisplayText
        {
            get
            {
                return this.jmSelectablePictureBox1.Text;
            }

            set
            {
                this.jmSelectablePictureBox1.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        public Image Image
        {
            get
            {
                return this.jmSelectablePictureBox1.Image;
            }

            set
            {
                this.jmSelectablePictureBox1.Image = value;
            }
        }

        /// <summary>
        /// Gets or sets the selected area.
        /// </summary>
        /// <value>The selected area.</value>
        public Rectangle SelectedArea
        {
            get
            {
                return this.jmSelectablePictureBox1.SelectedArea;
            }

            set
            {
                this.jmSelectablePictureBox1.SelectedArea = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the selection border.
        /// </summary>
        /// <value>The color of the selection border.</value>
        public Color SelectionBorderColor
        {
            get
            {
                return this.jmSelectablePictureBox1.SelectionBorderColor;
            }

            set
            {
                this.jmSelectablePictureBox1.SelectionBorderColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the selection fill.
        /// </summary>
        /// <value>The color of the selection fill.</value>
        public Color SelectionFillColor
        {
            get
            {
                return this.jmSelectablePictureBox1.SelectionFillColor;
            }

            set
            {
                this.jmSelectablePictureBox1.SelectionFillColor = value;
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Rotates the image.
        /// </summary>
        /// <param name="type">The rotation type.</param>
        public void RotateImage(RotateFlipType type)
        {
            this.jmSelectablePictureBox1.RotateImage(type);
        }

        /// <summary>
        /// Selects the nothing.
        /// </summary>
        public void SelectNothing()
        {
            this.jmSelectablePictureBox1.SelectNothing();
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Handles the drag over.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private static void HandleDragOver(DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FileNameW") && ((string[])e.Data.GetData(DataFormats.FileDrop)).Length == 1)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// Handles the drag drop.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void HandleDragDrop(DragEventArgs e)
        {
            this.OnDragDrop(this, e);
        }

        /// <summary>
        /// Handles the DoubleClick event of the jmSelectablePictureBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JMSelectablePictureBox1_DoubleClick(object sender, EventArgs e)
        {
            this.DoubleClick(sender, e);
        }

        /// <summary>
        /// Handles the DragDrop event of the jmSelectablePictureBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void JMSelectablePictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            this.HandleDragDrop(e);
        }

        /// <summary>
        /// Handles the DragEnter event of the jmSelectablePictureBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void JMSelectablePictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            HandleDragOver(e);
        }

        /// <summary>
        /// Handles the SelectionChanged event of the jmSelectablePictureBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JMSelectablePictureBox1_SelectionChanged(object sender, EventArgs e)
        {
            this.SelectionChanged(sender, e);
        }

        /// <summary>
        /// Handles the SelectionChanging event of the jmSelectablePictureBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JMSelectablePictureBox1_SelectionChanging(object sender, EventArgs e)
        {
            this.SelectionChanging(sender, e);
        }

        /// <summary>
        /// Handles the DragDrop event of the form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void WidgetImage_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            this.HandleDragDrop(e);
        }

        /// <summary>
        /// Handles the DragEnter event of the form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void WidgetImage_DragEnter(object sender, DragEventArgs e)
        {
            HandleDragOver(e);
        }

        #endregion Private methods
    }
}