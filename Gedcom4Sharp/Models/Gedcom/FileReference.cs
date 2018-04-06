using Gedcom4Sharp.Models.Gedcom.Base;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class FileReference : AbstractElement
    {
        public StringWithCustomFacts Format { get; set; }
        public StringWithCustomFacts MediaType { get; set; }
        public StringWithCustomFacts ReferenceToFile { get; set; }
        public StringWithCustomFacts Title { get; set; }
    }
}