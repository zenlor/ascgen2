//---------------------------------------------------------------------------------------
// <copyright file="FormConvertImage.cs" company="Jonathan Mathews Software">
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
    using System;
    using System.Collections.Specialized;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Printing;
    using System.IO;
    using System.Windows.Forms;
    using JMSoftware.AsciiConversion;
    using JMSoftware.AsciiConversion.Filters;
    using JMSoftware.ImageHelper;
    using JMSoftware.Interfaces;
    using JMSoftware.TextHelper;
    using JMSoftware.Widgets;

    /// <summary>
    /// Main form for the program
    /// </summary>
    public partial class FormConvertImage : Form
    {
        #region Fields

        /// <summary>Interface to the object used to get and set the brightness and contrast amounts</summary>
        private IBrightnessContrast brightnessContrast;

        /// <summary>Used for storing the size of the form</summary>
        private Size clientSize;

        /// <summary>
        /// Dialog used to get the level of image scaling
        /// </summary>
        private TextImageMagnificationDialog dialogChooseTextZoom;

        /// <summary>Object used to calculate the output dimensions</summary>
        private DimensionsCalculator dimensionsCalculator;

        /// <summary>Interface to the object used to get and set the dithering amounts</summary>
        private IDither dither;

        /// <summary>Are we doing the conversions?</summary>
        private bool doConversion;

        /// <summary>The full filename of the input image</summary>
        private string filename;

        /// <summary>
        /// The batch conversion form
        /// </summary>
        private FormBatchConversion formBatchConversion;

        /// <summary>The Save As form</summary>
        private FormSaveAs formSaveAs;

        /// <summary>Interface to the object used to get and set the input images brightness and contrast</summary>
        private IBrightnessContrast imageBrightnessContrast;

        /// <summary>Has the current image been saved?</summary>
        private bool imageSaved;

        /// <summary>Have the input settings changed so the output needs to be recalculated?</summary>
        private bool inputChanged;

        /// <summary>A value indicating whether the form is full screen.</summary>
        private bool isFullScreen;

        /// <summary>Interface to the object used to get and set the levels</summary>
        private ILevels levels;

        /// <summary>Stores the position of the selection rectangle</summary>
        private Rectangle oldSelectionPosition;

        /// <summary>Stores the position and size of the form when going to full screen</summary>
        private Rectangle previousFormPosition;

        /// <summary>Store the previous state of the window (to work around toggling problems with a maximized form)</summary>
        private FormWindowState previousWindowState;

        /// <summary>Are we printing in colour?</summary>
        private bool printColour;

        /// <summary>Used to store the settings used on the output image</summary>
        private TextProcessingSettings textSettings;

        /// <summary>Interface to the object used to display the text</summary>
        private ITextViewer textViewer;

        /// <summary>The original image converted into an array of bytes</summary>
        private byte[][] values;

        /// <summary>Handles checking for a new version</summary>
        private VersionChecker versionChecker;

        /// <summary>Brightness/Contrast widget used</summary>
        private WidgetBrightnessContrast widgetImageBrightnessContrast;

        /// <summary>Text settings widget</summary>
        private WidgetTextSettings widgetTextSettings;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FormConvertImage"/> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public FormConvertImage(string[] arguments)
        {
            // Required for Windows Form Designer support
            this.InitializeComponent();

            this.filename = String.Empty;

            this.doConversion = true;

            this.AlterInputImageToolStripIsEnabled = false;

            this.InputDirectory = Settings.Default.InitialInputDirectory;

            this.OutputDirectory = Settings.Default.InitialOutputDirectory;

            this.clientSize = this.pnlMain.ClientSize;

            this.textSettings = new TextProcessingSettings();

            this.textViewer = this.rtbxConvertedText;

            this.formSaveAs = new FormSaveAs();

            this.dimensionsCalculator = new DimensionsCalculator(this.CurrentImageSection.Size, this.CharacterSize, Settings.Default.DefaultWidth, Settings.Default.DefaultHeight);

            this.dimensionsCalculator.OnOutputSizeChanged += new EventHandler(this.DimensionsCalculator_OnOutputSizeChanged);

            this.Font = Settings.Default.DefaultFont;

            this.SetupWidgets();

            this.SetupControls();

            this.formBatchConversion = new FormBatchConversion();

            this.dialogChooseTextZoom = new TextImageMagnificationDialog(this.Font);

            this.IsLandscape = Screen.PrimaryScreen.Bounds.Width > Screen.PrimaryScreen.Bounds.Height;

            CheckForIllegalCrossThreadCalls = false;

            this.versionChecker = new VersionChecker(
                                        this,
                                        "http://ascgen2.sourceforge.net/version.xml",
                                        Variables.Version.Major,
                                        Variables.Version.Minor,
                                        Variables.Version.Patch,
                                        Variables.Version.SuffixNumber);

            this.versionChecker.OpenDownloadPageString = Resource.GetString("Open the download page") + "?";
            this.versionChecker.ThisIsLatestVersionString = Resource.GetString("This is the latest version");
            this.versionChecker.VersionAvailableString = Resource.GetString("Version {0} is available");

            if (Settings.Default.CheckForNewVersions)
            {
                this.versionChecker.Check();
            }

            // load a filename if one was passed
            if (arguments != null && arguments.Length == 1)
            {
                this.LoadImage(arguments[0]);
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether whether tstripAlterInputImage is enabled (while allowing it to be moved).
        /// </summary>
        /// <value>
        ///     <c>true</c> if tstripAlterInputImage is enabled; otherwise, <c>false</c>.
        /// </value>
        private bool AlterInputImageToolStripIsEnabled
        {
            get
            {
                return this.tsbRotateClockwise.Enabled;
            }

            set
            {
                foreach (ToolStripItem item in this.tstripAlterInputImage.Items)
                {
                    item.Enabled = value;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether an area of the image is selected.
        /// </summary>
        /// <value><c>true</c> if an area is selected; otherwise, <c>false</c>.</value>
        private bool AreaIsSelected
        {
            get
            {
                if (!this.ImageIsLoaded)
                {
                    return false;
                }

                return this.pbxMain.SelectedArea.Width > 0 && this.pbxMain.SelectedArea.Height > 0;
            }
        }

        /// <summary>
        /// Gets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        private Color BackgroundColor
        {
            get
            {
                return this.IsBlackTextOnWhite ? Color.White : Color.Black;
            }
        }

        /// <summary>
        /// Gets or sets the text brightness.
        /// </summary>
        /// <value>The text brightness.</value>
        private int Brightness
        {
            get
            {
                return this.textSettings.Brightness;
            }

            set
            {
                this.textSettings.Brightness = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether we are calculating the character size.
        /// </summary>
        /// <value>
        /// <c>true</c> if calculating character size; otherwise, <c>false</c>.
        /// </value>
        private bool CalculatingCharacterSize
        {
            get
            {
                return this.textSettings.CalculateCharacterSize;
            }

            set
            {
                this.textSettings.CalculateCharacterSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of one character.
        /// </summary>
        /// <value>The size of one character.</value>
        private Size CharacterSize
        {
            get
            {
                return this.textSettings.CharacterSize;
            }

            set
            {
                this.dimensionsCalculator.CharacterSize = this.textSettings.CharacterSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the text contrast.
        /// </summary>
        /// <value>The text contrast.</value>
        private int Contrast
        {
            get
            {
                return this.textSettings.Contrast;
            }

            set
            {
                this.textSettings.Contrast = value;
            }
        }

        /// <summary>
        /// Gets the currently active section of the image.
        /// </summary>
        /// <value>The current image section.</value>
        private Rectangle CurrentImageSection
        {
            get
            {
                if (!this.ImageIsLoaded)
                {
                    return Rectangle.Empty;
                }

                if (!this.AreaIsSelected)
                {
                    return new Rectangle(0, 0, this.pbxMain.Image.Width, this.pbxMain.Image.Height);
                }

                return this.pbxMain.SelectedArea;
            }
        }

        /// <summary>
        /// Gets or sets the dithering amount.
        /// </summary>
        /// <value>The dithering.</value>
        private int Dithering
        {
            get
            {
                return this.textSettings.Dithering;
            }

            set
            {
                if (this.textSettings.Dithering == value)
                {
                    return;
                }

                this.textSettings.Dithering = value;

                this.ApplyTextEffects();
            }
        }

        /// <summary>
        /// Gets or sets the dithering amount.
        /// </summary>
        /// <value>The dithering.</value>
        private int DitheringRandom
        {
            get
            {
                return this.textSettings.DitheringRandom;
            }

            set
            {
                if (this.textSettings.DitheringRandom == value)
                {
                    return;
                }

                this.textSettings.DitheringRandom = value;

                this.ApplyTextEffects();
            }
        }

        /// <summary>
        /// Gets or sets the full filename of the image.
        /// </summary>
        /// <value>The filename.</value>
        private string Filename
        {
            get
            {
                if (!this.ImageIsLoaded)
                {
                    return String.Empty;
                }

                return this.filename;
            }

            set
            {
                this.filename = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to flip the output horizontally.
        /// </summary>
        /// <value><c>true</c> if flipping horizontally; otherwise, <c>false</c>.</value>
        private bool FlipHorizontally
        {
            get
            {
                return this.textSettings.FlipHorizontally;
            }

            set
            {
                if (this.textSettings.FlipHorizontally == value)
                {
                    return;
                }

                this.textSettings.FlipHorizontally = value;

                this.ApplyTextEffects();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to flip the output vertically.
        /// </summary>
        /// <value><c>true</c> if flipping vertically; otherwise, <c>false</c>.</value>
        private bool FlipVertically
        {
            get
            {
                return this.textSettings.FlipVertically;
            }

            set
            {
                if (this.textSettings.FlipVertically == value)
                {
                    return;
                }

                this.textSettings.FlipVertically = value;

                this.ApplyTextEffects();
            }
        }

        /// <summary>
        /// Gets or sets the font for the text.
        /// </summary>
        private new Font Font
        {
            get
            {
                return this.textSettings.Font;
            }

            set
            {
                if (value == null)
                {
                    return;
                }

                this.textSettings.Font =
                                    this.textViewer.Font =
                                    Settings.Default.DefaultFont =
                                    this.dialogChooseFont.Font = value;

                this.tstripRamp.Visible =
                        this.chkGenerate.Visible =
                        this.lblRamp.Visible =
                        this.cmbRamp.Visible = this.IsFixedWidth;

                Size fontSize = FontFunctions.MeasureText("W", this.textSettings.Font);

                if (this.IsFixedWidth)
                {
                    if (this.IsGeneratedRamp)
                    {
                        this.Ramp = AsciiRampCreator.CreateRamp(this.textSettings.Font, this.ValidCharacters);
                    }
                }
                else
                {
                    fontSize.Width = 7;
                    this.inputChanged = true;
                }

                this.UpdateMenus();

                this.dimensionsCalculator.CharacterSize = fontSize;

                this.DoConvert();
            }
        }

        /// <summary>
        /// Gets a value indicating whether an image is loaded.
        /// </summary>
        /// <value><c>true</c> if an image is loaded; otherwise, <c>false</c>.</value>
        private bool ImageIsLoaded
        {
            get
            {
                return this.pbxMain.Image != null;
            }
        }

        /// <summary>
        /// Gets or sets the the directory for the input images
        /// </summary>
        /// <value>The input directory.</value>
        private string InputDirectory
        {
            get
            {
                return Settings.Default.InitialInputDirectory;
            }

            set
            {
                this.dialogLoadImage.InitialDirectory =
                    Settings.Default.InitialInputDirectory = value;
            }
        }

        /// <summary>
        /// Gets the size of the input image.
        /// </summary>
        /// <value>The size of the input.</value>
        private Size InputSize
        {
            get
            {
                if (!this.ImageIsLoaded)
                {
                    return new Size(0, 0);
                }

                if (this.AreaIsSelected)
                {
                    return this.pbxMain.SelectedArea.Size;
                }

                return this.pbxMain.Image.Size;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the output is black text on a white background.
        /// </summary>
        /// <value>
        /// <c>true</c> if black text on white; otherwise, <c>false</c>.
        /// </value>
        private bool IsBlackTextOnWhite
        {
            get
            {
                return this.textSettings.IsBlackTextOnWhite;
            }

            set
            {
                if (this.textSettings.IsBlackTextOnWhite == value)
                {
                    return;
                }

                this.textSettings.IsBlackTextOnWhite = value;

                this.textViewer.BackgroundColor = this.BackgroundColor;

                this.textViewer.TextColor = this.TextColor;

                this.ApplyTextEffects();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the font is fixed width.
        /// </summary>
        /// <value>
        /// <c>true</c> if fixed width; otherwise, <c>false</c>.
        /// </value>
        private bool IsFixedWidth
        {
            get
            {
                return this.textSettings.IsFixedWidth;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the form is full screen.
        /// </summary>
        /// <value><c>true</c> if full screen; otherwise, <c>false</c>.</value>
        private bool IsFullScreen
        {
            get
            {
                return this.isFullScreen;
            }

            set
            {
                if (this.isFullScreen == value)
                {
                    return;
                }

                this.isFullScreen = value;

                this.tsbFullScreen.Checked = this.IsFullScreen;

                if (this.isFullScreen)
                {
                    this.previousWindowState = this.WindowState;

                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        this.WindowState = FormWindowState.Normal;
                    }

                    this.previousFormPosition = new Rectangle(this.Location, this.Size);

                    this.FormBorderStyle = FormBorderStyle.None;

                    this.Location = new Point(0, 0);

                    this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                }
                else
                {
                    this.FormBorderStyle = FormBorderStyle.Sizable;

                    this.WindowState = this.previousWindowState;

                    this.Location = this.previousFormPosition.Location;

                    this.Size = this.previousFormPosition.Size;

                    this.Focus();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the ramp is generated or not.
        /// </summary>
        /// <value>
        /// <c>true</c> if generated ramp; otherwise, <c>false</c>.
        /// </value>
        private bool IsGeneratedRamp
        {
            get
            {
                return this.textSettings.IsGeneratedRamp;
            }

            set
            {
                if (this.textSettings.IsGeneratedRamp == value && this.chkGenerate.Checked == value && this.tstripCharacters.Visible == value)
                {
                    return;
                }

                this.tstripCharacters.Visible = this.chkGenerate.Checked = this.textSettings.IsGeneratedRamp = value;

                this.cmbRamp.Enabled = !value;

                if (!this.IsFixedWidth)
                {
                    return;
                }

                this.Ramp = value ?
                                    AsciiRampCreator.CreateRamp(this.Font, this.ValidCharacters) :
                                    this.cmbRamp.Text;

                this.inputChanged = true;

                this.imageSaved = false;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the forms orientation is landscape.
        /// </summary>
        /// <value>
        ///     <c>true</c> if landscape; otherwise, <c>false</c>.
        /// </value>
        private bool IsLandscape
        {
            get
            {
                return this.splitContainer1.Orientation == Orientation.Vertical;
            }

            set
            {
                Orientation orientation = value ? Orientation.Vertical : Orientation.Horizontal;

                if (this.splitContainer1.Orientation == orientation)
                {
                    return;
                }

                this.splitContainer1.Orientation = orientation;

                if (this.splitContainer1.SplitterDistance > 300)
                {
                    this.splitContainer1.SplitterDistance = 300;
                }

                TableLayoutPanelCellPosition buttonPosition = new TableLayoutPanelCellPosition(
                                                                                    value ? 1 : 0,
                                                                                    value ? 0 : 1);

                this.tableLayoutPanelText.SetCellPosition(this.buttonToggleImage, buttonPosition);

                if (value)
                {
                    this.buttonToggleImage.Width = 18;
                }
                else
                {
                    this.buttonToggleImage.Height = 18;
                }

                this.UpdateButtonToggleImageText();
            }
        }

        /// <summary>
        /// Gets or sets the maximum level.
        /// </summary>
        /// <value>The maximum level.</value>
        private int MaximumLevel
        {
            get
            {
                return this.textSettings.MaximumLevel;
            }

            set
            {
                this.textSettings.MaximumLevel = value;
            }
        }

        /// <summary>
        /// Gets or sets the median level.
        /// </summary>
        /// <value>The median level.</value>
        private float MedianLevel
        {
            get
            {
                return this.textSettings.MedianLevel;
            }

            set
            {
                this.textSettings.MedianLevel = value;
            }
        }

        /// <summary>
        /// Gets or sets the minimum level.
        /// </summary>
        /// <value>The minimum level.</value>
        private int MinimumLevel
        {
            get
            {
                return this.textSettings.MinimumLevel;
            }

            set
            {
                this.textSettings.MinimumLevel = value;
            }
        }

        /// <summary>
        /// Gets or sets the directory for the output images
        /// </summary>
        /// <value>The output directory.</value>
        private string OutputDirectory
        {
            get
            {
                return Settings.Default.InitialOutputDirectory;
            }

            set
            {
                this.dialogSaveImage.InitialDirectory =
                        this.dialogSaveText.InitialDirectory =
                        this.dialogSaveColour.InitialDirectory =
                        Settings.Default.InitialOutputDirectory = value;
            }
        }

        /// <summary>
        /// Gets or sets the height of the output text.
        /// </summary>
        /// <value>The height of the output.</value>
        private int OutputHeight
        {
            get
            {
                return this.textSettings.Height;
            }

            set
            {
                this.textSettings.Height = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the output text.
        /// </summary>
        /// <value>The width of the output.</value>
        private int OutputWidth
        {
            get
            {
                return this.textSettings.Width;
            }

            set
            {
                this.textSettings.Width = value;
            }
        }

        /// <summary>
        /// Gets or sets the ramp.
        /// </summary>
        /// <value>The ramp (black to white).</value>
        private string Ramp
        {
            get
            {
                return this.textSettings.Ramp;
            }

            set
            {
                this.textSettings.Ramp = value;

                this.ApplyTextEffects();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to sharpen the output.
        /// </summary>
        /// <value><c>true</c> if sharpening; otherwise, <c>false</c>.</value>
        private bool Sharpen
        {
            get
            {
                return this.textSettings.Sharpen;
            }

            set
            {
                this.textSettings.Sharpen = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the output is stretched.
        /// </summary>
        /// <value><c>true</c> if stretched; otherwise, <c>false</c>.</value>
        private bool Stretch
        {
            get
            {
                return this.textSettings.Stretch;
            }

            set
            {
                this.textSettings.Stretch = value;
            }
        }

        /// <summary>
        /// Gets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        private Color TextColor
        {
            get
            {
                return this.IsBlackTextOnWhite ? Color.Black : Color.White;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the output uses an unsharp mask.
        /// </summary>
        /// <value><c>true</c> if unsharp mask; otherwise, <c>false</c>.</value>
        private bool Unsharp
        {
            get
            {
                return this.textSettings.Unsharp;
            }

            set
            {
                this.textSettings.Unsharp = value;
            }
        }

        /// <summary>
        /// Gets or sets the valid characters.
        /// </summary>
        /// <value>The valid characters.</value>
        private string ValidCharacters
        {
            get
            {
                return this.textSettings.ValidCharacters;
            }

            set
            {
                if (value == null || this.textSettings.ValidCharacters == value)
                {
                    return;
                }

                this.textSettings.ValidCharacters = value;

                if (this.IsGeneratedRamp || !this.IsFixedWidth)
                {
                    this.ApplyTextEffects();
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the values array has been created.
        /// </summary>
        /// <value><c>true</c> if values created; otherwise, <c>false</c>.</value>
        private bool ValuesCreated
        {
            get
            {
                return this.values != null;
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Get a rectangle representing the total actual area printable on the page
        /// </summary>
        /// <param name="e">The print parameters</param>
        /// <param name="centerOnPage">Make the margins equal so the area is centered on the page?</param>
        /// <param name="isPrintPreview">Is this for a print preview?</param>
        /// <returns>The printable area</returns>
        public static Rectangle GetPrintableArea(PrintPageEventArgs e, bool centerOnPage, bool isPrintPreview)
        {
            Rectangle printableArea = Rectangle.Round(e.PageSettings.PrintableArea);

            int leftMargin = printableArea.X;
            int topMargin = printableArea.Y;
            int rightMargin = e.PageBounds.Width - (e.PageSettings.Landscape ? printableArea.Bottom : printableArea.Right);
            int bottomMargin = e.PageBounds.Height - (e.PageSettings.Landscape ? printableArea.Right : printableArea.Bottom);

            if (centerOnPage)
            {
                // use the biggest values to center the image
                leftMargin = rightMargin = Math.Max(leftMargin, rightMargin);
                topMargin = bottomMargin = Math.Max(topMargin, bottomMargin);
            }

            Rectangle result = new Rectangle(
                                    leftMargin,
                                    topMargin,
                                    e.PageBounds.Width - leftMargin - rightMargin - 1,
                                    e.PageBounds.Height - topMargin - bottomMargin - 1);

            if (!isPrintPreview)
            {
                // apply physical offset for the printer
                result.Offset(-(int)e.PageSettings.HardMarginX, -(int)e.PageSettings.HardMarginY);
            }

            return result;
        }

        /// <summary>
        /// Load an image into the picturebox, and setup the form etc.
        /// </summary>
        /// <param name="image">The image to load</param>
        /// <returns>Did everything work correctly?</returns>
        public bool LoadImage(Image image)
        {
            if (!this.CloseImage())
            {
                return false;
            }

            this.doConversion = false;

            this.pbxMain.DrawingImage = false;

            this.pbxMain.Image = image;

            this.pbxMain.DrawingImage = true;

            this.UpdateTitle();

            this.AlterInputImageToolStripIsEnabled =
                this.widgetTextSettings.Enabled = this.widgetImageBrightnessContrast.Enabled = true;

            this.widgetImageBrightnessContrast.Refresh();

            this.widgetTextSettings.Refresh();

            this.UpdateMenus();

            this.inputChanged = true;

            this.dimensionsCalculator.ImageSize = image.Size;

            this.UpdateTextSizeControls();

            this.inputChanged = true;

            this.doConversion = true;

            return this.DoConvert();
        }

        /// <summary>
        /// Load the specified image into the picturebox, and setup the form etc.
        /// </summary>
        /// <param name="filename">Path to the image</param>
        /// <returns>Did the image load correctly?</returns>
        public bool LoadImage(string filename)
        {
            if (!this.CloseImage())
            {
                return false;
            }

            try
            {
                Image image;

                using (Image loadedImage = Image.FromFile(filename))
                {
                    Size size;

                    if (loadedImage.GetType() == typeof(Metafile))
                    {
                        size = new Size(1000, (int)((1000f * ((float)loadedImage.Height / (float)loadedImage.Width)) + 0.5f));
                    }
                    else
                    {
                        size = new Size(loadedImage.Width, loadedImage.Height);
                    }

                    image = new Bitmap(size.Width, size.Height);

                    using (Graphics g = Graphics.FromImage(image))
                    {
                        g.Clear(this.BackgroundColor);
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(loadedImage, 0, 0, size.Width, size.Height);
                    }
                }

                this.dialogLoadImage.FileName = this.Filename = filename;

                return this.LoadImage(image);
            }
            catch (OutOfMemoryException)
            { // Catch any bad image files
                MessageBox.Show(
                            Resource.GetString("Unknown or Unsupported File"),
                            Resource.GetString("Error"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                return false;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(
                            Resource.GetString("File Not Found"),
                            Resource.GetString("Error"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                return false;
            }
        }

        /// <summary>
        /// Display the font dialog and process the result
        /// </summary>
        public void ShowFontDialog()
        {
            try
            {
                if (this.dialogChooseFont.ShowDialog() == DialogResult.OK)
                {
                    this.Font = this.dialogChooseFont.Font;
                }
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show(
                            Resource.GetString("Unable to select this font"),
                            Resource.GetString("Error"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.ServiceNotification);
            }
        }

        #endregion Public methods

        #region Protected methods

        /// <summary>
        /// Overriden OnResize to handle widget positions
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                return;
            }

            base.OnResize(e);
        }

        #endregion Protected methods

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
        /// Applies the text brightness contrast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ApplyTextBrightnessContrast(object sender, EventArgs e)
        {
            this.Brightness = this.brightnessContrast.Brightness;
            this.Contrast = this.brightnessContrast.Contrast;

            this.ApplyTextEffects();

            this.UpdateLevelsArray();
        }

        /// <summary>
        /// Applies the text effects.
        /// </summary>
        private void ApplyTextEffects()
        {
            if (!this.ValuesCreated)
            {
                return;
            }

            this.textViewer.Lines = AscgenConverter.Convert(this.values, this.textSettings);
        }

        /// <summary>
        /// Appies the image brightness.
        /// </summary>
        private void AppyImageBrightness()
        {
            this.pbxMain.Brightness = (float)this.imageBrightnessContrast.Brightness / (float)255;
            this.inputChanged = true;
            this.imageSaved = false;
        }

        /// <summary>
        /// Appies the image contrast.
        /// </summary>
        private void AppyImageContrast()
        {
            this.pbxMain.Contrast = ((float)this.imageBrightnessContrast.Contrast / (float)113) + 1f;
            this.inputChanged = true;
            this.imageSaved = false;
        }

        /// <summary>
        /// Handles the Click event of the buttonToggleImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonToggleImage_Click(object sender, EventArgs e)
        {
            this.ToggleImageVisibility();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the cbxLocked control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CbxLocked_CheckedChanged(object sender, System.EventArgs e)
        {
            this.dimensionsCalculator.DimensionsAreLocked = this.cbxLocked.Checked;
        }

        /// <summary>
        /// Checks if we want to close without saving.
        /// </summary>
        /// <returns>A value with whether we are closing without saving</returns>
        private bool CheckCloseWithoutSaving()
        {
            if (this.imageSaved || !this.ImageIsLoaded || !Settings.Default.ConfirmOnClose)
            {
                return true;
            }

            switch (MessageBox.Show(
                                Resource.GetString("Save the output before closing") + "?",
                                Resource.GetString("Warning"),
                                MessageBoxButtons.YesNoCancel,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1))
            {
                case DialogResult.Yes:      // save then close if save dialog ok
                    return this.ShowSaveDialog();

                case DialogResult.No:       // close
                    return true;

                default:
                case DialogResult.Cancel:   // don't do anything
                    return false;
            }
        }

        /// <summary>
        /// Checks the executables directory for translation files.
        /// </summary>
        private void CheckForTranslationFiles()
        {
            DirectoryInfo dir = new DirectoryInfo(Application.StartupPath);

            FileInfo[] files = dir.GetFiles("translation.*.xml");

            StringCollection strings = new StringCollection();

            foreach (FileInfo file in files)
            {
                strings.Add(file.ToString());
            }

            if (strings.Count < 2)
            {
                if (strings.Count == 1)
                {
                    Settings.Default.TranslationFile = strings[0];
                }

                return;
            }

            using (FormSelectLanguage formSelectLanguage = new FormSelectLanguage(strings))
            {
                if (formSelectLanguage.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                Settings.Default.TranslationFile = formSelectLanguage.SelectedItem;
            }
        }

        /// <summary>
        /// Update and check if the text image is valid
        /// </summary>
        /// <returns>Is it ok for the text image to be saved?</returns>
        private bool CheckIfSavable()
        {
            if (!this.DoConvert())
            {
                if (this.textViewer.IsEmpty)
                {
                    MessageBox.Show(
                                this,
                                Resource.GetString("Invalid Output Size"),
                                Resource.GetString("Error"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions.ServiceNotification);
                }

                return false;
            }

            if (this.Ramp.Length == 0)
            {
                MessageBox.Show(
                            this,
                            Resource.GetString("Invalid ASCII Ramp"),
                            Resource.GetString("Error"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.ServiceNotification);

                this.cmbRamp.Focus();

                return false;
            }

            return true;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkGenerate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ChkGenerate_CheckedChanged(object sender, System.EventArgs e)
        {
            this.IsGeneratedRamp = this.chkGenerate.Checked;
        }

        /// <summary>
        /// Closes the image.
        /// </summary>
        /// <returns>Did we close the image?</returns>
        private bool CloseImage()
        {
            if (!this.ImageIsLoaded)
            {
                return true;
            }

            if (!this.CheckCloseWithoutSaving())
            {
                return false;
            }

            this.pbxMain.Image = null;

            this.Filename = string.Empty;

            this.textViewer.Clear();

            this.values = null;

            this.UpdateTitle();

            this.imageBrightnessContrast.Brightness = Settings.Default.DefaultImageBrightness;

            this.imageBrightnessContrast.Contrast = Settings.Default.DefaultImageContrast;

            this.brightnessContrast.Brightness = Settings.Default.DefaultTextBrightness;

            this.brightnessContrast.Contrast = Settings.Default.DefaultTextContrast;

            this.MinimumLevel = this.levels.Minimum = Settings.Default.DefaultMinLevel;

            this.MedianLevel = this.levels.Median = Settings.Default.DefaultMedianLevel;

            this.MaximumLevel = this.levels.Maximum = Settings.Default.DefaultMaxLevel;

            this.widgetImageBrightnessContrast.Refresh();

            this.widgetTextSettings.Refresh();

            this.AlterInputImageToolStripIsEnabled =
                    this.widgetTextSettings.Enabled =
                    this.widgetImageBrightnessContrast.Enabled = false;

            this.UpdateMenus();

            return true;
        }

        /// <summary>
        /// Handles the DropDown event of the cmbCharacters control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmbCharacters_DropDown(object sender, System.EventArgs e)
        {
            int width = this.cmbCharacters.Width;

            foreach (string characters in this.cmbCharacters.Items)
            {
                Size textSize = FontFunctions.MeasureText(characters + "  ", this.cmbCharacters.Font);

                if (textSize.Width > width)
                {
                    width = textSize.Width;
                }
            }

            this.cmbCharacters.DropDownWidth = width;
        }

        /// <summary>
        /// Handles the TextChanged event of the cmbCharacters control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmbCharacters_TextChanged(object sender, System.EventArgs e)
        {
            if (!this.cmbCharacters.Visible || this.cmbCharacters.Text.Length < 1)
            {
                return;
            }

            this.ValidCharacters = this.cmbCharacters.Text;
        }

        /// <summary>
        /// Handles the DropDown event of the cmbRamp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmbRamp_DropDown(object sender, System.EventArgs e)
        {
            int width = this.cmbRamp.Width;

            foreach (string ramp in this.cmbRamp.Items)
            {
                Size size = FontFunctions.MeasureText(ramp + "  ", this.cmbRamp.Font);

                if (size.Width > width)
                {
                    width = size.Width;
                }
            }

            this.cmbRamp.DropDownWidth = width;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbRamp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmbRamp_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.Ramp = this.cmbRamp.Text;
        }

        /// <summary>
        /// Handles the TextChanged event of the cmbRamp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmbRamp_TextChanged(object sender, System.EventArgs e)
        {
            this.Ramp = this.cmbRamp.Text;
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the cmbSharpening control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmbSharpening_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            this.ApplyTextEffects();
        }

        /// <summary>
        /// Handles the Click event of the cmenuCopy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuCopy_Click(object sender, System.EventArgs e)
        {
            this.textViewer.Copy();
        }

        /// <summary>
        /// Handles the Click event of the cmenuImageFlipHorizontal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuImageFlipHorizontal_Click(object sender, EventArgs e)
        {
            this.DoRotateFlip(RotateFlipType.RotateNoneFlipX);
        }

        /// <summary>
        /// Handles the Click event of the cmenuImageFlipVertical control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuImageFlipVertical_Click(object sender, EventArgs e)
        {
            this.DoRotateFlip(RotateFlipType.RotateNoneFlipY);
        }

        /// <summary>
        /// Handles the Click event of the cmenuImageRotate180 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuImageRotate180_Click(object sender, EventArgs e)
        {
            this.DoRotateFlip(RotateFlipType.Rotate180FlipNone);
        }

        /// <summary>
        /// Handles the Click event of the cmenuImageRotate270 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuImageRotate270_Click(object sender, EventArgs e)
        {
            this.DoRotateFlip(RotateFlipType.Rotate270FlipNone);
        }

        /// <summary>
        /// Handles the Click event of the cmenuImageRotate90 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuImageRotate90_Click(object sender, EventArgs e)
        {
            this.DoRotateFlip(RotateFlipType.Rotate90FlipNone);
        }

        /// <summary>
        /// Handles the Click event of the cmenuImageSelectionBorderColor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuImageSelectionBorderColor_Click(object sender, System.EventArgs e)
        {
            this.pbxMain.SelectionBorderColor = this.ShowColorDialog(this.pbxMain.SelectionBorderColor);
        }

        /// <summary>
        /// Handles the Click event of the cmenuImageSelectionFillColor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuImageSelectionFillColor_Click(object sender, System.EventArgs e)
        {
            this.pbxMain.SelectionFillColor = this.ShowColorDialog(this.pbxMain.SelectionFillColor);
        }

        /// <summary>
        /// Handles the Click event of the cmenuImageSelectionLocked control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuImageSelectionLocked_Click(object sender, System.EventArgs e)
        {
            this.pbxMain.SelectionLocked = !this.pbxMain.SelectionLocked;
        }

        /// <summary>
        /// Handles the Click event of the cmenuImageSelectionShow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuImageSelectionShow_Click(object sender, System.EventArgs e)
        {
            this.pbxMain.FillSelectionRectangle = !this.pbxMain.FillSelectionRectangle;
        }

        /// <summary>
        /// Handles the Click event of the cmenuImageSelectNone control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuImageSelectNone_Click(object sender, System.EventArgs e)
        {
            this.pbxMain.SelectNothing();
        }

        /// <summary>
        /// Handles the Click event of the cmenuImageUpdateWhileSelecting control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuImageUpdateWhileSelecting_Click(object sender, EventArgs e)
        {
            Settings.Default.UpdateWhileSelecting = !Settings.Default.UpdateWhileSelecting;
        }

        /// <summary>
        /// Handles the Click event of the cmenuLoad control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuLoad_Click(object sender, System.EventArgs e)
        {
            this.LoadDialog();
        }

        /// <summary>
        /// Handles the Click event of the cmenuSelectAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuSelectAll_Click(object sender, System.EventArgs e)
        {
            this.textViewer.SelectAll();
        }

        /// <summary>
        /// Handles the Click event of the cmenuSelectNone control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuSelectNone_Click(object sender, System.EventArgs e)
        {
            this.textViewer.SelectNone();
        }

        /// <summary>
        /// Handles the Click event of the cmenuTextFont control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuTextFont_Click(object sender, System.EventArgs e)
        {
            this.ShowFontDialog();
        }

        /// <summary>
        /// Handles the Click event of the cmenuTextHorizontal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuTextHorizontal_Click(object sender, System.EventArgs e)
        {
            this.FlipHorizontally = !this.FlipHorizontally;
        }

        /// <summary>
        /// Handles the Click event of the cmenuTextSharpeningNone control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuTextSharpeningNone_Click(object sender, System.EventArgs e)
        {
            this.Unsharp = this.Sharpen = false;
            this.ApplyTextEffects();
        }

        /// <summary>
        /// Handles the Click event of the cmenuTextSharpeningSharpen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuTextSharpeningSharpen_Click(object sender, System.EventArgs e)
        {
            this.Sharpen = true;
            this.Unsharp = false;
            this.ApplyTextEffects();
        }

        /// <summary>
        /// Handles the Click event of the cmenuTextSharpeningUnsharp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuTextSharpeningUnsharp_Click(object sender, System.EventArgs e)
        {
            this.Sharpen = false;
            this.Unsharp = true;
            this.ApplyTextEffects();
        }

        /// <summary>
        /// Handles the Click event of the cmenuTextStretch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuTextStretch_Click(object sender, System.EventArgs e)
        {
            this.Stretch = !this.Stretch;
            this.ApplyTextEffects();
            this.UpdateLevelsArray();
        }

        /// <summary>
        /// Handles the Click event of the cmenuTextVertical control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuTextVertical_Click(object sender, System.EventArgs e)
        {
            this.FlipVertically = !this.FlipVertically;
        }

        /// <summary>
        /// Handles the Popup event of the contextMenuImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ContextMenuImage_Popup(object sender, System.EventArgs e)
        {
            this.cmenuImageSelectionLocked.Enabled =
                    this.cmenuImageSelectionShow.Enabled =
                    this.cmenuImageSelectNone.Enabled = this.AreaIsSelected;

            this.cmenuImageRotate90.Enabled =
                    this.cmenuImageRotate180.Enabled =
                    this.cmenuImageRotate270.Enabled =
                    this.cmenuImageFlipHorizontal.Enabled =
                    this.cmenuImageFlipVertical.Enabled = this.ImageIsLoaded;

            this.cmenuImageSelectionLocked.Checked = this.pbxMain.SelectionLocked;

            this.cmenuImageSelectionShow.Checked = this.pbxMain.FillSelectionRectangle;

            this.cmenuImageUpdateWhileSelecting.Checked = Settings.Default.UpdateWhileSelecting;
        }

        /// <summary>
        /// Handles the Popup event of the contextMenuText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ContextMenuText_Popup(object sender, System.EventArgs e)
        {
            this.cmenuTextFont.Enabled = this.cmenuTextSelectAll.Enabled = !this.textViewer.IsEmpty;

            this.cmenuTextCopy.Enabled = this.cmenuTextSelectNone.Enabled = this.textViewer.SelectionLength > 0;

            this.cmenuTextStretch.Checked = this.Stretch;

            this.cmenuTextStretch.Enabled =
                    this.cmenuTextSharpening.Enabled =
                    this.cmenuTextSharpeningNone.Enabled =
                    this.cmenuTextSharpeningSharpen.Enabled =
                    this.cmenuTextSharpeningUnsharp.Enabled =
                    this.cmenuTextHorizontal.Enabled =
                    this.cmenuTextVertical.Enabled = this.ImageIsLoaded;

            this.cmenuTextSharpeningNone.Checked = !this.Sharpen && !this.Unsharp;
            this.cmenuTextSharpeningSharpen.Checked = this.Sharpen;
            this.cmenuTextSharpeningUnsharp.Checked = this.Unsharp;

            this.cmenuTextHorizontal.Checked = this.FlipHorizontally;
            this.cmenuTextVertical.Checked = this.FlipVertically;
        }

        /// <summary>
        /// Creates the colour image.
        /// </summary>
        /// <param name="zoom">The zoom level to use.</param>
        /// <returns>The new colour image</returns>
        private Image CreateColourImage(float zoom)
        {
            Color[][] colors = ImageToColors.Convert(
                                    (Bitmap)this.pbxMain.BCImage,
                                    new Size(this.OutputWidth, this.OutputHeight),
                                    this.CurrentImageSection,
                                    (this.dialogSaveColour.FilterIndex == 1 || this.dialogSaveColour.FilterIndex == 2));

            Image image = TextToColorImage.Convert(
                                    this.textViewer.Lines,
                                    this.Font,
                                    colors,
                                    this.BackgroundColor,
                                    zoom);

            return image;
        }

        /// <summary>
        /// Handles the OnOutputSizeChanged event of the dimensionsCalculator object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DimensionsCalculator_OnOutputSizeChanged(object sender, EventArgs e)
        {
            this.UpdateTextSizeControls();

            this.inputChanged = true;

            this.OutputWidth = this.dimensionsCalculator.Width;
            this.OutputHeight = this.dimensionsCalculator.Height;

            this.DoConvert();
        }

        /// <summary>
        /// Raised when the dithering is changing
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DitheringChanging(object sender, EventArgs e)
        {
            this.Dithering = this.dither.DitherAmount;
        }

        /// <summary>
        /// Raised when the dithering random has changed
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DitheringRandomChanged(object sender, EventArgs e)
        {
            this.DitheringRandom = this.dither.DitherRandom;
        }

        /// <summary>
        /// Process the conversion
        /// </summary>
        /// <returns>Did the conversion succeed?</returns>
        private bool DoConvert()
        {
            if (!this.doConversion || !this.ImageIsLoaded)
            {
                return false;
            }

            if (!this.inputChanged)
            {
                return true;
            }

            if (this.OutputWidth < 1 || this.OutputHeight < 1)
            {
                this.textViewer.Clear();

                this.tbxWidth.Focus();

                return false;
            }

            // convert the image into values
            this.values = ImageToValues.Convert(
                              (Bitmap)this.pbxMain.Image,
                              new Size(this.OutputWidth, this.OutputHeight),
                              JMSoftware.Matrices.BrightnessContrast(this.pbxMain.Brightness, this.pbxMain.Contrast),
                              this.CurrentImageSection);

            if (!this.ValuesCreated)
            {
                this.textViewer.Clear();

                MessageBox.Show(
                            Resource.GetString("Out of Memory, Could not convert the image"),
                            Resource.GetString("Error"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                return false;
            }

            this.UpdateLevelsArray();

            this.ApplyTextEffects();

            this.inputChanged = false;
            this.imageSaved = false;

            return true;
        }

        /// <summary>
        /// Function called by the PrintDocument to print the image
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
        private void DocumentPrint(object sender, PrintPageEventArgs e)
        {
            if (!this.DoConvert())
            {
                return;
            }

            ImageAttributes ia = new ImageAttributes();

            Rectangle printarea = GetPrintableArea(e, true, this.printDocument.PrintController.IsPreview);

            Bitmap canvas;

            if (this.printColour)
            {
                Color[][] colors = ImageToColors.Convert(
                                    (Bitmap)this.pbxMain.BCImage,
                                    new Size(this.OutputWidth, this.OutputHeight),
                                    this.CurrentImageSection,
                                    false);

                canvas = (Bitmap)TextToColorImage.Convert(
                                     this.textViewer.Lines,
                                     this.Font,
                                     colors,
                                     this.BackgroundColor,
                                     100f);
            }
            else
            {
                canvas = TextToImage.Convert(
                                     this.textViewer.Text,
                                     this.Font,
                                     this.TextColor,
                                     this.BackgroundColor);

                // convert to greyscale to avoid cleartype colours problem
                ia.SetColorMatrix(JMSoftware.Matrices.Grayscale());
            }

            this.printDocument.DocumentName = "ASCII-" + Path.GetFileNameWithoutExtension(this.Filename);

            // calculate the area that the image will cover
            float printRatio = (float)printarea.Width / (float)printarea.Height;
            float imageRatio = (float)canvas.Width / (float)canvas.Height;

            Rectangle targetLocation = new Rectangle(printarea.X, printarea.Y, printarea.Width, printarea.Height);

            if (printRatio > imageRatio)
            {
                targetLocation.Width = (int)((imageRatio * (float)printarea.Height) + 0.5);
                targetLocation.X += (printarea.Width - targetLocation.Width) / 2;
            }
            else
            {
                targetLocation.Height = (int)(((float)printarea.Width / imageRatio) + 0.5);
                targetLocation.Y += (printarea.Height - targetLocation.Height) / 2;
            }

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            e.Graphics.DrawImage(canvas, targetLocation, 0, 0, canvas.Width, canvas.Height, GraphicsUnit.Pixel, ia);

            // Draw a border
            e.Graphics.DrawRectangle(Pens.Black, targetLocation);

            ia.Dispose();
        }

        /// <summary>
        /// Process the print request
        /// </summary>
        /// <param name="preview">Are we just showing a print preview?</param>
        /// <param name="useColor">Print with colour?</param>
        private void DoPrint(bool preview, bool useColor)
        {
            this.printColour = useColor;

            try
            {
                if (preview)
                {
                    this.printPreviewDialog.ShowDialog();
                }
                else
                {
                    this.printDocument.Print();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(
                        Resource.GetString("Print Error") + ": " + ex.Message,
                        Resource.GetString("Error"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Does the rotate/flip.
        /// </summary>
        /// <param name="type">The type of rotation/flip.</param>
        private void DoRotateFlip(RotateFlipType type)
        {
            this.pbxMain.RotateImage(type);

            this.dimensionsCalculator.ImageSize = this.CurrentImageSection.Size;

            this.inputChanged = true;

            this.DoConvert();
        }

        /// <summary>
        /// Handles the Closing event of the form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void FormConvertImage_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.ImageIsLoaded)
            {
                return;
            }

            e.Cancel = !this.CheckCloseWithoutSaving();
        }

        /// <summary>
        /// Handles the drag drop.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void HandleDragDrop(DragEventArgs e)
        {
            string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (fileNames.Length == 1)
            {
                this.LoadImage(fileNames[0]);
            }
        }

        /// <summary>
        /// Occurs when the images brightness has changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ImageBrightnessChanged(object sender, EventArgs e)
        {
            this.AppyImageBrightness();

            this.DoConvert();
        }

        /// <summary>
        /// Occurs when the images brightness is changing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ImageBrightnessChanging(object sender, EventArgs e)
        {
            this.AppyImageBrightness();
        }

        /// <summary>
        /// Occurs when the images contrast has changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ImageContrastChanged(object sender, EventArgs e)
        {
            this.AppyImageContrast();

            this.DoConvert();
        }

        /// <summary>
        /// Occurs when the images brightness is changing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ImageContrastChanging(object sender, EventArgs e)
        {
            this.AppyImageContrast();
        }

        /// <summary>
        /// Occurs when the levels values have changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LevelsChanged(object sender, EventArgs e)
        {
            if (this.MinimumLevel == this.levels.Minimum &&
                this.MedianLevel == this.levels.Median &&
                this.MaximumLevel == this.levels.Maximum)
            {
                return;
            }

            this.MinimumLevel = this.levels.Minimum;
            this.MedianLevel = this.levels.Median;
            this.MaximumLevel = this.levels.Maximum;

            this.ApplyTextEffects();
        }

        /// <summary>
        /// Show the load dialog, and process its result
        /// </summary>
        private void LoadDialog()
        {
            if (this.dialogLoadImage.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            this.LoadImage(this.dialogLoadImage.FileName);
        }

        /// <summary>
        /// Handles the Popup event of the menuEdit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEdit_Popup(object sender, System.EventArgs e)
        {
            this.menuEditInput.Enabled = this.menuEditOutput.Enabled = this.ImageIsLoaded;
        }

        /// <summary>
        /// Handles the Click event of the menuEditEditSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditEditSettings_Click(object sender, EventArgs e)
        {
            using (FormEditSettings settingsDialog = new FormEditSettings())
            {
                settingsDialog.DefaultFont = this.Font;

                if (settingsDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                this.InputDirectory = settingsDialog.InputDirectory;

                this.OutputDirectory = settingsDialog.OutputDirectory;

                if (this.Font != settingsDialog.DefaultFont)
                {
                    this.Font = settingsDialog.DefaultFont;
                }

                Settings.Default.ConfirmOnClose = settingsDialog.ConfirmOnClose;

                Settings.Default.CheckForNewVersions = settingsDialog.CheckForNewVersions;
            }
        }

        /// <summary>
        /// Handles the Click event of the menuEditFont control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditFont_Click(object sender, EventArgs e)
        {
            this.ShowFontDialog();
        }

        /// <summary>
        /// Handles the DropDownOpening event of the menuEditInput control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditInput_DropDownOpening(object sender, EventArgs e)
        {
            this.menuEditInput.DropDown.Enabled = this.menuEditInput.Enabled;
        }

        /// <summary>
        /// Handles the Popup event of the menuEditOutput control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditOutput_Popup(object sender, System.EventArgs e)
        {
            this.menuEditStretch.Checked = this.Stretch;

            this.menuEditFlipHorizontal.Checked = this.FlipHorizontally;

            this.menuEditFlipVertical.Checked = this.FlipVertically;

            this.menuEditOutput.DropDown.Enabled = this.menuEditOutput.Enabled;
        }

        /// <summary>
        /// Handles the Click event of the menuEditRampCopyRamp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditRampCopyRamp_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.Ramp, true);
        }

        /// <summary>
        /// Handles the DropDownOpening event of the menuEditRamps control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditRamps_DropDownOpening(object sender, EventArgs e)
        {
            this.menuEditRampsCopyRamp.Enabled = this.IsFixedWidth;
        }

        /// <summary>
        /// Handles the Click event of the menuEditSaveSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditSaveSettings_Click(object sender, System.EventArgs e)
        {
            this.SaveSettings();
        }

        /// <summary>
        /// Handles the Popup event of the menuEditSharpeningMethod control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditSharpeningMethod_Popup(object sender, System.EventArgs e)
        {
            this.menuEditSharpeningMethodNone.Checked = !this.Sharpen && !this.Unsharp;

            this.menuEditSharpeningMethodSharpen.Checked = this.Sharpen;

            this.menuEditSharpeningMethodUnsharp.Checked = this.Unsharp;
        }

        /// <summary>
        /// Handles the Click event of the menuEditSpecifyCharSize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditSpecifyCharSize_Click(object sender, EventArgs e)
        {
            using (CharacterSizeDialog characterDialog = new CharacterSizeDialog())
            {
                characterDialog.AutoCalculateSize = this.CalculatingCharacterSize;

                characterDialog.CharacterSize = this.CharacterSize;

                if (this.IsFixedWidth)
                {
                    if (this.CalculatingCharacterSize)
                    {
                        characterDialog.DefaultCharacterSize = this.CharacterSize;
                    }
                    else
                    {
                        characterDialog.DefaultCharacterSize = FontFunctions.GetFixedPitchFontSize(this.Font);
                    }
                }
                else
                {
                    characterDialog.DefaultCharacterSize = new Size(ValuesToVariableWidthTextConverter.CharacterWidth, FontFunctions.MeasureText("W", this.Font).Height);
                }

                if (characterDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                this.CalculatingCharacterSize = characterDialog.AutoCalculateSize;

                this.CharacterSize = characterDialog.CharacterSize;
            }
        }

        /// <summary>
        /// Handles the Click event of the menuEditValidChars control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditValidChars_Click(object sender, System.EventArgs e)
        {
            using (ValidRampCharsDialog validCharactersDialog = new ValidRampCharsDialog())
            {
                validCharactersDialog.Font = this.Font;

                validCharactersDialog.Characters = this.ValidCharacters;

                if (validCharactersDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                this.ValidCharacters = validCharactersDialog.Characters;

                this.cmbCharacters.Text = validCharactersDialog.Characters;
            }
        }

        /// <summary>
        /// Handles the Popup event of the menuFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFile_Popup(object sender, System.EventArgs e)
        {
            // enable the import item if an image is on the clipboard
            IDataObject data = Clipboard.GetDataObject();

            this.menuFileImportClipboard.Enabled = data.GetDataPresent(DataFormats.Bitmap, true);
        }

        /// <summary>
        /// Handles the Click event of the menuFileBatchConversion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFileBatchConversion_Click(object sender, EventArgs e)
        {
            this.formBatchConversion.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the menuFileClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFileClose_Click(object sender, System.EventArgs e)
        {
            if (!this.CloseImage())
            {
                return;
            }

            this.widgetTextSettings.LevelsArray = new int[256];
        }

        /// <summary>
        /// Handles the Click event of the menuFileExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFileExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the menuFileImportClipboard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFileImportClipboard_Click(object sender, System.EventArgs e)
        {
            IDataObject data = Clipboard.GetDataObject();

            if (!data.GetDataPresent(DataFormats.Bitmap) || !this.CloseImage())
            {
                return;
            }

            this.Filename = string.Format(Settings.Default.Culture, "Clipboard{0:yyyyMMddHHmmss}", System.DateTime.Now);

            this.LoadImage((Bitmap)data.GetData(DataFormats.Bitmap, true));
        }

        /// <summary>
        /// Handles the Click event of the menuFileLoad control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFileLoad_Click(object sender, System.EventArgs e)
        {
            this.LoadDialog();
        }

        /// <summary>
        /// Handles the Click event of the menuFilePageSetup control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFilePageSetup_Click(object sender, EventArgs e)
        {
            try
            {
                this.pageSetupDialog.ShowDialog();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(
                    Resource.GetString("Print Error") + ": " + ex.Message,
                    Resource.GetString("Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Click event of the menuFilePrint control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFilePrint_Click(object sender, EventArgs e)
        {
            if (this.printDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            this.DoPrint(false, false);
        }

        /// <summary>
        /// Handles the Click event of the menuFilePrintColour control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFilePrintColour_Click(object sender, EventArgs e)
        {
            if (this.printDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            this.DoPrint(false, false);
        }

        /// <summary>
        /// Handles the Click event of the menuFilePrintPreview control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFilePrintPreview_Click(object sender, EventArgs e)
        {
            this.DoPrint(true, false);
        }

        /// <summary>
        /// Handles the Click event of the menuFilePrintPreviewColour control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFilePrintPreviewColour_Click(object sender, EventArgs e)
        {
            this.DoPrint(true, true);
        }

        /// <summary>
        /// Handles the Click event of the menuFileSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFileSave_Click(object sender, System.EventArgs e)
        {
            this.ShowSaveDialog();
        }

        /// <summary>
        /// Handles the Click event of the menuHelpAbout control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuHelpAbout_Click(object sender, System.EventArgs e)
        {
            using (AboutDialog aboutDialog = new AboutDialog())
            {
                aboutDialog.ShowDialog();
            }
        }

        /// <summary>
        /// Handles the Click event of the menuHelpCheckForNewVersionToolStrip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuHelpCheckForNewVersionToolStrip_Click(object sender, EventArgs e)
        {
            this.versionChecker.Check(true);
        }

        /// <summary>
        /// Handles the Click event of the menuHelpDonate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuHelpDonate_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=wardog1uk%40gmail%2ecom&item_name=Ascii%20Generator%20%2eNET&no_shipping=2&no_note=1&tax=0&currency_code=GBP&bn=PP%2dDonationsBF&charset=UTF%2d8");
        }

        /// <summary>
        /// Handles the Click event of the menuHelpReportBug control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuHelpReportBug_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://sourceforge.net/tracker/?func=add&group_id=133786&atid=728164");
        }

        /// <summary>
        /// Handles the Click event of the menuHelpRequestFeature control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuHelpRequestFeature_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://sourceforge.net/tracker/?func=add&group_id=133786&atid=728167");
        }

        /// <summary>
        /// Handles the Click event of the menuHelpTutorials control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuHelpTutorials_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://ascgendotnet.jmsoftware.co.uk/tutorials");
        }

        /// <summary>
        /// Handles the Popup event of the menuView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuView_Popup(object sender, System.EventArgs e)
        {
            this.menuViewICBC.Checked = this.widgetImageBrightnessContrast.Visible;

            this.menuViewText.Checked = this.widgetTextSettings.Visible;

            this.menuViewFullScreen.Checked = this.IsFullScreen;

            this.menuViewImage.Checked = !this.splitContainer1.Panel2Collapsed;

            this.menuViewImagePosition.Checked = this.splitContainer1.Orientation == Orientation.Horizontal;
        }

        /// <summary>
        /// Handles the Click event of the menuViewColourPreview control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuViewColourPreview_Click(object sender, EventArgs e)
        {
            this.ShowColourPreview();
        }

        /// <summary>
        /// Handles the Click event of the menuViewFullScreen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuViewFullScreen_Click(object sender, System.EventArgs e)
        {
            this.IsFullScreen = !this.IsFullScreen;
        }

        /// <summary>
        /// Handles the Click event of the menuViewICBC control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuViewICBC_Click(object sender, System.EventArgs e)
        {
            this.widgetImageBrightnessContrast.Visible = !this.widgetImageBrightnessContrast.Visible;

            this.widgetImageBrightnessContrast.BringToFront();
        }

        /// <summary>
        /// Handles the Click event of the menuViewImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuViewImage_Click(object sender, EventArgs e)
        {
            this.ToggleImageVisibility();
        }

        /// <summary>
        /// Handles the Click event of the menuViewImagePosition control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuViewImagePosition_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Orientation = (this.splitContainer1.Orientation == Orientation.Horizontal) ?
                                                    Orientation.Vertical : Orientation.Horizontal;
        }

        /// <summary>
        /// Handles the Click event of the menuViewText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuViewText_Click(object sender, System.EventArgs e)
        {
            this.widgetTextSettings.Visible = !this.widgetTextSettings.Visible;

            this.widgetTextSettings.BringToFront();
        }

        /// <summary>
        /// Handles the DoubleClick event of the pbxMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PbxMain_DoubleClick(object sender, System.EventArgs e)
        {
            this.LoadDialog();
        }

        /// <summary>
        /// Handles the DragDrop event of the pbxMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void PbxMain_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            this.HandleDragDrop(e);
        }

        /// <summary>
        /// Handles the DragOver event of the pbxMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void PbxMain_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            HandleDragOver(e);
        }

        /// <summary>
        /// Handles the SelectionChanged event of the pbxMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PbxMain_SelectionChanged(object sender, System.EventArgs e)
        {
            this.ProcessSelectionAreaChange();
        }

        /// <summary>
        /// Handles the SelectionChanging event of the pbxMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PbxMain_SelectionChanging(object sender, EventArgs e)
        {
            if (!Settings.Default.UpdateWhileSelecting)
            {
                return;
            }

            this.ProcessSelectionAreaChange();
        }

        /// <summary>
        /// Handles the Resize event of the pnlMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PnlMain_Resize(object sender, EventArgs e)
        {
            this.RearrangeWidgets();
        }

        /// <summary>
        /// Updates the selection area if needed.
        /// </summary>
        private void ProcessSelectionAreaChange()
        {
            if (this.oldSelectionPosition == this.pbxMain.SelectedArea)
            {
                return;
            }

            if (this.oldSelectionPosition.Size == this.pbxMain.SelectedArea.Size)
            {
                this.inputChanged = true;

                this.DoConvert();
            }
            else
            {
                this.dimensionsCalculator.ImageSize = this.CurrentImageSection.Size;

                this.UpdateTextSizeControls();
            }

            this.oldSelectionPosition = this.pbxMain.SelectedArea;
        }

        /// <summary>
        /// Update the positions of the widgets
        /// </summary>
        private void RearrangeWidgets()
        {
            this.SuspendLayout();

            foreach (Control control in this.pnlMain.Controls)
            {
                if (control.GetType().BaseType != typeof(BaseWidget))
                {
                    continue;
                }

                if (control.Left > (this.clientSize.Width - control.Right))
                {
                    control.Left = this.pnlMain.ClientSize.Width - (this.clientSize.Width - control.Left);
                }

                if (control.Top > (this.clientSize.Height - control.Bottom))
                {
                    control.Top = this.pnlMain.ClientSize.Height - (this.clientSize.Height - control.Top);
                }

                control.Refresh();
            }

            this.ResumeLayout();

            this.clientSize = this.pnlMain.ClientSize;
        }

        /// <summary>
        /// Handles the DragDrop event of the rtbxConvertedText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void RtbxConvertedText_DragDrop(object sender, DragEventArgs e)
        {
            this.HandleDragDrop(e);
        }

        /// <summary>
        /// Handles the DragEnter event of the rtbxConvertedText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void RtbxConvertedText_DragEnter(object sender, DragEventArgs e)
        {
            HandleDragOver(e);
        }

        /// <summary>
        /// Show and process the dialog to save as an image
        /// </summary>
        /// <param name="file">Default filename for the output</param>
        /// <param name="useColour">Output as colour instead of black and white?</param>
        /// <returns>Was the file saved?</returns>
        private bool SaveAsImage(string file, bool useColour)
        {
            if (useColour && !this.IsFixedWidth)
            {
                throw new ArgumentException("Cannot use colour with variable width conversions");
            }

            if (!this.CheckIfSavable())
            {
                return false;
            }

            this.dialogChooseTextZoom.TextFont = this.Font;

            this.dialogChooseTextZoom.TextColor = this.TextColor;

            this.dialogChooseTextZoom.BackgroundColor = this.BackgroundColor;

            this.dialogChooseTextZoom.InputSize =
                FontFunctions.MeasureText(this.textViewer.Text, this.Font);

            if (this.dialogChooseTextZoom.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            this.dialogSaveImage.FileName = file;

            if (this.dialogSaveImage.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            string filename = this.dialogSaveImage.FileName;

            string extension = Path.GetExtension(filename).ToLower(Settings.Default.Culture);

            switch (this.dialogSaveImage.FilterIndex)
            {
                case 1:
                    if (extension != ".bmp" && extension != ".rle" && extension != ".dib")
                    {
                        filename += ".bmp";
                    }

                    break;

                case 2:
                    if (extension != ".gif")
                    {
                        filename += ".gif";
                    }

                    break;

                case 3:
                    if (extension != ".jpg" && extension != ".jpeg" && extension != ".jpe")
                    {
                        filename += ".jpg";
                    }

                    break;

                case 4:
                    if (extension != ".png")
                    {
                        filename += ".png";
                    }

                    break;

                case 5:
                    if (extension != ".tif")
                    {
                        filename += ".tif";
                    }

                    break;
            }

            if (useColour)
            {
                using (Image image = this.CreateColourImage(this.dialogChooseTextZoom.Value))
                {
                    image.Save(filename, ImageFunctions.GetImageFormat(extension));
                }

                this.imageSaved = true;
            }
            else
            {
                this.imageSaved = TextToImage.Save(
                                    this.textViewer.Text,
                                    filename,
                                    this.Font,
                                    this.dialogChooseTextZoom.TextColor,
                                    this.dialogChooseTextZoom.BackgroundColor,
                                    this.dialogChooseTextZoom.Value,
                                    true);
            }

            if (!this.imageSaved)
            {
                return false;
            }

            this.OutputDirectory = Path.GetDirectoryName(this.dialogSaveImage.FileName);

            return true;
        }

        /// <summary>
        /// Show and process the dialog to save as colour text
        /// </summary>
        /// <param name="filename">Default filename for the output</param>
        /// <returns>Was the file saved?</returns>
        private bool SaveColourTextDialog(string filename)
        {
            if (!this.IsFixedWidth)
            {
                throw new InvalidOperationException("Cannot use colour with variable width conversions");
            }

            if (!(this.CheckIfSavable() && this.IsFixedWidth))
            {
                return false;
            }

            this.dialogSaveColour.FileName = filename;

            if (this.dialogSaveColour.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            // create the array of Colors
            Color[][] colors = ImageToColors.Convert(
                                (Bitmap)this.pbxMain.BCImage,
                                new Size(this.OutputWidth, this.OutputHeight),
                                this.CurrentImageSection,
                                (this.dialogSaveColour.FilterIndex == 1 || this.dialogSaveColour.FilterIndex == 2));

            string[] strings = AscgenConverter.Convert(this.values, this.textSettings);

            string output;

            System.Text.Encoding encoding;

            switch (this.dialogSaveColour.FilterIndex)
            {
                case 2: // rtf 256 color
                case 4: // rtf 24-bit
                    output = OutputCreator.CreateRTF(strings, colors, this.textSettings);

                    encoding = System.Text.Encoding.ASCII;

                    break;

                case 1: // html 256 color
                case 3: // html 24-bit
                default:
                    output = OutputCreator.CreateHTML(
                                    strings,
                                    colors,
                                    this.BackgroundColor,
                                    this.textSettings,
                                    Path.GetFileNameWithoutExtension(this.Filename));

                    encoding = System.Text.Encoding.UTF8;

                    break;
            }

            using (StreamWriter writer = new StreamWriter(this.dialogSaveColour.FileName, false, encoding))
            {
                writer.Write(output);
            }

            this.imageSaved = true;

            this.OutputDirectory = Path.GetDirectoryName(this.dialogSaveColour.FileName);

            return true;
        }

        /// <summary>
        /// Save the current settings as XML
        /// </summary>
        private void SaveSettings()
        {
            Settings.Default.DefaultImageBrightness = this.imageBrightnessContrast.Brightness;
            Settings.Default.DefaultImageContrast = this.imageBrightnessContrast.Contrast;

            Settings.Default.DefaultWidth = this.OutputWidth;
            Settings.Default.DefaultHeight = this.OutputHeight;

            if (Settings.Default.DefaultWidth == -1 && Settings.Default.DefaultHeight == -1)
            {
                Settings.Default.DefaultWidth = 150;
            }

            if (this.dimensionsCalculator.DimensionsAreLocked)
            {
                if (this.dimensionsCalculator.WidthChangedLast)
                {
                    Settings.Default.DefaultHeight = -1;
                }
                else
                {
                    Settings.Default.DefaultWidth = -1;
                }
            }

            Settings.Default.DefaultFont = this.Font;

            Settings.Default.DefaultTextBrightness = this.Brightness;
            Settings.Default.DefaultTextContrast = this.Contrast;

            Settings.Default.DefaultMinLevel = this.MinimumLevel;
            Settings.Default.DefaultMedianLevel = this.MedianLevel;
            Settings.Default.DefaultMaxLevel = this.MaximumLevel;

            Settings.Default.DefaultDitheringLevel = this.Dithering;
            Settings.Default.DefaultDitheringRandom = this.DitheringRandom;

            Settings.Default.Stretch = this.Stretch;
            Variables.Sharpen = this.Sharpen;
            Settings.Default.UnsharpMask = this.Unsharp;

            Variables.FlipHorizontally = this.FlipHorizontally;
            Variables.FlipVertically = this.FlipVertically;

            Settings.Default.BlackTextOnWhite = this.IsBlackTextOnWhite;

            string[] ramps = new string[this.cmbRamp.Items.Count];
            this.cmbRamp.Items.CopyTo(ramps, 0);
            Variables.DefaultRamps = ramps;

            Settings.Default.CurrentSelectedRamp = this.cmbRamp.SelectedIndex;
            Settings.Default.CurrentRamp = this.cmbRamp.SelectedIndex == -1 ? this.cmbRamp.Text : String.Empty;

            Settings.Default.UseGeneratedRamp = this.IsGeneratedRamp;

            string[] characters = new string[this.cmbCharacters.Items.Count];
            this.cmbCharacters.Items.CopyTo(characters, 0);
            Variables.DefaultValidCharacters = characters;

            Settings.Default.CurrentSelectedValidCharacters = this.cmbCharacters.SelectedIndex;
            Settings.Default.CurrentCharacters = Settings.Default.CurrentSelectedValidCharacters == -1 ? this.cmbCharacters.Text : String.Empty;

            Settings.Default.ShowBCWidgetImage = this.widgetImageBrightnessContrast.Visible;
            Settings.Default.ShowWidgetText = this.widgetTextSettings.Visible;

            Settings.Default.SelectionBorderColor = this.pbxMain.SelectionBorderColor;
            Settings.Default.SelectionFillColor = this.pbxMain.SelectionFillColor;

            Variables.SaveSettings();
        }

        /// <summary>
        /// Show and process the dialog to save as text
        /// </summary>
        /// <param name="filename">Default filename for the output</param>
        /// <returns>Was the file saved?</returns>
        private bool SaveTextDialog(string filename)
        {
            if (!this.CheckIfSavable())
            {
                return false;
            }

            this.dialogSaveText.FileName = filename;

            if (this.dialogSaveText.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            RichTextBoxStreamType streamType = RichTextBoxStreamType.PlainText;

            bool saved = false;

            switch (this.dialogSaveText.FilterIndex)
            {
                case 1: // plain text
                    streamType = RichTextBoxStreamType.PlainText;
                    break;

                case 2: // plain text (unicode)
                case 3: // nfo
                    streamType = RichTextBoxStreamType.UnicodePlainText;
                    break;

                case 4: // rich text
                    streamType = RichTextBoxStreamType.RichText;
                    break;

                case 5: // XHTML
                    using (StreamWriter writer = new StreamWriter(this.dialogSaveText.FileName))
                    {
                        writer.Write(OutputCreator.CreateHTML(
                                            this.textViewer.Lines,
                                            null,
                                            this.BackgroundColor,
                                            this.textSettings,
                                            Path.GetFileNameWithoutExtension(this.Filename)));
                    }

                    saved = true;
                    break;
            }

            if (!saved)
            {
                this.rtbxConvertedText.SaveFile(this.dialogSaveText.FileName, streamType);
            }

            this.imageSaved = true;

            this.OutputDirectory = Path.GetDirectoryName(this.dialogSaveText.FileName);

            return true;
        }

        /// <summary>
        /// Sets up the forms controls.
        /// </summary>
        private void SetupControls()
        {
            this.UpdateTitle();

            this.tbxWidth.MaxLength = Settings.Default.MaximumWidth.ToString(Settings.Default.Culture).Length;

            this.tbxHeight.MaxLength = Settings.Default.MaximumHeight.ToString(Settings.Default.Culture).Length;

            this.cbxLocked.Checked = Settings.Default.DefaultWidth < 1 || Settings.Default.DefaultHeight < 1;

            this.pbxMain.SelectionBorderColor = Settings.Default.SelectionBorderColor;

            this.pbxMain.SelectionFillColor = Settings.Default.SelectionFillColor;

            this.pbxMain.AllowDrop = true;

            this.rtbxConvertedText.AllowDrop = true;
            this.rtbxConvertedText.DragDrop += new DragEventHandler(this.RtbxConvertedText_DragDrop);
            this.rtbxConvertedText.DragEnter += new DragEventHandler(this.RtbxConvertedText_DragEnter);

            this.tsbBlackOnWhite.Checked = !Settings.Default.BlackTextOnWhite;
            this.textViewer.BackgroundColor = this.BackgroundColor;
            this.textViewer.TextColor = this.TextColor;

            this.SetupToolstrip();

            this.cmbRamp.Items.Clear();
            this.cmbRamp.Items.AddRange(Variables.DefaultRamps);

            if (Settings.Default.CurrentSelectedRamp == -1)
            {
                this.cmbRamp.Text = Settings.Default.CurrentRamp;
            }
            else
            {
                this.cmbRamp.SelectedIndex = Settings.Default.CurrentSelectedRamp;
            }

            this.cmbRamp.Select(0, 0);

            this.IsGeneratedRamp = Settings.Default.UseGeneratedRamp;

            this.cmbCharacters.Items.Clear();
            this.cmbCharacters.Items.AddRange(Variables.DefaultValidCharacters);

            if (Settings.Default.CurrentSelectedValidCharacters == -1)
            {
                this.cmbCharacters.Text = Settings.Default.CurrentCharacters;
            }
            else
            {
                this.cmbCharacters.SelectedIndex = Settings.Default.CurrentSelectedValidCharacters;
            }

            this.cmbCharacters.Select(0, 0); // make sure the text isn't selected

            this.UpdateUI();

            this.UpdateMenus();

            this.splitContainer1.SendToBack();

            this.pbxMain.Select();
        }

        /// <summary>
        /// Setup the image widget.
        /// </summary>
        private void SetupImageWidget()
        {
            this.widgetImageBrightnessContrast = new WidgetBrightnessContrast();

            this.widgetImageBrightnessContrast.Enabled = this.ImageIsLoaded;

            this.widgetImageBrightnessContrast.BrightnessChanging += new EventHandler(this.ImageBrightnessChanging);
            this.widgetImageBrightnessContrast.BrightnessChanged += new EventHandler(this.ImageBrightnessChanged);

            this.widgetImageBrightnessContrast.ContrastChanging += new EventHandler(this.ImageContrastChanging);
            this.widgetImageBrightnessContrast.ContrastChanged += new EventHandler(this.ImageContrastChanged);

            this.widgetImageBrightnessContrast.Left = this.pnlMain.Width - this.widgetImageBrightnessContrast.Width - 4;
            this.widgetImageBrightnessContrast.Top = this.pnlMain.Height - this.widgetImageBrightnessContrast.Height - 4;
            this.widgetImageBrightnessContrast.MaximumBrightness = 200;
            this.widgetImageBrightnessContrast.MinimumBrightness = -200;
            this.widgetImageBrightnessContrast.MaximumContrast = 100;
            this.widgetImageBrightnessContrast.MinimumContrast = -100;

            this.imageBrightnessContrast = this.widgetImageBrightnessContrast;
            this.imageBrightnessContrast.Brightness = Settings.Default.DefaultImageBrightness;
            this.imageBrightnessContrast.Contrast = Settings.Default.DefaultImageContrast;
        }

        /// <summary>
        /// Setup the text widget.
        /// </summary>
        private void SetupTextWidget()
        {
            this.widgetTextSettings = new WidgetTextSettings();

            this.widgetTextSettings.ValueChanging += new EventHandler(this.ApplyTextBrightnessContrast);
            this.widgetTextSettings.ValueChanged += new EventHandler(this.ApplyTextBrightnessContrast);

            this.widgetTextSettings.LevelsChanged += new EventHandler(this.LevelsChanged);

            this.widgetTextSettings.DitheringChanging += new EventHandler(this.DitheringChanging);
            this.widgetTextSettings.DitheringChanged += new EventHandler(this.DitheringChanging);
            this.widgetTextSettings.DitheringRandomChanged += new EventHandler(this.DitheringRandomChanged);

            this.widgetTextSettings.MaximumBrightness = 200;
            this.widgetTextSettings.MinimumBrightness = -200;
            this.widgetTextSettings.MaximumContrast = 100;
            this.widgetTextSettings.MinimumContrast = -100;

            this.widgetTextSettings.Left = 4;
            this.widgetTextSettings.Top = this.pnlMain.Height - this.widgetTextSettings.Height - 4;
            this.widgetTextSettings.BringToFront();
            this.widgetTextSettings.Enabled = false;

            this.brightnessContrast = this.widgetTextSettings;
            this.brightnessContrast.Brightness = Settings.Default.DefaultTextBrightness;
            this.brightnessContrast.Contrast = Settings.Default.DefaultTextContrast;

            this.levels = this.widgetTextSettings;
            this.levels.Minimum = Settings.Default.DefaultMinLevel;
            this.levels.Maximum = Settings.Default.DefaultMaxLevel;
            this.levels.Median = Settings.Default.DefaultMedianLevel;

            this.dither = this.widgetTextSettings;
            this.dither.DitherAmount = Settings.Default.DefaultDitheringLevel;
            this.dither.DitherRandom = Settings.Default.DefaultDitheringRandom;
        }

        /// <summary>
        /// Empties and readds the toolstrips to get the desired layout (it adds from the bottom up).
        /// </summary>
        private void SetupToolstrip()
        {
            this.toolStripContainer1.TopToolStripPanel.Controls.Clear();

            this.toolStripContainer1.TopToolStripPanel.Join(this.mainMenu1);
            this.toolStripContainer1.TopToolStripPanel.Join(this.tstripAlterInputImage, 1);
            this.toolStripContainer1.TopToolStripPanel.Join(this.tstripButtons, 1);
            this.toolStripContainer1.TopToolStripPanel.Join(this.tstripCharacters, 1);
            this.toolStripContainer1.TopToolStripPanel.Join(this.tstripRamp, 1);
            this.toolStripContainer1.TopToolStripPanel.Join(this.tstripOutputSize, 1);

            foreach (ToolStrip strip in this.toolStripContainer1.TopToolStripPanel.Controls)
            {
                strip.BackColor = this.toolStripContainer1.TopToolStripPanel.BackColor;
                strip.GripMargin = new Padding(0);
                strip.GripStyle = ToolStripGripStyle.Hidden;
            }
        }

        /// <summary>
        /// Create and setup the widgets.
        /// </summary>
        private void SetupWidgets()
        {
            this.SetupImageWidget();

            this.SetupTextWidget();

            this.pnlMain.Controls.AddRange(new Control[] { this.widgetImageBrightnessContrast, this.widgetTextSettings });

            this.widgetImageBrightnessContrast.Visible = Settings.Default.ShowBCWidgetImage;

            this.widgetTextSettings.Visible = Settings.Default.ShowWidgetText;
        }

        /// <summary>
        /// Shows the color selection dialog.
        /// </summary>
        /// <param name="color">The current color.</param>
        /// <returns>The newly selected color</returns>
        private Color ShowColorDialog(Color color)
        {
            this.dialogSelectionColor.Color = color;

            if (this.dialogSelectionColor.ShowDialog() != DialogResult.OK)
            {
                return color;
            }

            return this.dialogSelectionColor.Color;
        }

        /// <summary>
        /// Shows the colour preview.
        /// </summary>
        private void ShowColourPreview()
        {
            if (!this.IsFixedWidth)
            {
                throw new InvalidOperationException("Cannot use colour with variable width conversions");
            }

            using (FormColourPreview preview = new FormColourPreview())
            {
                preview.Image = this.CreateColourImage(100);

                preview.ShowDialog(this);
            }
        }

        /// <summary>
        /// Shows the save dialog.
        /// </summary>
        /// <returns>Did we save the image?</returns>
        private bool ShowSaveDialog()
        {
            this.formSaveAs.IsFixedWidth = this.IsFixedWidth;

            bool saved = false;

            bool isText = this.formSaveAs.IsText;

            bool isColour = this.formSaveAs.IsColour;

            Size size = this.formSaveAs.Size;

            if (this.formSaveAs.ShowDialog() == DialogResult.OK)
            {
                string filename = Settings.Default.Prefix + Path.GetFileNameWithoutExtension(this.Filename);

                if (this.formSaveAs.IsText)
                {
                    saved = this.formSaveAs.IsColour ? this.SaveColourTextDialog(filename) : this.SaveTextDialog(filename);
                }
                else
                {
                    saved = this.SaveAsImage(filename, this.formSaveAs.IsColour);
                }
            }

            if (!saved)
            {
                this.formSaveAs.IsText = isText;
                this.formSaveAs.IsColour = isColour;
                this.formSaveAs.Size = size;
            }

            return saved;
        }

        /// <summary>
        /// Handles the TextChanged event of the tbxHeight control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TbxHeight_TextChanged(object sender, System.EventArgs e)
        {
            if (this.tbxHeight.Text == this.dimensionsCalculator.Height.ToString())
            {
                return;
            }

            try
            {
                this.dimensionsCalculator.Height = Convert.ToInt32(this.tbxHeight.Text, Settings.Default.Culture);
            }
            catch (FormatException)
            {
                return;
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the tbxWidth control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TbxWidth_TextChanged(object sender, System.EventArgs e)
        {
            if (this.tbxWidth.Text == this.dimensionsCalculator.Width.ToString())
            {
                return;
            }

            try
            {
                this.dimensionsCalculator.Width = Convert.ToInt32(this.tbxWidth.Text, Settings.Default.Culture);
            }
            catch (FormatException)
            {
                return;
            }
        }

        /// <summary>
        /// Toggles the image visibility.
        /// </summary>
        private void ToggleImageVisibility()
        {
            this.splitContainer1.Panel2Collapsed = !this.splitContainer1.Panel2Collapsed;

            this.UpdateButtonToggleImageText();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the tsbBlackOnWhite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TsbBlackOnWhite_CheckedChanged(object sender, System.EventArgs e)
        {
            this.IsBlackTextOnWhite = !this.tsbBlackOnWhite.Checked;
        }

        /// <summary>
        /// Handles the Click event of the tsbColourPreview control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TsbColourPreview_Click(object sender, EventArgs e)
        {
            this.ShowColourPreview();
        }

        /// <summary>
        /// Handles the Click event of the tsbFlipHorizontally control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TsbFlipHorizontally_Click(object sender, EventArgs e)
        {
            this.DoRotateFlip(RotateFlipType.RotateNoneFlipX);
        }

        /// <summary>
        /// Handles the Click event of the tsbFlipVertically control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TsbFlipVertically_Click(object sender, EventArgs e)
        {
            this.DoRotateFlip(RotateFlipType.RotateNoneFlipY);
        }

        /// <summary>
        /// Handles the Click event of the tsbFont control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TsbFont_Click(object sender, System.EventArgs e)
        {
            this.ShowFontDialog();
        }

        /// <summary>
        /// Handles the Click event of the tsbFullScreen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TsbFullScreen_Click(object sender, EventArgs e)
        {
            this.IsFullScreen = !this.IsFullScreen;
        }

        /// <summary>
        /// Handles the Click event of the tsbImageVisible control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TsbImageVisible_Click(object sender, EventArgs e)
        {
            this.ToggleImageVisibility();
        }

        /// <summary>
        /// Handles the Click event of the tstripRotateAnticlockwise control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TstripRotateAnticlockwise_Click(object sender, EventArgs e)
        {
            this.DoRotateFlip(RotateFlipType.Rotate270FlipNone);
        }

        /// <summary>
        /// Handles the Click event of the tstripRotateClockwise control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TstripRotateClockwise_Click(object sender, EventArgs e)
        {
            this.DoRotateFlip(RotateFlipType.Rotate90FlipNone);
        }

        /// <summary>
        /// Updates the text for the toggle image button.
        /// </summary>
        private void UpdateButtonToggleImageText()
        {
            if (this.IsLandscape)
            {
                this.buttonToggleImage.Text = this.splitContainer1.Panel2Collapsed ? "<" : ">";
            }
            else
            {
                this.buttonToggleImage.Text = this.splitContainer1.Panel2Collapsed ? @"/\" : @"\/";
            }
        }

        /// <summary>
        /// Updates the levels array.
        /// </summary>
        private void UpdateLevelsArray()
        {
            if (!this.ValuesCreated)
            {
                return;
            }

            byte[][] values = this.values;

            if (this.Brightness != 0 || this.Contrast != 0)
            {
                BrightnessContrast filter = new BrightnessContrast(
                    this.IsBlackTextOnWhite ? this.Brightness : -this.Brightness,
                    this.IsBlackTextOnWhite ? this.Contrast : -this.Contrast);

                values = filter.Apply(this.values);
            }

            if (this.Stretch)
            {
                Stretch filter = new Stretch();
                values = filter.Apply(values);
            }

            // Update the levels graph
            int[] levels = new int[256];

            for (int y = 0; y < this.OutputHeight; y++)
            {
                for (int x = 0; x < this.OutputWidth; x++)
                {
                    levels[(int)values[y][x]]++;
                }
            }

            this.widgetTextSettings.LevelsArray = levels;
        }

        /// <summary>
        /// Set enabled status for the menus
        /// </summary>
        private void UpdateMenus()
        {
            this.menuFileSaveAs.Enabled =
                this.menuFileClose.Enabled =
                this.menuFilePrint.Enabled =
                this.menuFilePrintPreview.Enabled = this.ImageIsLoaded;

            this.tsbColourPreview.Enabled =
                this.menuViewColourPreview.Enabled =
                this.menuFilePrintColour.Enabled =
                this.menuFilePrintPreviewColour.Enabled =
                    this.IsFixedWidth && this.ImageIsLoaded;
        }

        /// <summary>
        /// Updates the text size controls.
        /// </summary>
        private void UpdateTextSizeControls()
        {
            this.tbxWidth.Text = this.dimensionsCalculator.Width.ToString();
            this.tbxHeight.Text = this.dimensionsCalculator.Height.ToString();
            this.tstripOutputSize.Refresh();
        }

        /// <summary>
        /// Updates the form title.
        /// </summary>
        private void UpdateTitle()
        {
            this.Text = Variables.ProgramName + " v" + Variables.Version.GetVersion();

            if (this.Filename.Length > 0)
            {
                this.Text = Path.GetFileName(this.Filename) + " - " + this.Text;
            }
        }

        /// <summary>
        /// Update the form with the text strings for the current language
        /// </summary>
        private void UpdateUI()
        {
            this.CheckForTranslationFiles();

            this.menuFile.Text = Resource.GetString("&File");
            this.menuFileLoad.Text = Resource.GetString("&Load Image") + "...";
            this.menuFileClose.Text = Resource.GetString("&Close");
            this.menuFileSaveAs.Text = "&" + Resource.GetString("Save As") + "...";
            this.menuFileExit.Text = Resource.GetString("E&xit");
            this.menuFileImportClipboard.Text = Resource.GetString("I&mport from Clipboard");
            this.menuFileBatchConversion.Text = Resource.GetString("Batch Conversion") + "...";
            this.menuFilePrint.Text = Resource.GetString("Print") + "...";
            this.menuFilePrintPreview.Text = Resource.GetString("Print Preview") + "...";
            this.menuFilePrintColour.Text = Resource.GetString("Print Colour") + "...";
            this.menuFilePrintPreviewColour.Text = Resource.GetString("Colour Print Preview") + "...";
            this.menuFilePageSetup.Text = Resource.GetString("Page Setup") + "...";

            this.menuEdit.Text = "&" + Resource.GetString("Edit");
            this.menuEditFlipHorizontal.Text = Resource.GetString("Flip Horizontally");
            this.menuEditFlipVertical.Text = Resource.GetString("Flip Vertically");
            this.menuEditInput.Text = Resource.GetString("Input");
            this.menuEditInputRotate90.Text = Resource.GetString("Rotate") + " 90°";
            this.menuEditInputRotate180.Text = Resource.GetString("Rotate") + " 180°";
            this.menuEditInputRotate270.Text = Resource.GetString("Rotate") + " 270°";
            this.menuEditInputFlipHorizontal.Text = Resource.GetString("Flip Horizontally");
            this.menuEditInputFlipVertical.Text = Resource.GetString("Flip Vertically");
            this.menuEditOutput.Text = Resource.GetString("Output");
            this.menuEditSharpeningMethod.Text = Resource.GetString("Sharpening Method");
            this.menuEditSharpeningMethodNone.Text = Resource.GetString("None");
            this.menuEditSharpeningMethodSharpen.Text = Resource.GetString("Sharpen");
            this.menuEditSharpeningMethodUnsharp.Text = Resource.GetString("Unsharp Mask");
            this.menuEditFontsSpecifyCharSize.Text = Resource.GetString("Specify Character Size") + "...";
            this.menuEditStretch.Text = Resource.GetString("Stretch");
            this.menuEditRamps.Text = Resource.GetString("Ramps");
            this.menuEditRampsValidChars.Text = Resource.GetString("Valid Characters") + "...";
            this.menuEditRampsCopyRamp.Text = Resource.GetString("Copy Ramp to Clipboard");
            this.menuEditFontsFont.Text = Resource.GetString("Font") + "...";
            this.menuEditFonts.Text = Resource.GetString("Fonts");
            this.menuEditEditSettings.Text = Resource.GetString("Edit Settings") + "...";
            this.menuEditSaveSettings.Text = Resource.GetString("Save Settings as Default");

            this.menuView.Text = Resource.GetString("&View");
            this.menuViewColourPreview.Text = Resource.GetString("Colour Preview") + "...";
            this.menuViewICBC.Text = Resource.GetString("Image") + " " +
                Resource.GetString("Brightness") + "/" + Resource.GetString("Contrast");

            this.menuViewText.Text = Resource.GetString("Text Settings");

            this.menuViewFullScreen.Text = Resource.GetString("&Full Screen");
            this.menuViewImage.Text = Resource.GetString("Input Image");
            this.menuViewImagePosition.Text = Resource.GetString("Input Image Position");

            this.menuHelp.Text = Resource.GetString("&Help");
            this.menuHelpDonate.Text = Resource.GetString("&Donate") + "...";
            this.menuHelpReportBug.Text = Resource.GetString("Report a Bug") + "...";
            this.menuHelpRequestFeature.Text = Resource.GetString("Request a Feature") + "...";
            this.menuHelpTutorials.Text = Resource.GetString("Tutorials") + "...";
            this.menuHelpCheckForNewVersionToolStrip.Text = Resource.GetString("Check for a New Version") + "...";
            this.menuHelpAbout.Text = Resource.GetString("&About") + "...";

            this.lblOutputSize.Text = Resource.GetString("Size") + ":";
            this.lblRamp.Text = Resource.GetString("Ramp") + ":";
            this.tsbFont.Text = Resource.GetString("Font");
            this.chkGenerate.Text = Resource.GetString("Auto");

            this.lblCharacters.Text = Resource.GetString("Characters") + ":";

            this.cmenuTextCopy.Text = Resource.GetString("Copy");
            this.cmenuTextSelectAll.Text = Resource.GetString("Select All");
            this.cmenuTextSelectNone.Text = Resource.GetString("Select None");
            this.cmenuTextStretch.Text = Resource.GetString("Stretch");
            this.cmenuTextSharpening.Text = Resource.GetString("Sharpening Method");
            this.cmenuTextSharpeningNone.Text = Resource.GetString("None");
            this.cmenuTextSharpeningSharpen.Text = Resource.GetString("Sharpen");
            this.cmenuTextSharpeningUnsharp.Text = Resource.GetString("Unsharp Mask");
            this.cmenuTextFont.Text = Resource.GetString("Font") + "...";
            this.cmenuTextVertical.Text = Resource.GetString("Flip Vertically");
            this.cmenuTextHorizontal.Text = Resource.GetString("Flip Horizontally");

            this.cmenuImageLoad.Text = Resource.GetString("&Load Image") + "...";
            this.cmenuImageRotate90.Text = Resource.GetString("Rotate") + " 90°";
            this.cmenuImageRotate180.Text = Resource.GetString("Rotate") + " 180°";
            this.cmenuImageRotate270.Text = Resource.GetString("Rotate") + " 270°";
            this.cmenuImageFlipHorizontal.Text = Resource.GetString("Flip Horizontally");
            this.cmenuImageFlipVertical.Text = Resource.GetString("Flip Vertically");
            this.cmenuTextHorizontal.Text = Resource.GetString("Flip Horizontally");
            this.cmenuImageSelectNone.Text = Resource.GetString("Remove Selection");
            this.cmenuImageSelectionLocked.Text = Resource.GetString("Lock Selected Area");
            this.cmenuImageSelectionShow.Text = Resource.GetString("Fill Selected Area");
            this.cmenuImageSelectionFillColor.Text = Resource.GetString("Selection Area Fill Colour") + "...";
            this.cmenuImageSelectionBorderColor.Text = Resource.GetString("Selection Area Border Colour") + "...";
            this.cmenuImageUpdateWhileSelecting.Text = Resource.GetString("Update while Selection Changes");

            this.tsbColourPreview.ToolTipText = Resource.GetString("Colour Preview");
            this.tsbFont.ToolTipText = Resource.GetString("Choose the Font");
            this.tsbBlackOnWhite.ToolTipText = Resource.GetString("Invert the Output");
            this.tsbFullScreen.ToolTipText = Resource.GetString("Full Screen");

            this.tsbRotateClockwise.ToolTipText = Resource.GetString("Rotate Clockwise");
            this.tsbRotateAnticlockwise.ToolTipText = Resource.GetString("Rotate Anticlockwise");
            this.tsbFlipHorizontally.ToolTipText = Resource.GetString("Flip Horizontally");
            this.tsbFlipVertically.ToolTipText = Resource.GetString("Flip Vertically");

            this.dialogSaveText.Title = Resource.GetString("Save to a Text File") + "...";

            this.pbxMain.Text = Resource.GetString("Doubleclick to load an image, or drag and drop here.") +
                Environment.NewLine + Environment.NewLine + Resource.GetString("Click and drag on an image to select an area");

            this.dialogLoadImage.Filter =
                Resource.GetString("Image Files") + "|*.bmp;*.rle;*.dib;*.exif;*.gif;*.jpg;*.jpeg;*.jpe;*.png;*.tif;*.tiff;*.wmf;*.emf|" +
                Resource.GetString("Bitmap Images") + " (*.bmp, *.rle, *.dib)|*.bmp;*.rle;*.dib|" +
                Resource.GetString("Exchangeable Image Files") + " (*.exif)|*.exif|" +
                Resource.GetString("GIF Images") + " (*.gif)|*.gif|" +
                Resource.GetString("JPEG Images") + " (*.jpg, *.jpeg, *.jpe)|*.jpg;*.jpeg;*.jpg|" +
                Resource.GetString("Portable Network Graphics Images") + " (*.png)|*.png|" +
                Resource.GetString("TIF Images") + " (*.tif, *.tiff)|*.tif;*.tiff|" +
                Resource.GetString("Windows Metafile Images") + " (*.emf, *.wmf)|*.emf;*.wmf|" +
                Resource.GetString("All Files") + " (*.*)|*.*";

            this.dialogSaveImage.Filter =
                Resource.GetString("Bitmap Images") + " (*.bmp, *.rle, *.dib)|*.bmp;*.rle;*.dib|" +
                Resource.GetString("GIF Images") + " (*.gif)|*.gif|" +
                Resource.GetString("JPEG Images") + " (*.jpg, *.jpeg, *.jpe)|*.jpg;*.jpeg;*.jpg|" +
                Resource.GetString("Portable Network Graphics Images") + " (*.png)|*.png|" +
                Resource.GetString("TIF Images") + " (*.tif, *.tiff)|*.tif;*.tiff|" +
                Resource.GetString("All Files") + " (*.*)|*.*";

            this.dialogSaveImage.FilterIndex = 2; // gif = smallest

            this.dialogSaveText.Filter = Resource.GetString("Plain Text") + "|*.txt|" +
                Resource.GetString("Plain Text") + " (Unicode)|*.txt|" +
                "NFO|*.nfo|" +
                Resource.GetString("Rich Text") + "|*.rtf|" +
                "XHTML 1.1|*.html|" +
                Resource.GetString("All Files") + "|*.*";
            this.dialogSaveText.FilterIndex = 1;

            this.dialogSaveColour.Filter = "XHTML 1.1 (8-bit)|*.html|" +
                Resource.GetString("Rich Text") + " (8-bit)|*.rtf|" +
                "XHTML 1.1 (24-bit)|*.html|" +
                Resource.GetString("Rich Text") + " (24-bit)|*.rtf";
            this.dialogSaveColour.FilterIndex = 1;

            this.widgetTextSettings.Text = Resource.GetString("Text");
            this.widgetImageBrightnessContrast.Text = Resource.GetString("Image");

            this.widgetTextSettings.UpdateUI();

            this.widgetImageBrightnessContrast.UpdateUI();

            this.formSaveAs.UpdateUI();
        }

        #endregion Private methods
    }
}