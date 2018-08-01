using Gedcom4Sharp.Models.Gedcom.Enums;
using Gedcom4Sharp.Models.Utils;
using Gedcom4Sharp.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gedcom4Sharp.Parser
{
    public class StringTreeBuilder
    {
        /// <summary>
        /// An array of references to the most recently added node for each given level. Works as a fast index to the nodes so we can
        /// find parents quickly. Whenever a new node is added, set, or removed for level N, all entries in this index > N (i.e., all
        /// the child levels) need to be cleared.
        /// </summary>
        private readonly StringTree[] lastNodeAtLevel = new StringTree[100];

        /// <summary>
        /// A flag indicating whether the current line from the input file begins with a 1-2 digit level number followed by a space
        /// </summary>
        private bool _beginsWithLevelAndSpace;

        /// <summary>
        /// The string tree node that represents the current line and all its children.
        /// </summary>
        private StringTree _treeForCurrentLine;

        /// <summary>
        ///  A base StringTree to hold a single root-level node
        /// </summary>
        public readonly StringTree WrapperNode = new StringTree();

        /// <summary>
        /// The most recently added node
        /// </summary>
        private StringTree _mostRecentlyAdded;

        /// <summary>
        /// The GedcomParser instance this object is building StringTree instances for
        /// </summary>
        private readonly GedcomParser _parser;

        /// <summary>
        /// The line number we're on - 1-based!
        /// </summary>
        private int _lineNum = 0;

        /// <summary>
        /// The line we're currently processing
        /// </summary>
        private string _line;

        public StringTreeBuilder(GedcomParser parser)
        {
            _parser = parser;
            WrapperNode.Level = -1;
            _mostRecentlyAdded = null;
            _lineNum = parser.LineNum;
        }

        /// <summary>
        /// Add the supplied line to the right place in the StringTree being built
        /// </summary>
        /// <param name="line"></param>
        public void AppendLine(string line)
        {
            _line = line;
            _lineNum++;
            _treeForCurrentLine = new StringTree();
            _treeForCurrentLine.LineNum = _lineNum;

            if (IsNewLevelLine(_line))
            {
                AddNewNode();
                _mostRecentlyAdded = _treeForCurrentLine;
            }
            else
            {
                // if we can't determine whether the current line is a new leveled line in the file or not when strict line breaks
                // are off, or that the line does not begin with a level number when strict line breaks are enabled.
                if (_parser.StrictLineBreaks)
                {
                    throw new Exception($"Line {_lineNum} does not begin with a 1 or 2 digit number for the level followed by a space: {_line}");
                }
                else
                {
                    //MakeConcatenationOfPreviousNode();
                }
            }
        }

        /// <summary>
        /// Add a new node to the correct parent node in the StringTree
        /// </summary>
        private void AddNewNode()
        {
            var lp = new LinePieces(_line, _lineNum);
            _treeForCurrentLine.Level = lp.Level;
            _treeForCurrentLine.Xref = lp.Id;
            _treeForCurrentLine.Tag = lp.Tag;
            _treeForCurrentLine.Value = lp.Remainder;

            StringTree addTo;
            if(_treeForCurrentLine.Level == 0)
            {
                addTo = WrapperNode;
            } 
            else
            {
                addTo = lastNodeAtLevel[_treeForCurrentLine.Level - 1];
            }
            if(addTo == null)
            {
                _parser.Errors.Add($"{_treeForCurrentLine.Tag} tag at line {_treeForCurrentLine.LineNum}: Unable to find suitable parent node at level {_treeForCurrentLine.Level - 1}");
            } 
            else
            {
                addTo.Children.Add(_treeForCurrentLine);
                _treeForCurrentLine.Parent = addTo;
                lastNodeAtLevel[_treeForCurrentLine.Level] = _treeForCurrentLine;
            }
            lastNodeAtLevel.Fill(_treeForCurrentLine.Level + 1, 99, null);
        }

        /// <summary>
        /// Make the current node a concatenation of the previous node.
        /// </summary>
        private void MakeConcatenationOfPreviousNode()
        {
            // Doesn't begin with a level number followed by a space, and we don't have strictLineBreaks
            // required, so it's probably meant to be a continuation of the previous text value.
            if(_mostRecentlyAdded == null)
            {
                _parser.Warnings.Add($"Line {_lineNum} did not begin with a level and tag, so it was discarded.");
            }
            else
            {
                // Try to add as a CONT line to previous node, as if the file had been properly escaped
                _treeForCurrentLine.Level = _mostRecentlyAdded.Level + 1;
                _treeForCurrentLine.Tag = Tag.CONTINUATION.Desc();
                _treeForCurrentLine.Value = _line;
                _treeForCurrentLine.Parent = _mostRecentlyAdded;
                _mostRecentlyAdded.Children.Add(_treeForCurrentLine);
                _parser.Warnings.Add($"Line {_lineNum} did not begin with a level and tag, so it was treated as a non-standard continuation of the previous line.");
            }
        }

        /// <summary>
        /// true if and only if the line begins with a 1-2 digit level number followed by a space
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private bool IsNewLevelLine(string line)
        {
            var isNewLevelLine = false;
            if (Char.IsNumber(line[0]))
            {
                if (Char.IsWhiteSpace(line[1])) {
                    isNewLevelLine = true;
                }
                else if(Char.IsNumber(line[1]) && Char.IsWhiteSpace(line[2]))
                {
                    isNewLevelLine = true;
                }
            }
            return isNewLevelLine;
        }

    }
}
