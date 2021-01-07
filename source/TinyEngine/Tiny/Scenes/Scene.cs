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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Tiny
{
    /// <summary>
    ///     An abstract class used to create scenes within the game.
    /// </summary>
    public abstract class Scene
    {
        //  A cached refernce to the TInyEngine instance.
        protected Engine _engine;

        //  Used to load scene specific content.
        protected ContentManager _content;

        /// <summary>
        ///     Gets the <see cref="RenderTarget2D"/> instance that the scene
        ///     renders to.
        /// </summary>
        public RenderTarget2D RenderTarget { get; protected set; }

        /// <summary>
        ///     Creates a new <see cref="Scene"/> instance.
        /// </summary>
        /// <param name="engine">
        ///     A refernce to the <see cref="Game"/> instance that is derived
        ///     from <see cref="Engine"/>.
        /// </param>
        public Scene(Engine engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            _engine = engine;
        }

        /// <summary>
        ///     Perform all scene initilizations here. 
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         When overriding this method, ensure that <c>base.Initialize()</c> is called first.
        ///     </para>
        ///     <para>
        ///         This is called only once, immediatly after the scene becomes the active scene,
        ///         and before the first update is called for the scene.
        ///     </para>
        /// </remarks>
        /// 
        public virtual void Initialize()
        {
            _content = new ContentManager(_engine.Services);
            _content.RootDirectory = _engine.Content.RootDirectory;
            LoadContent();
        }

        /// <summary>
        ///     Perform any loading of scene specific content here.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         When overriding this method, ensure that <c>base.LoadContent()</c> is still called.
        ///         This is to allow the <see cref="RenderTarget"/> value to instantiate.
        ///     </para>
        ///     <para>
        ///         This is called only once, immediately at the end of the 
        ///         <see cref="Initialize"/> method.
        ///     </para>
        /// </remarks>
        public virtual void LoadContent()
        {
            GenerateRenderTarget();
        }

        /// <summary>
        ///     Perform any unloading of scene specific content here.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         When overriding this method, ensure that <c>base.UnloadContent()</c> is still
        ///         called so that the content manager for the scene can be unloaded.
        ///     </para>
        ///     <para>
        ///         This is called only once, immediatly after TinyEngine switches to a new scene
        ///         from this scene. 
        ///     </para>
        /// </remarks>
        public virtual void UnloadContent()
        {
            _content.Unload();
            _content = null;

            //  Dispose of the render target if it is not already dispoed
            if (RenderTarget != null && !RenderTarget.IsDisposed)
            {
                RenderTarget.Dispose();
                RenderTarget = null;
            }

            //  Remove the refeerence to the engine
            _engine = null;
        }

        /// <summary>
        ///     Perform any update logic for the scene here.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This method is called each update cycle, and only once per.
        ///     </para>
        /// </remarks>
        public virtual void Update() { }

        /// <summary>
        ///     Perofrm all drawing for the scene here.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This method is called each draw cycle, and only once per.
        ///     </para>
        /// </remarks>
        public virtual void Draw() { }

        /// <summary>
        ///     Gets the <see cref="Game"/> instance as <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">
        ///     The derived <see cref="Game"/> type.
        /// </typeparam>
        /// <returns>
        ///     The <see cref="Game"/> instance as <typeparamref name="T"/>
        /// </returns>
        public T GameAs<T>() where T : Game
        {
            if (_engine is T game)
            {
                return game;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///     Generates the <see cref="RenderTarget"/> isntance for this scene.
        /// </summary>
        protected virtual void GenerateRenderTarget()
        {
            //  If the RenderTarget instance has already been created previously, but has yet
            //  to be disposed of properly, dispose of the instance before setting a new one.
            if (RenderTarget != null && !RenderTarget.IsDisposed)
            {
                RenderTarget.Dispose();
            }

            RenderTarget = new RenderTarget2D(graphicsDevice: _engine.Graphics.Device,
                                              width: _engine.Graphics.Resolution.X,
                                              height: _engine.Graphics.Resolution.Y,
                                              mipMap: false,
                                              preferredFormat: SurfaceFormat.Color,
                                              preferredDepthFormat: DepthFormat.Depth24Stencil8,
                                              preferredMultiSampleCount: 0,
                                              usage: RenderTargetUsage.DiscardContents);
        }


        /// <summary>
        ///     Handles the <see cref="GraphicsDeviceManager.DeviceCreated"/> event for the
        ///     scene.
        /// </summary>
        public virtual void HandleGraphicsDeviceCreated()
        {
            GenerateRenderTarget();
        }

        /// <summary>
        ///     Handles the <see cref="GraphicsDeviceManager.DeviceReset"/> event for the
        ///     scene.
        /// </summary>
        public virtual void HandleGraphicsDeviceReset()
        {
            GenerateRenderTarget();
        }

        /// <summary>
        ///     Handles the <see cref="GameWindow.ClientSizeChanged"/> event for the
        ///     scene.
        /// </summary>
        public virtual void HandleClientSizeChanged() { }
    }
}
