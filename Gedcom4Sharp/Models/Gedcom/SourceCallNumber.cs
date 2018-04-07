using Gedcom4Sharp.Models.Gedcom.Base;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class SourceCallNumber : AbstractElement
    {
        public StringWithCustomFacts CallNumber { get; set; }
        public StringWithCustomFacts MediaType { get; set; }
    }
}