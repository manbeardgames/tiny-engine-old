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
using System;

namespace Tiny
{
    public static partial class Maths
    {

        /// <summary>
        ///     Given the x and y coordinate location of a vector, calculates
        ///     the angel, in radians, of the vector.
        /// </summary>
        /// <param name="x">
        ///     An <see cref="int"/> value representing the x-coordiante location
        ///     of the vector.
        /// </param>
        /// <param name="y">
        ///     An <see cref="int"/> value representing the y-coordinate location
        ///     of the vector.
        /// </param>
        /// <returns>
        ///     A <see cref="float"/> value which is the angel, in radians, of the
        ///     vector located at the x and y coordinates given.
        /// </returns>
        public static float Angle(int x, int y)
        {
            return (float)Math.Atan2(y, x);
        }

        /// <summary>
        ///     Given the x and y coordinate location of a vector, calculates
        ///     the angel, in radians, of the vector.
        /// </summary>
        /// <param name="x">
        ///     An <see cref="float"/> value representing the x-coordiante location
        ///     of the vector.
        /// </param>
        /// <param name="y">
        ///     An <see cref="float"/> value representing the y-coordinate location
        ///     of the vector.
        /// </param>
        /// <returns>
        ///     A <see cref="float"/> value which is the angel, in radians, of the
        ///     vector located at the x and y coordinates given.
        /// </returns>
        public static float Angle(float x, float y)
        {
            return (float)Math.Atan2(y, x);
        }

        /// <summary>
        ///     Calculates the angle, in radians, of a <see cref="Point"/> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="Point"/> value to calculate the angle of.
        /// </param>
        /// <returns>
        ///     A <see cref="float"/> value which is the angle, in radians, of the
        ///     <see cref="Point"/> value given.
        /// </returns>
        public static float Angle(this Point value)
        {
            return (float)Math.Atan2(value.Y, value.X);
        }

        /// <summary>
        ///     Calculates the angle, in radians, of a <see cref="Vector2"/> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="Vector2"/> value to calculate the angle of.
        /// </param>
        /// <returns>
        ///     A <see cref="float"/> value which is the angle, in radians, of the
        ///     <see cref="Vector2"/> value given.
        /// </returns>
        public static float Angle(this Vector2 value)
        {
            return (float)Math.Atan2(value.Y, value.X);
        }

        /// <summary>
        ///     Calculates the angle in radians between two points.
        /// </summary>
        /// <param name="x1">
        ///     A <see cref="int"/> value representing the x-coordinate position
        ///     of the starting point.
        /// </param>
        /// <param name="y1">
        ///     A <see cref="int"/> value representing the y-coordinate position
        ///     of the starting point.
        /// </param>
        /// <param name="x2">
        ///     A <see cref="int"/> value representing the x-coordinate position
        ///     of the ending point.
        /// </param>
        /// <param name="y2">
        ///     A <see cref="int"/> value representing the y-coordinate position
        ///     of the ending point.
        /// </param>
        /// <returns>
        ///     A <see cref="float"/> value containing the angle, in radians, of
        ///     the given points.
        /// </returns>
        public static float Angle(int x1, int y1, int x2, int y2)
        {
            return (float)Math.Atan2(y2 - y1, x2 - x1);
        }

        /// <summary>
        ///     Calculates the angle in radians between two points.
        /// </summary>
        /// <param name="x1">
        ///     A <see cref="float"/> value representing the x-coordinate position
        ///     of the starting point.
        /// </param>
        /// <param name="y1">
        ///     A <see cref="float"/> value representing the y-coordinate position
        ///     of the starting point.
        /// </param>
        /// <param name="x2">
        ///     A <see cref="float"/> value representing the x-coordinate position
        ///     of the ending point.
        /// </param>
        /// <param name="y2">
        ///     A <see cref="float"/> value representing the y-coordinate position
        ///     of the ending point.
        /// </param>
        /// <returns>
        ///     A <see cref="float"/> value containing the angle, in radians, of
        ///     the given points.
        /// </returns>
        public static float Angle(float x1, float y1, float x2, float y2)
        {
            return (float)Math.Atan2(y2 - y1, x2 - x1);
        }

        /// <summary>
        ///     Calculates the angle in radians between two points.
        /// </summary>
        /// <param name="from">
        ///     A <see cref="Point"/> value reperesnting the starting xy-coordinate
        ///     position of the starting point.
        /// </param>
        /// <param name="to">
        ///     A <see cref="Point"/> value reperesnting the starting xy-coordinate
        ///     position of the ending point.
        /// </param>
        /// <returns>
        ///     A <see cref="float"/> value containing the angle, in radians, of
        ///     the given points.
        /// </returns>
        public static float Angle(Point from, Point to)
        {
            return Angle(from.X, from.Y, to.X, to.Y);
        }
        /// <summary>
        ///     Calculates the angle in radians between two points.
        /// </summary>
        /// <param name="from">
        ///     A <see cref="Vector2"/> value reperesnting the starting xy-coordinate
        ///     position of the starting point.
        /// </param>
        /// <param name="to">
        ///     A <see cref="Vector2"/> value reperesnting the starting xy-coordinate
        ///     position of the ending point.
        /// </param>
        /// <returns>
        ///     A <see cref="float"/> value containing the angle, in radians, of
        ///     the given points.
        /// </returns>
        public static float Angle(Vector2 from, Vector2 to)
        {
            return Angle(from.X, from.Y, to.X, to.Y);
        }

        /// <summary>
        ///     Calcualtes a <see cref="Vector2"/> value based the angle, in radians,
        ///     and length of the vector.
        /// </summary>
        /// <param name="radians">
        ///     A <see cref="float"/> value representing the angle of the vector, in radians.
        /// </param>
        /// <param name="length">
        ///     A <see cref="float"/> value representing the length of the vector.
        /// </param>
        /// <returns></returns>
        public static Vector2 AngleToVector(float radians, float length)
        {
            float x = (float)Math.Cos(radians) * length;
            float y = (float)Math.Sin(radians) * length;
            return new Vector2(x, y);
        }

        /// <summary>
        ///     Calcualtes the perpendicular xy-coordiante position of a <see cref="Vector2"/>
        ///     value.
        /// </summary>
        /// <param name="vector2">
        ///     The <see cref="Vector2"/> value to calculate hte perpendicular position of.
        /// </param>
        /// <returns>
        ///     A <see cref="Vector2"/> value.
        /// </returns>
        public static Vector2 Perpendicular(Vector2 vector2)
        {
            return new Vector2(-vector2.Y, vector2.X);
        }

        /// <summary>
        ///     Given a vector and number of slices, calculates a new vector that is
        ///     snapped to the angle of a slice within a unit circle.
        /// </summary>
        /// <param name="vector">
        ///     A <see cref="Vector2"/> value to snap.
        /// </param>
        /// <param name="slices">
        ///     A <see cref="float"/> value indicating the number of times to evenly
        ///     slice a unit circle to create the angles to snap to.
        /// </param>
        /// <returns>
        ///     A <see cref="Vector2"/> value.
        /// </returns>
        public static Vector2 Snap(this Vector2 vector, float slices)
        {
            float dividers = MathHelper.TwoPi / slices;
            float angle = vector.Angle();
            float snappedAngle = (float)Math.Floor((angle + dividers / 2.0f) / dividers) * dividers;
            return AngleToVector(snappedAngle, vector.Length());
        }

        /// <summary>
        ///     Given a vector and number of slices, calculates a new vector that is
        ///     normalized and snapped to the angle of a slice within a unit circle
        /// </summary>
        /// <param name="vector">
        ///     A <see cref="Vector2"/> value to normalize and snap.
        /// </param>
        /// <param name="slices">
        ///     A <see cref="float"/> value indicating the number of times to evenly
        ///     slice a unit circle to create the angles to snap to.
        /// </param>
        /// <returns>
        ///     A <see cref="Vector2"/> value.
        /// </returns>
        public static Vector2 SnapNormal(this Vector2 vector, float slices)
        {
            float dividers = MathHelper.TwoPi / slices;
            float angle = vector.Angle();
            float snappedAngle = (float)Math.Floor((angle + dividers / 2.0f) / dividers) * dividers;
            return AngleToVector(snappedAngle, 1.0f);
        }

        /// <summary>
        ///     Gets half the <see cref="Vector2"/> value given.
        /// </summary>
        /// <param name="vector">
        ///     A <see cref="Vector2"/> value to half.
        /// </param>
        /// <returns>
        ///     A <see cref="Vector2"/> value that is half the original.
        /// </returns>
        public static Vector2 HalfValue(this Vector2 vector)
        {
            return vector * 0.5f;
        }

        /// <summary>
        ///     Gets half the <see cref="Point"/> value given.
        /// </summary>
        /// <param name="point">
        ///     A <see cref="Point"/> value to half.
        /// </param>
        /// <returns>
        ///     A <see cref="Point"/> value that is half the original.
        /// </returns>
        public static Point HalfValue(this Point point)
        {
            return new Point(point.X / 2, point.Y / 2);
        }
    }
}
