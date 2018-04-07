using Gedcom4Sharp.Models.Gedcom.Base;

namespace Gedcom4Sharp.Models.Gedcom
{
    /// <summary>
    /// A source system. Corresponds to the SOUR structure in HEADER in the GEDCOM file.
    /// If instantiating one of these programmatically rather than through parsing an existing GEDCOM file, you will probably want to
    /// change the value of the SourceSystem field.
    /// </summary>
    public class SourceSystem : AbstractElement
    {
        /// <summary>
        /// The corporation that owns this source system
        /// </summary>
        public Corporation Corporation { get; set; }

        /// <summary>
        /// The product name for this source system
        /// </summary>
        public StringWithCustomFacts ProductName { get; set; }

        /// <summary>
        /// Header source data for this source system.
        /// </summary>
        public HeaderSourceData SourceData { get; set; }

        /// <summary>
        /// The system ID for this source system. This field must be valued to pass validation, so the default value is "UNSPECIFIED".
        /// </summary>
        public string SystemId { get; set; } = "UNSPECIFIED";

        /// <summary>
        /// The version number of this source system
        /// </summary>
        public StringWithCustomFacts VersionNum { get; set; }
    }
}