using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    internal class FamilyParser : AbstractParser<Family>
    {

        public FamilyParser(GedcomParser gedcomParser, StringTree stringTree, Family loadInto) : base(gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if (Tag.HUSBAND.Desc().Equals(ch.Tag))
                    {
                        var husband = new IndividualReference(GetIndividual(ch.Tag));
                        _loadInto.Husband = husband;
                        if(ch.Children != null)
                        {
                            foreach(var gch in ch.Children)
                            {
                                var cf = new CustomFact(gch.Tag);
                                husband.CustomFacts.Add(cf);
                                new CustomFactParser(_gedcomParser, gch, cf).Parse();
                            }
                        }
                    }
                    else if (Tag.WIFE.Desc().Equals(ch.Tag))
                    {
                        var wife = new IndividualReference(GetIndividual(ch.Tag));
                        _loadInto.Wife = wife;
                        if (ch.Children != null)
                        {
                            foreach (var gch in ch.Children)
                            {
                                var cf = new CustomFact(gch.Tag);
                                wife.CustomFacts.Add(cf);
                                new CustomFactParser(_gedcomParser, gch, cf).Parse();
                            }
                        }
                    }
                    else if (Tag.CHILD.Desc().Equals(ch.Tag))
                    {
                        var child = new IndividualReference(GetIndividual(ch.Tag));
                        _loadInto.Children.Add(child);
                        if (ch.Children != null)
                        {
                            foreach (var gch in ch.Children)
                            {
                                var cf = new CustomFact(gch.Tag);
                                child.CustomFacts.Add(cf);
                                new CustomFactParser(_gedcomParser, gch, cf).Parse();
                            }
                        }
                    }
                    else if (Tag.NUM_CHILDREN.Desc().Equals(ch.Tag))
                    {
                        _loadInto.NumChildren = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.SOURCE.Desc().Equals(ch.Tag))
                    {
                        new CitationListParser(_gedcomParser, ch, _loadInto.Citations).Parse();
                    }
                    else if (Tag.OBJECT_MULTIMEDIA.Desc().Equals(ch.Tag))
                    {
                        new MultimediaLinkParser(_gedcomParser, ch, _loadInto.Multimedia).Parse();
                    }
                    else if (Tag.RECORD_ID_NUMBER.Desc().Equals(ch.Tag))
                    {
                        _loadInto.AutomatedRecordId = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.CHANGED_DATETIME.Desc().Equals(ch.Tag))
                    {
                        _loadInto.ChangeDate = new ChangeDate();
                        new ChangeDateParser(_gedcomParser, ch, _loadInto.ChangeDate).Parse();
                    }
                    else if (Tag.NOTE.Desc().Equals(ch.Tag))
                    {
                        new NoteStructureListParser(_gedcomParser, ch, _loadInto.NoteStructures).Parse();
                    }
                    else if (Tag.RESTRICTION.Desc().Equals(ch.Tag))
                    {
                        _loadInto.RestrictionNotice = ParseStringWithCustomFacts(ch);
                        if (G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but restriction notice was specified for family on line {ch.LineNum}, which is a GEDCOM 5.5.1 feature. Data loaded but cannot be re-written unless GEDCOM version changes.") ;
                        }
                    }
                    else if (Tag.REGISTRATION_FILE_NUMBER.Desc().Equals(ch.Tag))
                    {
                        _loadInto.RecFileNumber = ParseStringWithCustomFacts(ch);
                    }
                    else if (EnumExtensions.TryParseDescriptionToEnum(ch.Tag, out FamilyEventType fet))
                    {
                        var e = new FamilyEvent();
                        _loadInto.Events.Add(e);
                        new FamilyEventParser(_gedcomParser, ch, e).Parse();
                    }
                    else if (Tag.SEALING_SPOUSE.Desc().Equals(ch.Tag))
                    {
                        var ldsss = new LdsSpouseSealing();
                        _loadInto.LdsSpouseSealings.Add(ldsss);
                        new LdsSpouseSealingParser(_gedcomParser, ch, ldsss).Parse();
                    }
                }
            }
        }
    }
}