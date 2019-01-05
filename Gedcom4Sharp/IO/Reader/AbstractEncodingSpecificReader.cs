using Gedcom4Sharp.Parser;
using System.IO;

namespace Gedcom4Sharp.IO.Reader
{
    /// <summary>
    ///  A base class for the various kinds of readers needed based on the encoding used for the data
    /// </summary>
    public abstract class AbstractEncodingSpecificReader
    {
        /// <summary>
        /// The stream of bytes to read
        /// </summary>
        protected readonly Stream ByteStream;

        /// <summary>
        /// The number of lines read from the input file or stream
        /// </summary>
        protected int LinesRead = 0;

        /// <summary>
        /// The number of bytes read from the input file or stream
        /// </summary>
        protected int BytesRead = 0;

        /// <summary>
        /// The GedcomParser we're reading files for
        /// </summary>
        protected readonly GedcomParser Parser;

        public AbstractEncodingSpecificReader(GedcomParser parser, Stream byteStream)
        {
            Parser = parser;
            ByteStream = byteStream;
        }

        /// <summary>
        /// Get the next line of the file. Must not return empty strings, or lines that are not left-trimmed.
        /// </summary>
        /// <returns>the next line of the file, or null if no more lines to read.</returns>
        public abstract string NextLine();

        /// <summary>
        /// Close resources that might have been opened in the concrete class
        /// </summary>
        public abstract void CleanUp();
    }
}
