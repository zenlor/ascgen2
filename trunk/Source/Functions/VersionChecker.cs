//---------------------------------------------------------------------------------------
// <copyright file="VersionChecker.cs" company="Jonathan Mathews Software">
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
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;

    /// <summary>
    /// Class to handle checking for a new version
    /// </summary>
    public class VersionChecker
    {
        #region Fields

        /// <summary>Window to show the dialog</summary>
        private IWin32Window owner;

        /// <summary>Show a message if there is no new version?</summary>
        private bool showNoNewVersionMessage;

        /// <summary>Thread used for the version checking</summary>
        private Thread versionCheckThread;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionChecker"/> class.
        /// </summary>
        /// <param name="owner">The owner of the dialog.</param>
        public VersionChecker(IWin32Window owner)
        {
            this.owner = owner;
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Checks for a new version.
        /// </summary>
        public void Check()
        {
            this.Check(false);
        }

        /// <summary>
        /// Checks for a new version.
        /// </summary>
        /// <param name="showNoNewVersionMessage">if set to <c>true</c> the show a message if no new version.</param>
        public void Check(bool showNoNewVersionMessage)
        {
            this.showNoNewVersionMessage = showNoNewVersionMessage;

            this.versionCheckThread = new Thread(this.CheckForNewVersion);
            this.versionCheckThread.Name = String.Format(Settings.Default.Culture, "VersionCheckThread{0:HHmmss}", DateTime.Now);
            this.versionCheckThread.Start();
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Check if a new version of the program is available
        /// </summary>
        private void CheckForNewVersion()
        {
            XmlDocument doc = new XmlDocument();

            string html = this.ReadHtml("http://ascgen2.sourceforge.net/version.xml");

            if (html == null)
            {
                return;
            }

            doc.LoadXml(html);

            int major;
            int minor;
            int patch;
            string suffix;
            int suffixNumber;

            try
            {
                major = XmlProcessor.ReadNode(doc.SelectSingleNode("version/major"), 0, 100, 0);
                minor = XmlProcessor.ReadNode(doc.SelectSingleNode("version/minor"), 0, 100, 0);
                patch = XmlProcessor.ReadNode(doc.SelectSingleNode("version/patch"), 0, 100, 0);
                suffix = XmlProcessor.ReadNode(doc.SelectSingleNode("version/suffix"), String.Empty, true);
                suffixNumber = XmlProcessor.ReadNode(doc.SelectSingleNode("version/suffixnum"), 0, 100, 0);
            }
            catch (System.Xml.XmlException)
            {
                return;
            }

            bool newVersion = false;

            if (major > Variables.Version.Major)
            {
                newVersion = true;
            }
            else if (major == Variables.Version.Major)
            {
                if (minor > Variables.Version.Minor)
                {
                    newVersion = true;
                }
                else if (minor == Variables.Version.Minor)
                {
                    if (patch > Variables.Version.Patch)
                    {
                        newVersion = true;
                    }
                    else if (patch == Variables.Version.Patch)
                    {
                        newVersion = suffixNumber > Variables.Version.SuffixNumber;
                    }
                }
            }

            if (newVersion)
            {
                this.NewVersionDialog(
                            major.ToString() + "." + minor.ToString() + "." + patch.ToString() + suffix,
                            "http://sourceforge.net/project/showfiles.php?group_id=133786");
            }
            else
            {
                if (this.showNoNewVersionMessage)
                {
                    MessageBox.Show(
                                this.owner,
                                Resource.GetString("This is the latest version"),
                                Variables.ProgramName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Shows the new version dialog.
        /// </summary>
        /// <param name="version">The new version.</param>
        /// <param name="url">The URL to the new version.</param>
        private void NewVersionDialog(string version, string url)
        {
            string caption = Resource.GetString("Open the download page") + "?";
            string text = string.Format(Resource.GetString("Version {0} is available"), version);

            if (MessageBox.Show(this.owner, caption, text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                System.Diagnostics.Process.Start(url);
            }
        }

        /// <summary>
        /// Reads from the passed url as a string.
        /// </summary>
        /// <param name="url">The URL to be read.</param>
        /// <returns>A string containing the reponse from the server</returns>
        private string ReadHtml(string url)
        {
            WebClient webClient = new WebClient();

            byte[] buffer;

            try
            {
                buffer = webClient.DownloadData(url);
            }
            catch (System.Net.WebException)
            {
                buffer = null;
            }

            if (buffer == null)
            {
                return null;
            }

            UTF8Encoding utf8 = new UTF8Encoding();

            return utf8.GetString(buffer);
        }

        #endregion Private methods
    }
}