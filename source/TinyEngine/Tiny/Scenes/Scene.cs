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

namespace Tiny
{
    /// <summary>
    ///     An abstract class used to create scenes within the game.
    /// </summary>
    public abstract class Scene
    {
        ////  A cached refernce to the TInyEngine instance.
        //protected Engine _engine;

        ////  Used to load scene specific content.
        //protected ContentManager _content;

        /// <summary>
        ///     Gets the <see cref="TinyRenderTarget"/> instance that the scene
        ///     renders to.
        /// </summary>
        public TinyRenderTarget RenderTarget { get; protected set; }

        /// <summary>
        ///     Gets the <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/>
        ///     instance used for rendering.
        /// </summary>
        public SpriteBatch SpriteBatch => Engine.Instance.SpriteBatch;

        ///// <summary>
        /////     Gets the <see cref="Tiny.Graphics"/> instance used to manage and
        /////     present the graphics.
        ///// </summary>
        //public Graphics Graphics => _engine.Graphics;

        ///// <summary>
        /////     Gets the <see cref="Tiny.Time"/> instance which provides the
        /////     timing values for each update frame.
        ///// </summary>
        //public Time Time => _engine.Time;

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
        public ContentManager GlobalContent => Engine.Instance.Content;

        /// <summary>
        ///     Gets the <see cref="ContentManager"/> instance used to load content
        ///     specific for this scene. 
        /// </summary>
        /// <remarks>
        ///     Any content loaded through this <see cref="ContentManager"/> will
        ///     be unloaded when switch from this scene to another scene.s
        /// </remarks>
        public ContentManager Content { get; private set; }

        /// <summary>
        ///     Creates a new <see cref="Scene"/> instance.
        /// </summary>
        /// <param name="engine">
        ///     A refernce to the <see cref="Game"/> instance that is derived
        ///     from <see cref="Engine"/>.
        /// </param>
        public Scene()
        {
            Paused = true;
        }

        /// <summary>
        ///     Called internally by TinyEngine on the first update frame
        ///     after the scene has been fully transitioned into.
        /// </summary>
        internal void Begin()
        {
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
            Content = new ContentManager(Engine.Instance.Services);
            Content.RootDirectory = Engine.Instance.Content.RootDirectory;
            LoadContent();
        }

        /// <summary>
        ///     Perform any loading of scene specific content here.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         When overriding this method, ensure that <c>base.LoadContent()</c> is still called.
        ///         This is to allow the <see cref="TinyRenderTarget"/> value to instantiate.
        ///     </para>
        ///     <para>
        ///         This is called only once, immediately at the end of the 
        ///         <see cref="Initialize"/> method.
        ///     </para>
        /// </remarks>
        public virtual void LoadContent()
        {
            RenderTarget = new TinyRenderTarget(width: Engine.Resolution.X,
                                                height: Engine.Resolution.Y,
                                                multiSampleCount: 0,
                                                depth: true,
                                                preserve: true);
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
            Content.Unload();
            Content = null;

            //  Dispose of the render target if it is not already dispoed
            if (RenderTarget != null && !RenderTarget.IsDisposed)
            {
                RenderTarget.Dispose();
                RenderTarget = null;
            }
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
            if (Engine.Instance is T game)
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
            Engine.Instance.ChangeScene(to);
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
            Engine.Instance.ChangeScene(to, transitionOut, transitionIn);
        }

        /// <summary>
        ///     Handles the <see cref="GraphicsDeviceManager.DeviceCreated"/> event for the
        ///     scene.
        /// </summary>
        public virtual void HandleGraphicsDeviceCreated()
        {
            RenderTarget.Reload();
        }

        /// <summary>
        ///     Handles the <see cref="GraphicsDeviceManager.DeviceReset"/> event for the
        ///     scene.
        /// </summary>
        public virtual void HandleGraphicsDeviceReset()
        {
            RenderTarget.Reload();
        }

        /// <summary>
        ///     Handles the <see cref="GameWindow.ClientSizeChanged"/> event for the
        ///     scene.
        /// </summary>
        public virtual void HandleClientSizeChanged() { }
    }
}
