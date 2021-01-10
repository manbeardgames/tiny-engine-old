using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny
{
    public static partial class Maths
    {
        /// <summary>
        ///     Given a <see cref="int"/> value, determines if it is range of the
        ///     inclusive <paramref name="lower"/> and exclusive <paramref name="upper"/>
        ///     bounds.
        /// </summary>
        /// <param name="value">
        ///     A <see cref="int"/> value to check.,
        /// </param>
        /// <param name="lower">
        ///     A <see cref="int"/> value that defines the inclusive lower bounds.
        /// </param>
        /// <param name="upper">
        ///     A <see cref="int"/> value that defines the exclusive upper bounds.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the <paramref name="value"/> is within range;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInRange(int value, int lower, int upper) => value >= lower && value < upper;

        /// <summary>
        ///     Given a <see cref="float"/> value, determines if it is range of the
        ///     inclusive <paramref name="lower"/> and exclusive <paramref name="upper"/>
        ///     bounds.
        /// </summary>
        /// <param name="value">
        ///     A <see cref="float"/> value to check.,
        /// </param>
        /// <param name="lower">
        ///     A <see cref="float"/> value that defines the inclusive lower bounds.
        /// </param>
        /// <param name="upper">
        ///     A <see cref="float"/> value that defines the exclusive upper bounds.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the <paramref name="value"/> is within range;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInRange(float value, float lower, float upper) => value >= lower && value < upper;

        /// <summary>
        ///     Given a <see cref="double"/> value, determines if it is range of the
        ///     inclusive <paramref name="lower"/> and exclusive <paramref name="upper"/>
        ///     bounds.
        /// </summary>
        /// <param name="value">
        ///     A <see cref="double"/> value to check.,
        /// </param>
        /// <param name="lower">
        ///     A <see cref="double"/> value that defines the inclusive lower bounds.
        /// </param>
        /// <param name="upper">
        ///     A <see cref="double"/> value that defines the exclusive upper bounds.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the <paramref name="value"/> is within range;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInRange(double value, double lower, double upper) => value >= lower && value < upper;
    }
}
