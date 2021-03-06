﻿using Gedcom4Sharp.Models.Gedcom.Base;
using System.Collections.Generic;

namespace Gedcom4Sharp.Models.Gedcom
{
    /// <summary>
    ///  Main (root) class for an entire GEDCOM file.
    /// </summary>
    public class Gedcom : AbstractElement
    {
        /// <summary>
        /// All the families in the GEDCOM file. The map is keyed on family cross-reference numbers, and the families themselves are in the value set.
        /// </summary>
        public Dictionary<string, Family> Families { get; set; } = new Dictionary<string, Family>();

        /// <summary>
        /// Header information about the GEDCOM
        /// </summary>
        public Header Header { get; set; }

        /// <summary>
        /// All the individuals in the GEDCOM file. The map is keyed on the individual cross-reference numbers and the individuals themselves are in the value set.
        /// </summary>
        public Dictionary<string, Individual> Individuals { get; set; } = new Dictionary<string, Individual>();

        /// <summary>
        /// All the multimedia items in the GEDCOM file. The map is keyed by the multimedia cross-reference numbers, and the
        /// multimedia items themselves(well, the metadata about them for 5.5.1) are in the value set.Remember, GEDCOM 5.5.1 multimedia
        /// is not embedded in the GEDCOM, but the GEDCOM contains metadata about the multimedia.
        /// </summary>
        public Dictionary<string, Multimedia> Multimedia { get; set; } = new Dictionary<string, Multimedia>();

        /// <summary>
        /// Notes. The map is keyed with cross-reference numbers and the notes themselves are the values.
        /// </summary>
        public Dictionary<string, NoteRecord> Notes { get; set; } = new Dictionary<string, NoteRecord>();

        /// <summary>
        /// A map of all the source repositories in the GEDCOM file. The map is keyed on the repository cross-reference numbers, and the
        /// repositories themselves are in the value set.
        /// </summary>
        public Dictionary<string, Repository> Repositories { get; set; } = new Dictionary<string, Repository>();

        /// <summary>
        /// A map of all the sources in the GEDCOM file. The map is keyed on source cross-reference numbers, and the sources themselves
        /// are in the value set.
        /// </summary>
        public Dictionary<string, Source> Sources { get; set; } = new Dictionary<string, Source>();

        /// <summary>
        /// Information about the GEDCOM submission. There is only one and it is required, so the xref ID has a default.
        /// </summary>
        public Submission Submission { get; set; }

        /// <summary>
        /// A map of the submitters in the GEDCOM file. The map is keyed on submitter cross-reference numbers, and the submitters
        /// themselves are in the value set
        /// </summary>
        public Dictionary<string, Submitter> Submitters { get; set; } = new Dictionary<string, Submitter>();

        /// <summary>
        /// The trailer of the file
        /// </summary>
        public Trailer Trailer { get; set; }
    }
}
