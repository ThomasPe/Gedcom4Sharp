using Gedcom4Sharp.Models.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Gedcom4Sharp.IO
{
    public static class EncodingHelper
    {
        private static readonly Encoding DEFAULT_ENCODING = Encoding.UTF8;

        public static async Task<Encoding> GetEncoding(string filePath)
        {
            using (Stream fileStream = new FileStream(filePath, FileMode.Open))
            {
                return await GetEncoding(fileStream);
            }
        }

        public static async Task<Encoding> GetEncoding(Stream fileStream)
        {
            var enc = GetEncodingFromFile(fileStream);
            if (enc == null)
            {
                enc = await GetEncodingFromGedcom(fileStream);
            }
            if (enc == null)
            {
                enc = DEFAULT_ENCODING;
            }
            return enc;
        }

        public static Encoding GetEncodingFromFile(string filePath)
        {
            using (Stream fileStream = new FileStream(filePath, FileMode.Open))
            {
                return GetEncodingFromFile(fileStream);
            }
        }

        /// <summary>
        /// Detects the byte order mark of a file and returns
        /// an appropriate encoding for the file.
        /// https://weblog.west-wind.com/posts/2007/Nov/28/Detecting-Text-Encoding-for-StreamReader
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        private static Encoding GetEncodingFromFile(Stream fileStream)
        {
            Encoding enc = null;

            // *** Detect byte order mark if any - otherwise assume default
            byte[] buffer = new byte[5];
            fileStream.Read(buffer, 0, 5);
            fileStream.Seek(0, SeekOrigin.Begin);

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
            fileStream.Seek(0, SeekOrigin.Begin);
            return enc;
        }

        public static async Task<Encoding> GetEncodingFromGedcom(string filePath)
        {
            Stream fileStream = new FileStream(filePath, FileMode.Open);
            return await GetEncodingFromGedcom(fileStream);
        }


        public static async Task<Encoding> GetEncodingFromGedcom(Stream fileStream)
        {
            int i = 0;
            Encoding encoding = null;
            // Read encdoding from gedcom file
            // Don't dispose (using) the StreamReader as that will close the stream as well
            var reader = new StreamReader(fileStream);
            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                if (line.StartsWith("1 CHAR"))
                {
                    var e = line.Substring("1 CHAR ".Length);
                    if (e.Equals("ANSEL", StringComparison.CurrentCultureIgnoreCase))
                    {
                        encoding = new Marc8Encoding();
                        break;
                    }
                    else if (e.Equals("UTF-8", StringComparison.CurrentCultureIgnoreCase))
                    {
                        encoding = Encoding.UTF8;
                        break;
                    }
                    else if (e.Equals("ASCII", StringComparison.CurrentCultureIgnoreCase))
                    {
                        encoding = Encoding.ASCII;
                        break;
                    }
                    else if (e.Equals("ANSI", StringComparison.CurrentCultureIgnoreCase))
                    {
                        encoding = Encoding.Default;
                        break;
                    }
                    else
                    {
                        return null;
                    }
                }
                // Read a max of 1000 lines
                if (++i == 1000)
                {
                    encoding = Encoding.Default;
                    break;
                }
            }
            fileStream.Seek(0, SeekOrigin.Begin);
            return encoding;
        }
    }
}
