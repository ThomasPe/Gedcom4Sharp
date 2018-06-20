using Gedcom4Sharp.Models.Gedcom.Base;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class EventRecorded : AbstractElement
    {
        /// <summary>
        /// The date period covered in the source
        /// </summary>
        public StringWithCustomFacts DatePeriod { get; set; }

        /// <summary>
        /// The event types recorded in the source
        /// </summary>
        public StringWithCustomFacts EventType { get; set; }

        /// <summary>
        /// he jurisdiction of the source. Corresponds to SOURCE_JURISDICTION_PLACE in the GEDCOM spec.
        /// </summary>
        public StringWithCustomFacts Jurisdiction { get; set; }
    }
}