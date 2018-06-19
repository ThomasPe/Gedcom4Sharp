using Gedcom4Sharp.Models.Gedcom.Base;
using Gedcom4Sharp.Models.Gedcom.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class Individual : AbstractAddressableElement, HasCitations, HasXref
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
        /// Aliases for the current individual.
        /// </summary>
        public List<StringWithCustomFacts> Aliases { get; set; }

        /// <summary>
        /// A list of submitter(s) who are interested in the ancestry of this individual.
        /// </summary>
        public List<Submitter> AncestorInterest { get; set; }

        /// <summary>
        /// The Ancestral File Number of this individual.
        /// </summary>
        public StringWithCustomFacts AncestralFileNumber { get; set; }

        /// <summary>
        /// A list of associations to which this individual belongs/belonged.
        /// </summary>
        public List<Association> Associations { get; set; }

        /// <summary>
        /// A list of individual attributes about this individual.
        /// </summary>
        public List<IndividualAttribute> Attributes { get; set; }

        /// <summary>
        /// The change date for this individual
        /// </summary>
        public ChangeDate ChangeDate { get; set; }

        /// <summary>
        /// A list of submitters who are interested in the descendants of this individual.
        /// </summary>
        public List<Submitter> DescendantInterest { get; set; }

        /// <summary>
        /// A list of events for this individual.
        /// </summary>
        public List<IndividualEvent> Events { get; set; }

        /// <summary>
        /// A list of families to which this individual was a child
        /// </summary>
        public List<FamilyChild> FamiliesWhereChild { get; set; }
        /// <summary>
        /// A list of families to which this individual was either the husband or wife
        /// </summary>
        public List<FamilySpouse> FamiliesWhereSpouse { get; set; }
        /// <summary>
        /// A list of LDS individual ordinances for this individual
        /// </summary>
        public List<LdsIndividualOrdinance> LdsIndividualOrdinances { get; set; }

        /// <summary>
        /// Multimedia links for this source citation
        /// </summary>
        public List<MultimediaReference> Multimedia { get; set; }

        /// <summary>
        /// A list of names for this individual
        /// </summary>
        public List<PersonalName> Names { get; set; }

        /// <summary>
        /// The permanent record file number for this individual
        /// </summary>
        public StringWithCustomFacts PermanentRecFileNumber { get; set; }

        /// <summary>
        /// The record ID number
        /// </summary>
        public StringWithCustomFacts RecIdNumber { get; set; }

        /// <summary>
        /// The restriction notice (if any) for this individual
        /// </summary>
        public StringWithCustomFacts RestrictionNotice { get; set; }

        /// <summary>
        /// The sex of this individual
        /// </summary>
        public StringWithCustomFacts Sex { get; set; }

        /// <summary>
        /// A list of submitter(s) of this individual
        /// </summary>
        public List<Submitter> Submitters { get; set; }

        /// <summary>
        /// The user references for this submitter
        /// </summary>
        public List<UserReference> UserReferences { get; set; }
    }
}
