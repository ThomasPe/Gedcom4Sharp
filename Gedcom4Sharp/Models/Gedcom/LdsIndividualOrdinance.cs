using Gedcom4Sharp.Models.Gedcom.Base;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class LdsIndividualOrdinance : AbstractLdsOrdinance
    {
        /// <summary>
        /// The family in which the individual was a child - used for SLGC types
        /// </summary>
        public FamilyChild FamilyWhereChild { get; set; }
        public LdsIndividualOrdinanceType Type { get; set; }
        /// <summary>
        /// Allows for a Y or null to be processed after the type. Not strictly part of the GEDCOM, but allows for flexibility
        /// </summary>
        public string Ynull { get; set; }
    }
}