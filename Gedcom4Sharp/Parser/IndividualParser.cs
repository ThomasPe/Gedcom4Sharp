using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    public class IndividualParser : AbstractParser<Individual>
    {
        public IndividualParser(GedcomParser gedcomParser, StringTree stringTree, Individual loadInto) : base(gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if (Tag.NAME.Desc().Equals(ch.Tag))
                    {
                        var pn = new PersonalName();
                        _loadInto.Names.Add(pn);
                        new PersonalNameParser(_gedcomParser, ch, pn).Parse();
                    }
                    else if (Tag.SEX.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Sex = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.ADDRESS.Desc().Equals(ch.Tag))
                    {
                        new AddressParser(_gedcomParser, ch, _loadInto.Address).Parse();
                    }
                    else if (Tag.PHONE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.PhoneNumbers.Add(ParseStringWithCustomFacts(ch));
                    }
                    else if (Tag.WEB_ADDRESS.Desc().Equals(ch.Tag))
                    {
                        _loadInto.WwwUrls.Add(ParseStringWithCustomFacts(ch));
                        if (G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but WWW URL was specified for individual {_loadInto.Xref} on line {ch.LineNum}, which is a GEDCOM 5.5.1 feature. Data loaded but cannot be re-written unless GEDCOM version changes.");
                        }
                    }
                    else if (Tag.FAX.Desc().Equals(ch.Tag))
                    {
                        _loadInto.FaxNumbers.Add(ParseStringWithCustomFacts(ch));
                        if (G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but fax was specified for individual {_loadInto.Xref} on line {ch.LineNum}, which is a GEDCOM 5.5.1 feature. Data loaded but cannot be re-written unless GEDCOM version changes.");
                        }
                    }
                    else if (Tag.EMAIL.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Emails.Add(ParseStringWithCustomFacts(ch));
                        if (G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but email was specified for individual {_loadInto.Xref} on line {ch.LineNum}, which is a GEDCOM 5.5.1 feature. Data loaded but cannot be re-written unless GEDCOM version changes.");
                        }
                    }
                    else if (EnumExtensions.TryParseDescriptionToEnum(ch.Tag, out IndividualEventType i)){
                        var individualEvent = new IndividualEvent();
                        _loadInto.Events.Add(individualEvent);
                        new IndividualEventParser(_gedcomParser, ch, individualEvent).Parse();
                    }
                    // TODO IndividualParser Finish
                }
            }
        }
    }
}