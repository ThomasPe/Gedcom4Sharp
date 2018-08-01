using System;
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
            var submitter = g.Submitters.Values.First();
            Assert.AreEqual("/Submitter-Name/", submitter.Name.Value);
        }



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
