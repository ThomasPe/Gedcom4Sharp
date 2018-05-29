using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Parser
{
    public class LinePieces
    {
        /// <summary>
        /// The current character index into the line
        /// </summary>
        private int currCharrIndex;

        /// <summary>
        /// The characters in the line
        /// </summary>
        private readonly string chars;

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


        /// <summary>
        ///  Process the level portion of the line
        /// </summary>
        private void ProcessLevel()
        {
            try
            {
                currCharrIndex = -1;

                if (Char.IsWhiteSpace(chars[1])){
                    // Second character in line is a space, so assume a 1-digit level
                    Level = Convert.ToInt16(Char.GetNumericValue(chars[1]));
                    currCharrIndex = 2; // Continue parsing at 3rd character in line
                }
                else
                {
                    // Second character in line is not a space, so assume a 2-digit level
                    Level = int.Parse(chars.Substring(0, 2));
                    currCharrIndex = 3; // Continue parsing at 4th character in line
                }
            }
            catch
            {
                throw new Exception($"Line {lineNum} does not begin with a 1 or 2 digit number for the level followed by a space: {chars}");
            }
            if(Level < 0 || Level > 99)
            {
                throw new Exception($"Line {lineNum} does not begin with a 1 or 2 digit number for the level followed by a space: {chars}");
            }
        }

        /// <summary>
        /// Process the remainder of the line
        /// </summary>
        private void ProcessRemainder()
        {
            if(currCharrIndex < chars.Length)
            {
                Remainder = chars.Substring(currCharrIndex + 1);
            }
        }
        
    }
}
