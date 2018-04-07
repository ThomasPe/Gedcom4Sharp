using Gedcom4Sharp.Models.Gedcom.Base;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class CharacterSet : AbstractElement
    {
        public StringWithCustomFacts CharacterSetName { get; set; }
        public StringWithCustomFacts VersionNum { get; set; }
    }
}