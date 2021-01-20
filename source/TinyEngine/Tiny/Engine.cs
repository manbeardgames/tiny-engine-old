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
using System.IO;
using System.Reflection;
using System.Runtime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tiny
{
    public class Engine : Game
    {
        //  Fully-qualified path to the entry assembly.
        private readonly string _assemblyDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        public static Engine Instance { get; private set; }

#if DEBUG
        //  An FPS counter used to get the FPS of the game during a draw call.
        private readonly FPS _fps;
#endif

        // --------------------------------------------------------------------
        //
        //  Graphics
        //  
        // --------------------------------------------------------------------
        #region Graphics
        //  The amount of padding to apply to the outside of the viewport.
        private static int _viewPadding;

        //  Indicates if the client (window) is currently resizing.
        private static bool _isResizing;

        //  The size of the backbuffer resolution.
        private static Point _resolution;

        //  The size of the virtual rendering resolution.
        private static Point _vResolution;

        //  The number of pixels that make up one graphics unit.
        private static int _pixelsPerUnit;

        /// <summary>
        ///     Gets the <see cref="GraphicsDeviceManager"/> instance used to control
        ///     the presentation of grpahics.
        /// </summary>
        public static GraphicsDeviceManager Graphics { get; private set; }

        /// <summary>
        ///     Gets a <see cref="Point"/> value that describes the width and
        ///     height, in pixels, of the rendering resolution of the back buffer.
        /// </summary>
        public static Point Resolution => _resolution;

        /// <summary>
        ///     Gets a <see cref="Point"/> value that desctives the width and
        ///     height, in pixels, of the virtual rendering resolution.
        /// </summary>
        public static Point VirtualResolution => _vResolution;

        /// <summary>
        ///     Gets or Sets an <see cref="int"/> vlaue that describes the amount
        ///     of padding, in pixels, to apply to the border of the viewport.
        /// </summary>
        public static int ViewPadding
        {
            get { return _viewPadding; }
            set
            {
                if (_viewPadding == value) { return; }
                _viewPadding = value;
                UpdateView();
            }
        }

        /// <summary>
        ///     Gets a <see cref="Microsoft.Xna.Framework.Graphics.Viewport"/> value
        ///     that describes the bounds to use when rendering to the game window.
        /// </summary>
        public static Viewport Viewport { get; private set; }

        /// <summary>
        ///     Gets a <see cref="Matrix"/> value that describes the scale to use
        ///     when rendering graphics to the game window.
        /// </summary>
        /// <remarks>
        ///     This value should be set as the <c>transformationMatrix</c> parameter
        ///     of the <see cref="SpriteBatch"/> when calling <c>SpriteBatch.Begin()</c>
        ///     when rendering to the game window.
        /// </remarks>
        public static Matrix ScreenMatrix { get; private set; }

        /// <summary>
        ///     Gets or Sets a <see cref="Color"/> value to use by default when clearing
        ///     the back buffer.
        /// </summary>
        public static Color ClearColor { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="int"/> value that describes the number of pixels
        ///     that equal one graphical unit.
        /// </summary>
        public static int PixelPerUnit
        {
            get { return _pixelsPerUnit; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Pixels per unit must be greater than 0");
                }

                _pixelsPerUnit = value;
                TileCount = new Point
                {
                    X = Resolution.X / value,
                    Y = Resolution.Y / value
                };
            }
        }

        /// <summary>
        ///     Gets a <see cref="Point"/> value that describes the total number of
        ///     tiles that can fit within the bounds of the <see cref="Resolution"/>
        ///     on the x and y axes, based on the <see cref="PixelPerUnit"/> value.
        /// </summary>
        public static Point TileCount { get; private set; }
        #endregion Graphics


        // --------------------------------------------------------------------
        //
        //  Scenes
        //  
        // --------------------------------------------------------------------
        //  The current active scene.
        private Scene _activeScene;

        //  The next scene to transition to.
        private Scene _nextScene;

        //  The scene transition to use when transitioning out the current active scene.
        private SceneTransition _transitionOut;

        //  The scene transition to use when transitioning in the next scene.
        private SceneTransition _transitionIn;

        //  The current active scene trasntion being used.
        private SceneTransition _activeTransition;

        /// <summary>
        ///     Gets the <see cref="SpriteBatch"/> instance used for rendering.
        /// </summary>
        public SpriteBatch SpriteBatch { get; private set; }

        ///// <summary>
        /////     Gets the <see cref="SceneManager"/> instance used to manage the
        /////     scenes for the game.
        ///// </summary>
        //public SceneManager Scene { get; private set; }

        ///// <summary>
        /////     Gets the <see cref="Tiny.Graphics"/> instance used to control and maange the
        /////     presentation of graphics for the game.
        ///// </summary>
        //public Graphics Graphics { get; protected set; }

        /// <summary>
        ///     Gets a <see cref="Time"/> instance containing the timing values
        ///     for the game.
        /// </summary>
        public static Time Time { get; protected set; }

        /// <summary>
        ///     Gets a <see cref="string"/> value contining the fully-qualified path to the
        ///     directory where game content is located.
        /// </summary>
        public string ContentDirectory => Path.Combine(_assemblyDirectory, Content.RootDirectory);

        /// <summary>
        ///     Gets a <see cref="string"/> value containing the title of the game.
        /// </summary>
        /// <remarks>
        ///     The title of the game is displayed in the title bar of the game window
        ///     when rendering in windowed mode.
        /// </remarks>
        public string Title { get; private set; }

        /// <summary>
        ///     Gets a <see cref="Version"/> instance that describes the version of
        ///     the game.
        /// </summary>
        public Version Version { get; protected set; }

        /// <summary>
        ///     Creates a new <see cref="Engine"/> instance.
        /// </summary>
        /// <param name="title">
        ///     A <see cref="string"/> value containing the title of the game.
        /// </param>
        /// <param name="graphicsOptions">
        ///     A <see cref="GraphicsOptions"/> value containing the settings for
        ///     the <see cref="Tiny.Graphics"/> instance.
        /// </param>
        public Engine(string title, GraphicsOptions graphicsOptions)
        {
            Instance = this;
            Title = title;

            //  Instantiate the graphics.
            //Graphics = new Graphics(this, graphicsOptions);
            ClearColor = Color.Black;

            //  Create the GraphicsDeviceManager instance.
            Graphics = new GraphicsDeviceManager(this);

            //  Listen for the graphics events
            Graphics.DeviceCreated += OnGraphicsDeviceCreated;
            Graphics.DeviceReset += OnGraphicsDeviceReset;
            Window.ClientSizeChanged += OnClientSizeChanged;
            //Graphics.ClientSizeChanged += OnClientSizeChanged;

            //  Set graphics options
            Graphics.SynchronizeWithVerticalRetrace = graphicsOptions.SynchronizeWithVerticalRetrace;
            Graphics.PreferMultiSampling = graphicsOptions.PreferMultiSampling;
            Graphics.GraphicsProfile = graphicsOptions.GraphicsProfile;
            Graphics.PreferredBackBufferFormat = graphicsOptions.PreferredBackBufferFormat;
            Graphics.PreferredDepthStencilFormat = graphicsOptions.PrefferedDepthStencilFormat;
            Window.AllowUserResizing = graphicsOptions.AllowUserResizeWindow;
            IsMouseVisible = graphicsOptions.IsMouseVisible;


#if DEBUG
            //  Instantiate the fps counter
            _fps = new FPS();
#endif

            //  Set the root directory for contnet
            Content.RootDirectory = @"Content";

            ////  Initialize the Scene system
            //Scene = new SceneManager(this);

            //  Initilize the timing system
            Time = new Time();

            //  isFixedTimeStep is disabled by default in TinyEngine.  This can be overriden
            //  in the derived game by setting the value to true in the constructor
            IsFixedTimeStep = false;

            //  InactiveSleepTime is a value that determines hwo long the main thread will
            //  sleep for each update cycle when the game window is inactive.  TinyEngine sets
            //  this to 0 seconds by default.  This can be overridden in by the dervied game
            //  by setting the value manually in the constructor
            InactiveSleepTime = TimeSpan.Zero;

            //  Garbage collection is set to sustanted low latency in TinyEngine. This can
            //  be overridden by the dervied game class by setting the value manullay in 
            //  the constructor
            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
        }

        /// <summary>
        ///     This will be called anytime the client game window size is changed. This
        ///     shoudl be overridden by the derived game class to handle any logic that
        ///     needs to be performed by the game.
        /// </summary>
        protected virtual void OnClientSizeChanged(object sender, EventArgs e)
        {
            if (Window.ClientBounds.Width > 0 && Window.ClientBounds.Height > 0 && !_isResizing)
            {
                _isResizing = true;

                Graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
                Graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;

                UpdateView();

                _isResizing = false;
            }

            if (_activeScene != null)
            {
                _activeScene.HandleClientSizeChanged();
            }

            if (_nextScene != null)
            {
                _nextScene.HandleClientSizeChanged();
            }

            if (_transitionOut != null)
            {
                _transitionOut.HandleClientSizeChanged();
            }

            if (_transitionIn != null)
            {
                _transitionIn.HandleClientSizeChanged();
            }
        }

        /// <summary>
        ///     This will be called anytime the <see cref="GraphicsDeviceManager.DeviceCreated"/>
        ///     event is triggered. This should be overritten by the derived game calss to handle
        ///     any logic that needs to be perforemd by the game.
        /// </summary>
        protected virtual void OnGraphicsDeviceCreated(object sender, EventArgs e)
        {
            UpdateView();
            if (_activeScene != null)
            {
                _activeScene.HandleGraphicsDeviceCreated();
            }

            if (_nextScene != null)
            {
                _nextScene.HandleGraphicsDeviceCreated();
            }

            if (_transitionOut != null)
            {
                _transitionOut.HandleGraphicsDeviceCreated();
            }

            if (_transitionIn != null)
            {
                _transitionIn.HandleGraphicsDeviceCreated();
            }
        }

        /// <summary>
        ///     This will be called anytime the <see cref="GraphicsDeviceManager.DeviceReset"/>
        ///     event is triggered.  When this happens, all contents of VRAM will be discarded
        ///     and things like RenderTargets will need to be recreated.  This shdould be 
        ///     overridden by the derived game class to handle this scenario.
        /// </summary>
        protected virtual void OnGraphicsDeviceReset(object sender, EventArgs e)
        {
            UpdateView();

            if (_activeScene != null)
            {
                _activeScene.HandleGraphicsDeviceReset();
            }

            if (_nextScene != null)
            {
                _nextScene.HandleGraphicsDeviceReset();
            }

            if (_transitionOut != null)
            {
                _transitionOut.HandleGraphicsDeviceReset();
            }

            if (_transitionIn != null)
            {
                _transitionIn.HandleGraphicsDeviceReset();
            }
        }

        /// <summary>
        ///     Sets the initial resolution values for the game.
        /// </summary>
        /// <remarks>
        ///     This should only be called once at the start of the <c>Initialize()</c>
        ///     method for your game.
        /// </remarks>
        /// <param name="width">
        ///     A <see cref="int"/> value that describes the width, in pixels, of
        ///     the virtual rendering resolution of the game.
        /// </param>
        /// <param name="height">
        ///     A <see cref="int"/> value that describes the height, in pixels, of
        ///     the virtual rendering reoslution of the game.
        /// </param>
        /// <param name="windowWidth">
        ///     A <see cref="int"/> value that describes the width, in pixels, of
        ///     the rendering resolution of the back buffer.
        /// </param>
        /// <param name="windowHeight">
        ///     A <see cref="int"/> value that describes the height, in pixels, of
        ///     the rendering resolution of the back buffer.
        /// </param>
        /// <param name="fullscreen">
        ///     A <see cref="bool"/> value that indicates if the graphics should be presented
        ///     in fullscreen mode.
        /// </param>
        protected void SetInitialResolution(int width, int height, int windowWidth, int windowHeight, bool fullscreen)
        {
            //  Vaidate and set the values.
            _vResolution.X = windowWidth > 0 ? windowWidth
                : throw new ArgumentOutOfRangeException("The client widnow width must be greater than 0", nameof(windowWidth));
            _vResolution.Y = windowHeight > 0 ? windowHeight
                : throw new ArgumentOutOfRangeException("The client window height must be greater than 0", nameof(windowHeight));
            _resolution.X = width > 0 ? width
                : throw new ArgumentOutOfRangeException("The game width must be greater than 0", nameof(width));
            _resolution.Y = height > 0 ? height
                : throw new ArgumentOutOfRangeException("The game height must be greater than 0", nameof(height));

            //  Perform full screen check and set values based on it.
            if (fullscreen)
            {
                Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                Graphics.IsFullScreen = true;
            }
            else
            {
                Graphics.PreferredBackBufferWidth = windowWidth;
                Graphics.PreferredBackBufferHeight = windowHeight;
                Graphics.IsFullScreen = false;
            }

            //  Apply the changes.
            Graphics.ApplyChanges();
        }

        /// <summary>
        ///     Sets the graphics to render the game in windowed mode.
        /// </summary>
        /// <param name="width">
        ///     An <see cref="int"/> value that describes the width, in pixels,
        ///     to set the game window to.
        /// </param>
        /// <param name="height">
        ///     A <see cref="int"/> value taht describes the height, in pixels,
        ///     to set the game window to.
        /// </param>
        public static void SetWindowed(int width, int height)
        {
            _isResizing = true;

            Graphics.PreferredBackBufferWidth = width > 0 ? width
                : throw new ArgumentOutOfRangeException(nameof(width), "The client window width must be greater than zero");
            Graphics.PreferredBackBufferHeight = height > 0 ? height
                : throw new ArgumentOutOfRangeException(nameof(height), "The client window height must be greater than zero");
            Graphics.IsFullScreen = false;

            Graphics.ApplyChanges();

            _isResizing = false;
        }

        /// <summary>
        ///     Sets the grpahics to render the game in fullscreen mode.
        /// </summary>
        public static void SetFullscren()
        {
            _isResizing = true;

            Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Graphics.IsFullScreen = true;

            Graphics.ApplyChanges();

            _isResizing = false;
        }

        /// <summary>
        ///     Updates the values for the graphics view such as the screen matrix and
        ///     viewport to provide independent resolution rendering.
        /// </summary>
        /// <!--
        ///     The method for indpendent resolution rendering comes from the Monocle Engine
        ///     developed by Matt Thorson and used in the games Towerfall and Celeste. The
        ///     Monocle Engine was originally found at https://bitbucket.org/MattThorson/monocle-engine
        ///     however the source code does not seem to be available any more at this linke.
        ///     
        ///     Monocole is licensed under the MIT License.
        /// -->
        private static void UpdateView()
        {
            float screenWidth = Graphics.GraphicsDevice.PresentationParameters.BackBufferWidth;
            float screenHeight = Graphics.GraphicsDevice.PresentationParameters.BackBufferHeight;

            if (screenWidth / _resolution.X > screenHeight / _resolution.Y)
            {
                _vResolution.X = (int)(screenHeight / _resolution.Y * _resolution.X);
                _vResolution.Y = (int)screenHeight;
            }
            else
            {
                _vResolution.X = (int)screenWidth;
                _vResolution.Y = (int)(screenWidth / _resolution.X * _resolution.Y);
            }

            float aspect = _vResolution.Y / (float)_vResolution.X;
            _vResolution.X -= _viewPadding * 2;
            _vResolution.Y -= (int)(aspect * _viewPadding * 2);

            ScreenMatrix = Matrix.CreateScale(_vResolution.X / (float)_resolution.X);

            Viewport = new Viewport
            {
                X = (int)(screenWidth / 2 - _vResolution.X / 2),
                Y = (int)(screenHeight / 2 - _vResolution.Y / 2),
                Width = _vResolution.X,
                Height = _vResolution.Y,
                MinDepth = 0,
                MaxDepth = 1
            };
        }

        /// <summary>
        ///     Called by the MonoGame framework after the <see cref="Game"/> instance
        ///     is created.  The derived game class should override this method to 
        ///     perform any initializations here, but ensure that base.Initialize() is 
        ///     still called first so TinyEngine can Initialize as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            Input.Initialize();
        }

        /// <summary>
        ///     Called by the MonoGame framework during <see cref="Initialize"/> method 
        ///     execution.  The dervied game calss shoudl override this method to perform
        ///     any content loading here, ut ensure that base.LoadContent() is still called
        ///     so the TinyEngine can load its content as well.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();
            SpriteBatch = new SpriteBatch(Instance.GraphicsDevice);
            SpriteBatchExtensions.Initialize(Instance.GraphicsDevice);

        }

        /// <summary>
        ///     Called by the MonoGame framework. All update logic for the game should be
        ///     perforemd here.  TinyEngine uses a scene system and will call the scene update
        ///     method from here. The derived game class shouldn't need to override this method.
        /// </summary>
        /// <param name="gameTime">
        ///     A <see cref="GameTime"/> instance containing a snapshot
        ///     of the timing values provided by the MonOGame framework during
        ///     an update cycle.
        /// </param>
        protected override void Update(GameTime gameTime)
        {
            //  Time should always be the first thing updates
            Time.Update(gameTime);

#if DEBUG
            //  Update the FPS instance.
            _fps.Update();
#endif

            //  Update the input state
            Input.Update();

            //  If there is an active transition, then we need to update it; otherwise, if there
            //  is no active transition, but there is a next scene to switch to, switch to that
            //  scene instead.
            if (_activeTransition != null && _activeTransition.IsTransitioning)
            {
                _activeTransition.Update();
            }
            else if (_activeTransition == null && _nextScene != null)
            {
                TransitionScene();
                _activeScene.Begin();
            }

            //  If there is a current active scene, update it.
            if (_activeScene != null)
            {
                _activeScene.Update();
            }

            ////  Update the current scene
            //Scene.Update();

            //  Always ensure we call base.Update() at the end of this.
            base.Update(gameTime);
        }

        /// <summary>
        ///     Renders the game.
        /// </summary>
        /// <param name="gameTime">
        ///     A <see cref="GameTime"/> instance containing a snapshot
        ///     of the timing values provided by the MonOGame framework during
        ///     an draw cycle.
        /// </param>
        protected override void Draw(GameTime gameTime)
        {
            //Scene.Draw();
            DrawCore();

            //  Always ensure we call base.Render().
            base.Draw(gameTime);

#if DEBUG
            //  Update the FPS counter
            _fps.UpdateCounter();
            if (_fps.HasUpdate)
            {
                float memoryUsage = GC.GetTotalMemory(false) / 1048576.0f;
                Window.Title = $"{Title} | {_fps.FrameRate}fps | {memoryUsage:F}MB";
            }
#endif
        }

        private void DrawCore()
        {
            //  If there is an active scene to draw, draw it.
            if (_activeScene != null)
            {
                _activeScene.Draw();
            }

            //  If there is an active transition happening, render the transition
            if (_activeTransition != null && _activeTransition.IsTransitioning)
            {
                _activeTransition.Render();
            }


            //  Prepare the grpahics device for the final render.
            //Graphics.SetViewport();
            //Graphics.Clear();
            GraphicsDevice.Viewport = Viewport;
            GraphicsDevice.Clear(ClearColor);

            DrawAlways();

            //  Begin the spritebatch
            SpriteBatch.Begin(blendState: BlendState.AlphaBlend,
                              samplerState: SamplerState.PointClamp,
                              transformMatrix: ScreenMatrix);

            //  If there is an active transition we draw its render target; otherwise, we draw 
            //  the render target of the active scene
            if (_activeTransition != null && _activeTransition.IsTransitioning)
            {
                SpriteBatch.Draw(texture: _activeTransition.RenderTarget,
                                 destinationRectangle: _activeTransition.RenderTarget.Bounds,
                                 sourceRectangle: _activeTransition.RenderTarget.Bounds,
                                 color: Color.White);
            }
            else if (_activeScene != null)
            {

                SpriteBatch.Draw(texture: _activeScene.RenderTarget,
                                 destinationRectangle: _activeScene.RenderTarget.Bounds,
                                 sourceRectangle: _activeScene.RenderTarget.Bounds,
                                 color: Color.White);
            }

            //  End the sprite batch
            SpriteBatch.End();

        }

        protected virtual void DrawAlways() { }


        /// <summary>
        ///     Changes the current active <see cref="Scene"/> to the one provided.
        /// </summary>
        /// <param name="to">
        ///     The <see cref="Scene"/> instance to change to.
        /// </param>
        public void ChangeScene(Scene to)
        {
            //  Only set the next scene if the Scene instance given is not the
            //  same Scene instance that is the current active scene
            if (_activeScene != to)
            {
                _nextScene = to;
            }
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
            if (_activeTransition == null || !_activeTransition.IsTransitioning)
            {
                if (_activeScene != to)
                {
                    _nextScene = to;
                    _transitionOut = transitionOut;

                    if (_transitionOut != null)
                    {
                        _transitionOut.Kind = SceneTransitionKind.Out;
                    }

                    _transitionIn = transitionIn;

                    if (_transitionIn != null)
                    {
                        _transitionIn.Kind = SceneTransitionKind.In;
                    }

                    //  Subscribe to the transition compoleted events for each
                    if (_transitionOut != null)
                    {
                        _transitionOut.TransitionCompleted += TransitionOutCompleted;
                    }

                    if (_transitionIn != null)
                    {
                        _transitionIn.TransitionCompleted += TransitionInCompleted;
                    }

                    //  Set the current active transition
                    if (_transitionOut != null)
                    {
                        _activeTransition = _transitionOut;
                        _activeTransition.Start(_activeScene.RenderTarget);
                    }
                    else
                    {
                        if (_transitionIn != null)
                        {
                            TransitionScene();
                            _activeTransition = _transitionIn;
                            _activeTransition.Start(_activeScene.RenderTarget);
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Handles the logic when the <see cref="_transitionOut"/> instance 
        ///     triggers the <see cref="SceneTransition.TransitionCompleted"/> event.
        /// </summary>
        private void TransitionOutCompleted(object sender, EventArgs e)
        {
            //  Unsubscribe from the event so we don't leave any lingering references
            _transitionOut.TransitionCompleted -= TransitionOutCompleted;

            //  Dispose of the instance
            _transitionOut.Dispose();
            _transitionOut = null;

            //  Change the scene
            TransitionScene();

            if (_transitionIn == null)
            {
                _activeTransition = null;
                _activeScene.Begin();
            }
            else
            {
                //  Set the current transition to the in transition and start it
                _activeTransition = _transitionIn;
                _activeTransition.Start(_activeScene.RenderTarget);
            }
        }

        /// <summary>
        ///     Handles the logic when the <see cref="_transitionIn"/> instance
        ///     triggers the <see cref="SceneTransition.TransitionCompleted"/> event.
        /// </summary>
        private void TransitionInCompleted(object sender, EventArgs e)
        {
            //  Unsubscribe from the event so we don't leave any lingering references
            _transitionIn.TransitionCompleted -= TransitionInCompleted;

            //  Dispose of the instance
            _transitionIn.Dispose();
            _transitionIn = null;

            //  Set the active transition to null
            _activeTransition = null;

            //  Tell the scene it has begun
            _activeScene.Begin();
        }

        /// <summary>
        ///     Transitions from the current active <see cref="Scene"/> to the next
        ///     <see cref="Scene"/>.
        /// </summary>
        private void TransitionScene()
        {
            //  If there is an active scene, unload the content from the scene.
            if (_activeScene != null)
            {
                _activeScene.UnloadContent();
            }

            //  Perform garbage collection to ensure memory is cleared
            GC.Collect();

            //  Set the active scene to the next scene
            _activeScene = _nextScene;

            //  Set the time rate to default 1.0
            Time.TimeRate = 1.0f;

            //  Null out the _nextScene field so we don't keep trying to transition
            _nextScene = null;

            //  If the now active scene is not null, initilize it here.
            //  Reminder that the Initialize() method calls the LoadContnet method
            if (_activeScene != null)
            {
                _activeScene.Initialize();
            }
        }
    }
}
