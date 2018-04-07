using Gedcom4Sharp.Models.Gedcom.Base;
using System.Collections.Generic;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class Family : AbstractNotesElement, HasCitations, HasXref
    {
        /// <summary>
        /// The citations for this object
        /// </summary>
        public List<AbstractCitation> Citations { get; set; }

        /// <summary>
        /// The xref for this submitter
        /// </summary>
        public string Xref { get; set; }

        /// <summary>
        /// The automated record ID number
        /// </summary>
        public StringWithCustomFacts AutomatedRecordId { get; set; }

        /// <summary>
        /// The change date information for this family record
        /// </summary>
        public ChangeDate ChangeDate { get; set; }

        /// <summary>
        /// A list of the children in the family
        /// </summary>
        public List<IndividualReference> Children { get; set; }

        /// <summary>
        /// All the family events
        /// </summary>
        public List<FamilyEvent> Events { get; set; }

        /// <summary>
        /// The husband in the family
        /// </summary>
        public IndividualReference Husband { get; set; }

        /// <summary>
        /// The LDS Spouse Sealings for this family
        /// </summary>
        public List<LdsSpouseSealing> LdsSpouseSealings { get; set; }

        /// <summary>
        /// Multimedia links for this source citation
        /// </summary>
        public List<MultimediaReference> Multimedia { get; set; }

        /// <summary>
        /// The number of children
        /// </summary>
        public StringWithCustomFacts NumChildren { get; set; }

        /// <summary>
        /// The permanent record file number
        /// </summary>
        public StringWithCustomFacts RecFileNumber { get; set; }

        /// <summary>
        /// A notification that this record is in some way restricted. New for GEDCOM 5.5.1. Values are supposed to be "confidential",
        /// "locked", or "privacy" but this implementation allows any value.
        /// </summary>
        public StringWithCustomFacts RestrictionNotice { get; set; }

        /// <summary>
        /// A list of the submitters for this family
        /// </summary>
        public List<SubmitterReference> Submitters { get; set; }

        /// <summary>
        /// The user references for this submitter
        /// </summary>
        public List<UserReference> UserReferences { get; set; }

        /// <summary>
        /// The wife in the family
        /// </summary>
        public IndividualReference Wife { get; set; }
    }
}
