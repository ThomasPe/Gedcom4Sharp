using Gedcom4Sharp.Models.Gedcom.Base;
using Gedcom4Sharp.Models.Gedcom.Enums;
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
        public List<AbstractCitation> Citations { get; set; } = new List<AbstractCitation>();

        /// <summary>
        /// The xref for this submitter
        /// </summary>
        public string Xref { get; set; }

        /// <summary>
        /// Aliases for the current individual.
        /// </summary>
        public List<StringWithCustomFacts> Aliases { get; set; } = new List<StringWithCustomFacts>();

        /// <summary>
        /// A list of submitter(s) who are interested in the ancestry of this individual.
        /// </summary>
        public List<Submitter> AncestorInterest { get; set; } = new List<Submitter>();

        /// <summary>
        /// The Ancestral File Number of this individual.
        /// </summary>
        public StringWithCustomFacts AncestralFileNumber { get; set; }

        /// <summary>
        /// A list of associations to which this individual belongs/belonged.
        /// </summary>
        public List<Association> Associations { get; set; } = new List<Association>();

        /// <summary>
        /// A list of individual attributes about this individual.
        /// </summary>
        public List<IndividualAttribute> Attributes { get; set; } = new List<IndividualAttribute>();

        /// <summary>
        /// The change date for this individual
        /// </summary>
        public ChangeDate ChangeDate { get; set; }

        /// <summary>
        /// A list of submitters who are interested in the descendants of this individual.
        /// </summary>
        public List<Submitter> DescendantInterest { get; set; } = new List<Submitter>();

        /// <summary>
        /// A list of events for this individual.
        /// </summary>
        public List<IndividualEvent> Events { get; set; } = new List<IndividualEvent>();

        /// <summary>
        /// A list of families to which this individual was a child
        /// </summary>
        public List<FamilyChild> FamiliesWhereChild { get; set; } = new List<FamilyChild>();
        /// <summary>
        /// A list of families to which this individual was either the husband or wife
        /// </summary>
        public List<FamilySpouse> FamiliesWhereSpouse { get; set; } = new List<FamilySpouse>();
        /// <summary>
        /// A list of LDS individual ordinances for this individual
        /// </summary>
        public List<LdsIndividualOrdinance> LdsIndividualOrdinances { get; set; } = new List<LdsIndividualOrdinance>();

        /// <summary>
        /// Multimedia links for this source citation
        /// </summary>
        public List<MultimediaReference> Multimedia { get; set; } = new List<MultimediaReference>();

        /// <summary>
        /// A list of names for this individual
        /// </summary>
        public List<PersonalName> Names { get; set; } = new List<PersonalName>();

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
        public List<Submitter> Submitters { get; set; } = new List<Submitter>();

        /// <summary>
        /// The user references for this submitter
        /// </summary>
        public List<UserReference> UserReferences { get; set; } = new List<UserReference>();


        /// <summary>
        /// Get a list of attributes of the supplied type for this individual. For example, calling this method passing
        /// IndividualAttributeType.OCCUPATION will return a list of all the occupations recorded for this individual.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<IndividualAttribute> GetAttributesOfType(IndividualAttributeType type)
        {
            var result = new List<IndividualAttribute>();
            foreach (IndividualAttribute ir in Attributes)
            {
                if (ir.Type == type)
                {
                    result.Add(ir);
                }
            }
            return result;
        }
    }
}
