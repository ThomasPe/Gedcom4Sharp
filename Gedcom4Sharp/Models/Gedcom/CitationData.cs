using Gedcom4Sharp.Models.Gedcom.Base;
using System.Collections.Generic;

namespace Gedcom4Sharp.Models.Gedcom
{
    /// <summary>
    /// A class for source citation data.
    /// </summary>
    public class CitationData : AbstractElement
    {
        public StringWithCustomFacts EntryDate { get; set; }
        public List<MultiStringWithCustomFacts> SourceText { get; set; } = new List<MultiStringWithCustomFacts>();
    }
}