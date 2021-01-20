using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tiny
{
    public class BMFontCommon
    {
        /// <summary>
        ///     Gets or Sets a <see cref="int"/> value that describes the distance
        ///     in pixels between each line of text.
        /// </summary>
        public int LineHeight { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="int"/> value that describes the number of
        ///     pixels from the absolute top of the lien to the base of each character.
        /// </summary>
        public int Base { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="Point"/> value who's <see cref="Point.X"/> and
        ///     <see cref="Point.Y"/> values desribes the widht and height respectivly of the
        ///     texture, normally used to scale the x or y position of the character image.
        /// </summary>
        public Point Scale { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="int"/> valuet that describes the total number
        ///     of texture pages included for the font.
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="bool"/> value that describes if monochrom
        ///     characters have been packed into each of the texture channels.  If
        ///     <c>true</c>, the <see cref="AlphaChannel"/> describes what is stored
        ///     in each channel.
        /// </summary>
        public bool Packed { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="int"/> value that describes what type of data
        ///     is stored in the alpha channel of the font texture.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Per the BMFont documentation
        ///     </para>
        ///     <para>
        ///         <c>0</c> means the alpha channel holds the glyph data.
        ///     </para>
        ///     <para>
        ///         <c>1</c> means the alpha channel holds the outline data.
        ///     </para>
        ///     <para>
        ///         <c>2</c> means the alpha channel holds the glyph and outline data.
        ///     </para>
        ///     <para>
        ///         <c>3</c> means it is set to 0
        ///     </para>
        ///     <para>
        ///         <c>4</c> means it is set to 1
        ///     </para>
        /// </remarks>
        public int AlphaChannel { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="int"/> value that describes what type of data
        ///     is stored in the red channel of the font texture.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Per the BMFont documentation
        ///     </para>
        ///     <para>
        ///         <c>0</c> means the red channel holds the glyph data.
        ///     </para>
        ///     <para>
        ///         <c>1</c> means the red channel holds the outline data.
        ///     </para>
        ///     <para>
        ///         <c>2</c> means the red channel holds the glyph and outline data.
        ///     </para>
        ///     <para>
        ///         <c>3</c> means it is set to 0
        ///     </para>
        ///     <para>
        ///         <c>4</c> means it is set to 1
        ///     </para>
        /// </remarks>
        public int RedChannel { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="int"/> value that describes what type of data
        ///     is stored in the green channel of the font texture.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Per the BMFont documentation
        ///     </para>
        ///     <para>
        ///         <c>0</c> means the green channel holds the glyph data.
        ///     </para>
        ///     <para>
        ///         <c>1</c> means the green channel holds the outline data.
        ///     </para>
        ///     <para>
        ///         <c>2</c> means the green channel holds the glyph and outline data.
        ///     </para>
        ///     <para>
        ///         <c>3</c> means it is set to 0
        ///     </para>
        ///     <para>
        ///         <c>4</c> means it is set to 1
        ///     </para>
        /// </remarks>
        public int GreenChannel { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="int"/> value that describes what type of data
        ///     is stored in the blue channel of the font texture.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Per the BMFont documentation
        ///     </para>
        ///     <para>
        ///         <c>0</c> means the blue channel holds the glyph data.
        ///     </para>
        ///     <para>
        ///         <c>1</c> means the blue channel holds the outline data.
        ///     </para>
        ///     <para>
        ///         <c>2</c> means the blue channel holds the glyph and outline data.
        ///     </para>
        ///     <para>
        ///         <c>3</c> means it is set to 0
        ///     </para>
        ///     <para>
        ///         <c>4</c> means it is set to 1
        ///     </para>
        /// </remarks>
        public int BlueChannel { get; set; }
    }
}
