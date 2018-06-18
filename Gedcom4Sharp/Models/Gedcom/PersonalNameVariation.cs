using Gedcom4Sharp.Models.Gedcom.Base;
using System.Collections.Generic;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class PersonalNameVariation : AbstractNameVariation
    {
        /// <summary>
        /// The citations for this object
        /// </summary>
        public List<AbstractCitation> Citations { get; set; } = new List<AbstractCitation>();

        /// <summary>
        /// The given (aka "Christian" or "first") names
        /// </summary>
        public StringWithCustomFacts GivenName { get; set; }

        /// <summary>
        /// Nickname
        /// </summary>
        public StringWithCustomFacts Nickname { get; set; }

        /// <summary>
        /// Notes about this object
        /// </summary>
        public List<NoteStructure> NoteStructures { get; set; }

        /// <summary>
        ///  The prefix for the name
        /// </summary>
        public StringWithCustomFacts Prefix { get; set; }

        /// <summary>
        /// The suffix
        /// </summary>
        public StringWithCustomFacts Suffix { get; set; }

        /// <summary>
        /// The surname (aka "family" or "last" name)
        /// </summary>
        public StringWithCustomFacts Surname { get; set; }

        /// <summary>
        /// Surname prefix
        /// </summary>
        public StringWithCustomFacts SurnamePrefix { get; set; }
    }
}