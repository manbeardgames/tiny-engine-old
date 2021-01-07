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
        ///     Renders a hollow circle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="centerX">
        ///     A <see cref="float"/> value representing the x-coordinate point of the center
        ///     of the circle to render.
        /// </param>
        /// <param name="centerY">
        ///     A <see cref="float"/> value representing the y-coordinate point of the center
        ///     of the circle to render.
        /// </param>
        /// <param name="radius">
        ///     A <see cref="float"/> value representing the radius of the circle.
        /// </param>
        /// <param name="resolution">
        ///     A <see cref="int"/> value representing to total number of perpendicular points on
        ///     the circle to create per quadrant to draw line between to create the circle. A higher
        ///     value creates a smoother rendered circle, but requires more draw calls.
        /// </param>
        public static void DrawCircle(this SpriteBatch spriteBatch, float centerX, float centerY, float radius, int resolution)
        {
            spriteBatch.DrawCircle(new Vector2(centerX, centerY), radius, resolution, Color.White, 1.0f);
        }

        /// <summary>
        ///     Renders a hollow circle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="centerX">
        ///     A <see cref="float"/> value representing the x-coordinate point of the center
        ///     of the circle to render.
        /// </param>
        /// <param name="centerY">
        ///     A <see cref="float"/> value representing the y-coordinate point of the center
        ///     of the circle to render.
        /// </param>
        /// <param name="radius">
        ///     A <see cref="float"/> value representing the radius of the circle.
        /// </param>
        /// <param name="resolution">
        ///     A <see cref="int"/> value representing to total number of perpendicular points on
        ///     the circle to create per quadrant to draw line between to create the circle. A higher
        ///     value creates a smoother rendered circle, but requires more draw calls.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Color"/> value to use as the color mask when rendering the circle.
        /// </param>
        public static void DrawCircle(this SpriteBatch spriteBatch, float centerX, float centerY, float radius, int resolution, Color color)
        {
            spriteBatch.DrawCircle(new Vector2(centerX, centerY), radius, resolution, color, 1.0f);
        }

        /// <summary>
        ///     Renders a hollow circle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="centerX">
        ///     A <see cref="float"/> value representing the x-coordinate point of the center
        ///     of the circle to render.
        /// </param>
        /// <param name="centerY">
        ///     A <see cref="float"/> value representing the y-coordinate point of the center
        ///     of the circle to render.
        /// </param>
        /// <param name="radius">
        ///     A <see cref="float"/> value representing the radius of the circle.
        /// </param>
        /// <param name="resolution">
        ///     A <see cref="int"/> value representing to total number of perpendicular points on
        ///     the circle to create per quadrant to draw line between to create the circle. A higher
        ///     value creates a smoother rendered circle, but requires more draw calls.
        /// </param>
        /// <param name="thickness">
        ///     A <see cref="float"/> value representing the thickness of the lines rendered to
        ///     create the perimiter of the circle.
        /// </param>
        public static void DrawCircle(this SpriteBatch spriteBatch, float centerX, float centerY, float radius, int resolution, float thickness)
        {
            spriteBatch.DrawCircle(new Vector2(centerX, centerY), radius, resolution, Color.White, thickness);
        }

        /// <summary>
        ///     Renders a hollow circle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="centerX">
        ///     A <see cref="float"/> value representing the x-coordinate point of the center
        ///     of the circle to render.
        /// </param>
        /// <param name="centerY">
        ///     A <see cref="float"/> value representing the y-coordinate point of the center
        ///     of the circle to render.
        /// </param>
        /// <param name="radius">
        ///     A <see cref="float"/> value representing the radius of the circle.
        /// </param>
        /// <param name="resolution">
        ///     A <see cref="int"/> value representing to total number of perpendicular points on
        ///     the circle to create per quadrant to draw line between to create the circle. A higher
        ///     value creates a smoother rendered circle, but requires more draw calls.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Color"/> value to use as the color mask when rendering the circle.
        /// </param>
        /// <param name="thickness">
        ///     A <see cref="float"/> value representing the thickness of the lines rendered to
        ///     create the perimiter of the circle.
        /// </param>
        public static void DrawCircle(this SpriteBatch spriteBatch, float centerX, float centerY, float radius, int resolution, Color color, float thickness)
        {
            spriteBatch.DrawCircle(new Vector2(centerX, centerY), radius, resolution, color, thickness);
        }

        /// <summary>
        ///     Renders a hollow circle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="centerPosition">
        ///     A <see cref="Vector2"/> value that represents the xy-coordinate point of
        ///     the center of the circle.
        /// </param>
        /// <param name="radius">
        ///     A <see cref="float"/> value representing the radius of the circle.
        /// </param>
        /// <param name="resolution">
        ///     A <see cref="int"/> value representing to total number of perpendicular points on
        ///     the circle to create per quadrant to draw line between to create the circle. A higher
        ///     value creates a smoother rendered circle, but requires more draw calls.
        /// </param>
        public static void DrawCircle(this SpriteBatch spriteBatch, Vector2 centerPosition, float radius, int resolution)
        {
            spriteBatch.DrawCircle(centerPosition, radius, resolution, Color.White, 1.0f);
        }

        /// <summary>
        ///     Renders a hollow circle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="centerPosition">
        ///     A <see cref="Vector2"/> value that represents the xy-coordinate point of
        ///     the center of the circle.
        /// </param>
        /// <param name="radius">
        ///     A <see cref="float"/> value representing the radius of the circle.
        /// </param>
        /// <param name="resolution">
        ///     A <see cref="int"/> value representing to total number of perpendicular points on
        ///     the circle to create per quadrant to draw line between to create the circle. A higher
        ///     value creates a smoother rendered circle, but requires more draw calls.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Color"/> value to use as the color mask when rendering the circle.
        /// </param>
        public static void DrawCircle(this SpriteBatch spriteBatch, Vector2 centerPosition, float radius, int resolution, Color color)
        {
            spriteBatch.DrawCircle(centerPosition, radius, resolution, color, 1.0f);
        }

        /// <summary>
        ///     Renders a hollow circle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="centerPosition">
        ///     A <see cref="Vector2"/> value that represents the xy-coordinate point of
        ///     the center of the circle.
        /// </param>
        /// <param name="radius">
        ///     A <see cref="float"/> value representing the radius of the circle.
        /// </param>
        /// <param name="resolution">
        ///     A <see cref="int"/> value representing to total number of perpendicular points on
        ///     the circle to create per quadrant to draw line between to create the circle. A higher
        ///     value creates a smoother rendered circle, but requires more draw calls.
        /// </param>
        /// <param name="thickness">
        ///     A <see cref="float"/> value representing the thickness of the lines rendered to
        ///     create the perimiter of the circle.
        /// </param>
        public static void DrawCircle(this SpriteBatch spriteBatch, Vector2 centerPosition, float radius, int resolution, float thickness)
        {
            spriteBatch.DrawCircle(centerPosition, radius, resolution, Color.White, thickness);
        }

        /// <summary>
        ///     Renders a hollow circle.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="centerPosition">
        ///     A <see cref="Vector2"/> value that represents the xy-coordinate point of
        ///     the center of the circle.
        /// </param>
        /// <param name="radius">
        ///     A <see cref="float"/> value representing the radius of the circle.
        /// </param>
        /// <param name="resolution">
        ///     A <see cref="int"/> value representing to total number of perpendicular points on
        ///     the circle to create per quadrant to draw line between to create the circle. A higher
        ///     value creates a smoother rendered circle, but requires more draw calls.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Color"/> value to use as the color mask when rendering the circle.
        /// </param>
        /// <param name="thickness">
        ///     A <see cref="float"/> value representing the thickness of the lines rendered to
        ///     create the perimiter of the circle.
        /// </param>
        public static void DrawCircle(this SpriteBatch spriteBatch, Vector2 centerPosition, float radius, int resolution, Color color, float thickness)
        {
            Vector2 lastPoint = Vector2.UnitX * radius;
            Vector2 lastPerpendicularPoint = Maths.Perpendicular(lastPoint);

            for (int i = 1; i <= resolution; i++)
            {
                Vector2 point = Maths.AngleToVector(i * MathHelper.PiOver2 / resolution, radius);
                Vector2 perpendicularPoint = Maths.Perpendicular(point);

                spriteBatch.DrawLine(centerPosition + lastPoint, centerPosition + point, color, thickness);
                spriteBatch.DrawLine(centerPosition - lastPoint, centerPosition - point, color, thickness);
                spriteBatch.DrawLine(centerPosition + lastPerpendicularPoint, centerPosition + perpendicularPoint, color, thickness);
                spriteBatch.DrawLine(centerPosition - lastPerpendicularPoint, centerPosition - perpendicularPoint, color, thickness);

                lastPoint = point;
                lastPerpendicularPoint = perpendicularPoint;
            }
        }
    }
}
