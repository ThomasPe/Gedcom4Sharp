using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    internal class PersonalNameParser : AbstractParser<PersonalName>
    {
        public PersonalNameParser(GedcomParser gedcomParser, StringTree stringTree, PersonalName loadInto) : base(gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            _loadInto.Basic = _stringTree.Value;
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if (Tag.NAME_PREFIX.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Prefix = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.GIVEN_NAME.Desc().Equals(ch.Tag))
                    {
                        _loadInto.GivenName = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.NICKNAME.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Nickname = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.SURNAME_PREFIX.Desc().Equals(ch.Tag))
                    {
                        _loadInto.SurnamePrefix = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.SURNAME.Desc().Equals(ch))
                    {
                        _loadInto.Surname = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.NAME_SUFFIX.Desc().Equals(ch))
                    {
                        _loadInto.Suffix = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.SOURCE.Desc().Equals(ch))
                    {
                        new CitationListParser(_gedcomParser, ch, _loadInto.Citations);
                    }
                    // TODO Finish PersonalNameParser
                }
            }
        }
    }
}