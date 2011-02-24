//---------------------------------------------------------------------------------------
// <copyright file="VersionChecker.cs" company="Jonathan Mathews Software">
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

        /// <summary>
        /// The current major version.
        /// </summary>
        private int majorVersion;

        /// <summary>
        /// The current minor version.
        /// </summary>
        private int minorVersion;

        /// <summary>
        /// Window to show the dialog
        /// </summary>
        private IWin32Window owner;

        /// <summary>
        /// The current patch version.
        /// </summary>
        private int patchVersion;

        /// <summary>
        /// Show a message if there is no new version?
        /// </summary>
        private bool showNoNewVersionMessage;

        /// <summary>
        /// The current suffix version.
        /// </summary>
        private int suffixVersion;

        /// <summary>
        /// The URL of the xml file containing the version.
        /// </summary>
        private string url;

        /// <summary>
        /// Thread used for the version checking
        /// </summary>
        private Thread versionCheckThread;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionChecker"/> class.
        /// </summary>
        /// <param name="owner">The owner of the dialog.</param>
        /// <param name="url">The URL of the xml file containing the version.</param>
        /// <param name="currentMajor">The current major version.</param>
        /// <param name="currentMinor">The current minor version.</param>
        /// <param name="currentPatch">The current patch version.</param>
        /// <param name="currentSuffix">The current suffix version.</param>
        public VersionChecker(IWin32Window owner, string url, int currentMajor, int currentMinor, int currentPatch, int currentSuffix)
        {
            this.owner = owner;

            this.url = url;

            this.majorVersion = currentMajor;

            this.minorVersion = currentMinor;

            this.patchVersion = currentPatch;

            this.suffixVersion = currentSuffix;
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
            this.versionCheckThread.Name = String.Format("VersionCheckThread{0:HHmmss}", DateTime.Now);
            this.versionCheckThread.Start();
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Reads from the passed url as a string.
        /// </summary>
        /// <param name="url">The URL to be read.</param>
        /// <returns>A string containing the reponse from the server</returns>
        private static string ReadHtml(string url)
        {
            byte[] buffer;

            using (WebClient webClient = new WebClient())
            {
                try
                {
                    buffer = webClient.DownloadData(url);
                }
                catch (System.Net.WebException)
                {
                    buffer = null;
                }
            }

            if (buffer == null)
            {
                return null;
            }

            UTF8Encoding utf8 = new UTF8Encoding();

            return utf8.GetString(buffer);
        }

        /// <summary>
        /// Check if a new version of the program is available
        /// </summary>
        private void CheckForNewVersion()
        {
            XmlDocument doc = new XmlDocument();

            string html = ReadHtml(this.url);

            if (html == null)
            {
                return;
            }

            doc.LoadXml(html);

            int latestMajor;
            int latestMinor;
            int latestPatch;
            int latestSuffix;
            string suffixString;
            string downloadUrl;

            try
            {
                latestMajor = XmlProcessor.ReadNode(doc.SelectSingleNode("version/major"), 0, 100, 0);
                latestMinor = XmlProcessor.ReadNode(doc.SelectSingleNode("version/minor"), 0, 100, 0);
                latestPatch = XmlProcessor.ReadNode(doc.SelectSingleNode("version/patch"), 0, 100, 0);
                latestSuffix = XmlProcessor.ReadNode(doc.SelectSingleNode("version/suffixnum"), 0, 100, 0);
                suffixString = XmlProcessor.ReadNode(doc.SelectSingleNode("version/suffix"), String.Empty, true);
                downloadUrl = XmlProcessor.ReadNode(doc.SelectSingleNode("version/url"), String.Empty, true);
            }
            catch (System.Xml.XmlException)
            {
                return;
            }

            bool newVersionAvailable = false;

            if (latestMajor > this.majorVersion)
            {
                newVersionAvailable = true;
            }
            else if (latestMajor == this.majorVersion)
            {
                if (latestMinor > this.minorVersion)
                {
                    newVersionAvailable = true;
                }
                else if (latestMinor == this.minorVersion)
                {
                    if (latestPatch > this.patchVersion)
                    {
                        newVersionAvailable = true;
                    }
                    else if (latestPatch == this.patchVersion)
                    {
                        newVersionAvailable = latestSuffix > this.suffixVersion;
                    }
                }
            }

            newVersionAvailable = true;

            if (newVersionAvailable)
            {
                this.NewVersionDialog(string.Format("{0}.{1}.{2}{3}", latestMajor, latestMinor, latestPatch, suffixString), downloadUrl);
            }
            else
            {
                if (this.showNoNewVersionMessage)
                {
                    MessageBox.Show(
                                this.owner,
                                "This is the latest version",
                                string.Empty,
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
            string text = string.Format("Version {0} is available", version);

            if (url.Length > 0)
            {
                string caption = "Open the download page?";

                if (MessageBox.Show(this.owner, caption, text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    System.Diagnostics.Process.Start(url);
                }
            }
            else
            {
                MessageBox.Show(this.owner, text, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion Private methods
    }
}