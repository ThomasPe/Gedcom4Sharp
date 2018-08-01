using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    internal class PersonalNameParser : AbstractParser<PersonalName>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gedcomParser"> reference to the root GedcomParser</param>
        /// <param name="stringTree">StringTree to be parsed</param>
        /// <param name="loadInto">the object we are loading data into</param>
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
                    else if (Tag.SURNAME.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Surname = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.NAME_SUFFIX.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Suffix = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.SOURCE.Desc().Equals(ch.Tag))
                    {
                        new CitationListParser(_gedcomParser, ch, _loadInto.Citations);
                    }
                    else if (Tag.NOTE.Desc().Equals(ch.Tag))
                    {
                        new NoteStructureListParser(_gedcomParser, ch, _loadInto.NoteStructures).Parse();
                    }
                    else if (Tag.ROMANIZED.Desc().Equals(ch.Tag))
                    {
                        var pnv = new PersonalNameVariation();
                        _loadInto.Romanized.Add(pnv);
                        LoadPersonalNameVariation(ch, pnv);
                    }
                    else if (Tag.PHONETIC.Desc().Equals(ch.Tag))
                    {
                        var pnv = new PersonalNameVariation();
                        _loadInto.Phonetic.Add(pnv);
                        LoadPersonalNameVariation(ch, pnv);
                    }
                    else if (Tag.TYPE.Desc().Equals(ch.Tag))
                    {
                        _loadInto.Type = ParseStringWithCustomFacts(ch);
                    }
                    else
                    {
                        UnknownTag(ch, _loadInto);
                    }
                }
            }
        }

        /// <summary>
        /// Load a personal name variation (romanization or phonetic version) from a string tree node
        /// </summary>
        /// <param name="romnOrPhon">the ROMN or PHON string tree node to load from</param>
        /// <param name="pnv">the personal name variation to fill in</param>
        private void LoadPersonalNameVariation(StringTree romnOrPhon, PersonalNameVariation pnv)
        {
            pnv.Variation = romnOrPhon.Value;
            if(romnOrPhon.Children != null)
            {
                foreach(var ch in romnOrPhon.Children)
                {
                    if (Tag.NAME_PREFIX.Desc().Equals(ch.Tag))
                    {
                        pnv.Prefix = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.GIVEN_NAME.Desc().Equals(ch.Tag))
                    {
                        pnv.GivenName = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.NICKNAME.Desc().Equals(ch.Tag))
                    {
                        pnv.Nickname = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.SURNAME_PREFIX.Desc().Equals(ch.Tag))
                    {
                        pnv.SurnamePrefix = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.SURNAME.Desc().Equals(ch.Tag))
                    {
                        pnv.Surname = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.NAME_SUFFIX.Desc().Equals(ch.Tag))
                    {
                        pnv.Suffix = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.SOURCE.Desc().Equals(ch.Tag))
                    {
                        new CitationListParser(_gedcomParser, ch, pnv.Citations).Parse();
                    }
                    else if (Tag.NOTE.Desc().Equals(ch.Tag))
                    {
                        new NoteStructureListParser(_gedcomParser, ch, pnv.NoteStructures).Parse();
                    }
                    else if (Tag.TYPE.Desc().Equals(ch.Tag))
                    {
                        pnv.VariationType = ParseStringWithCustomFacts(ch);
                    }
                    else
                    {
                        UnknownTag(ch, pnv);
                    }
                }
            }
        }
    }
}