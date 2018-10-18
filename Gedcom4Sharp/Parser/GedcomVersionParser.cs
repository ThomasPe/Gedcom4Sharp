using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    public class GedcomVersionParser :  AbstractParser<GedcomVersion>
    {

        public GedcomVersionParser(GedcomParser gedcomParser, StringTree stringTree, GedcomVersion loadInto)
            : base(gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if (Tag.VERSION.Desc().Equals(ch.Tag))
                    {
                        if (!SupportedVersion.TestSupportedVersion(ch.Value))
                        {
                            _gedcomParser.Errors.Add($"Unsupported Version {ch.Value}");
                        }
                        _loadInto.VersionNumber = new StringWithCustomFacts(ch.Value);
                        RemainingChildrenAreCustomTags(ch, _loadInto.VersionNumber);
                    }
                    else if (Tag.FORM.Desc().Equals(ch.Tag))
                    {
                        _loadInto.GedcomForm = ParseStringWithCustomFacts(ch);
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