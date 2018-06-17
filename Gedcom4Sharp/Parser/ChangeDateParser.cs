using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Parser
{
    public class ChangeDateParser : AbstractParser<ChangeDate>
    {
        public ChangeDateParser(GedcomParser gedcomParser, StringTree stringTree, ChangeDate loadInto) : base(gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if(ch.Tag == Tag.DATE.Desc())
                    {
                        _loadInto.Date = new StringWithCustomFacts(ch.Value);
                        if(ch.Children != null)
                        {
                            foreach(var gch in ch.Children)
                            {
                                if(gch.Tag == "TIME")
                                {
                                    _loadInto.Time = ParseStringWithCustomFacts(gch);
                                } 
                                else
                                {
                                    UnknownTag(gch, _loadInto.Date);
                                }
                            }
                        }
                    } 
                    else if(ch.Tag == Tag.NOTE.Desc())
                    {
                        List<NoteStructure> notes = _loadInto.NoteStructures;
                        new NoteStructureListParser(_gedcomParser, ch, notes).Parse();
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
