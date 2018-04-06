using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom.Base
{
    public interface HasAddresses
    {
        Address Address { get; set; }
        List<StringWithCustomFacts> Emails { get; set; }
        List<StringWithCustomFacts> FaxNumbers { get; set; }
        List<StringWithCustomFacts> PhoneNumbers { get; set; }
        List<StringWithCustomFacts> WwwUrls { get; set; }
    }
}
