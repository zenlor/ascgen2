//---------------------------------------------------------------------------------------
// <copyright file="OutputCreator.cs" company="Jonathan Mathews Software">
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
namespace JMSoftware
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Text;
    using JMSoftware.AsciiConversion;
    using JMSoftware.AsciiGeneratorDotNet;

    /// <summary>
    /// Class to create the formatted text
    /// </summary>
    public abstract class OutputCreator
    {
        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="OutputCreator"/> class from being created.
        /// </summary>
        private OutputCreator()
        {
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Creates the html text
        /// </summary>
        /// <param name="strings">The strings.</param>
        /// <param name="colors">The colors.</param>
        /// <param name="backgroundColor">The backgroundcolor.</param>
        /// <param name="textSettings">The text settings.</param>
        /// <param name="title">The title.</param>
        /// <returns>The formatted html</returns>
        public static string CreateHTML(string[] strings, Color[,] colors, Color backgroundColor, TextProcessingSettings textSettings, string title)
        {
            bool useColor = (colors != null) && textSettings.IsFixedWidth;

            //--
            // Create the unique color array, and the array of int pointers
            //--
            int[,] characterToColor = null;

            ArrayList uniqueColors = new ArrayList();

            if (useColor)
            {
                characterToColor = new int[colors.GetLength(0), colors.GetLength(1)];
                int colorId;
                int previousColorId = -1;

                for (int y = 0; y < colors.GetLength(1); y++)
                {
                    for (int x = 0; x < colors.GetLength(0); x++)
                    {
                        colorId = uniqueColors.IndexOf(colors[x, y]);

                        if (colorId > -1)
                        {
                            if (colorId == previousColorId)
                            {
                                characterToColor[x, y] = -1;
                            }
                            else
                            {
                                previousColorId = characterToColor[x, y] = colorId;
                            }
                        }
                        else
                        {
                            // New Color
                            previousColorId = characterToColor[x, y] = uniqueColors.Add(colors[x, y]);
                        }
                    }
                }
            }

            StringBuilder builder = new StringBuilder();

            builder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            builder.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\" \"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\">");
            builder.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"en\">");
            builder.AppendLine(String.Empty);
            builder.AppendLine("<head>");
            builder.AppendLine(String.Empty);

            builder.Append("<title>");
            builder.Append(title);
            builder.Append("</title>");
            builder.Append(Environment.NewLine);

            builder.AppendLine(String.Empty);

            builder.Append("<meta name=\"generator\" content=\"Ascgen dotNET ");
            builder.Append(Variables.Version.GetVersion());
            builder.Append("\" />");
            builder.Append(Environment.NewLine);

            builder.AppendLine(String.Empty);

            builder.AppendLine("<style type=\"text/css\">");
            builder.AppendLine("<!--");

            builder.AppendLine("#ascgen-image pre {");
            builder.Append("	font-family: \"");
            builder.Append(textSettings.Font.Name);
            builder.Append("\", monospace;");
            builder.Append(Environment.NewLine);

            builder.Append("	font-size: ");
            builder.Append(textSettings.Font.Size);
            builder.Append("pt;");
            builder.Append(Environment.NewLine);

            builder.Append("	background-color: #");
            builder.Append(backgroundColor.R.ToString("X2", null));
            builder.Append(backgroundColor.G.ToString("X2", null));
            builder.Append(backgroundColor.B.ToString("X2", null));
            builder.Append(";");
            builder.Append(Environment.NewLine);

            Color forecolor = Color.FromArgb(
                                        (byte)(~backgroundColor.R), 
                                        (byte)(~backgroundColor.G), 
                                        (byte)(~backgroundColor.B));

            builder.Append("	color: #");
            builder.Append(forecolor.R.ToString("X2", null));
            builder.Append(forecolor.G.ToString("X2", null));
            builder.Append(forecolor.B.ToString("X2", null));
            builder.Append(";");
            builder.Append(Environment.NewLine);

            builder.AppendLine("	float: left;");     // avoids firefox problem with scrolling horizontally
            builder.Append("	line-height: ");  // fixes firefox problem with extra space between lines
            builder.Append(textSettings.CharacterSize.Height);
            builder.Append("px;");
            builder.Append(Environment.NewLine);

            builder.AppendLine("	border: 1px solid #000000;");

            builder.AppendLine("}");

            if (useColor)
            {
                builder.Append(Environment.NewLine);

                int count = 0;

                foreach (Color c in uniqueColors)
                {
                    builder.Append(".c");
                    builder.Append(count++);
                    builder.Append(" { color: #");
                    builder.Append(c.R.ToString("X2", null));
                    builder.Append(c.G.ToString("X2", null));
                    builder.Append(c.B.ToString("X2", null));
                    builder.Append("; }");
                    builder.Append(Environment.NewLine);
                }
            }

            builder.AppendLine("-->");
            builder.AppendLine("</style>");
            builder.AppendLine(String.Empty);
            builder.AppendLine("</head>");
            builder.AppendLine(String.Empty);
            builder.AppendLine("<body>");
            builder.AppendLine(String.Empty);
            builder.AppendLine("<div id=\"ascgen-image\">");
            builder.Append("<pre>");

            bool spanIsOpen = false;

            // the text
            if (textSettings.IsFixedWidth)
            {
                for (int y = 0; y < textSettings.Height; y++)
                {
                    for (int x = 0; x < textSettings.Width; x++)
                    {
                        if (useColor && characterToColor[x, y] != -1)
                        {
                            if (spanIsOpen)
                            {
                                builder.Append("</span>");
                                spanIsOpen = false;
                            }

                            builder.Append("<span class=\"c");
                            builder.Append(characterToColor[x, y]);
                            builder.Append("\">");
                            spanIsOpen = true;
                        }

                        builder.Append(strings[y][x]);
                    }

                    if (y < textSettings.Height - 1)
                    {
                        builder.Append(Environment.NewLine);
                    }
                }
            }
            else
            {
                foreach (string s in strings)
                {
                    builder.Append(s);
                    builder.Append(Environment.NewLine);
                }
            }

            if (useColor)
            {
                builder.Append("</span>");
            }

            builder.Append("</pre>");
            builder.Append(Environment.NewLine);
            builder.AppendLine("</div>");
            builder.AppendLine(String.Empty);
            builder.AppendLine("</body>");
            builder.AppendLine(String.Empty);
            builder.Append("</html>");

            return builder.ToString();
        }

        /// <summary>
        /// Creates the RTF text
        /// </summary>
        /// <param name="strings">The strings.</param>
        /// <param name="colors">The colors.</param>
        /// <param name="textSettings">The text settings.</param>
        /// <returns>The formatted rtf text</returns>
        public static string CreateRTF(string[] strings, Color[,] colors, TextProcessingSettings textSettings)
        {
            if (!textSettings.IsFixedWidth)
            {
                return null;
            }

            //--
            // Create the unique color array, and the array of int pointers
            //--
            int[,] characterToColor = new int[colors.GetLength(0), colors.GetLength(1)];

            ArrayList uniqueColors = new ArrayList();
            int colorId;
            int previousColorId = -1;

            for (int y = 0; y < colors.GetLength(1); y++)
            {
                for (int x = 0; x < colors.GetLength(0); x++)
                {
                    colorId = uniqueColors.IndexOf(colors[x, y]);

                    if (colorId > -1)
                    {
                        if (colorId == previousColorId)
                        {
                            characterToColor[x, y] = -1;
                        }
                        else
                        {
                            previousColorId = characterToColor[x, y] = colorId;
                        }
                    }
                    else
                    {
                        // New Color
                        previousColorId = characterToColor[x, y] = uniqueColors.Add(colors[x, y]);
                    }
                }
            }

            // Create and output the text
            StringBuilder builder = new StringBuilder();

            // the rtf header
            builder.Append(@"{\rtf1\ansi\ansicpg1252\deff0{\fonttbl{\f0\fnil\fcharset0 ");
            builder.Append(textSettings.Font.Name);
            builder.Append(";}}");
            builder.Append(Environment.NewLine);

            // the rtf colortbl
            builder.Append(@"{\colortbl ;");

            foreach (Color c in uniqueColors)
            {
                builder.Append(@"\red");
                builder.Append(c.R);
                builder.Append(@"\green");
                builder.Append(c.G);
                builder.Append(@"\blue");
                builder.Append(c.B);
                builder.Append(";");
            }

            builder.Append("}");
            builder.Append(Environment.NewLine);
            builder.Append(@"{\*\generator Ascgen dotNET ");
            builder.Append(Variables.Version.GetVersion());
            builder.Append(";}");

            // the font settings
            builder.Append(@"\viewkind4\uc1\pard\lang2057\f0");

            if (textSettings.Font.Bold)
            {
                builder.Append(@"\b");
            }

            if (textSettings.Font.Italic)
            {
                builder.Append(@"\i");
            }

            if (textSettings.Font.Underline)
            {
                builder.Append(@"\ul");
            }

            if (textSettings.Font.Strikeout)
            {
                builder.Append(@"\strike");
            }

            builder.Append(@"\fs");
            builder.Append((int)(textSettings.Font.Size * 2));

            // the text
            for (int y = 0; y < textSettings.Height; y++)
            {
                for (int x = 0; x < textSettings.Width; x++)
                {
                    if (characterToColor[x, y] != -1)
                    {
                        builder.Append(@"\cf");
                        builder.Append(characterToColor[x, y] + 1);
                        builder.Append(" ");
                    }

                    builder.Append(strings[y][x]);
                }

                builder.Append(@"\par");
                builder.Append(Environment.NewLine);
            }

            builder.Append("}");

            return builder.ToString();
        }

        #endregion Public methods
    }
}