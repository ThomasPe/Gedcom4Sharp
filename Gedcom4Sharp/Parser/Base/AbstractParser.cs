using Gedcom4Sharp.Models.Gedcom;
using Gedcom4Sharp.Models.Gedcom.Base;
using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gedcom4Sharp.Parser.Base
{
    public abstract class AbstractParser<T>
    {
        /// <summary>
        /// The StringTree to be parsed
        /// </summary>
        protected readonly StringTree _stringTree;

        /// <summary>
        /// Reference to the root GedcomParser
        /// </summary>
        protected readonly GedcomParser _gedcomParser;

        /// <summary>
        /// reference to the object we are loading data into
        /// </summary>
        protected readonly T _loadInto;

        public AbstractParser(GedcomParser gedcomParser, StringTree stringTree, T loadInto)
        {
            _stringTree = stringTree;
            _gedcomParser = gedcomParser == null && this is GedcomParser ? this as GedcomParser : gedcomParser;
            _loadInto = loadInto;
        }

        /// <summary>
        /// Returns true if and only if the Gedcom data says it is for the 5.5 standard.
        /// </summary>
        /// <returns></returns>
        protected bool G55()
        {
            var versionNumber = _gedcomParser.Gedcom?.Header?.GedcomVersion?.VersionNumber?.Value;
            return versionNumber != null && versionNumber.Equals(SupportedVersion.V5_5);
        }

        /// <summary>
        /// Get a family by their xref, adding them to the gedcom collection of families if needed.
        /// </summary>
        /// <param name="xref"></param>
        /// <returns></returns>
        protected Family GetFamily(string xref)
        {
            //var f = _gedcomParser.Gedcom.Families[xref];
            if(!_gedcomParser.Gedcom.Families.TryGetValue(xref, out Family f))
            {
                f = new Family
                {
                    Xref = xref
                };
                _gedcomParser.Gedcom.Families.Add(xref, f);
            }
            return f;
        }

        /// <summary>
        /// Get an individual by their xref, adding them to the gedcom collection of individuals if needed.
        /// </summary>
        /// <param name="xref"></param>
        /// <returns></returns>
        protected Individual GetIndividual(string xref)
        {
            if(!_gedcomParser.Gedcom.Individuals.TryGetValue(xref, out Individual i)){
                i = new Individual
                {
                    Xref = xref
                };
                _gedcomParser.Gedcom.Individuals.Add(xref, i);
            }
            return i;
        }

        /// <summary>
        /// Get a multimedia by its xref, adding them to the gedcom collection of individuals if needed.
        /// </summary>
        /// <param name="xref"></param>
        /// <returns></returns>
        protected Multimedia GetMultimedia(string xref)
        {
            if (!_gedcomParser.Gedcom.Multimedia.TryGetValue(xref, out Multimedia m)){
                m = new Multimedia
                {
                    Xref = xref
                };
                _gedcomParser.Gedcom.Multimedia.Add(xref, m);
            }
            return m;
        }

        /// <summary>
        /// Get a note record by its xref, adding them to the gedcom collection of individuals if needed.
        /// </summary>
        /// <param name="xref"></param>
        /// <returns></returns>
        protected NoteRecord GetNoteRecord(string xref)
        {
            if(!_gedcomParser.Gedcom.Notes.TryGetValue(xref, out NoteRecord nr))
            {
                nr = new NoteRecord(xref);
                _gedcomParser.Gedcom.Notes.Add(xref, nr);
            }
            return nr;
        }

        /// <summary>
        /// Get a repository by its xref, adding them to the gedcom collection of individuals if needed.
        /// </summary>
        /// <param name="xref"></param>
        /// <returns></returns>
        protected Repository GetRepository(string xref)
        {
            if(!_gedcomParser.Gedcom.Repositories.TryGetValue(xref, out Repository r))
            {
                r = new Repository
                {
                    Xref = xref
                };
                _gedcomParser.Gedcom.Repositories.Add(xref, r);
            }
            return r;
        }

        /// <summary>
        /// Get a source by its xref, adding them to the gedcom collection of individuals if needed.
        /// </summary>
        /// <param name="xref"></param>
        /// <returns></returns>
        protected Source GetSource(string xref)
        {
            if(!_gedcomParser.Gedcom.Sources.TryGetValue(xref, out Source src))
            {
                src = new Source
                {
                    Xref = xref
                };
                _gedcomParser.Gedcom.Sources.Add(xref, src);
            }
            return src;
        }

        /// <summary>
        /// Get a submitter by its xref, adding them to the gedcom collection of individuals if needed.
        /// </summary>
        /// <param name="xref"></param>
        /// <returns></returns>
        protected Submitter GetSubmitter(string xref)
        {
            if (!_gedcomParser.Gedcom.Submitters.TryGetValue(xref, out Submitter s))
            {
                s = new Submitter()
                {
                    Xref = xref
                };
                _gedcomParser.Gedcom.Submitters.Add(xref, s);
            }
            return s;
        }

        /// <summary>
        /// Load multiple (continued) lines of text from a string tree node
        /// </summary>
        /// <param name="stringTreeWithLinesOfText"></param>
        /// <param name="listOfString"></param>
        /// <param name="element"></param>
        protected void LoadMultiLinesOfText(StringTree stringTreeWithLinesOfText, List<String> listOfString, AbstractElement element)
        {
            if(stringTreeWithLinesOfText.Value != null)
            {
                listOfString.Add(stringTreeWithLinesOfText.Value);
            }
            if(stringTreeWithLinesOfText.Children != null)
            {
                foreach(var ch in stringTreeWithLinesOfText.Children)
                {
                    if (Tag.CONTINUATION.Equals(ch.Tag))
                    {
                        if(ch.Value == null)
                        {
                            listOfString.Add(string.Empty);
                        }
                        else
                        {
                            listOfString.Add(ch.Value);
                        }
                    }
                    else if (Tag.CONCATENATION.Equals(ch.Tag))
                    {
                        // If there's no value to concatenate, ignore it
                        if(ch.Value != null)
                        {
                            if(listOfString.Count == 0)
                            {
                                listOfString.Add(ch.Value);
                            }
                            else
                            {
                                listOfString[listOfString.Count - 1] = listOfString[listOfString.Count - 1] + ch.Value;
                            }
                        }
                    }
                    else
                    {
                        UnknownTag(ch, element);
                    }
                }
            }
        }

        protected void LoadMultiStringWithCustomFacts(StringTree stringTreeWithLinesOfText, MultiStringWithCustomFacts multiString)
        {
            var listOfStrings = multiString.Lines;
            if(stringTreeWithLinesOfText.Value != null)
            {
                listOfStrings.Add(stringTreeWithLinesOfText.Value);
            }
            if(stringTreeWithLinesOfText.Children != null)
            {
                foreach(var ch in stringTreeWithLinesOfText.Children)
                {
                    if (Tag.CONTINUATION.Desc().Equals(ch.Tag))
                    {
                        if(ch.Value == null)
                        {
                            listOfStrings.Add(string.Empty);
                        }
                        else
                        {
                            listOfStrings.Add(ch.Value);
                        }
                    } 
                    else if (Tag.CONCATENATION.Desc().Equals(ch.Tag))
                    {
                        // If there's no value to concatenate, ignore it
                        if(ch.Value != null)
                        {
                            if (!listOfStrings.Any())
                            {
                                listOfStrings.Add(ch.Value);
                            }
                            else
                            {
                                var i = listOfStrings.Count - 1;
                                listOfStrings[i] = listOfStrings[i] + ch.Value;
                            }
                        }
                    } else
                    {
                        UnknownTag(ch, multiString);
                    }
                }
            }
        }


        /// <summary>
        /// Helper method to take a string tree and all its children and load them into a StringWithCustomFacts object
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        protected StringWithCustomFacts ParseStringWithCustomFacts(StringTree ch)
        {
            var swcf = new StringWithCustomFacts(ch.Value);
            if(ch.Children != null)
            {
                foreach(var gch in ch.Children)
                {
                    var cf = new CustomFact(gch.Tag);
                    swcf.CustomFacts.Add(cf);
                    cf.Xref = gch.Xref;
                    new CustomFactParser(_gedcomParser, gch, cf).Parse();
                }
            }
            return swcf;
        }

        /// <summary>
        /// Returns true if the node passed in uses a cross-reference to another node
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        protected bool ReferencesAnotherNode(StringTree st)
        {
            if(st.Value == null)
            {
                return false;
            }
            int r1 = st.Value.IndexOf('@');
            if(r1 == -1)
            {
                return false;
            }
            int r2 = st.Value.IndexOf('@', r1);
            return r2 > -1;
        }

        protected void RemainingChildrenAreCustomTags(StringTree st, HasCustomFacts into)
        {
            if(st == null || st.Children == null)
            {
                return;
            }
            foreach (var ch in st.Children)
            {
                UnknownTag(ch, into);
            }
        }

        /// <summary>
        /// Default handler for a tag that the parser was not expecting to see.
        /// - If custom tags are ignored in the parser (see {@link GedcomParser#isIgnoreCustomTags()}), the custom/unknown tag and all
        ///   its children will be ignored, regardless of the value of the strict custom tags setting(because it doesn't make sense to be
        ///   strict about tags that are being ignored).
        /// - If the tag begins with an underscore, it is a user-defined tag, which is stored in the customFacts collection of the
        ///   passed in element, and returns.
        /// - If GedcomParser.StrictCustomTags parsing is turned off (i.e., == false), it is treated as a user-defined tag
        ///   (despite the lack of beginning underscore) and treated like any other user-defined tag.
        /// - If {@link GedcomParser#isStrictCustomTags()} parsing is turned on (i.e., == true), it is treated as bad tag and an error
        ///   is logged in the GedcomParser.Errors collection, and then the tag and its children are ignored.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="element"></param>
        protected void UnknownTag(StringTree node, HasCustomFacts element)
        {
            if (_gedcomParser.IgnoreCustomTags)
            {
                return;
            }
            bool beginsWithUnderscore = !string.IsNullOrEmpty(node.Tag) && node.Tag[0] == '_';
            if(beginsWithUnderscore || !_gedcomParser.StrictCustomTags || _gedcomParser.InsideCustomTag)
            {
                var cf = new CustomFact(node.Tag);
                cf.Xref = node.Xref;
                cf.Description = new StringWithCustomFacts(node.Value);
                element.CustomFacts.Add(cf);
                // Save current value
                var saveIsInisdeCustomTag = _gedcomParser.InsideCustomTag;
                _gedcomParser.InsideCustomTag = true;
                new CustomFactParser(_gedcomParser, node, cf).Parse();
                // Restore prior value
                _gedcomParser.InsideCustomTag = saveIsInisdeCustomTag;
                return;
            }

            var sb = new StringBuilder();
            sb.Append($"Line {node.LineNum}: Cannot handle tag {node.Tag}");
            var st = node;
            while(st.Parent != null)
            {
                st = st.Parent;
                sb.Append($", child of {st.Tag}");
                if(st.Xref != null)
                {
                    sb.Append($" {st.Xref}");
                }
                sb.Append($" on line {st.LineNum}");
            }
            _gedcomParser.Errors.Add(sb.ToString());
        }

        /// <summary>
        /// Parse the string tree passed into the constructor, and load it into the object model
        /// </summary>
        public abstract void Parse();
    }
}
