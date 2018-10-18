using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    public class HeaderSourceDataParser : AbstractParser<HeaderSourceData>
    {

        public HeaderSourceDataParser(GedcomParser gedcomParser, StringTree stringTree, HeaderSourceData loadInto)
            : base(gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            _loadInto.Name = new StringWithCustomFacts(_stringTree.Value);
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if (Tag.DATE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.PublishDate = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.COPYRIGHT.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Copyright = ParseStringWithCustomFacts(ch);
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