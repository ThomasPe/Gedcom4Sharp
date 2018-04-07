using Gedcom4Sharp.Models.Gedcom.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class Header : AbstractNotesElement
    {
        /// <summary>
        /// The character set in use in the GEDCOM file
        /// </summary>
        public CharacterSet CharacterSet { get; set; }

        /// <summary>
        /// Copyright information for the GEDCOM file.
        /// </summary>
        public List<string> CopyrightData { get; set; }

        /// <summary>
        /// The date of the GEDCOM file
        /// </summary>
        public StringWithCustomFacts Date { get; set; }

        /// <summary>
        /// The destination system for the GEDCOM file.
        /// </summary>
        public StringWithCustomFacts DestinationSystem { get; set; }

        /// <summary>
        /// The filename for the GEDCOM file
        /// </summary>
        public StringWithCustomFacts FileName { get; set; }

        /// <summary>
        /// The version information for the GEDCOM file
        /// </summary>
        public GedcomVersion GedcomVersion { get; set; }

        /// <summary>
        /// The language for the file
        /// </summary>
        public StringWithCustomFacts Language { get; set; }

        /// <summary>
        /// The place structure for the file
        /// </summary>
        public StringWithCustomFacts PlaceHierarchy { get; set; }

        /// <summary>
        /// The source system for the GEDCOM file
        /// </summary>
        public SourceSystem SourceSystem { get; set; }

        /// <summary>
        /// Information about the file submissionReference
        /// </summary>
        public SubmissionReference SubmissionReference { get; set; }

        /// <summary>
        /// Information about the submitter of the file
        /// </summary>
        public SubmitterReference SubmitterReference { get; set; }
    }
}
