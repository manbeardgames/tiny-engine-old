/* ----------------------------------------------------------------------------
    MIT License

    Copyright (c) 2020 Christopher Whitley

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
---------------------------------------------------------------------------- */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tiny
{
    /// <summary>
    ///     Extension class for the <see cref="SpriteBatch"/> for rendering 2D primatives.
    /// </summary>
    public static partial class SpriteBatchExtensions
    {
        /// <summary>
        ///     Renders a 1x1 pixel.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="x">
        ///     A <see cref="float"/> value representing the x-coordinate of the
        ///     point.
        /// </param>
        /// <param name="y">
        ///     A <see cref="float"/> value representing the y-coordinate of the
        ///     point.
        /// </param>
        public static void DrawPoint(this SpriteBatch spriteBatch, float x, float y)
        {
            spriteBatch.DrawPoint(new Vector2(x, y), Color.White);
        }

        /// <summary>
        ///     Renders a 1x1 pixel.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="x">
        ///     A <see cref="float"/> value representing the x-coordinate of the
        ///     point.
        /// </param>
        /// <param name="y">
        ///     A <see cref="float"/> value representing the y-coordinate of the
        ///     point.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Color"/> value to use as the color mask when rendering the point.
        /// </param>
        public static void DrawPoint(this SpriteBatch spriteBatch, float x, float y, Color color)
        {
            spriteBatch.DrawPoint(new Vector2(x, y), color);
        }

        /// <summary>
        ///     Renders a 1x1 pixel.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="pos">
        ///     A <see cref="Vector2"/> value representing the xy-coordinate of
        ///     the point.
        /// </param>
        public static void DrawPoint(this SpriteBatch spriteBatch, Vector2 pos)
        {
            spriteBatch.DrawPoint(pos, Color.White);
        }

        /// <summary>
        ///     Renders a 1x1 pixel.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="pos">
        ///     A <see cref="Vector2"/> value representing the xy-coordinate of
        ///     the point.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Color"/> value to use as the color mask when rendering the point.
        /// </param>
        public static void DrawPoint(this SpriteBatch spriteBatch, Vector2 pos, Color color)
        {

            spriteBatch.Draw(texture: Pixel.Texture,
                             position: pos,
                             sourceRectangle: Pixel.SourceRectangle,
                             color: color,
                             rotation: 0.0f,
                             origin: Vector2.Zero,
                             scale: Vector2.One,
                             effects: SpriteEffects.None,
                             layerDepth: 0.0f);
        }
    }
}
