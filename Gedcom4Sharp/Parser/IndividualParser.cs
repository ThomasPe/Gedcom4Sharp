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
                    // TODO IndividualParser Finish
                }
            }
        }
    }
}