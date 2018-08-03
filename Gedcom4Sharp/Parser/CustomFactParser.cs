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
                    else if (Tag.CHANGED_DATETIME.Desc().Equals(ch.Tag)) 
                    {
                        var changeDate = new ChangeDate();
                        _loadInto.ChangeDate = changeDate;
                        new ChangeDateParser(_gedcomParser, ch, changeDate).Parse();
                    }
                    else if(Tag.DATE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Date = new StringWithCustomFacts(ch.Value);
                    }
                    else if(Tag.PLACE.Desc().Equals(ch.Tag))
                    {
                        var place = new Place();
                        _loadInto.Place = place;
                        new PlaceParser(_gedcomParser, ch, place).Parse();
                    }
                    else if(Tag.NOTE.Desc().Equals(ch.Tag))
                    {
                        new NoteStructureListParser(_gedcomParser, ch, _loadInto.NoteStructures).Parse();
                    }
                    else if(Tag.SOURCE.Desc().Equals(ch.Tag))
                    {
                        new CitationListParser(_gedcomParser, ch, _loadInto.Citations).Parse();
                    }
                    else if(Tag.CONCATENATION.Desc().Equals(ch.Tag))
                    {
                        if(_loadInto.Description == null)
                        {
                            _loadInto.Description = ParseStringWithCustomFacts(ch);
                        }
                        else
                        {
                            _loadInto.Description.Value += ch.Value;
                        }
                    }
                    else if (Tag.CONTINUATION.Desc().Equals(ch.Tag))
                    {
                        if(_loadInto.Description == null)
                        {
                            _loadInto.Description = new StringWithCustomFacts(ch.Value ?? string.Empty);
                        }
                        else
                        {
                            _loadInto.Description.Value += "\n" + ch.Value;
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