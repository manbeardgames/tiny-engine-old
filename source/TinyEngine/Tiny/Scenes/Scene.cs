﻿/* ----------------------------------------------------------------------------
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
        ///     Gets the <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/>
        ///     instance used for rendering.
        /// </summary>
        public SpriteBatch SpriteBatch => _engine.SpriteBatch;

        /// <summary>
        ///     Gets the <see cref="Tiny.Graphics"/> instance used to manage and
        ///     present the graphics.
        /// </summary>
        public Graphics Graphics => _engine.Graphics;

        /// <summary>
        ///     Gets the <see cref="Tiny.Time"/> instance which provides the
        ///     timing values for each update frame.
        /// </summary>
        public Time Time => _engine.Time;

        /// <summary>
        ///     Gets or Sets a <see cref="bool"/> value indicating if this
        ///     scene is paused. 
        /// </summary>
        /// <remarks>
        ///     When paused, the update method will be skipped for this scene.
        /// </remarks>
        public bool Paused { get; set; }

        /// <summary>
        ///     Gets the <see cref="ContentManager"/> instance used to load global
        ///     content.
        /// </summary>
        public ContentManager GlobalContent => _engine.Content;

        /// <summary>
        ///     Gets the <see cref="ContentManager"/> instance used to load content
        ///     specific for this scene. 
        /// </summary>
        /// <remarks>
        ///     Any content loaded through this <see cref="ContentManager"/> will
        ///     be unloaded when switch from this scene to another scene.s
        /// </remarks>
        public ContentManager Content => _content;

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
            Paused = true;
        }

        /// <summary>
        ///     Called internally by TinyEngine on the first update frame
        ///     after the scene has been fully transitioned into.
        /// </summary>
        internal void Begin()
        {
            System.Diagnostics.Debug.WriteLine($"[{DateTime.Now}]: Scene Begin");
            Paused = false;
            Start();
        }

        /// <summary>
        ///     Perform any logic that should occur at the start of the scene here.
        /// </summary>
        /// <remarks>
        ///     This is called only once, on the first frame after the scene has been
        ///     fully transitioned into, but before the first update for the scene
        ///     has occured.
        /// </remarks>
        public virtual void Start() { }

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
        ///     Changes the current active <see cref="Scene"/> to the one provided.
        /// </summary>
        /// <param name="to">
        ///     The <see cref="Scene"/> instance to change to.
        /// </param>
        public void ChangeScene(Scene to)
        {
            _engine.ChangeScene(to);
        }

        /// <summary>
        ///     Changes the current active <see cref="Scene"/> to the one provided using
        ///     the <see cref="SceneTransition"/> instances given to transition the scenes
        ///     in and out.
        /// </summary>
        /// <param name="to">
        ///     The <see cref="Scene"/> instance to change to.
        /// </param>
        /// <param name="transitionOut">
        ///     A <see cref="SceneTransition"/> instance that is used to transition the current
        ///     active <see cref="Scene"/> out.
        /// </param>
        /// <param name="transitionIn">
        ///     A <see cref="SceneTransition"/> instance that is used to transition next
        ///     <see cref="Scene"/> in.
        /// </param>
        public void ChangeScene(Scene to, SceneTransition transitionOut, SceneTransition transitionIn)
        {
            _engine.ChangeScene(to, transitionOut, transitionIn);
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
