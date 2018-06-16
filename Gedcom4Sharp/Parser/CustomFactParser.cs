using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser.Base
{
    public class CustomFactParser : AbstractParser<CustomFact>
    {
        public CustomFactParser(GedcomParser gedcomParser, StringTree stringTree, CustomFact loadInto) : base(gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            _loadInto.Xref = _stringTree.Xref;
            _loadInto.Description = new StringWithCustomFacts(_stringTree.Value);
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if (ch.Tag == Tag.TYPE.Desc())
                    {
                        _loadInto.Type = ParseStringWithCustomFacts(ch);
                    }
                    else if (ch.Tag == Tag.CHANGED_DATETIME.Desc()) 
                    {
                        var changeDate = new ChangeDate();
                        _loadInto.ChangeDate = changeDate;
                        new ChangeDateParser(_gedcomParser, ch, changeDate);
                    }
                }
            }
        }
    }
}