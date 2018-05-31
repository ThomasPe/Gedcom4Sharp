using Gedcom4Sharp.Models.Utils;
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
        private readonly StringTree _wrapperNode = new StringTree();

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
            _wrapperNode.setLevel(-1);
        }

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
