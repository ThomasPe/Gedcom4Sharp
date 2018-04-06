using Gedcom4Sharp.Models.Gedcom.Base;
using System.Collections.Generic;

namespace Gedcom4Sharp.Models.Gedcom.Models
{
    public class Association : AbstractNotesElement
    {
        public StringWithCustomFacts AssociatedEntityType { get; set; }
        public string AssociatedEntityXref { get; set; }
        public List<AbstractCitation> Citations { get; set; }
        public StringWithCustomFacts Relationship { get; set; }
    }
}