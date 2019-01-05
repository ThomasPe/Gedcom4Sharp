using Gedcom4Sharp.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gedcom4Sharp.IO.Reader
{
    /// <summary>
    /// A reader that loads from an input stream and gives back a collection of strings representing the data therein. 
    /// This implementation handles UTF-8 data
    /// </summary>
    public sealed class Utf8Reader : AbstractEncodingSpecificReader
    {
        /// <summary>
        /// Was a byte order marker read when inspecting the file to detect encoding?
        /// </summary>
        private bool _byteOrderMarkerRead = false;

        /// <summary>
        /// Input stream reader for internal use over the byte stream
        /// </summary>
        private readonly StreamReader _inputStreamReader;

        /// <summary>
        /// Buffered reader over the input stream reader, for internal use
        /// </summary>
        private readonly StreamReader _bufferedReader;

        public Utf8Reader(GedcomParser parser, Stream byteStream): base(parser, byteStream)
        {
            _inputStreamReader = new StreamReader(byteStream, true);
            _inputStreamReader.Peek();
            var encoding = _inputStreamReader.CurrentEncoding;
        }

        public override string NextLine()
        {
            string result;
            string s = _inputStreamReader.ReadLine();

        }

        public override void CleanUp()
        {
            throw new NotImplementedException();
        }
    }
}
