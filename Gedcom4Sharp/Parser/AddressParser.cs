using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;
using System.Linq;

namespace Gedcom4Sharp.Parser
{
    public class AddressParser : AbstractParser<Address>
    {
        public AddressParser(GedcomParser gedcomParser, StringTree stringTree, Address loadInto) : base(gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            if(_stringTree.Value != null)
            {
                _loadInto.Lines.Add(_stringTree.Value);
            }
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if (Tag.ADDRESS_1.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Addr1 = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.ADDRESS_2.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Addr2 = ParseStringWithCustomFacts(ch);
                    } 
                    else if (Tag.ADDRESS_3.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Addr3 = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.CITY.Desc().Equals(ch.Tag))
                    {
                        _loadInto.City = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.STATE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.StateProvince = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.POSTAL_CODE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.PostalCode = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.COUNTRY.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Country = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.CONCATENATION.Desc().Equals(ch.Tag))
                    {
                        if (!_loadInto.Lines.Any())
                        {
                            _loadInto.Lines.Add(ch.Value);
                        } 
                        else
                        {
                            var last = _loadInto.Lines.Count - 1;
                            _loadInto.Lines[last] = _loadInto.Lines[last] + ch.Value;
                        }
                    } 
                    else if (Tag.CONTINUATION.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Lines.Add(string.IsNullOrEmpty(ch.Value) ? "" : ch.Value);
                    } 
                    else
                    {
                        UnknownTag(ch, _loadInto);
                    }
                }
            }
        }
    }
}