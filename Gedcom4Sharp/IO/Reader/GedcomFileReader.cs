using Gedcom4Sharp.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gedcom4Sharp.IO.Reader
{
    /// <summary>
    /// An encoding-agnostic class for reading the GEDCOM files and handling ASCII, ANSEL, and UNICODE coding as needed. It's basic job
    /// is to turn the bytes from the file into a buffer (a List of Strings) that the
    /// GedcomParser can work with. This class is needed because the built-in character encodings in C#
    /// don't support ANSEL encoding, which is the default encoding for gedcom files in v5.5 standard.
    /// </summary>
    public class GedcomFileReader
    {
        /// <summary>
        /// The size of the first chunk of the GEDCOM to just load into memory for easy review. 16K.
        /// </summary>
        private const int FIRST_CHUNK_SIZE = 16384;

        /// <summary>
        /// A long constant representing the UTF-8 Byte Order Marker signature, which is the six hex characters EF BB BF.
        /// </summary>
        private const long UTF8_BYTE_ORDER_MARKER = 0xEFBBBFL;

        /// <summary>
        /// The first chunk of the file
        /// </summary>
        readonly byte[] _firstChunk = new byte[FIRST_CHUNK_SIZE];

        /// <summary>
        /// The buffered input stream of bytes to read
        /// </summary>
        private readonly Stream _byteStream;

        /// <summary>
        /// The encoding-specific reader helper class to actually read the bytes
        /// </summary>
        private readonly AbstractEncodingSpecificReader _encodingSpecificReader;

        /// <summary>
        /// The GedcomParser we're reading files for
        /// </summary>
        private readonly GedcomParser _parser;

        /// <summary>
        /// Number of lines processed
        /// </summary>
        private int _linesProcessed = 0;

        public GedcomFileReader(GedcomParser parser, Stream inputStream)
        {
            _parser = parser;
            _byteStream = inputStream;
            SaveFirstChunk();
            //_encodingSpecificReader = getEncodingSpecificReader();
        }

        /// <summary>
        /// Get the next line of the file.
        /// </summary>
        /// <returns>the next line of the file, or null if no more lines to read.</returns>
        public string nextLine()
        {
            if (_parser.IsCancelled)
            {
                // TODO throw canceled exception
            }
            string result = _encodingSpecificReader.NextLine();
            _linesProcessed++;
            if (result == null)
            {
                // TOOD: Update File Observer
                //parser.notifyFileObservers(new FileProgressEvent(this, linesProcessed, encodingSpecificReader.bytesRead, true));
            }
            // else if (linesProcessed % parser.getReadNotificationRate() == 0)
            else if(true)
            {
                // TODO: Update File Observer
                // parser.notifyFileObservers(new FileProgressEvent(this, linesProcessed, encodingSpecificReader.bytesRead, false));

            }
            return result;
        }

        /// <summary>
        /// Return the first n bytes of the array as a single long value, for checking against hex literals
        /// </summary>
        /// <param name="n"></param>
        /// <returns>the first n bytes, as long</returns>
        private long FirstNBytes(int n)
        {
            long result = 0;
            for(int i = 0; i < n; i++)
            {
                result = (result << 8) + (_firstChunk[i] & 0xFF); // Shift existing bits 8 to the left, and AND in this
            }
            return result;
        }

        //private AbstractEncodingSpecificReader AnselAsciiOrUtf8()
        //{
        //    // Finish
        //}


        private void SaveFirstChunk()
        {
            var bytesRead = _byteStream.Read(_firstChunk, 0, FIRST_CHUNK_SIZE);
            if (bytesRead < 0)
            {
                // Exception=
            }
            _byteStream.Seek(0, SeekOrigin.Begin);
        }
    }
}
