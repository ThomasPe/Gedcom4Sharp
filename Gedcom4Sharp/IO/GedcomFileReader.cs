using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gedcom4Sharp.IO
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
        private static readonly int FIRST_CHUNK_SIZE = 16384;

        /// <summary>
        /// A long constant representing the UTF-8 Byte Order Marker signature, which is the six hex characters EF BB BF.
        /// </summary>
        private static readonly long UTF8_BYTE_ORDER_MARKER = 0xEFBBBFL;

        /// <summary>
        /// The first chunk of the file
        /// </summary>
        readonly byte[] firstChunk = new byte[FIRST_CHUNK_SIZE];

        /// <summary>
        /// The buffered input stream of bytes to read
        /// </summary>
        private readonly Stream byteStream;
    }
}
