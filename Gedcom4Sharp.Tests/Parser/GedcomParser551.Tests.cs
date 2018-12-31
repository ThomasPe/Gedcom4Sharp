using System.Linq;
using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gedcom4Sharp.Tests.Parser
{
    [TestClass]
    public class GedcomParser551
    {

        /// <summary>
        /// Test that header copyright data is now multi-line capable
        /// </summary>
        [TestMethod]
        public void TestContinuationForCopyright()
        {
            var gp = new GedcomParser();
            gp.Load(@"Assets\Samples\john_of_sea_20101009.ged");
            Assert.AreEqual(0, gp.Errors.Count);
            // There should be a warning because the file says it's 5.5 but has 5.5.1 tags in it"

			// TODO: Can't check if its 5.5 as the Version Tag comes after the Copyright Tag
            // Assert.AreNotEqual(0, gp.Warnings.Count);
            var g = gp.Gedcom;
            Assert.IsNotNull(g);
            Assert.IsNotNull(g.Header);
            Assert.IsNotNull(g.Header.CopyrightData);
            Assert.AreEqual(3, g.Header.CopyrightData.Count);
            Assert.AreEqual("License: Creative Commons Attribution-ShareAlike 3.0", g.Header.CopyrightData[1]);
        }

        /// <summary>
        /// Test for parsing of the new "EMAIL" tag in 5.5.1
        /// </summary>
        [TestMethod]
        public void TestEmail()
        {
            var gp = new GedcomParser();
            gp.Load(@"Assets\Samples\5.5.1 sample 3.ged");
            var g = gp.Gedcom;

            Assert.IsNotNull(g);
            Assert.IsNotNull(g.Submitters);
            Assert.AreEqual(1, g.Submitters.Count);
            var s = g.Submitters["@SUBM01@"];
            Assert.IsNotNull(s);
            Assert.AreEqual("Matt /Harrah/", s.Name.Value);
            Assert.IsNotNull(s.Emails);
            Assert.AreEqual(2, s.Emails.Count);
            Assert.AreEqual("frizbog@charter.net", s.Emails[1].Value);
        }

        /// <summary>
        /// Test for parsing of the new "FACT" tag in 5.5.1
        /// </summary>
        [TestMethod]
        public void TestFact()
        {
            var gp = new GedcomParser();
            gp.Load(@"Assets\Samples\john_of_sea_20101009.ged");
            Assert.AreEqual(0, gp.Errors.Count);
            // There should be a warning because the file says it's 5.5 but has 5.5.1 tags in it
            Assert.AreNotEqual(0, gp.Warnings.Count);

            var g = gp.Gedcom;
            Finder f = new Finder(g);
			
        }

    }
}
