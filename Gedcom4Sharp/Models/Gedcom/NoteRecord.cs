using Gedcom4Sharp.Models.Gedcom.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class NoteRecord : AbstractElement, HasCitations, HasXref
    {
        public ChangeDate ChangeDate { get; set; }
        public List<AbstractCitation> Citations { get; set; } = new List<AbstractCitation>();
        public string Xref { get; set; }
        public List<string> Lines { get; set; } = new List<string>();
        public StringWithCustomFacts RecIdNumber { get; set; }
        public List<UserReference> UserReferences { get; set; } = new List<UserReference>();

        public NoteRecord(string xref)
        {
            Xref = xref;
        }
    }
}
