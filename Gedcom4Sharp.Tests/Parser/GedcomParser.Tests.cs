using System.Diagnostics;
using System.Linq;
using Gedcom4Sharp.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gedcom4Sharp.Tests.Parser
{
    [TestClass]
    public class GedcomParserTests
    {
		[TestMethod]
		public void Test()
        {
            var gp = new GedcomParser();
            gp.Load(@"Assets\Samples\EnglishTudorHouse.ged");
            Debug.WriteLine("done");
        }

        [TestMethod]
        public void BadCustomTag()
        {
            var gp = new GedcomParser();
            gp.Load(@"Assets\Samples\Bad_custom_tag.ged");
            Assert.IsNotNull(gp.Errors);
            Assert.AreEqual(1, gp.Errors.Count);
            Assert.IsNotNull(gp.Errors[0]);
            Assert.IsTrue(gp.Errors[0].Contains("Line 28: Cannot handle tag BUST"));
        }

        [TestMethod]
        public void Load1()
        {
            var gp = new GedcomParser();
            gp.Load(@"Assets\Samples\TGC551.ged");
            checkTGC551LF(gp);
        }

        [TestMethod]
        public void Load2()
        {
            var gp = new GedcomParser();
            Assert.IsTrue(gp.Errors.Count == 0);
            gp.Load(@"Assets\Samples\allged.ged");
            Assert.IsTrue(gp.Errors.Count == 0);
            Assert.IsTrue(gp.Warnings.Count == 0);

            // There is exactly 1 custom tag on the file as a whole
            Assert.AreEqual(1, gp.Gedcom.CustomFacts.Count);
            // There is exactly 1 custom tag in the header
            Assert.AreEqual(1, gp.Gedcom.Header.CustomFacts.Count);

            var g = gp.Gedcom;
            Assert.IsTrue(g.Submitters.Count > 0);
            var submitter = g.Submitters.Values.FirstOrDefault();
            Assert.AreEqual("/Submitter-Name/", submitter.Name.Value);
        }

        [TestMethod]
        public void Load3()
        {
            var gp = new GedcomParser();
            gp.Load(@"Assets\Samples\a31486.ged");
            Assert.IsTrue(gp.Errors.Count == 0);
            Assert.IsTrue(gp.Warnings.Count == 0);

            var g = gp.Gedcom;

            // Check submitter
            Assert.IsTrue(g.Submitters.Count > 0);
            var submitter = g.Submitters.Values.FirstOrDefault();
            Assert.IsNotNull(submitter);
            Assert.AreEqual("UNSPECIFIED", submitter.Name.Value);

            // Check header
            Assert.AreEqual("6.00", g.Header.SourceSystem.VersionNum.Value);
            Assert.AreEqual("(510) 794-6850", g.Header.SourceSystem.Corporation.PhoneNumbers.FirstOrDefault().Value);

            // There are two sources in this file, and their names should be as
            // shown
            Assert.AreEqual(2, g.Sources.Count);
            foreach(var s in g.Sources.Values)
            {
                Assert.IsTrue(s.Title.Lines.FirstOrDefault().Equals("William Barnett Family.FTW") ||
                              s.Title.Lines.FirstOrDefault().Equals("Warrick County, IN WPA Indexes"));
            }

            Assert.AreEqual(17, g.Families.Count);
            Assert.AreEqual(64, g.Individuals.Count);

            // Check a specific family
            var family = g.Families["@F1428@"];
            Assert.IsNotNull(family);
            Assert.AreEqual(3, family.Children.Count);
            var h = family.Husband.Individual;
            Assert.AreEqual("Lawrence Henry /Barnett/", h.Names.FirstOrDefault().Basic);
            var w = family.Wife.Individual;
            Assert.AreEqual("Velma //", w.Names.FirstOrDefault().Basic);
        }

        // TODO Finish Tests
        // https://github.com/frizbog/gedcom4j/blob/master/src/test/java/org/gedcom4j/parser/GedcomParserTest.java


        private void checkTGC551LF(GedcomParser gp)
        {
            var g = gp.Gedcom;
            Assert.IsNotNull(g.Header);
            Assert.AreEqual(3, g.Submitters.Count);
            var submitter = g.Submitters["@SUBMITTER@"];
            Assert.IsNotNull(submitter);
            Assert.AreEqual("John A. Nairn", submitter.Name.Value);

            Assert.AreEqual(7, g.Families.Count);
            Assert.AreEqual(2, g.Sources.Count);
            Assert.AreEqual(1, g.Multimedia.Count);
            Assert.AreEqual(15, g.Individuals.Count);
        }
    }
}
