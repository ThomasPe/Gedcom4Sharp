using Gedcom4Sharp.Models.Gedcom.Base;
using System.Collections.Generic;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class Source : AbstractNotesElement, HasXref
    {
        /// <summary>
        /// The xref for this submitter
        /// </summary>
        public string Xref { get; set; }

        /// <summary>
        /// The change date for this source
        /// </summary>
        public ChangeDate ChangeDate { get; set; }

        /// <summary>
        /// Source data
        /// </summary>
        public SourceData Data { get; set; }

        /// <summary>
        /// Multimedia links for this source citation
        /// </summary>
        public List<MultimediaReference> Multimedia { get; set; } = new List<MultimediaReference>();

        /// <summary>
        /// The originators/authors
        /// </summary>
        public MultiStringWithCustomFacts OriginatorsAuthors { get; set; }

        /// <summary>
        /// Publication facts on this source
        /// </summary>
        public MultiStringWithCustomFacts PublicationFacts { get; set; }

        /// <summary>
        /// The record ID number
        /// </summary>
        public StringWithCustomFacts RecIdNumber { get; set; }

        /// <summary>
        /// A repository Citation
        /// </summary>
        public RepositoryCitation RepositoryCitation { get; set; }

        /// <summary>
        /// Who filed the source
        /// </summary>
        public StringWithCustomFacts SourceFiledBy { get; set; }

        /// <summary>
        /// Text from the source
        /// </summary>
        public MultiStringWithCustomFacts SourceText { get; set; }

        /// <summary>
        /// The title text
        /// </summary>
        public MultiStringWithCustomFacts Title { get; set; }

        /// <summary>
        /// The user references for this submitter
        /// </summary>
        public List<UserReference> UserReferences { get; set; } = new List<UserReference>();
    }
}