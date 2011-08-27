//---------------------------------------------------------------------------------------
// <copyright file="Variables.cs" company="Jonathan Mathews Software">
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
    using System.Drawing;
    using System.Globalization;

    /// <summary>
    /// Abstract class containing global variables for the program
    /// </summary>
    public abstract class Variables
    {
        #region Fields

        /// <summary>
        /// Check for a new version of the program?
        /// </summary>
        private static bool checkForNewVersion = true;

        /// <summary>
        /// Confirm to save the image on exit?
        /// </summary>
        private static bool confirmOnClose = true;

        /// <summary>
        /// The culture used by the application
        /// </summary>
        private static CultureInfo culture = new CultureInfo(string.Empty);

        /// <summary>
        /// The default dithering level.
        /// </summary>
        private static int defaultDitheringLevel = 4;

        /// <summary>
        /// The default dithering random.
        /// </summary>
        private static int defaultDitheringRandom = 3;

        /// <summary>
        /// The default font
        /// </summary>
        private static Font defaultFont = new Font("Lucida Console", 9f);

        /// <summary>
        /// The default height
        /// </summary>
        private static int defaultHeight = -1;

        /// <summary>
        /// The default max level
        /// </summary>
        private static int defaultMaxLevel = 255;

        /// <summary>
        /// The default median level.
        /// </summary>
        private static float defaultMedianLevel = 0.5f;

        /// <summary>
        /// The default list of ramps.
        /// </summary>
        private static string[] defaultRamps = new string[]
        {
            "MMMMMMM@@@@@@@WWWWWWWWWBBBBBBBB000000008888888ZZZZZZZZZaZaaaaaa2222222SSSSSSSXXXXXXXXXXX7777777rrrrrrr;;;;;;;;iiiiiiiii:::::::,:,,,,,,.........       ",
            "@@@@@@@######MMMBBHHHAAAA&&GGhh9933XXX222255SSSiiiissssrrrrrrr;;;;;;;;:::::::,,,,,,,........        ",
            "#WMBRXVYIti+=;:,. ", "##XXxxx+++===---;;,,...    ",
            "@%#*+=-:. ",
            "#¥¥®®ØØ$$ø0oo°++=-,.    ",
            "# ",
            "01 ",
            "█▓▒░ "
        };

        /// <summary>
        /// The different strings of valid characters.
        /// </summary>
        private static string[] defaultValidCharacters = new string[]
        {
            " #,.0123456789:;@ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz$",
            " ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz",
            " 1234567890",
            "M@WB08Za2SX7r;i:;. ",
            "@#MBHAGh93X25Sisr;:, ",
            "█▓▒░ "
        };

        /// <summary>
        /// The default width
        /// </summary>
        private static int defaultWidth = 150;

        /// <summary>
        /// The maximum height for the output.
        /// </summary>
        private static int maximumHeight = 999;

        /// <summary>
        /// The maximum width for the output.
        /// </summary>
        private static int maximumWidth = 999;

        /// <summary>
        /// The filename prefix for output images
        /// </summary>
        private static string prefix = "ASCII-";

        /// <summary>
        /// The selection border color.
        /// </summary>
        private static Color selectionBorderColor = Color.DarkBlue;

        /// <summary>
        /// The selection fill color.
        /// </summary>
        private static Color selectionFillColor = Color.FromArgb(128, 173, 216, 230);

        /// <summary>
        /// Show the image widget?
        /// </summary>
        private static bool showWidgetImage = true;

        /// <summary>
        /// Show the text settings widget?
        /// </summary>
        private static bool showWidgetTextSettings = true;

        /// <summary>
        /// Stretch the output?
        /// </summary>
        private static bool stretch = true;

        /// <summary>
        /// Use an unsharp mask?
        /// </summary>
        private static bool unsharpMask = true;

        /// <summary>
        /// Update the output while selecting an area of the image?
        /// </summary>
        private static bool updateWhileSelecting = true;

        /// <summary>
        /// Use a generated ramp?
        /// </summary>
        private static bool useGeneratedRamp = true;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether to check for a new version.
        /// </summary>
        /// <value><c>true</c> if checking for new versions; otherwise, <c>false</c>.</value>
        public static bool CheckForNewVersion
        {
            get
            {
                return checkForNewVersion;
            }

            set
            {
                checkForNewVersion = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether confirm on close with an unsaved image.
        /// </summary>
        /// <value><c>true</c> if confirm on close; otherwise, <c>false</c>.</value>
        public static bool ConfirmOnClose
        {
            get
            {
                return confirmOnClose;
            }

            set
            {
                confirmOnClose = value;
            }
        }

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        /// <value>The culture.</value>
        public static CultureInfo Culture
        {
            get
            {
                return culture;
            }

            set
            {
                culture = value;
            }
        }

        /// <summary>
        /// Gets or sets the current characters.
        /// </summary>
        /// <value>The current characters.</value>
        public static string CurrentCharacters { get; set; }

        /// <summary>
        /// Gets or sets the current ramp.
        /// </summary>
        /// <value>The current ramp.</value>
        public static string CurrentRamp { get; set; }

        /// <summary>
        /// Gets or sets the currently selected ramp.
        /// </summary>
        /// <value>The currently selected ramp.</value>
        public static int CurrentSelectedRamp { get; set; }

        /// <summary>
        /// Gets or sets the currently selected valid characters.
        /// </summary>
        /// <value>The currently selected valid characters.</value>
        public static int CurrentSelectedValidCharacters { get; set; }

        /// <summary>
        /// Gets or sets the default dithering level.
        /// </summary>
        /// <value>The default dithering level.</value>
        public static int DefaultDitheringLevel
        {
            get
            {
                return defaultDitheringLevel;
            }

            set
            {
                defaultDitheringLevel = value;
            }
        }

        /// <summary>
        /// Gets or sets 
        /// </summary>
        /// <value>The default dithering random.</value>
        public static int DefaultDitheringRandom
        {
            get
            {
                return defaultDitheringRandom;
            }

            set
            {
                defaultDitheringRandom = value;
            }
        }

        /// <summary>
        /// Gets or sets the default font.
        /// </summary>
        /// <value>The default font.</value>
        public static Font DefaultFont
        {
            get
            {
                return defaultFont;
            }

            set
            {
                defaultFont = value;
            }
        }

        /// <summary>
        /// Gets or sets the default height.
        /// </summary>
        /// <value>The default height.</value>
        public static int DefaultHeight
        {
            get
            {
                return defaultHeight;
            }

            set
            {
                defaultHeight = value;
            }
        }

        /// <summary>
        /// Gets or sets the default max level.
        /// </summary>
        /// <value>The default max level.</value>
        public static int DefaultMaxLevel
        {
            get
            {
                return defaultMaxLevel;
            }

            set
            {
                defaultMaxLevel = value;
            }
        }

        /// <summary>
        /// Gets or sets the default median level.
        /// </summary>
        /// <value>The default median level.</value>
        public static float DefaultMedianLevel
        {
            get
            {
                return defaultMedianLevel;
            }

            set
            {
                defaultMedianLevel = value;
            }
        }

        /// <summary>
        /// Gets or sets the default min level.
        /// </summary>
        /// <value>The default min level.</value>
        public static int DefaultMinLevel { get; set; }

        /// <summary>
        /// Gets or sets the default list of ramps
        /// </summary>
        /// <value>The default ramps.</value>
        public static string[] DefaultRamps
        {
            get
            {
                return defaultRamps;
            }

            set
            {
                defaultRamps = value;
            }
        }

        /// <summary>
        /// Gets or sets the default text brightness.
        /// </summary>
        /// <value>The default text brightness.</value>
        public static int DefaultTextBrightness { get; set; }

        /// <summary>
        /// Gets or sets the default text contrast.
        /// </summary>
        /// <value>The default text contrast.</value>
        public static int DefaultTextContrast { get; set; }

        /// <summary>
        /// Gets or sets the default settings used for all ASCII ramp valid character strings
        /// </summary>
        /// <value>The default valid characters.</value>
        public static string[] DefaultValidCharacters
        {
            get
            {
                return defaultValidCharacters;
            }

            set
            {
                defaultValidCharacters = value;
            }
        }

        /// <summary>
        /// Gets or sets the default width.
        /// </summary>
        /// <value>The default width.</value>
        public static int DefaultWidth
        {
            get
            {
                return defaultWidth;
            }

            set
            {
                defaultWidth = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the default value for whether the output should be flipped horizontally
        /// </summary>
        /// <value><c>true</c> if flipping horizontally; otherwise, <c>false</c>.</value>
        public static bool FlipHorizontally { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the default value for whether the output should be flipped vertically
        /// </summary>
        /// <value><c>true</c> if flip vertically; otherwise, <c>false</c>.</value>
        public static bool FlipVertically { get; set; }

        /// <summary>
        /// Gets or sets the initial input directory.
        /// </summary>
        /// <value>The initial input directory.</value>
        public static string InitialInputDirectory { get; set; }

        /// <summary>
        /// Gets or sets the initial output directory.
        /// </summary>
        /// <value>The initial output directory.</value>
        public static string InitialOutputDirectory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to invert the output image.
        /// </summary>
        /// <value><c>true</c> if inverting image; otherwise, <c>false</c>.</value>
        public static bool InvertImage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the default value for whether brightness and contrast should be loaded from the settings
        /// </summary>
        /// <value>
        /// <c>true</c> if loading image brightness contrast; otherwise, <c>false</c>.
        /// </value>
        public static bool LoadImageBrightnessContrast { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the default value for whether the levels should be loaded from the settings
        /// </summary>
        /// <value><c>true</c> if loading levels; otherwise, <c>false</c>.</value>
        public static bool LoadLevels { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the default value for whether the text brightness and contrast should be loaded from the settings
        /// </summary>
        /// <value>
        /// <c>true</c> if loading text brightness contrast; otherwise, <c>false</c>.
        /// </value>
        public static bool LoadTextBrightnessContrast { get; set; }

        /// <summary>
        /// Gets or sets the maximum height for the output.
        /// </summary>
        /// <value>The maximum height.</value>
        public static int MaximumHeight
        {
            get
            {
                return maximumHeight;
            }

            set
            {
                maximumHeight = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum width for the output.
        /// </summary>
        /// <value>The maximum width.</value>
        public static int MaximumWidth
        {
            get
            {
                return maximumWidth;
            }

            set
            {
                maximumWidth = value;
            }
        }

        /// <summary>
        /// Gets or sets the filename prefix.
        /// </summary>
        /// <value>The prefix.</value>
        public static string Prefix
        {
            get
            {
                return prefix;
            }

            set
            {
                prefix = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the selection border.
        /// </summary>
        /// <value>The color of the selection border.</value>
        public static Color SelectionBorderColor
        {
            get
            {
                return selectionBorderColor;
            }

            set
            {
                selectionBorderColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the selection fill.
        /// </summary>
        /// <value>The color of the selection fill.</value>
        public static Color SelectionFillColor
        {
            get
            {
                return selectionFillColor;
            }

            set
            {
                selectionFillColor = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the default value for whether the output should be sharpened
        /// </summary>
        /// <value><c>true</c> if sharpen; otherwise, <c>false</c>.</value>
        public static bool Sharpen { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show the image widget.
        /// </summary>
        /// <value><c>true</c> if showing the image widget; otherwise, <c>false</c>.</value>
        public static bool ShowWidgetImage
        {
            get
            {
                return showWidgetImage;
            }

            set
            {
                showWidgetImage = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show the text settings widget.
        /// </summary>
        /// <value><c>true</c> if showing the text settings widget; otherwise, <c>false</c>.</value>
        public static bool ShowWidgetTextSettings
        {
            get
            {
                return showWidgetTextSettings;
            }

            set
            {
                showWidgetTextSettings = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to stretch the output.
        /// </summary>
        /// <value><c>true</c> if stretching; otherwise, <c>false</c>.</value>
        public static bool Stretch
        {
            get
            {
                return stretch;
            }

            set
            {
                stretch = value;
            }
        }

        /// <summary>
        /// Gets or sets the translation file.
        /// </summary>
        /// <value>The translation file.</value>
        public static string TranslationFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use an unsharp mask.
        /// </summary>
        /// <value><c>true</c> if using an unsharp mask; otherwise, <c>false</c>.</value>
        public static bool UnsharpMask
        {
            get
            {
                return unsharpMask;
            }

            set
            {
                unsharpMask = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to update the output while selecting an area of the image.
        /// </summary>
        /// <value>
        /// <c>true</c> if update while selecting; otherwise, <c>false</c>.
        /// </value>
        public static bool UpdateWhileSelecting
        {
            get
            {
                return updateWhileSelecting;
            }

            set
            {
                updateWhileSelecting = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use a generated ramp.
        /// </summary>
        /// <value><c>true</c> if using a generated ramp; otherwise, <c>false</c>.</value>
        public static bool UseGeneratedRamp
        {
            get
            {
                return useGeneratedRamp;
            }

            set
            {
                useGeneratedRamp = value;
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Save the current settings
        /// </summary>
        public static void SaveSettings()
        {
        }

        #endregion Public methods
    }
}