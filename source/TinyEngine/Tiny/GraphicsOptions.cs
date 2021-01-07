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

using Microsoft.Xna.Framework.Graphics;

namespace Tiny
{
    /// <summary>
    ///     Provieds the values for the <see cref="Graphics"/> instnace when it is created.
    /// </summary>
    public struct GraphicsOptions
    {
        //  Default values.
        private static readonly GraphicsOptions _default = new GraphicsOptions
        {
            SynchronizeWithVerticalRetrace = true,
            PreferMultiSampling = false,
            GraphicsProfile = GraphicsProfile.HiDef,
            PreferredBackBufferFormat = SurfaceFormat.Color,
            PrefferedDepthStencilFormat = DepthFormat.Depth24Stencil8,
            AllowUserResizeWindow = false,
            IsMouseVisible = true,
        };

        /// <summary>
        ///     Gets a <see cref="GraphicsOptions"/> instnace with default value
        /// </summary>
        public static GraphicsOptions Default => _default;

        /// <summary>
        ///     A <see cref="bool"/> value that indicates a desire for vsync when
        ///     presenting the back buffer.
        /// </summary>
        public bool SynchronizeWithVerticalRetrace;

        /// <summary>
        ///     A <see cref="bool"/> value that indicates a desire for a
        ///     multisampled back buffer.
        /// </summary>
        public bool PreferMultiSampling;

        /// <summary>
        ///     A <see cref="Microsoft.Xna.Framework.Graphics.GraphicsProfile"/>
        ///     value that describes the graphics feature level to use.
        /// </summary>
        public GraphicsProfile GraphicsProfile;

        /// <summary>
        ///     A <see cref="SurfaceFormat"/> value that describes the back buffer
        ///     color format.
        /// </summary>
        public SurfaceFormat PreferredBackBufferFormat;

        /// <summary>
        ///     A <see cref="DepthFormat"/> value that indicates the desired
        ///     depth-stencil buffer format.
        /// </summary>
        public DepthFormat PrefferedDepthStencilFormat;

        /// <summary>
        ///     A <see cref="bool"/> value that indicates if users can resize the
        ///     <see cref="Microsoft.Xna.Framework.GameWindow"/>.
        /// </summary>
        public bool AllowUserResizeWindow;

        /// <summary>
        ///     A <see cref="bool"/> value that indicates if the mouse cursor is visible
        ///     on the game client window.
        /// </summary>
        public bool IsMouseVisible;

        /// <summary>
        ///     Creates a new <see cref="GraphicsOptions"/> instance.
        /// </summary>
        /// <param name="synchronizeWithVerticalRetrace">
        ///     A <see cref="bool"/> value indicating the desire for vsync when
        ///     presenting the back buffer.
        /// </param>
        /// <param name="preferMultiSampling">
        ///     A <see cref="bool"/> value indicating the desire for multisampled back buffer.
        /// </param>
        /// <param name="graphicsProfile">
        ///     A <see cref="Microsoft.Xna.Framework.Graphics.GraphicsProfile"/> value
        ///     which determines the graphics feature level to use.
        /// </param>
        /// <param name="preferredBackBufferFormat">
        ///     A <see cref="SurfaceFormat"/> value indicating the desired back buffer color format.
        /// </param>
        /// <param name="prefferedDepthStencilFormat">
        ///     A <see cref="DepthFormat"/> value indicating the desired depth-stencil buffer format.
        /// </param>
        /// <param name="allowUserResizeWindow">
        ///     A <see cref="bool"/> value indicating if the users can resize the <see cref="Microsoft.Xna.Framework.GameWindow"/>.
        /// </param>
        /// <param name="isMouseVisible">
        ///     A <see cref="bool"/> value indicating if the mouse cursof is visible on the game screen.
        /// </param>
        public GraphicsOptions(bool synchronizeWithVerticalRetrace = true,
                               bool preferMultiSampling = false,
                               GraphicsProfile graphicsProfile = GraphicsProfile.HiDef,
                               SurfaceFormat preferredBackBufferFormat = SurfaceFormat.Color,
                               DepthFormat prefferedDepthStencilFormat = DepthFormat.Depth24Stencil8,
                               bool allowUserResizeWindow = false,
                               bool isMouseVisible = true)
        {
            SynchronizeWithVerticalRetrace = synchronizeWithVerticalRetrace;
            PreferMultiSampling = preferMultiSampling;
            GraphicsProfile = graphicsProfile;
            PreferredBackBufferFormat = preferredBackBufferFormat;
            PrefferedDepthStencilFormat = prefferedDepthStencilFormat;
            AllowUserResizeWindow = allowUserResizeWindow;
            IsMouseVisible = isMouseVisible;
        }
    }
}
