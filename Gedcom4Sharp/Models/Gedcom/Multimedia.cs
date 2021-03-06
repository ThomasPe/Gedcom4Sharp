﻿using System.Collections.Generic;
using Gedcom4Sharp.Models.Gedcom.Base;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class Multimedia : AbstractNotesElement, HasCitations, HasXref
    {
        public List<AbstractCitation> Citations { get; set; } = new List<AbstractCitation>();
        public string Xref { get; set; }
        public List<string> Blob { get; set; } = new List<string>();
        public ChangeDate ChangeDate { get; set; }
        public MultimediaReference ContinuedObject { get; set; }
        public StringWithCustomFacts EmbeddedMediaFormat { get; set; }
        public StringWithCustomFacts EmbeddedTitle { get; set; }
        public List<FileReference> FileReferences { get; set; } = new List<FileReference>();
        public StringWithCustomFacts RecIdNumber { get; set; }
        public List<UserReference> UserReferences { get; set; } = new List<UserReference>();
    }
}