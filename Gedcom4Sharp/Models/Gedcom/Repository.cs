using Gedcom4Sharp.Models.Gedcom.Base;
using System.Collections.Generic;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class Repository : AbstractAddressableElement, HasXref
    {
        /// <summary>
        /// The xref for this submitter
        /// </summary>
        public string Xref { get; set; }

        /// <summary>
        /// The change date for this repository
        /// </summary>
        public ChangeDate ChangeDate { get; set; }

        /// <summary>
        /// The name of this repository
        /// </summary>
        public StringWithCustomFacts Name { get; set; }

        /// <summary>
        /// The record ID number
        /// </summary>
        public StringWithCustomFacts RecIdNumber { get; set; }

        /// <summary>
        /// The user references for this submitter
        /// </summary>
        public List<UserReference> UserReferences { get; set; }

    }
}