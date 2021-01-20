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
        ///     Gets a <see cref="bool"/> value indicating if this instance has
        ///     been properly disposed of.
        /// </summary>
        public static bool IsDisposed { get; private set; }

        /// <summary>
        ///     Gets a <see cref="TinyTexture"/> instance containing a 1x1 white
        ///     pixel texture used for rendering primatives.
        /// </summary>
        public static TinyTexture Pixel { get; private set; }

        /// <summary>
        ///     Initializes the <see cref="SpriteBatchExtensions"/> for use.
        /// </summary>
        /// <param name="device">
        ///     The <see cref="GraphicsDevice"/> instance used for the presenation
        ///     of graphics.
        /// </param>
        public static void Initialize(GraphicsDevice device)
        {
            Pixel = new TinyTexture(1, 1, Color.White);
        }

        /// <summary>
        ///     Unloads all resources managed by this.
        /// </summary>
        public static void Unload()
        {
            if (IsDisposed) { return; }

            Pixel.Dispose();
            Pixel = null;
            IsDisposed = true;
        }

    }
}
