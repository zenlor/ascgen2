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

    /// <summary>
    /// Abstract class containing global variables for the program
    /// </summary>
    public abstract class Variables
    {
        #region Constants

        /// <summary>The program name</summary>
        public const string ProgramName = "ASCII Generator";

        #endregion Constants

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
        /// The default width
        /// </summary>
        private static int defaultWidth = 150;

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
                string[] result = new string[Settings.Default.DefaultRamps.Count];

                Settings.Default.DefaultRamps.CopyTo(result, 0);

                return result;
            }

            set
            {
                Settings.Default.DefaultRamps.Clear();

                Settings.Default.DefaultRamps.AddRange(value);
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
                string[] result = new string[Settings.Default.DefaultValidCharacters.Count];

                Settings.Default.DefaultValidCharacters.CopyTo(result, 0);

                return result;
            }

            set
            {
                Settings.Default.DefaultValidCharacters.Clear();

                Settings.Default.DefaultValidCharacters.AddRange(value);
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

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Save the current settings
        /// </summary>
        public static void SaveSettings()
        {
            Settings.Default.Save();
        }

        #endregion Public methods

        #region Nested classes

        /// <summary>
        /// Class Holding the current application version
        /// </summary>
        public abstract class Version
        {
            #region Constants

            /// <summary>Major version number</summary>
            public const int Major = 2;

            /// <summary>Minor version number</summary>
            public const int Minor = 0;

            /// <summary>Patch version number</summary>
            public const int Patch = 0;

            /// <summary>Version Suffix</summary>
            public const string Suffix = "dev";

            /// <summary>Version Suffix Number</summary>
            public const int SuffixNumber = 0;

            #endregion Constants

            #region Public methods

            /// <summary>
            /// Build and return the current application version
            /// </summary>
            /// <returns>The current version as a string</returns>
            public static string GetVersion()
            {
                return Major.ToString() + "." + Minor.ToString() + "." + Patch.ToString() + Suffix;
            }

            #endregion Public methods
        }
        #endregion Nested classes
    }
}