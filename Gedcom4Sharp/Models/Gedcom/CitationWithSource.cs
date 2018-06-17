using Gedcom4Sharp.Models.Gedcom.Base;
using Gedcom4Sharp.Parser;
using System.Collections.Generic;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class CitationWithSource : AbstractCitation
    {
        /// <summary>
        /// A list of citation data entries
        /// </summary>
        public List<CitationData> Data { get; set; } = new List<CitationData>();

        /// <summary>
        /// The type of event or attribute cited from. Will be the tag from one of the the following three enum types:
        ///FamilyEventType, IndividualEventType, IndividualAttributeType
        /// </summary>
        public StringWithCustomFacts EventCited { get; set; }

        /// <summary>
        /// The role in the event cited
        /// </summary>
        public StringWithCustomFacts RoleInEvent { get; set; }

        /// <summary>
        /// A reference to the cited source
        /// </summary>
        public Source Source { get; set; }

        /// <summary>
        /// Where within the source is the information being cited
        /// </summary>
        public StringWithCustomFacts WhereInSource { get; set; }
    }
}