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
using Microsoft.Xna.Framework;

namespace Tiny
{
    /// <summary>
    ///     A class used to manage the timing values within a game.
    /// </summary>
    public class Time
    {
        /// <summary>
        ///     Gets a <see cref="float"/> value that describes the amount of time,
        ///     in seconds, that have elapsed since the previous update cycle.
        /// </summary>
        /// <remarks>
        ///     This value calcualted using the <see cref="TimeRate"/> value.
        /// </remarks>
        public float DeltaTime { get; private set; }

        /// <summary>
        ///     Gets a <see cref="float"/> value that describes the amount of time,
        ///     in seconds, that have elapsed since the previous update cycle.
        /// </summary>
        public float RawDeltaTime { get; private set; }

        /// <summary>
        ///     Gets or Sets a <see cref="float"/> value that desribes the rate at
        ///     which time is calculated for the <see cref="DeltaTime"/> value.
        /// </summary>
        public float TimeRate { get; set; } = 1.0f;

        /// <summary>
        ///     Gets or Sets a <see cref="float"/> value that describes the amount of
        ///     time, in seconds, to freeze all updates.
        /// </summary>
        public float FreezeTimer { get; set; }

        /// <summary>
        ///     Gets a <see cref="bool"/> value that indicates if time is currently
        ///     frozen.
        /// </summary>
        public bool IsTimeFrozen => FreezeTimer > 0.0f;

        /// <summary>
        ///     Gets a <see cref="TimeSpan"/> value that describes the amount of time
        ///     that has elapsed since the previous update cycle.
        /// </summary>
        public TimeSpan ElapsedGameTime { get; private set; }

        /// <summary>
        ///     Updates this TimeManager instance.
        /// </summary>
        /// <param name="gameTime">
        ///     A <see cref="GameTime"/> instance containing a snapshot
        ///     of the timing values provided by the MonOGame framework during
        ///     an update cycle.
        /// </param>
        internal void Update(GameTime gameTime)
        {
            ElapsedGameTime = gameTime.ElapsedGameTime;
            RawDeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            DeltaTime = RawDeltaTime * TimeRate;

            if (IsTimeFrozen)
            {
                FreezeTimer = Math.Max(FreezeTimer - RawDeltaTime, 0.0f);
            }
        }
    }
}
