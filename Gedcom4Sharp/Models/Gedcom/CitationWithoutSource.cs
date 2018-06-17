using Gedcom4Sharp.Models.Gedcom.Base;
using System.Collections.Generic;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class CitationWithoutSource : AbstractCitation
    {
        public List<string> Description { get; set; } = new List<string>();
        public List<string> TextFromSource { get; set; } = new List<string>();
    }
}