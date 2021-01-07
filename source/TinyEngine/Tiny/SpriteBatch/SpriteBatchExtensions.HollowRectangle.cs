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
        ///     Draws a hollow rectangle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance used for rendering.
        /// </param>
        /// <param name="rect">
        ///     A <see cref="Rectangle"/> value that describes the bounds of the hollow
        ///     rectangle to render.
        /// </param>
        public static void DrawHollowRectangle(this SpriteBatch spriteBatch, Rectangle rect)
        {
            spriteBatch.DrawHollowRectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }

        /// <summary>
        ///     Renders a hollow rectangle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="x">
        ///     A <see cref="float"/> value that represents the top-left x-coordiante
        ///     position of the hollow rectangle to rener.
        /// </param>
        /// <param name="y">
        ///     A <see cref="float"/> value that represents the top-left y-coordiante
        ///     position of the hollow rectnagle to render.
        /// </param>
        /// <param name="width">
        ///     A <see cref="float"/> value that represents the width, in pixels, of
        ///     the hollow rectangle to render.
        /// </param>
        /// <param name="height">
        ///     A <see cref="float"/> value that represents the height, in pixels, of
        ///     the hollow rectangle to render.
        /// </param>
        public static void DrawHollowRectangle(this SpriteBatch spriteBatch, float x, float y, float width, float height)
        {
            spriteBatch.DrawHollowRectangle(new Vector2(x, y), new Vector2(width, height), Color.White);
        }

        /// <summary>
        ///     Renders a hollow rectangle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="x">
        ///     A <see cref="float"/> value that represents the top-left x-coordiante
        ///     position of the hollow rectangle to rener.
        /// </param>
        /// <param name="y">
        ///     A <see cref="float"/> value that represents the top-left y-coordiante
        ///     position of the hollow rectnagle to render.
        /// </param>
        /// <param name="width">
        ///     A <see cref="float"/> value that represents the width, in pixels, of
        ///     the hollow rectangle to render.
        /// </param>
        /// <param name="height">
        ///     A <see cref="float"/> value that represents the height, in pixels, of
        ///     the hollow rectangle to render.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Color"/> value to use as the color mask when rendering the hollow rectangle.
        /// </param>
        public static void DrawHollowRectangle(this SpriteBatch spriteBatch, float x, float y, float width, float height, Color color)
        {
            spriteBatch.DrawHollowRectangle(new Vector2(x, y), new Vector2(width, height), color);
        }

        /// <summary>
        ///     Renders a hollow rectangle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="topLeft">
        ///     A <see cref="Vector2"/> value that represents the top-left xy-coordinate
        ///     point of the hollow rectangle to render.
        /// </param>
        /// <param name="size">
        ///     A <see cref="Vector2"/> value that represents the width and height, in
        ///     pixels, of the hollow rectangle to render.
        /// </param>
        public static void DrawHollowRectangle(this SpriteBatch spriteBatch, Vector2 topLeft, Vector2 size)
        {
            spriteBatch.DrawHollowRectangle(topLeft, size, Color.White);
        }

        /// <summary>
        ///     Renders a hollow rectangle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="topLeft">
        ///     A <see cref="Vector2"/> value that represents the top-left xy-coordinate
        ///     point of the hollow rectangle to render.
        /// </param>
        /// <param name="size">
        ///     A <see cref="Vector2"/> value that represents the width and height, in
        ///     pixels, of the hollow rectangle to render.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Color"/> value to use as the color mask when rendering the hollow rectangle.
        /// </param>
        public static void DrawHollowRectangle(this SpriteBatch spriteBatch, Vector2 topLeft, Vector2 size, Color color)
        {
            Vector2 topRight = topLeft + (Vector2.UnitX * size);
            Vector2 bottomRight = topRight + (Vector2.UnitY * size);
            Vector2 bottomLeft = bottomRight - (Vector2.UnitX * size);

            spriteBatch.DrawLine(topLeft, topRight, color);
            spriteBatch.DrawLine(topRight, bottomRight, color);
            spriteBatch.DrawLine(bottomRight, bottomLeft, color);
            spriteBatch.DrawLine(bottomLeft, topLeft, color);
        }
    }
}
