using Gedcom4Sharp.Models.Gedcom.Base;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class FamilyChild : AbstractNotesElement
    {
        public StringWithCustomFacts AdoptedBy { get; set; }
        public Family Family { get; set; }
        public StringWithCustomFacts Pedigree { get; set; }
        public StringWithCustomFacts Status { get; set; }
    }
}