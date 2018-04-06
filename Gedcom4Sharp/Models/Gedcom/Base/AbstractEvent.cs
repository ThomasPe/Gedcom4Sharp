using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom.Base
{
    public abstract class AbstractEvent : AbstractAddressableElement, HasCitations
    {
        public List<AbstractCitation> Citations { get; set; }
        public StringWithCustomFacts Age { get; set; }
        public StringWithCustomFacts Cause { get; set; }
        public StringWithCustomFacts Date { get; set; }
        public StringWithCustomFacts Description { get; set; }
        public List<MultimediaReference> Multimedia { get; set; }
        public Place Place { get; set; }
        public StringWithCustomFacts ReligiousAffiliation { get; set; }
        public StringWithCustomFacts RespAgency { get; set; }
        public StringWithCustomFacts RestrictionNotice { get; set; }
        public StringWithCustomFacts SubType { get; set; }
        public string Ynull { get; set; }
    }
}
