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
        ///     Reanders a filled rectangle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="rectangle">
        ///     A <see cref="Rectangle"/> value that describes the bounds of the
        ///     rectangle to render.
        /// </param>
        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rectangle)
        {
            DrawRectangle(spriteBatch, rectangle, Color.White);
        }

        /// <summary>
        ///     Reanders a filled rectangle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="rectangle">
        ///     A <see cref="Rectangle"/> value that describes the bounds of the
        ///     rectangle to render.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Color"/> value to use as the color mask when rendering the  rectangle.
        /// </param>
        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            spriteBatch.Draw(texture: Pixel,
                             destinationRectangle: rectangle,
                             sourceRectangle: new Rectangle(0, 0, Pixel.Width, Pixel.Height),
                             color: color);
        }

        /// <summary>
        ///     Reanders a filled rectangle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="x">
        ///     A <see cref="float"/> value that represents the top-left x-coordiante
        ///     position of the rectangle to rener.
        /// </param>
        /// <param name="y">
        ///     A <see cref="float"/> value that represents the top-left y-coordiante
        ///     position of the rectnagle to render.
        /// </param>
        /// <param name="width">
        ///     A <see cref="float"/> value that represents the width, in pixels, of
        ///     the rectangle to render.
        /// </param>
        /// <param name="height">
        ///     A <see cref="float"/> value that represents the height, in pixels, of
        ///     the rectangle to render.
        /// </param>
        public static void DrawRectangle(this SpriteBatch spriteBatch, float x, float y, float width, float height)
        {
            DrawRectangle(spriteBatch, new Vector2(x, y), new Vector2(width, height), Color.White);
        }

        /// <summary>
        ///     Reanders a filled rectangle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="x">
        ///     A <see cref="float"/> value that represents the top-left x-coordiante
        ///     position of the rectangle to rener.
        /// </param>
        /// <param name="y">
        ///     A <see cref="float"/> value that represents the top-left y-coordiante
        ///     position of the rectnagle to render.
        /// </param>
        /// <param name="width">
        ///     A <see cref="float"/> value that represents the width, in pixels, of
        ///     the rectangle to render.
        /// </param>
        /// <param name="height">
        ///     A <see cref="float"/> value that represents the height, in pixels, of
        ///     the rectangle to render.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Color"/> value to use as the color mask when rendering the  rectangle.
        /// </param>
        public static void DrawRectangle(this SpriteBatch spriteBatch, float x, float y, float width, float height, Color color)
        {
            DrawRectangle(spriteBatch, new Vector2(x, y), new Vector2(width, height), color);
        }

        /// <summary>
        ///     Reanders a filled rectangle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="topLeft">
        ///     A <see cref="Vector2"/> value that represents the top-left xy-coordinate
        ///     point of the rectangle to render.
        /// </param>
        /// <param name="size">
        ///     A <see cref="Vector2"/> value that represents the width and height, in
        ///     pixels, of the rectangle to render.
        /// </param>
        public static void DrawRectangle(this SpriteBatch spriteBatch, Vector2 topLeft, Vector2 size)
        {
            DrawRectangle(spriteBatch, topLeft, size, Color.White);
        }

        /// <summary>
        ///     Reanders a filled rectangle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="topLeft">
        ///     A <see cref="Vector2"/> value that represents the top-left xy-coordinate
        ///     point of the rectangle to render.
        /// </param>
        /// <param name="size">
        ///     A <see cref="Vector2"/> value that represents the width and height, in
        ///     pixels, of the rectangle to render.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Color"/> value to use as the color mask when rendering the rectangle.
        /// </param>
        public static void DrawRectangle(this SpriteBatch spriteBatch, Vector2 topLeft, Vector2 size, Color color)
        {
            spriteBatch.Draw(texture: Pixel,
                             position: topLeft,
                             sourceRectangle: new Rectangle(0, 0, Pixel.Width, Pixel.Height),
                             color: color,
                             rotation: 0.0f,
                             origin: Vector2.Zero,
                             scale: size,
                             effects: SpriteEffects.None,
                             layerDepth: 0.0f);
        }
    }
}
