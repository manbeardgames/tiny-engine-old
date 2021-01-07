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

using System;

namespace Tiny
{
    public static partial class Maths
    {
        //  A Random instance that can be used globally for random value generation.
        private static readonly Random s_random = new Random();

        /// <summary>
        ///     Gets a <see cref="System.Random"/> instance that can be used globally for
        ///     random value generation.
        /// </summary>
        public static Random Random => s_random;

        /// <summary>
        ///     Returns a random <see cref="float"/> value that is greater than or equal 
        ///     to <c>0.0f</c> and less than <c>1.0f</c>.
        /// </summary>
        /// <param name="random">
        ///     A <see cref="System.Random"/> instance.
        /// </param>
        /// <returns>
        ///     A random <see cref="float"/> value that is greater than or equal to <c>0.0f</c> and
        ///     less than <c>1.0f</c>.
        /// </returns>
        public static float NextFloat(this Random random)
        {
            return (float)random.NextDouble();
        }

        /// <summary>
        ///     Returns a random <see cref="float"/> value that is greater than or equal to
        ///     the <paramref name="min"/> value and less than the <paramref name="max"/> value.
        /// </summary>
        /// <param name="random">
        ///     A <see cref="System.Random"/> instance.
        /// </param>
        /// <param name="min">
        ///     The inclusive lower bound of the random number returned.
        /// </param>
        /// <param name="max">
        ///     The exclusive upper bound of the random number returned.
        /// </param>
        /// <returns>
        ///     A random <see cref="float"/> value that is greater than or equal to the
        ///     <paramref name="min"/> value and less than the <paramref name="max"/> value.
        /// </returns>
        public static float NextFloat(this Random random, float min, float max)
        {
            return (float)random.NextDouble() * (max - min) + min;
        }

        /// <summary>
        ///     Returns a random <see cref="double"/> value that is greater than or equal to
        ///     the <paramref name="min"/> value and less than the <paramref name="max"/> value.
        /// </summary>
        /// <param name="random">
        ///     A <see cref="System.Random"/> instance.
        /// </param>
        /// <param name="min">
        ///     The inclusive lower bound of the random number returned.
        /// </param>
        /// <param name="max">
        ///     The exclusive upper bound of the random number returned.
        /// </param>
        /// <returns>
        ///     A random <see cref="double"/> value that is greater than or equal to the
        ///     <paramref name="min"/> value and less than the <paramref name="max"/> value.
        /// </returns>
        public static double NextDouble(this Random random, double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }

        /// <summary>
        ///     Given a series of <typeparamref name="T"/> values, chooses one at random and
        ///     returns it.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of values.
        /// </typeparam>
        /// <param name="random">
        ///     A <see cref="System.Random"/> instance.
        /// </param>
        /// <param name="values">
        ///     The values to choose from.
        /// </param>
        /// <returns>
        ///     A <typeparamref name="T"/> value randomly choosen from the ones given.
        /// </returns>
        public static T Next<T>(this Random random, params T[] values)
        {
            return values[random.Next(values.Length)];
        }
    }
}
