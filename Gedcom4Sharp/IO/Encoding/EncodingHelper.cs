using Gedcom4Sharp.Models.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gedcom4Sharp.IO
{
    public static class EncodingHelper
    {
        private static readonly Encoding DEFAULT_ENCODING = Encoding.UTF8;

        public static Encoding GetEncoding(string filePath)
        {
            var enc = GetEncodingFromFile(filePath);
            if (enc == null)
            {
                enc = GetEncodingFromGedcom(filePath);
            }
            if (enc == null)
            {
                enc = DEFAULT_ENCODING;
            }
            return enc;
        }


        /// <summary>
        /// Detects the byte order mark of a file and returns
        /// an appropriate encoding for the file.
        /// https://weblog.west-wind.com/posts/2007/Nov/28/Detecting-Text-Encoding-for-StreamReader
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Encoding GetEncodingFromFile(string filePath)
        {
            Encoding enc = null;

            // *** Detect byte order mark if any - otherwise assume default
            byte[] buffer = new byte[5];
            FileStream file = new FileStream(filePath, FileMode.Open);
            file.Read(buffer, 0, 5);
            file.Close();

            // Detect BOM
            if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
            {
                enc = Encoding.UTF8;
            }
            else if ((buffer[0] == 0xfe && buffer[1] == 0xff) ||
                     (buffer[0] == 0x00 && buffer[1] == 0x30) ||
                     (buffer[0] == 0x00 && buffer[1] == 0x0D) ||
                     (buffer[0] == 0x00 && buffer[1] == 0x0A))
            {
                enc = Encoding.BigEndianUnicode;
            }
            else if ((buffer[0] == 0xff && buffer[1] == 0xfe) || 
                     (buffer[0] == 0x30 && buffer[1] == 0x00) || 
                     (buffer[0] == 0x0D && buffer[1] == 0x00) || 
                     (buffer[0] == 0x0A && buffer[1] == 0x00))
            {
                enc = Encoding.Unicode;
            }
            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
            {
                enc = Encoding.UTF32;
            }
            else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
            {
                enc = Encoding.UTF7;
            }
            return enc;
        }

        public static Encoding GetEncodingFromGedcom(string filePath)
        {
            int i = 0;
            // Read encdoding from gedcom file
            foreach (var line in File.ReadLines(filePath))
            {
                if (line.StartsWith("1 CHAR"))
                {
                    var e = line.Substring("1 CHAR ".Length);
                    if (e.Equals("ANSEL", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return new Marc8Encoding();
                    }
                    else if (e.Equals("UTF-8", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return Encoding.UTF8;
                    }
                    else if (e.Equals("ASCII", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return Encoding.ASCII;
                    }
                    else if (e.Equals("ANSI", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return Encoding.Default;
                    }
                    else
                    {
                        return null;
                    }
                }
                // Read a max of 1000 lines
                if (++i == 1000)
                {
                    return Encoding.Default;
                }
            }
            return Encoding.Default;
        }
    }
}
