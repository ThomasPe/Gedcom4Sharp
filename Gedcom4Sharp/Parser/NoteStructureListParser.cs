using System;
using System.Collections.Generic;
using System.Linq;
using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Parser.Base;
using Gedcom4Sharp.Utility.Extensions;

namespace Gedcom4Sharp.Parser
{
    public class NoteStructureListParser : AbstractParser<List<NoteStructure>>
    {

        public NoteStructureListParser(GedcomParser gedcomParser, StringTree stringTree, List<NoteStructure> loadInto) : base(gedcomParser, stringTree, loadInto)
        {
        }

        public override void Parse()
        {
            var noteStructure = new NoteStructure();
            if(_stringTree.Xref == null && ReferencesAnotherNode(_stringTree))
            {
                noteStructure.NoteReference = getNote(_stringTree.Value);
                _loadInto.Add(noteStructure);
                RemainingChildrenAreCustomTags(_stringTree, noteStructure);
                return;
            }
            _loadInto.Add(noteStructure);
            noteStructure.Lines.Add(_stringTree.Value);
            if(_stringTree.Children != null)
            {
                foreach(var ch in _stringTree.Children)
                {
                    if(ch.Tag == Tag.CONCATENATION.Desc())
                    {
                        if (!noteStructure.Lines.Any())
                        {
                            noteStructure.Lines.Add(ch.Value);
                        }
                        else
                        {
                            var lastNote = noteStructure.Lines.LastOrDefault();
                            if (string.IsNullOrEmpty(lastNote))
                            {
                                noteStructure.Lines[noteStructure.Lines.Count - 1] = ch.Value;
                            }
                            else
                            {
                                noteStructure.Lines[noteStructure.Lines.Count - 1] = lastNote + ch.Value;

                            }
                        }
                    }
                    else if (ch.Tag == Tag.CONTINUATION.Desc())
                    {
                        noteStructure.Lines.Add(string.IsNullOrEmpty(ch.Value) ? "" : ch.Value);
                    }
                    else
                    {
                        UnknownTag(ch, noteStructure);
                    }
                }
            }
        }

        private NoteRecord getNote(string xref)
        {
            _gedcomParser.Gedcom.Notes.TryGetValue(xref, out NoteRecord note);
            if(note == null)
            {
                note = new NoteRecord(xref);
                _gedcomParser.Gedcom.Notes.Add(xref, note);
            }
            return note;
        }
    }
}