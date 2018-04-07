using Gedcom4Sharp.Models.Gedcom.Base;
using Gedcom4Sharp.Models.Gedcom.Enums;

namespace Gedcom4Sharp.Models.Gedcom
{
    /// <summary>
    /// Represents a family event. 
    /// Corresponds to the FAMILY_EVENT_STRUCTURE 
    /// from the GEDCOM standard along with the 
    /// two child elements of the wife and husband ages.
    /// </summary>
    public class FamilyEvent : AbstractEvent
    {
        /// <summary>
        /// Age of husband at time of event
        /// </summary>
        public StringWithCustomFacts HusbandAge { get; set; }

        /// <summary>
        /// The type of event. See FAMILY_EVENT_STRUCTURE in the GEDCOM standard for more info.
        /// </summary>
        public FamilyEventType Type { get; set; }

        /// <summary>
        /// Age of wife at time of event
        /// </summary>
        public StringWithCustomFacts WifeAge { get; set; }
    }
}