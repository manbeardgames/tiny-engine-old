using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tiny
{
    public class BMFontInfo
    {
        /// <summary>
        ///     Gets or Sets a <see cref="string"/> value containing the name
        ///     of the font.
        /// </summary>
        public string FontName { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="int"/> value describing the size of
        ///     the font.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="bool"/> value indicating if smoothing was
        ///     enabled when the font was exported from BMFont
        /// </summary>
        public bool IsSmooth { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="bool"/> value indicating if this font
        ///     contains a Unicode character set.
        /// </summary>
        public bool IsUnicode { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="bool"/> value indicating if the characters of
        ///     this font are in italic.
        /// </summary>
        public bool IsItalic { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="bool"/> value indicating if the characters of
        ///     this font are bold.
        /// </summary>
        public bool IsBold { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="bool"/> value indicating if the height of the
        ///     characters are fixed.
        /// </summary>
        public bool IsFixedHeight { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="int"/> value that describes the OEM character set
        ///     of this font. This is only valid if <see cref="IsUnicode"/> is <c>false</c>.
        /// </summary>
        public int CharSet { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="int"/> value that describes the percentage to stretch
        ///     the height of the font. <c>100</c> means there is no stretch.
        /// </summary>
        public int StretchHeight { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="int"/> value that describes the supersampling level
        ///     used when the font was exported from BMFont.  <c>1</c> means no supersampling
        ///     was used.
        /// </summary>
        public int AA { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="int"/> value that describes the amount of padding
        ///     to apply to the top of each character.
        /// </summary>
        public int PaddingTop { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="int"/> value that describes the amount of padding
        ///     to apply to the right of each character.
        /// </summary>
        public int PaddingRight { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="int"/> value that describes the amount of padding
        ///     to apply to the bottom of each character.
        /// </summary>
        public int PaddingBottom { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="int"/> value that describes the amount of padding
        ///     to apply to the left of each character.
        /// </summary>
        public int PaddingLeft { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="Point"/> value who's <see cref="Point.X"/> and
        ///     <see cref="Point.Y"/> values describe the amount of spacing for each character
        ///     on the horizontal and vertical axes respectivly.
        /// </summary>
        public Point Spacing { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="int"/> value that describes the outline thickness
        ///     set for each character when exported from BMFont.
        /// </summary>
        public int Outline { get; set; }
    }
}
