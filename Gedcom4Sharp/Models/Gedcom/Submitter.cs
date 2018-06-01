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
        public List<StringWithCustomFacts> LanguagePref { get; set; }
        public List<MultimediaReference> Multimedia { get; set; }
        public StringWithCustomFacts Name { get; set; }
        public StringWithCustomFacts RecIdNumber { get; set; }
        public StringWithCustomFacts RegFileNumber { get; set; }
        public List<UserReference> UserReferences { get; set; }

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
