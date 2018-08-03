using System;
using Gedcom4Sharp.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gedcom4Sharp.Tests.Parser
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

        /// <summary>
        /// Negative test case when there is no tag
        /// </summary>
        [TestMethod]
        public void LinePieces_NegativeNothingAfterXref()
        {
            var ex = Assert.ThrowsException<Exception>(() => new LinePieces("4 @XREF@", 5));
            Assert.AreEqual("All GEDCOM lines are required to have a tag value, but no tag could be found on line 5", ex.Message);
        }

        [TestMethod]
        public void LinePieces_Positive()
        {
            var lp = new LinePieces("4 @XREF@ TAGG additional stuff", 5);
            Assert.IsNotNull(lp);
            Assert.AreEqual(4, lp.Level);
            Assert.AreEqual("@XREF@", lp.Id);
            Assert.AreEqual("TAGG", lp.Tag);
            Assert.AreEqual("additional stuff", lp.Remainder);
        }

        [TestMethod]
        public void LinePieces_PositiveNothingAfterTag()
        {
            var lp = new LinePieces("4 TAGG", 5);
            Assert.IsNotNull(lp);
            Assert.AreEqual(4, lp.Level);
            Assert.IsTrue(String.IsNullOrEmpty(lp.Id));
            Assert.AreEqual("TAGG", lp.Tag);
            Assert.IsTrue(String.IsNullOrEmpty(lp.Remainder));
        }

        [TestMethod]
        public void LinePieces_PositiveNoXref()
        {
            var lp = new LinePieces("4 TAGG additional stuff", 5);
            Assert.IsNotNull(lp);
            Assert.AreEqual(4, lp.Level);
            Assert.IsTrue(String.IsNullOrEmpty(lp.Id));
            Assert.AreEqual("TAGG", lp.Tag);
            Assert.AreEqual("additional stuff", lp.Remainder);
        }
    }
}
