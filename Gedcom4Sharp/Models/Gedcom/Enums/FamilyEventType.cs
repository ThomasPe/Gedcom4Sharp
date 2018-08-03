using System.ComponentModel;

namespace Gedcom4Sharp.Models.Gedcom.Enums
{
    public enum FamilyEventType
    {
        /** Annulment */
        [Description("ANUL")]
        ANNULMENT,

        /** Census */
        [Description("CENS")]
        CENSUS,

        /** Divorce */
        [Description("DIV")]
        DIVORCE,

        /** Divorce filed */
        [Description("DIVF")]
        DIVORCE_FILED,

        /** Engagement */
        [Description("ENGA")]
        ENGAGEMENT,

        /** General event */
        [Description("EVEN")]
        EVENT,

        /** Marriage */
        [Description("MARR")]
        MARRIAGE,

        /** Marriage Banner */
        [Description("MARB")]
        MARRIAGE_BANNER,

        /** Marriage Contract */
        [Description("MARC")]
        MARRIAGE_CONTRACT,

        /** Marriage License */
        [Description("MARL")]
        MARRIAGE_LICENSE,

        /** Marriage Settlement */
        [Description("MARS")]
        MARRIAGE_SETTLEMENT
    }
}
