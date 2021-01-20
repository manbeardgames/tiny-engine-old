using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tiny
{
    public class PixelFontCharacter
    {
        //  The dictionary holding the kerning values for this character.
        private Dictionary<int, int> _kerining;

        /// <summary>
        ///     Gets a <see cref="int"/> value defining the Unicode value of the
        ///     <see cref="char"/> this represents.
        /// </summary>
        public int Character { get; }

        /// <summary>
        ///     Gets a <see cref="Texture2D"/> instance that contains the character
        ///     glyph used when rendering.
        /// </summary>
        public Texture2D Texture { get; }

        /// <summary>
        ///     Gets a <see cref="Rectangle"/> value that defines the boundry within
        ///     the <see cref="Texture"/> that contains the glyph.
        /// </summary>
        public Rectangle SourceRectangle { get; }

        /// <summary>
        ///     Gets a <see cref="int"/> value that defines the amount to offset
        ///     on the x-axis when rendering.
        /// </summary>
        public int XOffset { get; }

        /// <summary>
        ///     Gets a <see cref="int"/> value that defines the amount to offset
        ///     on the y-axis when rendering.
        /// </summary>
        public int YOffset { get; }

        /// <summary>
        ///     Gets a <see cref="Vector2"/> value where <see cref="Vector2.X"/> and
        ///     <see cref="Vector2.Y"/> are equal to this <see cref="XOffset"/> and
        ///     <see cref="YOffset"/>
        /// </summary>
        public Vector2 Offset => new Vector2(XOffset, YOffset);

        /// <summary>
        ///     Gets a <see cref="int"/> value that defines the total number of
        ///     pixels to advance on the x-axis after rendering this.
        /// </summary>
        public int XAdvance { get; }

        /// <summary>
        ///     Creates a new <see cref="PixelFontCharacter"/> instance.
        /// </summary>
        /// <param name="character">
        ///     A <see cref="int"/> value that defines the Unicode value of the
        ///     <see cref="char"/> represented by this.
        /// </param>
        /// <param name="texture">
        ///     The <see cref="Texture2D"/> instance that contains the glyph used
        ///     to render this.
        /// </param>
        /// <param name="xml">
        ///     The <see cref="XmlElement"/> instance that contains the data for
        ///     the glpyh.
        /// </param>
        public PixelFontCharacter(int character, Texture2D texture, XmlElement xml)
        {
            Character = character;
            Texture = texture;

            SourceRectangle = new Rectangle
            {
                X = xml.GetIntAttribute("x"),
                Y = xml.GetIntAttribute("y"),
                Width = xml.GetIntAttribute("width"),
                Height = xml.GetIntAttribute("height")
            };

            XOffset = xml.GetIntAttribute("xoffset");
            YOffset = xml.GetIntAttribute("yoffset");
            XAdvance = xml.GetIntAttribute("xadvance");

            _kerining = new Dictionary<int, int>();
        }

        /// <summary>
        ///     Adds a new kerning value to this character.
        /// </summary>
        /// <param name="character">
        ///     A <see cref="int"/> value that defines the Unicode value of the
        ///     <see cref="char"/> that follows immidiatly after this character
        ///     to apply the kerning amount for.
        /// </param>
        /// <param name="amount">
        ///     A <see cref="int"/> value that defines the amount of kerning to
        ///     apply.
        /// </param>
        public void AddKerning(int character, int amount)
        {
            _kerining.Add(character, amount);
        }

        /// <summary>
        ///     Removes an existing kerning value from this character.
        /// </summary>
        /// <param name="character">
        ///     A <see cref="int"/> value that defines the Unicode value of the
        ///     <see cref="char"/> that follows immidiatly after this character
        ///     to remove the kerning amount for.
        /// </param>
        public void RemoveKerning(int character)
        {
            _kerining.Remove(character);
        }

        /// <summary>
        ///     Trys to get the kerning value of a character that follows
        ///     immediatly after this character.
        /// </summary>
        /// <param name="character">
        ///     A <see cref="int"/> value that defines the Unicode value of the
        ///     <see cref="char"/> that follows immidiatly after this character
        ///     that we are getting the kerning value of.
        /// </param>
        /// <param name="value">
        ///     When this method returns, contains the kerning value, if this
        ///     method returns <c>true</c>.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the given character exists within the kerning
        ///     dictionary; otherwise, <c>false</c>.
        /// </returns>
        public bool TryGetKerning(int character, out int value)
        {
            return _kerining.TryGetValue(character, out value);
        }
    }
}
