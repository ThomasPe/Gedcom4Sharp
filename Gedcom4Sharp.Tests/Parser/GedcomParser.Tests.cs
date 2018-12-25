using System.Diagnostics;
using System.Linq;
using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gedcom4Sharp.Tests.Parser
{
    [TestClass]
    public class GedcomParserTests
    {
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

        [TestMethod]
        public void Load4()
        {
            var gp = new GedcomParser();
            // Different line end char seq than the other file
            gp.Load(@"Assets\Samples\TGC551LF.ged");
            checkTGC551LF(gp);
        }

        /// <summary>
        /// Test loading a minimal GEDCOM 5.5 file that only has a submitter. This test uses a file which indents lines by their tag
        /// level, even though the spec says not to. However, the spec also says to ignore leading spaces on lines, so we're doing that.
        /// </summary>
        [TestMethod]
        public void TestLoadIndentedMinimal55File()
        {
            var gp = new GedcomParser();
            gp.Load(@"Assets\Samples\minimal55indented.ged");

            // No problems detected, right?
            Assert.AreEqual(0, gp.Errors.Count);
            Assert.AreEqual(0, gp.Warnings.Count);

            // And the data is as we expected, right?
            Assert.IsNotNull(gp.Gedcom);
            Assert.AreEqual(0, gp.Gedcom.Individuals.Count);
            Assert.AreEqual(0, gp.Gedcom.Families.Count);
            Assert.AreEqual(0, gp.Gedcom.Multimedia.Count);
            Assert.AreEqual(0, gp.Gedcom.Sources.Count);
            Assert.AreEqual(1, gp.Gedcom.Submitters.Count);
        }

        /// <summary>
        /// Test loading a minimal GEDCOM 5.5 file that only has a submitter.
        /// </summary>
        [TestMethod]
        public void TestLoadMinimal55File()
        {
            var gp = new GedcomParser();
            gp.Load(@"Assets\Samples\minimal55.ged");

            // No problems detected, right?
            Assert.AreEqual(0, gp.Errors.Count);
            Assert.AreEqual(0, gp.Warnings.Count);

            // And the data is as we expected, right?
            Assert.IsNotNull(gp.Gedcom);
            Assert.AreEqual(0, gp.Gedcom.Individuals.Count);
            Assert.AreEqual(0, gp.Gedcom.Families.Count);
            Assert.AreEqual(0, gp.Gedcom.Multimedia.Count);
            Assert.AreEqual(0, gp.Gedcom.Sources.Count);
            Assert.AreEqual(1, gp.Gedcom.Submitters.Count);
        }

        // TODO public void TestLoadStream()

        [TestMethod]
        public void TestLoadTGC55C()
        {
            var gp = new GedcomParser();
            gp.Load(@"Assets\Samples\TGC55C.ged");
            CheckTGC55C(gp);
        }


        // TODO Finish Tests
        // https://github.com/frizbog/gedcom4j/blob/master/src/test/java/org/gedcom4j/parser/GedcomParserTest.java


        /// <summary>
        /// The same sample file is used several times, this helper method ensures consistent assertions for all tests using the same file
        /// </summary>
        /// <param name="gp"></param>
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

        /// <summary>
        /// The same sample file is used several times, this helper method ensures consistent assertions for all tests using the same file
        /// </summary>
        /// <param name="gp"></param>
        private void CheckTGC55C(GedcomParser gp)
        {
            var g = gp.Gedcom;
            Assert.IsNotNull(g.Header);
            Assert.AreEqual(3, g.Submitters.Count);
            var submitter = g.Submitters["@SUBMITTER@"];
            Assert.IsNotNull(submitter);
            Assert.AreEqual("John A. Nairn", submitter.Name.ToString());

            Assert.AreEqual(7, g.Families.Count);
            Assert.AreEqual(2, g.Sources.Count);
            Assert.AreEqual(1, g.Multimedia.Count);
            Assert.AreEqual(15, g.Individuals.Count);

            // ===============================================================
            // Individual @PERSON1@
            // ===============================================================

            var indi = g.Individuals["@PERSON1@"];

            Assert.AreEqual("@PERSON1@", indi.Xref);

            Assert.AreEqual(3, indi.Citations.Count);
            Assert.AreEqual(2, indi.Names.Count);
            Assert.AreEqual(2, indi.NoteStructures.Count);

            // Name 0
            var name = indi.Names[0];
            Assert.AreEqual("Joseph Tag /Torture/", name.Basic);
            Assert.AreEqual("Torture, Joseph \"Joe\"", name.ToString());

            Assert.AreEqual(1, name.Citations.Count);
            Assert.AreEqual(1, name.NoteStructures.Count);

            // Name 0 - Citation 0
            Assert.IsTrue(name.Citations[0] is CitationWithSource);
            var citWithSource = name.Citations[0] as CitationWithSource;
            var source = citWithSource.Source;

            Assert.AreEqual("@SOURCE1@", source.Xref);
            Assert.AreEqual("42", citWithSource.WhereInSource.ToString());

            Assert.AreEqual(0, citWithSource.Multimedia.Count);
            Assert.AreEqual(0, citWithSource.NoteStructures.Count);

            // Name 0 - Note 0
            var note = name.NoteStructures[0];
            Assert.AreEqual(5, note.Lines.Count);
            Assert.AreEqual(
                "These are notes about the first NAME structure in this record. These notes are embedded in the INDIVIDUAL record itself.",
                note.Lines[0]);

            // Name 1
            name = indi.Names[1];
            Assert.AreEqual("William John /Smith/", name.Basic);
            Assert.AreEqual("William John /Smith/", name.ToString());

            Assert.AreEqual(1, name.Citations.Count);
            Assert.AreEqual(1, name.NoteStructures.Count);

            // Name 1 - Citation 0
            Assert.IsTrue(name.Citations[0] is CitationWithSource);
            citWithSource = name.Citations[0] as CitationWithSource;
            source = citWithSource.Source;

            Assert.AreEqual("@SOURCE1@", source.Xref);
            Assert.AreEqual("55", citWithSource.WhereInSource.ToString());

            Assert.AreEqual(1, citWithSource.Multimedia.Count);
            Assert.AreEqual(1, citWithSource.NoteStructures.Count);

            // Name 1 - Multimedia 0
            var multimedia = citWithSource.Multimedia[0].Multimedia;
            Assert.AreEqual(0, multimedia.Citations.Count);
            Assert.AreEqual(1, multimedia.FileReferences.Count);
            Assert.AreEqual(1, multimedia.NoteStructures.Count);

            // Name 1 - Multimedia 0 - FileReference 0
            var fileReference = multimedia.FileReferences[0];
            Assert.AreEqual("jpeg", fileReference.Format.ToString());
            Assert.IsNull(fileReference.MediaType);
            Assert.AreEqual("ImgFile.JPG", fileReference.ReferenceToFile.ToString());
            Assert.IsNull(fileReference.Title);

            // Name 1 - Multimedia 0 - Note 0
            note = multimedia.NoteStructures[0];
            Assert.AreEqual(1, note.NoteReference.Lines[0]);
            Assert.AreEqual("These are some notes of this multimedia link in the NAME structure.", note.NoteReference.Lines[0]);

            // Name 1 - Citation 0 - Note 0
            note = citWithSource.NoteStructures[0];
            Assert.IsNotNull(note);
            Assert.IsNotNull(note.NoteReference);
            Assert.IsNotNull(note.NoteReference.Lines);
            Assert.AreEqual(3, note.NoteReference.Lines.Count);
            Assert.AreEqual(
                @"This source citation has all fields possible in a source citation to a separate SOURCE record. 
                Besides the link to the SOURCE record there are possible fields about this citation (e.g., PAGE, TEXT, etc.)",
                note.NoteReference.Lines[0]);

            // Name 1 - Note 0
            note = name.NoteStructures[0];
            Assert.AreEqual(3, note.Lines.Count);
            Assert.AreEqual(
                @"This is a second personal NAME structure in a single INDIVIDUAL record which is allowed in GEDCOM. 
                This second NAME structure has all possible fields for a NAME structure.",
                note.Lines[0]);

            // Note 0
            note = indi.NoteStructures[0];
            Assert.AreEqual(40, note.NoteReference.Lines.Count);
            Assert.AreEqual("Comments on \"Joseph Tag Torture\" INDIVIDUAL Record.", note.NoteReference.Lines[0]);

            // Note 1
            note = indi.NoteStructures[1];
            Assert.AreEqual(3, note.NoteReference.Lines.Count);
            Assert.AreEqual(
                @"This is a second set of notes for this single individual record. 
                It is embedded in the INDIVIDUAL record instead of being in a separate NOTE record.",
                note.Lines[0]);

            // Citation 0
            Assert.IsTrue(indi.Citations[0] is CitationWithSource);
            citWithSource = indi.Citations[0] as CitationWithSource;
            source = citWithSource.Source;

            Assert.AreEqual("@SOURCE1@", source.Xref);
            Assert.AreEqual("42", citWithSource.WhereInSource.ToString());

            Assert.AreEqual(0, citWithSource.Multimedia.Count);
            Assert.AreEqual(1, citWithSource.NoteStructures.Count);

            // Citation 0 - Note 0
            note = citWithSource.NoteStructures[0];
            Assert.AreEqual(1, note.Lines.Count);
            Assert.AreEqual("A source note.", note.Lines[0]);

            // Citation 1
            Assert.IsTrue(indi.Citations[1] is CitationWithSource);
            citWithSource = indi.Citations[1] as CitationWithSource;
            source = citWithSource.Source;

            Assert.AreEqual("@SR2@", source.Xref);
            Assert.AreEqual(null, citWithSource.WhereInSource);

            Assert.AreEqual(0, citWithSource.Multimedia.Count);
            Assert.AreEqual(1, citWithSource.NoteStructures.Count);

            // Citation 1 - Note 0
            note = citWithSource.NoteStructures[0];
            Assert.AreEqual(1, note.Lines.Count);
            Assert.AreEqual("This is a second source citation in this record.", note.NoteReference.Lines[0]);

            // Citation 2
            Assert.IsTrue(indi.Citations[2] is CitationWithoutSource);
            var citWithoutSource = indi.Citations[2] as CitationWithoutSource;

            Assert.AreEqual(1, citWithoutSource.NoteStructures.Count);

            // Citation 2 - Note 0
            note = citWithoutSource.NoteStructures[0];
            Assert.AreEqual(1, note.NoteReference.Lines.Count);
            Assert.AreEqual(
                @"How does software handle embedded SOURCE records on import? Such source citations are common in old GEDCOM files. 
                More modern GEDCOM files should use source citations to SOURCE records.",
                note.NoteReference.Lines[0]);
        }

    }
}
