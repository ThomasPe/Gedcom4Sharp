using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    public class CorporationParser : AbstractParser<Corporation>
    {

        public CorporationParser(GedcomParser gedcomParser, StringTree stringTree, Corporation loadInto) : base(gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            _loadInto.BusinessName = _stringTree.Value;
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if (Tag.ADDRESS.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Address = new Address();
                        new AddressParser(_gedcomParser, ch, _loadInto.Address).Parse();
                    }
                    else if(Tag.PHONE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.PhoneNumbers.Add(ParseStringWithCustomFacts(ch));
                    }
                    else if (Tag.WEB_ADDRESS.Desc().Equals(ch.Tag))
                    {
                        _loadInto.WwwUrls.Add(ParseStringWithCustomFacts(ch));
                        if (G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but WWW URL was specified for the corporation in the source system on line " +
                                $"{ch.LineNum} , which is a GEDCOM 5.5.1 feature. " +
                                $"Data loaded but cannot be re-written unless GEDCOM version changes.");

                        }
                    }
                    else if (Tag.FAX.Desc().Equals(ch.Tag))
                    {
                        _loadInto.FaxNumbers.Add(ParseStringWithCustomFacts(ch));
                        if (G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but fax number was specified for the corporation in the source system on line " +
                                $"{ch.LineNum} , which is a GEDCOM 5.5.1 feature. " +
                                $"Data loaded but cannot be re-written unless GEDCOM version changes.");

                        }
                    }
                    else if (Tag.EMAIL.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Emails.Add(ParseStringWithCustomFacts(ch));
                        if (G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but emails was specified for the corporation in the source system on line " +
                                $"{ch.LineNum} , which is a GEDCOM 5.5.1 feature. " +
                                $"Data loaded but cannot be re-written unless GEDCOM version changes.");

                        }
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