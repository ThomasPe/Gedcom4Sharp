using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom.Enums
{
    /// <summary>
    /// The types of attributes that can be recorded for an individual. This enum covers the valid tags for an individual attribute.
    /// Corresponds to ATTRIBUTE_TYPE in the GEDCOM spec.
    /// </summary>
    public enum IndividualAttributeType
    {
        /** Caste name/type */
        [Description("CAST")]
        CASTE_NAME,

        /** Count of children */
        [Description("NCHI")]
        COUNT_OF_CHILDREN,

        /** Count of marriages */
        [Description("NMR")]
        COUNT_OF_MARRIAGES,

        /** Generic fact. New for GEDCOM 5.5.1. */
        [Description("FACT")]
        FACT,

        /** National ID number */
        [Description("IDNO")]
        NATIONAL_ID_NUMBER,

        /** National or tribal origin */
        [Description("NATI")]
        NATIONAL_OR_TRIBAL_ORIGIN,

        /** Nobility type title */
        [Description("TITL")]
        NOBILITY_TYPE_TITLE,

        /** Occupation */
        [Description("OCCU")]
        OCCUPATION,

        /** Physical description */
        [Description("DSCR")]
        PHYSICAL_DESCRIPTION,

        /** Possessions */
        [Description("PROP")]
        POSSESSIONS,

        /** Religous affiliation */
        [Description("RELI")]
        RELIGIOUS_AFFILIATION,

        /** Residence */
        [Description("RESI")]
        RESIDENCE,

        /** Scholastic achievement */
        [Description("EDUC")]
        SCHOLASTIC_ACHIEVEMENT,

        /** Social Security Number */
        [Description("SSN")]
        SOCIAL_SECURITY_NUMBER,
    }
}
