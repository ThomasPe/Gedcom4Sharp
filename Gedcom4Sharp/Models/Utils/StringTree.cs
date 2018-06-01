using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Utils
{
    public class StringTree
    {
        /// <summary>
        /// All the elements that are child elements of this element
        /// </summary>
        public List<StringTree> Children { get; set; } = new List<StringTree>();

        /// <summary>
        /// The level of this element
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// The line number of the GEDCOM from which this element was derived
        /// </summary>
        public int LineNum { get; set; }

        /// <summary>
        /// The element to which this element is a child.
        /// </summary>
        public StringTree Parent { get; set; }

        /// <summary>
        /// The tag for this element
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// The value for this element (basically everything after the tag)
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The Xref ID number of this element
        /// </summary>
        public string Xref { get; set; }
    }
}
