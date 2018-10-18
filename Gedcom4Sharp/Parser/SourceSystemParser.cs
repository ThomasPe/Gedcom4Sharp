using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    public class SourceSystemParser : AbstractParser<SourceSystem>
    {

        public SourceSystemParser(GedcomParser gedcomParser, StringTree stringTree, SourceSystem loadInto) : base(gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            _loadInto.SystemId = _stringTree.Value;
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if (Tag.VERSION.Desc().Equals(ch.Tag)){
                        _loadInto.VersionNum = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.NAME.Desc().Equals(ch.Tag))
                    {
                        _loadInto.ProductName = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.CORPORATION.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Corporation = new Corporation();
                        new CorporationParser(_gedcomParser, ch, _loadInto.Corporation).Parse();
                    }
                    // TODO
                    //else if (Tag.DATA_FOR_CITATION.Desc().Equals(ch.Tag))
                    else if (Tag.DATA_FOR_SOURCE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.SourceData = new HeaderSourceData();
                        new HeaderSourceDataParser(_gedcomParser, ch, _loadInto.SourceData).Parse();
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