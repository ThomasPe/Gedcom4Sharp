using Gedcom4Sharp.Models.Gedcom.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class CustomFact : AbstractNotesElement, HasCitations, HasXref
    {
        public string Xref { get; set; }
        public List<AbstractCitation> Citations { get; set; } = new List<AbstractCitation>();
        public StringWithCustomFacts Date { get; set; }
        public StringWithCustomFacts Description { get; set; }
        public Place Place { get; set; }
        public string Tag { get; }
        public StringWithCustomFacts Type { get; set; }
        public ChangeDate ChangeDate { get; set; }

        public CustomFact(string tag)
        {
            Tag = tag;
        }
    }
}
