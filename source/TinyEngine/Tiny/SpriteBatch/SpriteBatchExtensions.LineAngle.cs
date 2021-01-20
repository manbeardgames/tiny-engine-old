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
        ///     Renders a line at an angle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="x">
        ///     A <see cref="float"/> value representing the staring x-coordinate point
        ///     of the line.
        /// </param>
        /// <param name="y">
        ///     A <see cref="float"/> value representing the staring y-coordinate point
        ///     of the line.
        /// </param>
        /// <param name="angle">
        ///     A <see cref="float"/> value representing the angle of the line, in radians.
        /// </param>
        /// <param name="length">
        ///     A <see cref="float"/> value representing the length of the line.
        /// </param>
        public static void DrawLineAngle(this SpriteBatch spriteBatch, float x, float y, float angle, float length)
        {
            DrawLineAngle(spriteBatch, new Vector2(x, y), angle, length, Color.White, 1.0f);
        }

        /// <summary>
        ///     Renders a line at an angle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="x">
        ///     A <see cref="float"/> value representing the staring x-coordinate point
        ///     of the line.
        /// </param>
        /// <param name="y">
        ///     A <see cref="float"/> value representing the staring y-coordinate point
        ///     of the line.
        /// </param>
        /// <param name="angle">
        ///     A <see cref="float"/> value representing the angle of the line, in radians.
        /// </param>
        /// <param name="length">
        ///     A <see cref="float"/> value representing the length of the line.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Color"/> value to use as the color mask when rendering the line.
        /// </param>
        public static void DrawLineAngle(this SpriteBatch spriteBatch, float x, float y, float angle, float length, Color color)
        {
            DrawLineAngle(spriteBatch, new Vector2(x, y), angle, length, color, 1.0f);
        }

        /// <summary>
        ///     Renders a line at an angle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="x">
        ///     A <see cref="float"/> value representing the staring x-coordinate point
        ///     of the line.
        /// </param>
        /// <param name="y">
        ///     A <see cref="float"/> value representing the staring y-coordinate point
        ///     of the line.
        /// </param>
        /// <param name="angle">
        ///     A <see cref="float"/> value representing the angle of the line, in radians.
        /// </param>
        /// <param name="length">
        ///     A <see cref="float"/> value representing the length of the line.
        /// </param>
        /// <param name="thickness">
        ///     A <see cref="float"/> value representing the thickness of the line.
        /// </param>
        public static void DrawLineAngle(this SpriteBatch spriteBatch, float x, float y, float angle, float length, float thickness)
        {
            DrawLineAngle(spriteBatch, new Vector2(x, y), angle, length, Color.White, thickness);
        }

        /// <summary>
        ///     Renders a line at an angle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="x">
        ///     A <see cref="float"/> value representing the staring x-coordinate point
        ///     of the line.
        /// </param>
        /// <param name="y">
        ///     A <see cref="float"/> value representing the staring y-coordinate point
        ///     of the line.
        /// </param>
        /// <param name="angle">
        ///     A <see cref="float"/> value representing the angle of the line, in radians.
        /// </param>
        /// <param name="length">
        ///     A <see cref="float"/> value representing the length of the line.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Color"/> value to use as the color mask when rendering the line.
        /// </param>
        /// <param name="thickness">
        ///     A <see cref="float"/> value representing the thickness of the line.
        /// </param>
        public static void DrawLineAngle(this SpriteBatch spriteBatch, float x, float y, float angle, float length, Color color, float thickness)
        {
            DrawLineAngle(spriteBatch, new Vector2(x, y), angle, length, color, thickness);
        }

        /// <summary>
        ///     Renders a line at an angle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="start">
        ///     A <see cref="Vector2"/> value representing the starting xy-coordiante
        ///     piont of the line.
        /// </param>
        /// <param name="angle">
        ///     A <see cref="float"/> value representing the angle of the line, in radians.
        /// </param>
        /// <param name="length">
        ///     The length of the line.
        /// </param>
        public static void DrawLineAngle(this SpriteBatch spriteBatch, Vector2 start, float angle, float length)
        {
            DrawLineAngle(spriteBatch, start, angle, length, Color.White, 1.0f);
        }

        /// <summary>
        ///     Renders a line at an angle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="start">
        ///     A <see cref="Vector2"/> value representing the starting xy-coordiante
        ///     piont of the line.
        /// </param>
        /// <param name="angle">
        ///     A <see cref="float"/> value representing the angle of the line, in radians.
        /// </param>
        /// <param name="length">
        ///     The length of the line.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Color"/> value to use as the color mask when rendering the line.
        /// </param>
        public static void DrawLineAngle(this SpriteBatch spriteBatch, Vector2 start, float angle, float length, Color color)
        {
            DrawLineAngle(spriteBatch, start, angle, length, color, 1.0f);
        }

        /// <summary>
        ///     Renders a line at an angle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="start">
        ///     A <see cref="Vector2"/> value representing the starting xy-coordiante
        ///     piont of the line.
        /// </param>
        /// <param name="angle">
        ///     A <see cref="float"/> value representing the angle of the line, in radians.
        /// </param>
        /// <param name="length">
        ///     The length of the line.
        /// </param>
        /// <param name="thickness">
        ///     A <see cref="float"/> value representing the thickness of the line.
        /// </param>
        public static void DrawLineAngle(this SpriteBatch spriteBatch, Vector2 start, float angle, float length, float thickness)
        {
            DrawLineAngle(spriteBatch, start, angle, length, Color.White, thickness);
        }

        /// <summary>
        ///     Renders a line at an angle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="start">
        ///     A <see cref="Vector2"/> value representing the starting xy-coordiante
        ///     piont of the line.
        /// </param>
        /// <param name="angle">
        ///     A <see cref="float"/> value representing the angle of the line, in radians.
        /// </param>
        /// <param name="length">
        ///     The length of the line.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Color"/> value to use as the color mask when rendering the line.
        /// </param>
        /// <param name="thickness">
        ///     A <see cref="float"/> value representing the thickness of the line.
        /// </param>
        public static void DrawLineAngle(this SpriteBatch spriteBatch, Vector2 start, float angle, float length, Color color, float thickness)
        {
            spriteBatch.Draw(texture: Pixel.Texture,
                             position: start,
                             sourceRectangle: Pixel.SourceRectangle,
                             color: color,
                             rotation: angle,
                             origin: thickness > 1.0f ? new Vector2(0.0f, 0.5f) : Vector2.Zero,
                             scale: new Vector2(length, thickness),
                             effects: SpriteEffects.None,
                             layerDepth: 0.0f);
        }
    }
}
