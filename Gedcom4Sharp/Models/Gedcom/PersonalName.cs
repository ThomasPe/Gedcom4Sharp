using System.Collections.Generic;
using Gedcom4Sharp.Models.Gedcom.Base;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class PersonalName : AbstractNotesElement, HasCitations
    {
        public List<AbstractCitation> Citations { get; set; }
        /// <summary>
        /// The name in basic, unbroken-down format
        /// </summary>
        public string Basic { get; set; }
        public StringWithCustomFacts GivenName { get; set; }
        public StringWithCustomFacts Nickname { get; set; }
        public List<PersonalNameVariation> Phonetic { get; set; }
        public StringWithCustomFacts Prefix { get; set; }
        public List<PersonalNameVariation> Romanized { get; set; }
        public StringWithCustomFacts Suffix { get; set; }
        public StringWithCustomFacts Surname { get; set; }
        public StringWithCustomFacts SurnamePrefix { get; set; }
        public StringWithCustomFacts Type { get; set; }
    }
}