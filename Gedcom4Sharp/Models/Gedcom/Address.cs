using Gedcom4Sharp.Models.Gedcom.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom
{
    public class Address : AbstractElement
    {
        public StringWithCustomFacts Addr1 { get; set; }
        public StringWithCustomFacts Addr2 { get; set; }
        public StringWithCustomFacts Addr3 { get; set; }
        public StringWithCustomFacts City { get; set; }
        public StringWithCustomFacts Country { get; set; }
        public List<string> Lines { get; set; } = new List<string>();
        public StringWithCustomFacts PostalCode { get; set; }
        public StringWithCustomFacts StateProvince { get; set; }
    }
}
