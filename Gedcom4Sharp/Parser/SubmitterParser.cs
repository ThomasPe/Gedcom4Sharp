using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    public class SubmitterParser : AbstractParser<Submitter>
    {
        public SubmitterParser(GedcomParser gedcomParser, StringTree stringTree, Submitter loadInto) : base(gedcomParser, stringTree, loadInto)
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
                        _loadInto.Name = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.ADDRESS.Desc().Equals(ch.Tag))
                    {
                        var address = new Address();
                        _loadInto.Address = address;
                        new AddressParser(_gedcomParser, ch, _loadInto.Address);
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
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but WWW URL was specified on submitter on line {ch.LineNum} which is a GEDCOM 5.5.1 feature. Data loaded but cannot be re-written unless GEDCOM version changes.");
                        }
                    }
                    else if (Tag.FAX.Desc().Equals(ch.Tag))
                    {
                        _loadInto.FaxNumbers.Add(ParseStringWithCustomFacts(ch));
                        if (G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but fax number was specified on submitter on line {ch.LineNum} which is a GEDCOM 5.5.1 feature. Data loaded but cannot be re-written unless GEDCOM version changes.");
                        }
                    }
                    else if (Tag.EMAIL.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Emails.Add(ParseStringWithCustomFacts(ch));
                        if (G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but email was specified on submitter on line {ch.LineNum} which is a GEDCOM 5.5.1 feature. Data loaded but cannot be re-written unless GEDCOM version changes.");
                        }
                    }
                    else if (Tag.LANGUAGE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.LanguagePref.Add(ParseStringWithCustomFacts(ch));
                    }
                    else if (Tag.CHANGED_DATETIME.Desc().Equals(ch.Tag))
                    {
                        _loadInto.ChangeDate = new ChangeDate();
                        new ChangeDateParser(_gedcomParser, ch, _loadInto.ChangeDate);
                    }
                }
                // TODO finish SubmitterParser
            }
        }
    }
}