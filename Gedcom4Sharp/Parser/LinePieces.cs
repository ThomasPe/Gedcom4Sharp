using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gedcom4Sharp.Parser
{
    public class LinePieces
    {
        /// <summary>
        /// The current character index into the line
        /// </summary>
        private int currCharIndex;

        /// <summary>
        /// The characters in the line
        /// </summary>
        private readonly string line;

        /// <summary>
        /// The number of the line we are breaking into pieces
        /// </summary>
        private readonly int lineNum;

        /// <summary>
        /// The level of the line
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// The ID number of the item (optional)
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The tag for the line
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// The remainder of the line after the tag
        /// </summary>
        public string Remainder { get; set; }


        public LinePieces(string lineToParse, int lineNum)
        {
            this.lineNum = lineNum;
            line = lineToParse;
            
            // iterate over line parts
            var i = 0;
            var linePieces = line.Split(' ');

            Level = ProcessLevel(linePieces[i]);
            i++;

            Id = ProcessXrefId(linePieces[i]);
            if (!String.IsNullOrEmpty(Id))
            {
                i++;
            }

            Tag = ProcessTag(linePieces[i]);
            i++;

            Remainder = ProcessRemainder(linePieces, i);
        }


        /// <summary>
        ///  Process the level portion of the line
        /// </summary>
        private int ProcessLevel(string lvlStr)
        {
            var success = int.TryParse(lvlStr, out int lvl);
            if (success && lvl >= 0 && lvl < 100)
            {
                return lvl;
            }
            else
            {
                throw new Exception($"Line {lineNum} does not begin with a 1 or 2 digit number for the level followed by a space: {line}");
            }
        }

        /// <summary>
        /// Process the remainder of the line
        /// </summary>
        private string ProcessRemainder(string[] chars, int i)
        {
            if(chars.Length <= i)
            {
                // no remaining content
                return String.Empty;
            }
            try
            {
                return string.Join(" ", chars.Skip(i - 1));
            }
            catch
            {
                throw new Exception($"All GEDCOM lines are required to have a tag value, but no tag could be found on line {line}");
            }
        }

        /// <summary>
        /// Process the tag portion of the line
        /// </summary>
        private string ProcessTag(string tag)
        {
            if (String.IsNullOrEmpty(tag))
            {
                throw new Exception($"All GEDCOM lines are required to have a tag value, but no tag could be found on line {line}");
            }
            return tag;
        }

        
        private string ProcessXrefId(string xrefId)
        {
            if (xrefId.StartsWith("@"))
            {
                if (xrefId.EndsWith("@"))
                {
                    // return Tag without surrounding @'s
                    return xrefId.Substring(1, xrefId.Length - 2);
                } else
                {
                    throw new Exception($"XRef ID begins with @ sign but is not terminated with one on line {lineNum}");
                }
            }
            else
            {
                return String.Empty;
            }
        }

    }
}
