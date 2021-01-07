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
        ///     Renders a line.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="x1"></param>
        ///     A <see cref="float"/> value that represents the starting x-coordinate
        ///     point of the line.
        /// <param name="y1">
        ///     A <see cref="float"/> value that represents the starting y-coordinate
        ///     point of the line.
        /// </param>
        /// <param name="x2">
        ///     A <see cref="float"/> value that represents the ending x-coordinate
        ///     point of the line.
        /// </param>
        /// <param name="y2">
        ///     A <see cref="float"/> value that represents the ending y-coordinate
        ///     point of the line.
        /// </param>
        public static void DrawLine(this SpriteBatch spriteBatch, float x1, float y1, float x2, float y2)
        {
            DrawLine(spriteBatch, new Vector2(x1, y1), new Vector2(x2, y2), Color.White, 1.0f);
        }

        /// <summary>
        ///     Renders a line.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="x1"></param>
        ///     A <see cref="float"/> value that represents the starting x-coordinate
        ///     point of the line.
        /// <param name="y1">
        ///     A <see cref="float"/> value that represents the starting y-coordinate
        ///     point of the line.
        /// </param>
        /// <param name="x2">
        ///     A <see cref="float"/> value that represents the ending x-coordinate
        ///     point of the line.
        /// </param>
        /// <param name="y2">
        ///     A <see cref="float"/> value that represents the ending y-coordinate
        ///     point of the line.
        /// </param>
        /// <param name="thickness">
        ///     A <see cref="float"/> value representing the thickness of the line.
        /// </param>
        public static void DrawLine(this SpriteBatch spriteBatch, float x1, float y1, float x2, float y2, float thickness)
        {
            DrawLine(spriteBatch, new Vector2(x1, y1), new Vector2(x2, y2), Color.White, thickness);
        }

        /// <summary>
        ///     Renders a line.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="x1"></param>
        ///     A <see cref="float"/> value that represents the starting x-coordinate
        ///     point of the line.
        /// <param name="y1">
        ///     A <see cref="float"/> value that represents the starting y-coordinate
        ///     point of the line.
        /// </param>
        /// <param name="x2">
        ///     A <see cref="float"/> value that represents the ending x-coordinate
        ///     point of the line.
        /// </param>
        /// <param name="y2">
        ///     A <see cref="float"/> value that represents the ending y-coordinate
        ///     point of the line.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Color"/> value to use as the color mask when rendering the line.
        /// </param>
        public static void DrawLine(this SpriteBatch spriteBatch, float x1, float y1, float x2, float y2, Color color)
        {
            DrawLine(spriteBatch, new Vector2(x1, y1), new Vector2(x2, y2), color, 1.0f);
        }

        /// <summary>
        ///     Renders a line.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="x1"></param>
        ///     A <see cref="float"/> value that represents the starting x-coordinate
        ///     point of the line.
        /// <param name="y1">
        ///     A <see cref="float"/> value that represents the starting y-coordinate
        ///     point of the line.
        /// </param>
        /// <param name="x2">
        ///     A <see cref="float"/> value that represents the ending x-coordinate
        ///     point of the line.
        /// </param>
        /// <param name="y2">
        ///     A <see cref="float"/> value that represents the ending y-coordinate
        ///     point of the line.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Color"/> value to use as the color mask when rendering the line.
        /// </param>
        /// <param name="thickness">
        ///     A <see cref="float"/> value representing the thickness of the line.
        /// </param>
        public static void DrawLine(this SpriteBatch spriteBatch, float x1, float y1, float x2, float y2, Color color, float thickness)
        {
            DrawLine(spriteBatch, new Vector2(x1, y1), new Vector2(x2, y2), color, thickness);
        }

        /// <summary>
        ///     Renders a line.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="start">
        ///     A <see cref="Vector2"/> value representing the starting xy-coordinate point
        ///     of the line.
        /// </param>
        /// <param name="end">
        ///     A <see cref="Vector2"/> value representing the ending xy-coordinate point
        ///     of the line.
        /// </param>
        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 start, Vector2 end)
        {
            DrawLine(spriteBatch, start, end, Color.White, 1.0f);
        }

        /// <summary>
        ///     Renders a line.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="start">
        ///     A <see cref="Vector2"/> value representing the starting xy-coordinate point
        ///     of the line.
        /// </param>
        /// <param name="end">
        ///     A <see cref="Vector2"/> value representing the ending xy-coordinate point
        ///     of the line.
        /// </param>
        /// <param name="thickness">
        ///     A <see cref="float"/> value representing the thickness of the line.
        /// </param>
        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 start, Vector2 end, float thickness)
        {
            DrawLine(spriteBatch, start, end, Color.White, thickness);
        }

        /// <summary>
        ///     Renders a line.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="start">
        ///     A <see cref="Vector2"/> value representing the starting xy-coordinate point
        ///     of the line.
        /// </param>
        /// <param name="end">
        ///     A <see cref="Vector2"/> value representing the ending xy-coordinate point
        ///     of the line.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Color"/> value to use as the color mask when rendering the line.
        /// </param>
        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
        {
            DrawLine(spriteBatch, start, end, color, 1.0f);
        }

        /// <summary>
        ///     Renders a line.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="start">
        ///     A <see cref="Vector2"/> value representing the starting xy-coordinate point
        ///     of the line.
        /// </param>
        /// <param name="end">
        ///     A <see cref="Vector2"/> value representing the ending xy-coordinate point
        ///     of the line.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Color"/> value to use as the color mask when rendering the line.
        /// </param>
        /// <param name="thickness">
        ///     A <see cref="float"/> value representing the thickness of the line.
        /// </param>
        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, float thickness)
        {
            float distance = Vector2.Distance(start, end);
            float angle = Maths.Angle(start, end);
            spriteBatch.DrawLineAngle(start, angle, distance, color, thickness);
        }
    }
}
