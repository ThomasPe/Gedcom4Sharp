using System.Collections.Generic;
using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Gedcom.Models;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    public class IndividualParser : AbstractParser<Individual>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gedcomParser">a reference to the root GedcomParser</param>
        /// <param name="stringTree">StringTree to be parsed</param>
        /// <param name="loadInto">the object we are loading data into</param>
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
                    else if (EnumExtensions.TryParseDescriptionToEnum(ch.Tag, out IndividualEventType i))
                    {
                        var individualEvent = new IndividualEvent();
                        _loadInto.Events.Add(individualEvent);
                        new IndividualEventParser(_gedcomParser, ch, individualEvent).Parse();
                    }
                    else if (EnumExtensions.TryParseDescriptionToEnum(ch.Tag, out IndividualAttributeType ia))
                    {
                        var a = new IndividualAttribute();
                        _loadInto.Attributes.Add(a);
                        new IndividualAttributeParser(_gedcomParser, ch, a).Parse();
                    }
                    else if (EnumExtensions.TryParseDescriptionToEnum(ch.Tag, out LdsIndividualOrdinanceType l))
                    {
                        var ord = new LdsIndividualOrdinance();
                        _loadInto.LdsIndividualOrdinances.Add(ord);
                        new LdsIndividualOrdinanceParser(_gedcomParser, ch, ord).Parse();
                    }
                    else if (Tag.NOTE.Desc().Equals(ch.Tag))
                    {
                        new NoteStructureListParser(_gedcomParser, ch, _loadInto.NoteStructures).Parse();
                    }
                    else if (Tag.CHANGED_DATETIME.Desc().Equals(ch.Tag))
                    {
                        _loadInto.ChangeDate = new ChangeDate();
                        new ChangeDateParser(_gedcomParser, ch, _loadInto.ChangeDate).Parse();
                    }
                    else if (Tag.RECORD_ID_NUMBER.Desc().Equals(ch.Tag))
                    {
                        _loadInto.RecIdNumber = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.REGISTRATION_FILE_NUMBER.Desc().Equals(ch.Tag))
                    {
                        _loadInto.PermanentRecFileNumber = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.OBJECT_MULTIMEDIA.Desc().Equals(ch.Tag))
                    {
                        new MultimediaLinkParser(_gedcomParser, ch, _loadInto.Multimedia).Parse();
                    }
                    else if (Tag.RESTRICTION.Desc().Equals(ch.Tag))
                    {
                        _loadInto.RestrictionNotice = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.SOURCE.Desc().Equals(ch.Tag))
                    {
                        new CitationListParser(_gedcomParser, ch, _loadInto.Citations).Parse();
                    }
                    else if (Tag.ALIAS.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Aliases.Add(ParseStringWithCustomFacts(ch));
                    }
                    else if (Tag.FAMILY_WHERE_SPOUSE.Desc().Equals(ch.Tag))
                    {
                        LoadFamilyWhereSpouse(ch, _loadInto.FamiliesWhereSpouse);
                    }
                    else if (Tag.FAMILY_WHERE_CHILD.Desc().Equals(ch.Tag))
                    {
                        var fc = new FamilyChild();
                        _loadInto.FamiliesWhereChild.Add(fc);
                        new FamilyChildParser(_gedcomParser, ch, fc).Parse();
                    }
                    else if (Tag.ASSOCIATION.Desc().Equals(ch.Tag))
                    {
                        var a = new Association();
                        _loadInto.Associations.Add(a);
                        new AssociationParser(_gedcomParser, ch, a).Parse();
                    }
                    else if (Tag.ANCESTOR_INTEREST.Desc().Equals(ch.Tag))
                    {
                        _loadInto.AncestorInterest.Add(GetSubmitter(ch.Value));
                    }
                    else if (Tag.DESCENDANT_INTEREST.Desc().Equals(ch.Tag))
                    {
                        _loadInto.DescendantInterest.Add(GetSubmitter(ch.Value));
                    }
                    else if (Tag.ANCESTRAL_FILE_NUMBER.Desc().Equals(ch.Tag))
                    {
                        _loadInto.AncestralFileNumber = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.REFERENCE.Desc().Equals(ch.Tag))
                    {
                        var u = new UserReference();
                        _loadInto.UserReferences.Add(u);
                        new UserReferenceParser(_gedcomParser, ch, u).Parse();
                    }
                    else if (Tag.SUBMITTER.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Submitters.Add(GetSubmitter(ch.Value));
                    }
                    else
                    {
                        UnknownTag(ch, _loadInto);
                    }
                }
            }
        }

        /// <summary>
        /// Load a reference to a family where this individual was a spouse, from a string tree node
        /// </summary>
        /// <param name="st">the string tree node</param>
        /// <param name="familiesWhereSpouse">the list of families where the individual was a child</param>
        private void LoadFamilyWhereSpouse(StringTree st, List<FamilySpouse> familiesWhereSpouse)
        {
            var f = GetFamily(st.Value);
            var fs = new FamilySpouse();
            fs.Family = f;
            familiesWhereSpouse.Add(fs);
            if(st.Children != null)
            {
                foreach(var ch in st.Children)
                {
                    if (Tag.NOTE.Desc().Equals(ch.Tag))
                    {
                        new NoteStructureListParser(_gedcomParser, ch, fs.NoteStructures).Parse();
                    }
                    else
                    {
                        UnknownTag(ch, fs);
                    }
                }
            }
        }
    }
}