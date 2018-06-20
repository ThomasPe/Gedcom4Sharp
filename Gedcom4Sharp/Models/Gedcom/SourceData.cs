using Gedcom4Sharp.Models.Gedcom.Base;
using System.Collections.Generic;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class SourceData : AbstractNotesElement
    {
        public List<EventRecorded> EventsRecorded { get; set; } = new List<EventRecorded>();
        public StringWithCustomFacts RespAgency { get; set; }
    }
}