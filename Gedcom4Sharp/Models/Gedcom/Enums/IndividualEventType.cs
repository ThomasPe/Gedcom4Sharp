using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom.Enums
{
    /// TODO: needs support for shorthand parsing
    public enum IndividualEventType
    {
        /** Adoption */
        [Description("ADOP")]
        ADOPTION,

        /** Arrival */
        [Description("ARVL")]
        ARRIVAL,

        /** Baptism */
        [Description("BAPM")]
        BAPTISM,

        /** Bar Mitzvah */
        [Description("BARM")]
        BAR_MITZVAH,

        /** Bas Mitzvah */
        [Description("BASM")]
        BAS_MITZVAH,

        /** Birth */
        [Description("BIRT")]
        BIRTH,

        /** Blessing */
        [Description("BLES")]
        BLESSING,

        /** Burial */
        [Description("BURI")]
        BURIAL,

        /** Census */
        [Description("CENS")]
        CENSUS,

        /** Christening */
        [Description("CHR")]
        CHRISTENING,

        /** Christening as an adult */
        [Description("CHRA")]
        CHRISTENING_ADUL,

        /** Confirmation */
        [Description("CONF")]
        CONFIRMATION,

        /** Cremation */
        [Description("CREM")]
        CREMATION,

        /** Death */
        [Description("DEAT")]
        DEATH,

        /** Emigration */
        [Description("EMIG")]
        Emigration,

        /** Event */
        [Description("EVEN")]
        Event,

        /** First Communion */
        [Description("FCOM")]
        FIRST_COMMUNION,

        /** Graduation */
        [Description("GRAD")]
        GRADUATION,

        /** Immigration */
        [Description("IMMI")]
        IMMIGRATION,

        /** Naturalization */
        [Description("NATU")]
        NATURALIZATION,

        /** Ordination */
        [Description("ORDN")]
        ORDINATION,

        /** PROBATE */
        [Description("PROB")]
        PROBATE,

        /** Retirement */
        [Description("RETI")]
        RETIREMENT,

        /** Will */
        [Description("WILL")]
        WILL
    }
}
