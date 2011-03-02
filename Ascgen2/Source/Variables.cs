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
    /// <summary>
    /// Abstract class containing global variables for the program
    /// </summary>
    public abstract class Variables
    {
        #region Constants

        /// <summary>The program name</summary>
        public const string ProgramName = "ASCII Generator";

        #endregion Constants

        #region Properties

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