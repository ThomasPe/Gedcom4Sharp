using Gedcom4Sharp.Models.Gedcom.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class Submitter : AbstractAddressableElement, HasXref
    {
        public string Xref { get; set; }
        public ChangeDate ChangeDate { get; set; }
        public List<StringWithCustomFacts> LanguagePref { get; set; } = new List<StringWithCustomFacts>();
        public List<MultimediaReference> Multimedia { get; set; } = new List<MultimediaReference>();
        public StringWithCustomFacts Name { get; set; } = new StringWithCustomFacts();
        public StringWithCustomFacts RecIdNumber { get; set; } = new StringWithCustomFacts();
        public StringWithCustomFacts RegFileNumber { get; set; } = new StringWithCustomFacts();
        public List<UserReference> UserReferences { get; set; } = new List<UserReference>();

        public Submitter()
        {
            // Default constructor
        }
        public Submitter(string xref, string submitterName)
        {
            Xref = xref;
            Name = new StringWithCustomFacts { Value = submitterName };
        }
    }
}
