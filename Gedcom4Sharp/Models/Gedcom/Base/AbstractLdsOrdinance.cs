using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom.Base
{
    public class AbstractLdsOrdinance : AbstractNotesElement, HasCitations
    {
        public List<AbstractCitation> Citations { get; set; } = new List<AbstractCitation>();
        public StringWithCustomFacts Date { get; set; }
        public StringWithCustomFacts Place { get; set; }
        public StringWithCustomFacts Status { get; set; }
        public StringWithCustomFacts Temple { get; set; }
    }
}
