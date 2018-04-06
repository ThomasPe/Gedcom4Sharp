using Gedcom4Sharp.Models.Gedcom.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class Family : AbstractNotesElement, HasCitations, HasXref
    {
        public List<AbstractCitation> Citations { get; set; }
        public string Xref { get; set; }
        public StringWithCustomFacts AutomatedRecordId { get; set; }
        public ChangeDate ChangeDate { get; set; }
        public List<IndividualReference> Children { get; set; }

    }
}
