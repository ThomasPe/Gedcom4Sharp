using Gedcom4Sharp.Models.Gedcom.Base;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class HeaderSourceData : AbstractElement
    {
        /// <summary>
        /// Copyright information
        /// </summary>
        public StringWithCustomFacts Copyright { get; set; }

        /// <summary>
        /// The name of the source data. This field must be valued to pass validation, so the default value is "UNSPECIFIED".
        /// </summary>
        public StringWithCustomFacts Name { get; set; } = new StringWithCustomFacts("UNSPECIFIED");

        /// <summary>
        /// The publish date
        /// </summary>
        public StringWithCustomFacts PublishDate { get; set; }
    }
}