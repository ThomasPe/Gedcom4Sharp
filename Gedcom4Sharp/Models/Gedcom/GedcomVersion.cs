using Gedcom4Sharp.Models.Gedcom.Base;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class GedcomVersion : AbstractElement
    {
        /// <summary>
        /// The form
        /// </summary>
        public StringWithCustomFacts GedcomForm { get; set; }

        /// <summary>
        /// The version number for this GEDCOM
        /// </summary>
        public StringWithCustomFacts VersionNumber { get; set; }
    }
}