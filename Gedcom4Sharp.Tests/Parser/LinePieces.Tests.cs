using System;
using Gedcom4Sharp.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gedcom4Sharp.Tests
{
    [TestClass]
    public class LinePiecesTests
    {
        // https://github.com/frizbog/gedcom4j/blob/master/src/test/java/org/gedcom4j/parser/LinePiecesTest.java

        /// <summary>
        /// Negative test case when the level is non-numeric
        /// </summary>
        [TestMethod]
        public void LinePieces_NegativeBadLevel()
        {
            var line = "BAD TAGG additional stuff";
            var ex = Assert.ThrowsException<Exception>(() => new LinePieces(line, 5));
            Assert.AreEqual($"Line 5 does not begin with a 1 or 2 digit number for the level followed by a space: {line}", ex.Message);
        }

        /// <summary>
        /// Negative test case when the xref is not terminated
        /// </summary>
        [TestMethod]
        public void LinePieces_NegativeBadXref()
        {
            var ex = Assert.ThrowsException<Exception>(() => new LinePieces("4 @XREF TAGG additional stuff", 5));
            Assert.AreEqual("XRef ID begins with @ sign but is not terminated with one on line 5", ex.Message);
        }
    }
}
