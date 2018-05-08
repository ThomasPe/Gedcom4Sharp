using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Models.Gedcom.Enums
{
    /// <summary>
    /// An enumeration of who adopted a child. Corresponds to the ADOPTED_BY_WHICH_PARENT definition in the GEDCOM specification.
    /// </summary>
    public enum AdoptedByWhichParent
    {
        /// <summary>
        /// Both parents adopted
        /// </summary>
        BOTH,

        /// <summary>
        /// The husband did the adopting
        /// </summary>
        HUSB,

        /// <summary>
        /// The wife did the adopting
        /// </summary>
        WIFE
    }
}
