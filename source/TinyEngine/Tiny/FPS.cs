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
    ///     A simple FPS counter used to measure the frames per second of
    ///     a gme.
    /// </summary>
    public class FPS
    {
        //  A 1-second timespan instance.
        private readonly TimeSpan _oneSecond = TimeSpan.FromSeconds(1);

        //  Keeps track of elapsed time
        private TimeSpan _elapsed;

        //  Counts the number of frames.
        private int _counter;

        //  The frames per second as a string value.
        private string _frameRate;

        /// <summary>
        ///     A <see cref="string"/> value contining the current calculated
        ///     frames per second.
        /// </summary>
        public string FrameRate
        {
            get
            {
                return _frameRate;
            }
            private set
            {
                _frameRate = value;
                HasUpdate = true;
            }
        }

        /// <summary>
        ///     Gets a <see cref="bool"/> value indicating if this <see cref="FPS"/>
        ///     instance has an updated <see cref="FrameRate"/> value.
        /// </summary>
        public bool HasUpdate { get; private set; }

        /// <summary>
        ///     Creates a new <see cref="FPS"/> instance.
        /// </summary>
        public FPS() { }

        /// <summary>
        ///     Updates the fps timing values.
        /// </summary>
        /// <remarks>
        ///     This should be called during an update cycle.
        /// </remarks>
        /// <param name="gameTime">
        ///     A <see cref="GameTime"/> instance containing a snapshot
        ///     of the timing values provided by the MonOGame framework during
        ///     an update cycle.
        /// </param>
        public void Update(GameTime gameTime)
        {
            _elapsed += gameTime.ElapsedGameTime;

            if (_elapsed > _oneSecond)
            {
                _elapsed -= _oneSecond;
                FrameRate = $"{_counter}";
                _counter = 0;
            }
            else
            {
                HasUpdate = false;
            }
        }

        /// <summary>
        ///     Updates the fps counter. 
        /// </summary>
        /// <remarks>
        ///     This should be called during a draw cycle.
        /// </remarks>
        public void UpdateCounter() => _counter++;
    }
}
