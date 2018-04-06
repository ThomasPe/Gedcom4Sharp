using Gedcom4Sharp.Models.Gedcom.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class NoteRecord : AbstractElement, HasCitations, HasXref
    {
        public ChangeDate ChangeDate { get; set; }
        public List<AbstractCitation> Citations { get; set; }
        public string Xref { get; set; }
        public List<string> Lines { get; set; }
        public StringWithCustomFacts RecIdNumber { get; set; }
        public List<UserReference> UserReferences { get; set; }

        public NoteRecord(string xref)
        {
            Xref = xref;
        }
    }
}
