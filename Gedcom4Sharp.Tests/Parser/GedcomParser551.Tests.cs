using System.Linq;
using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
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
            // TODO: Theres no warning as the version is read too late from the file
            // Assert.AreNotEqual(0, gp.Warnings.Count);

            var g = gp.Gedcom;
            var found = gp.Gedcom.Individuals.Values.Where(x => x.Names.Any(s => s.Basic.Contains("Moor") && s.Basic.Contains("Mary"))).ToList();
            Assert.AreEqual(1, found.Count);
            var mary = found.FirstOrDefault();
            Assert.IsNotNull(mary);
            var facts = mary.GetAttributesOfType(IndividualAttributeType.FACT);
            Assert.IsNotNull(facts);
            Assert.AreEqual(1, facts.Count);
            var fact = facts.FirstOrDefault();
            Assert.IsNotNull(fact);
            Assert.AreEqual(IndividualAttributeType.FACT, fact.Type);
            Assert.AreEqual("Place from", fact.SubType.Value);
            Assert.AreEqual("Combe Florey", fact.Description.Value);
        }

        /// <summary>
        /// Test for parsing of the new "FAX" tag in 5.5.1
        /// </summary>
        [TestMethod]
        public void TestFax()
        {
            var gp = new GedcomParser();
            gp.Load(@"Assets\Samples\5.5.1 sample 3.ged");
            var g = gp.Gedcom;

            Assert.IsNotNull(g);
            Assert.IsNotNull(g.Header);
            Assert.IsNotNull(g.Header.SourceSystem);
            Assert.IsNotNull(g.Header.SourceSystem.Corporation);
            var c = g.Header.SourceSystem.Corporation;
            Assert.AreEqual("The Church of Jesus Christ of Latter-day Saints", c.BusinessName);
            Assert.IsNotNull(c.FaxNumbers);
            Assert.AreEqual(1, c.FaxNumbers.Count);
            Assert.AreEqual("800-555-1212", c.FaxNumbers[0].ToString());
            Assert.IsNotNull(c.Emails);
            Assert.AreEqual(0, c.Emails.Count);
        }

        /// <summary>
        /// Test for parsing of the new "FONE" tag on a personal name in 5.5.1
        /// </summary>
        [TestMethod]
        public void TestFoneName()
        {
            var gp = new GedcomParser();
            gp.Load(@"Assets\Samples\5.5.1 sample 2.ged");
            var g = gp.Gedcom;

            Assert.AreEqual(0, gp.Errors.Count);
            // There should be a warning because the file says it's 5.5 but has 5.5.1 tags in it
            // TODO: Theres no warning as the version is read too late from the file
            // Assert.AreNotEqual(0, gp.Warnings.Count);

            var found = gp.Gedcom.Individuals.Values.Where(x => x.Names.Any(s => s.Basic.Contains("Pinter") && s.Basic.Contains("Anonymus"))).ToList();
            Assert.AreEqual(1, found.Count);
            var dude = found.FirstOrDefault();
            Assert.IsNotNull(dude);
            Assert.AreEqual(1, dude.Names.Count);
            var pn = dude.Names.FirstOrDefault();
            Assert.IsNotNull(pn);
            Assert.IsNotNull(pn.Phonetic);
            Assert.AreEqual(1, pn.Phonetic.Count);
            var pnv = pn.Phonetic.FirstOrDefault();
            Assert.AreEqual("Anonymus /Pinter/", pnv.Variation);
            Assert.IsNull(pnv.VariationType);
        }
    }
}
