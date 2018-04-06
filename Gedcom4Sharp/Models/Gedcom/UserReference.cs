using Gedcom4Sharp.Models.Gedcom.Base;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class UserReference : AbstractElement
    {
        public StringWithCustomFacts ReferenceNum { get; set; }
        public StringWithCustomFacts Type { get; set; }
    }
}