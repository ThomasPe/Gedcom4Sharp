using System;
using System.Text;
using System.Threading.Tasks;
using Gedcom4Sharp.IO;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gedcom4Sharp.Tests.IO.Reader
{
    [TestClass]
    public class ReadEncodingTests
    {
        [TestMethod]
        public async Task TestGetEncodingFromGedcom()
        {
            var gp = new GedcomParser();
            var enc = await EncodingHelper.GetEncodingFromGedcom(@"Assets\Samples\5.5.1 sample 1.ged");
            Assert.AreEqual(Encoding.UTF8, enc);

            enc = await EncodingHelper.GetEncodingFromGedcom(@"Assets\Samples\ANSEL.GED");
            Assert.IsTrue(enc is Marc8Encoding);

            enc = await EncodingHelper.GetEncodingFromGedcom(@"Assets\Samples\allged.ged");
            Assert.AreEqual(Encoding.ASCII, enc);

            enc = await EncodingHelper.GetEncodingFromGedcom(@"Assets\Samples\a31486.ged");
            Assert.AreEqual(Encoding.Default, enc);
        }

        [TestMethod]
        public void TestGetEncodingFromFile()
        {
            var gp = new GedcomParser();
            var enc = EncodingHelper.GetEncodingFromFile(@"Assets\Samples\5.5.1 sample 1.ged");
            Assert.AreEqual(null, enc);

            enc = EncodingHelper.GetEncodingFromFile(@"Assets\Samples\ANSEL.GED");
            Assert.AreEqual(null, enc);

            enc = EncodingHelper.GetEncodingFromFile(@"Assets\Samples\utf16be.ged");
            Assert.AreEqual(Encoding.BigEndianUnicode, enc);

            enc = EncodingHelper.GetEncodingFromFile(@"Assets\Samples\utf16le.ged");
            Assert.AreEqual(Encoding.Unicode, enc);

            enc = EncodingHelper.GetEncodingFromFile(@"Assets\Samples\utf8.ged");
            Assert.AreEqual(Encoding.UTF8, enc);

            enc = EncodingHelper.GetEncodingFromFile(@"Assets\Samples\utf8.ged");
            Assert.AreEqual(Encoding.UTF8, enc);
        }

        [TestMethod]
        public async Task TestGetEncoding()
        {
            var gp = new GedcomParser();
            var enc = await EncodingHelper.GetEncoding(@"Assets\Samples\5.5.1 sample 1.ged");
            //Assert.AreEqual(null, enc);

            enc = await EncodingHelper.GetEncoding(@"Assets\Samples\ANSEL.GED");
            //Assert.AreEqual(null, enc);

            enc = await EncodingHelper.GetEncoding(@"Assets\Samples\utf16be.ged");
            //Assert.AreEqual(Encoding.BigEndianUnicode, enc);

            enc = await EncodingHelper.GetEncoding(@"Assets\Samples\utf16le.ged");
            Assert.AreEqual(Encoding.Unicode, enc);

            enc = await EncodingHelper.GetEncoding(@"Assets\Samples\utf8.ged");
            Assert.AreEqual(Encoding.UTF8, enc);

            enc = EncodingHelper.GetEncodingFromFile(@"Assets\Samples\utf8.ged");
            Assert.AreEqual(Encoding.UTF8, enc);
        }
    }
}
