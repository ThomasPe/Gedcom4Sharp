using Gedcom4Sharp.Models.Gedcom.Base;
using Gedcom4Sharp.Models.Gedcom.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class Individual : AbstractAddressableElement, HasCitations, HasXref
    {
        public List<AbstractCitation> Citations { get; set; }
        public string Xref { get; set; }
        public List<StringWithCustomFacts> Aliases { get; set; }
        public List<Submitter> AncestorInterest { get; set; }
        public StringWithCustomFacts AncestralFileNumber { get; set; }
        public List<Association> Associations { get; set; }
        public List<IndividualAttribute> Attributes { get; set; }
        public ChangeDate ChangeDate { get; set; }
        public List<Submitter> DescendantInterest { get; set; }
        public List<IndividualEvent> Events { get; set; }
        /// <summary>
        /// A list of families to which this individual was a child
        /// </summary>
        public List<FamilyChild> FamiliesWhereChild { get; set; }

        // TBC

    }
}
