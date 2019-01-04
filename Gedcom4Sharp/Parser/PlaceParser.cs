using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Base;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    internal class PlaceParser : AbstractParser<Place>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gedcomParser">a reference to the root GedcomParser</param>
        /// <param name="ch">StringTree to be parsed</param>
        /// <param name="place">the object we are loading data into</param>
        public PlaceParser(GedcomParser gedcomParser, StringTree ch, Place place) : base (gedcomParser, ch, place)
        {
        }

        public override void Parse()
        {
            _loadInto.PlaceName = _stringTree.Value;
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if (Tag.FORM.Desc().Equals(ch.Tag))
                    {
                        _loadInto.PlaceFormat = ParseStringWithCustomFacts(ch);
                    }
                    else if (Tag.SOURCE.Desc().Equals(ch.Tag))
                    {
                        new CitationListParser(_gedcomParser, ch, _loadInto.Citations).Parse();
                    }
                    else if (Tag.NOTE.Desc().Equals(ch.Tag))
                    {
                        new NoteStructureListParser(_gedcomParser, ch, _loadInto.NoteStructures).Parse();
                    }
                    else if (Tag.CONCATENATION.Desc().Equals(ch.Tag))
                    {
                        _loadInto.PlaceName += (ch.Value ?? string.Empty);
                    }
                    else if (Tag.CONTINUATION.Desc().Equals(ch.Tag))
                    {
                        _loadInto.PlaceName += $"\n {ch.Value ?? string.Empty}";
                    }
                    else if (Tag.ROMANIZED.Desc().Equals(ch.Tag))
                    {
                        if (G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but a romanized variation was specified on a place on line {ch.LineNum} which is a GEDCOM 5.5.1 feature. Data loaded but cannot be re-written unless GEDCOM version changes.");
                        }
                        AbstractNameVariation nv = new PlaceNameVariation();
                        _loadInto.Romanized.Add(nv);
                        nv.Variation = ch.Value;
                        if(ch.Children != null)
                        {
                            foreach(var gch in ch.Children)
                            {
                                if (Tag.TYPE.Desc().Equals(gch.Tag))
                                {
                                    nv.VariationType = ParseStringWithCustomFacts(gch);
                                } else
                                {
                                    UnknownTag(gch, nv);
                                }
                            }
                        }
                    }
                    else if (Tag.PHONETIC.Desc().Equals(ch.Tag))
                    {
                        if (G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but a phonetic variation was specified on a place on line {ch.LineNum}, which is a GEDCOM 5.5.1 feature. Data loaded but cannot be re-written unless GEDCOM version changes.");
                        }
                        AbstractNameVariation nv = new PlaceNameVariation();
                        _loadInto.Phonetic.Add(nv);
                        nv.Variation = ch.Value;
                        if(ch.Children != null)
                        {
                            foreach(var gch in ch.Children)
                            {
                                if (Tag.TYPE.Desc().Equals(gch.Tag))
                                {
                                    nv.VariationType = ParseStringWithCustomFacts(gch);
                                }
                                else
                                {
                                    UnknownTag(gch, nv);
                                }
                            }
                        }
                    }
                    else if (Tag.MAP.Desc().Equals(ch.Tag))
                    {
                        if (G55())
                        {
                            _gedcomParser.Warnings.Add($"GEDCOM version is 5.5 but a map coordinate was specified on a place on line {ch.LineNum}, which is a GEDCOM 5.5.1 feature. Data loaded but cannot be re-written unless GEDCOM version changes.");
                        }
                        if(ch.Children != null)
                        {
                            foreach(var gch in ch.Children)
                            {
                                if (Tag.LATITUDE.Desc().Equals(gch.Tag))
                                {
                                    _loadInto.Latitude = ParseStringWithCustomFacts(gch);
                                }
                                else if (Tag.LONGITUDE.Desc().Equals(gch.Tag))
                                {
                                    _loadInto.Longitude = ParseStringWithCustomFacts(gch);
                                }
                                else
                                {
                                    UnknownTag(gch, _loadInto);
                                }
                            }
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