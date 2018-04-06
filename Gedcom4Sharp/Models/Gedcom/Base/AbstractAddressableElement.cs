using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom.Base
{
    public abstract class AbstractAddressableElement : AbstractNotesElement, HasAddresses
    {
        public Address Address { get; set; }
        public List<StringWithCustomFacts> Emails { get; set; }
        public List<StringWithCustomFacts> FaxNumbers { get; set; }
        public List<StringWithCustomFacts> PhoneNumbers { get; set; }
        public List<StringWithCustomFacts> WwwUrls { get; set; }
    }
}
