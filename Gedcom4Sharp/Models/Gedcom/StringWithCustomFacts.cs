using Gedcom4Sharp.Models.Gedcom.Base;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class StringWithCustomFacts : AbstractElement
    {
        public string Value { get; set; }

        public StringWithCustomFacts()
        {

        }

        public StringWithCustomFacts(string value)
        {
            Value = value;
        }
    }
}
