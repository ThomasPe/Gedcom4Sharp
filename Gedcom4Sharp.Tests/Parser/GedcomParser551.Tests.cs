using System.Linq;
using System.Threading.Tasks;
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
        public async Task TestContinuationForCopyright()
        {
            var gp = new GedcomParser();
            await gp.Load(@"Assets\Samples\john_of_sea_20101009.ged");
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
        public async Task TestEmail()
        {
            var gp = new GedcomParser();
            await gp.Load(@"Assets\Samples\5.5.1 sample 3.ged");
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
        public async Task TestFact()
        {
            var gp = new GedcomParser();
            await gp.Load(@"Assets\Samples\john_of_sea_20101009.ged");
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
        public async Task TestFax()
        {
            var gp = new GedcomParser();
            await gp.Load(@"Assets\Samples\5.5.1 sample 3.ged");
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
        public async Task TestFoneName()
        {
            var gp = new GedcomParser();
            await gp.Load(@"Assets\Samples\5.5.1 sample 2.ged");
            var g = gp.Gedcom;

            Assert.AreEqual(0, gp.Errors.Count);
            // There should be a warning because the file says it's 5.5 but has 5.5.1 tags in it
            Assert.AreNotEqual(0, gp.Warnings.Count);

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

        [TestMethod]
        public async Task TestFonePlace()
        {
            var gp = new GedcomParser();
            await gp.Load(@"Assets\Samples\5.5.1 sample 4.ged");
            var g = gp.Gedcom;

            Assert.AreEqual(0, gp.Errors.Count);
            // There should be a warning because the file says it's 5.5 but has 5.5.1 tags in it
            Assert.AreNotEqual(0, gp.Warnings.Count);

            Assert.IsNotNull(g);
            Assert.AreEqual(1, g.Individuals.Count);
            var i = g.Individuals["@I1@"];
            Assert.AreEqual(1, i.Events.Count);
            var e = i.Events.FirstOrDefault();
            Assert.AreEqual(IndividualEventType.BIRTH, e.Type);
            Assert.IsNotNull(e.Place);
            var p = e.Place;
            Assert.AreEqual("Tarnowie, Krak\u00F3w, Poland", p.PlaceName);
            Assert.IsNotNull(p.NoteStructures);
            Assert.AreEqual(1, p.NoteStructures.Count);
            Assert.IsNotNull(p.Phonetic);
            Assert.AreEqual(1, p.Phonetic.Count);
            var nv = p.Phonetic.FirstOrDefault();
            Assert.IsNotNull(nv);
            Assert.IsTrue(nv is PlaceNameVariation);
            Assert.AreEqual("Tarr-now, Krack-ow, Poh-land", nv.Variation);
            Assert.AreEqual("guessing", nv.VariationType.ToString());
        }

        // TODO Finish Tests

        [TestMethod]
        public async Task TestMapLatLong()
        {
            var gp = new GedcomParser();
            await gp.Load(@"Assets\Samples\5.5.1 sample 4.ged");
            var g = gp.Gedcom;

            Assert.AreEqual(0, gp.Errors.Count);
            // There should be a warning because the file says it's 5.5 but has 5.5.1 tags in it
            Assert.AreNotEqual(0, gp.Warnings.Count);

            Assert.IsNotNull(g);
            Assert.AreEqual(1, g.Individuals.Count);
            var i = g.Individuals["@I1@"];
            Assert.AreEqual(1, i.Events.Count);
            var e = i.Events.FirstOrDefault();
            Assert.AreEqual(IndividualEventType.BIRTH, e.Type);
            Assert.IsNotNull(e.Place);
            var p = e.Place;
            Assert.AreEqual("Tarnowie, Krak\u00F3w, Poland", e.Place.PlaceName);
            Assert.IsNotNull(p.NoteStructures);
            Assert.AreEqual(1, p.NoteStructures.Count);
            Assert.AreEqual("+50\u00B0 3' 1.49\"", p.Latitude.ToString());
            Assert.AreEqual("+19\u00B0 56' 21.48\"", p.Longitude.ToString());
        }

        [TestMethod]
        public async Task TestMultimediaChanges1()
        {
            var gp = new GedcomParser();
            await gp.Load(@"Assets\Samples\TGC551.ged");
            foreach(var w in gp.Warnings)
            {
                Assert.IsFalse(w.Contains("BLOB"));
            }
            Assert.IsNotNull(gp.Gedcom);
        }

        [TestMethod]
        public async Task TestMultimediaChanges2()
        {
            var gp = new GedcomParser();

            // 5.5.1 data with a BLOB - illegal
            await gp.Load(@"Assets\Samples\5.5.1 sample 4.ged");
            Assert.AreEqual(0, gp.Errors.Count);
            Assert.AreNotEqual(0, gp.Warnings.Count);

            var found = false;
            foreach (var w in gp.Warnings)
            {
                if (w.Contains("BLOB"))
                {
                    found = true;
                }
            }
            Assert.IsTrue(found);
            Assert.IsNotNull(gp.Gedcom);
        }


        [TestMethod]
        public async Task TestMultimediaChanges3()
        {
            var gp = new GedcomParser();
            // 5.5 data, has a blob - OK
            await gp.Load(@"Assets\Samples\5.5.1 sample 1.ged");
            Assert.AreEqual(0, gp.Errors.Count);

            foreach (var w in gp.Warnings)
            {
                Assert.IsFalse(w.Contains("BLOB"));
            }
            Assert.IsNotNull(gp.Gedcom);
        }

        /// <summary>
        /// Test the file references sections for a multimedia object
        /// </summary>
        [TestMethod]
        public async Task TestMultimediaFileRef()
        {
            var gp = new GedcomParser();
            await gp.Load(@"Assets\Samples\5.5.1 sample 5.ged");
            Assert.AreEqual(0, gp.Errors.Count);
        }

        /// <summary>
        /// Test the new religious affiliation tag added to family events in GEDCOM 5.5.1
        /// </summary>
        [TestMethod]
        public async Task TestReligionOnFamilyEventDetail()
        {
            var gp = new GedcomParser();
            await gp.Load(@"Assets\Samples\5.5.1 sample 1.ged");
            Assert.AreEqual(0, gp.Errors.Count);
            Assert.AreEqual(0, gp.Warnings.Count);

            var g = gp.Gedcom;
            Assert.IsNotNull(g);

            var f = g.Families["@F1@"];
            Assert.IsNotNull(f);

            // Negative test
            var divorce = f.Events[0];
            Assert.IsNotNull(divorce);
            Assert.AreEqual(FamilyEventType.DIVORCE, divorce.Type);
            Assert.AreEqual("2 Sep 1880", divorce.Date.Value);
            Assert.IsNull(divorce.ReligiousAffiliation);

            // Positive test
            var marriage = f.Events[1];
            Assert.IsNotNull(marriage);
            Assert.AreEqual(FamilyEventType.MARRIAGE, marriage.Type);
            Assert.AreEqual("25 Oct 1875", marriage.Date.Value);
            Assert.AreEqual("Civil", marriage.ReligiousAffiliation.Value);
        }

        /// <summary>
        /// Test the new restriction tag on Families added in GEDCOM 5.5.1
        /// </summary>
        [TestMethod]
        public async Task TestRestrictionOnEvent()
        {
            var gp = new GedcomParser();
            await gp.Load(@"Assets\Samples\5.5.1 sample 3.ged");
            Assert.AreEqual(0, gp.Errors.Count);
            // There should be a warning because the file says it's 5.5 but has 5.5.1 tags in it
            Assert.AreNotEqual(0, gp.Warnings.Count);

            var g = gp.Gedcom;
            Assert.IsNotNull(g);

            var f = g.Families["@F1@"];
            Assert.IsNotNull(f);
            Assert.IsNotNull(f.Events);
            Assert.AreEqual(1, f.Events.Count);
            var e = f.Events[0];
            Assert.IsNotNull(e);
            Assert.AreEqual("locked", e.RestrictionNotice.Value);

            f = g.Families["@F2@"];
            Assert.IsNotNull(f);
            Assert.IsNull(f.RestrictionNotice);
            Assert.IsNotNull(f.Events);
            Assert.AreEqual(1, f.Events.Count);
            e = f.Events.FirstOrDefault();
            Assert.IsNull(e.RestrictionNotice);
        }

        /// <summary>
        /// Test the new restriction tag on Families added in GEDCOM 5.5.1
        /// </summary>
        [TestMethod]
        public async Task TestRestrictionOnFamily()
        {
            var gp = new GedcomParser();
            await gp.Load(@"Assets\Samples\5.5.1 sample 3.ged");
            Assert.AreEqual(0, gp.Errors.Count);
            // There should be a warning because the file says it's 5.5 but has 5.5.1 tags in it
            Assert.AreNotEqual(0, gp.Warnings.Count);

            var g = gp.Gedcom;
            Assert.IsNotNull(g);
            Assert.IsNotNull(g.Families);
            var f = g.Families["@F1@"];
            Assert.IsNotNull(f);
            Assert.AreEqual("locked", f.RestrictionNotice.Value);
            f = g.Families["@F2@"];
            Assert.IsNotNull(f);
            Assert.IsNull(f.RestrictionNotice);
        }

        /// <summary>
        /// Test for parsing of the new "ROMN" tag on a personal name in 5.5.1
        /// </summary>
        [TestMethod]
        public async Task TestRomnName()
        {
            var gp = new GedcomParser();
            await gp.Load(@"Assets\Samples\5.5.1 sample 3.ged");
            Assert.AreEqual(0, gp.Errors.Count);
            // There should be a warning because the file says it's 5.5 but has 5.5.1 tags in it
            Assert.AreNotEqual(0, gp.Warnings.Count);
            var g = gp.Gedcom;
            Assert.IsNotNull(g);

            var ladislaus = g.Individuals["@I2797@"];
            Assert.IsNotNull(ladislaus);
            Assert.AreEqual(1, ladislaus.Names.Count);
            var pn = ladislaus.Names.FirstOrDefault();
            Assert.IsNotNull(pn);
            Assert.AreEqual(1, pn.Romanized.Count);
            var pnv = pn.Romanized.FirstOrDefault();
            Assert.AreEqual("Walter /Borgula/", pnv.Variation);
            Assert.IsNull(pnv.VariationType);
        }

        /// <summary>
        /// Test parsing the ROMN tag on places in GEDCOM 5.5.1
        /// </summary>
        [TestMethod]
        public async Task TestRomnPlace()
        {
            var gp = new GedcomParser();
            await gp.Load(@"Assets\Samples\5.5.1 sample 4.ged");
            Assert.AreEqual(0, gp.Errors.Count);
            // There should be a warning because the file says it's 5.5 but has 5.5.1 tags in it
            Assert.AreNotEqual(0, gp.Warnings.Count);
            var g = gp.Gedcom;
            Assert.IsNotNull(g);

            Assert.AreEqual(1, g.Individuals.Count);
            var i = g.Individuals["@I1@"];
            Assert.AreEqual(1, i.Events.Count);
            var e = i.Events.FirstOrDefault();
            Assert.AreEqual(IndividualEventType.BIRTH, e.Type);
            Assert.IsNotNull(e.Place);
            var p = e.Place;
            Assert.AreEqual("Tarnowie, Krak\u00F3w, Poland", e.Place.PlaceName);
            Assert.IsNotNull(p.NoteStructures);
            Assert.AreEqual(1, p.NoteStructures.Count);
            Assert.IsNotNull(p.Romanized);
            Assert.AreEqual(1, p.Romanized.Count);
            var nv = p.Romanized.FirstOrDefault();
            Assert.IsNotNull(nv);
            Assert.AreEqual("Tarnow, Cracow, Poland", nv.Variation);
            Assert.AreEqual("Google translate", nv.VariationType.Value);
        }

        /// <summary>
        /// Test for parsing of the new STAT sub-tag on FAMC tags in 5.5.1
        /// </summary>
        [TestMethod]
        public async Task TestStatusOnFamilyChild()
        {
            var gp = new GedcomParser();
            await gp.Load(@"Assets\Samples\5.5.1 sample 1.ged");
            Assert.AreEqual(0, gp.Errors.Count);
            Assert.AreEqual(0, gp.Warnings.Count);
            var g = gp.Gedcom;
            Assert.IsNotNull(g);

            // Positive test
            var george = g.Individuals["@I3@"];
            Assert.IsNotNull(george);
            Assert.AreEqual(1, george.FamiliesWhereChild.Count);

            var fc1 = george.FamiliesWhereChild.First();
            Assert.IsNotNull(fc1);
            Assert.IsNotNull(fc1.Family);
            Assert.AreEqual("@F3@", fc1.Family.Xref);
            Assert.AreEqual("proven", fc1.Status.Value);

            // Negative test
            var anne = g.Individuals["@I4@"];
            Assert.IsNotNull(anne);
            Assert.AreEqual(1, anne.FamiliesWhereChild.Count);
            var fc2 = anne.FamiliesWhereChild.FirstOrDefault();
            Assert.IsNotNull(fc2);
            Assert.IsNotNull(fc2.Family);
            Assert.AreEqual("@F2@", fc2.Family.Xref);
            Assert.IsNull(fc2.Status);
        }

        /// <summary>
        /// Test for parsing of the new "WWW" tag in 5.5.1
        /// </summary>
        [TestMethod]
        public async Task TestWWW()
        {
            var gp = new GedcomParser();
            await gp.Load(@"Assets\Samples\5.5.1 sample 3.ged");
            Assert.AreEqual(0, gp.Errors.Count);
            // There should be a warning because the file says it's 5.5 but has 5.5.1 tags in it
            Assert.AreNotEqual(0, gp.Warnings.Count);
            var g = gp.Gedcom;
            Assert.IsNotNull(g);

            Assert.IsNotNull(g.Submitters);
            var s = g.Submitters["@SUBM01@"];
            Assert.IsNotNull(s);
            Assert.AreEqual("Matt /Harrah/", s.Name.Value);
            Assert.IsNotNull(s.WwwUrls);
            Assert.AreEqual(2, s.WwwUrls.Count);
            Assert.AreEqual("https://www.facebook.com/Gedcom4j", s.WwwUrls[1].Value);
        }
    }
}
