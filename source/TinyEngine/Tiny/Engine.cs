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

#if DEBUG
        //  An FPS counter used to get the FPS of the game during a draw call.
        private readonly FPS _fps;
#endif

        /// <summary>
        ///     Gets the <see cref="SpriteBatch"/> instance used for rendering.
        /// </summary>
        public SpriteBatch SpriteBatch { get; private set; }

        /// <summary>
        ///     Gets the <see cref="SceneManager"/> instance used to manage the
        ///     scenes for the game.
        /// </summary>
        public SceneManager Scene { get; private set; }

        /// <summary>
        ///     Gets the <see cref="Tiny.Graphics"/> instance used to control and maange the
        ///     presentation of graphics for the game.
        /// </summary>
        public Graphics Graphics { get; protected set; }

        /// <summary>
        ///     Gets a <see cref="Time"/> instance containing the timing values
        ///     for the game.
        /// </summary>
        public Time Time { get; protected set; }

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
            Title = title;

            //  Instantiate the graphics.
            Graphics = new Graphics(this, graphicsOptions);

            //  Listen for the graphics events
            Graphics.GraphicsDeviceCreated += OnGraphicsDeviceCreated;
            Graphics.GraphicsDeviceReset += OnGraphicsDeviceReset;
            Graphics.ClientSizeChanged += OnClientSizeChanged;

#if DEBUG
            //  Instantiate the fps counter
            _fps = new FPS();
#endif

            //  Set the root directory for contnet
            Content.RootDirectory = @"Content";

            //  Initialize the Scene system
            Scene = new SceneManager(this);

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
            Scene.HandleClientSizeChanged();
        }

        /// <summary>
        ///     This will be called anytime the <see cref="GraphicsDeviceManager.DeviceCreated"/>
        ///     event is triggered. This should be overritten by the derived game calss to handle
        ///     any logic that needs to be perforemd by the game.
        /// </summary>
        protected virtual void OnGraphicsDeviceCreated(object sender, EventArgs e)
        {
            Scene.HandleGraphicsDeviceCreated();
        }

        /// <summary>
        ///     This will be called anytime the <see cref="GraphicsDeviceManager.DeviceReset"/>
        ///     event is triggered.  When this happens, all contents of VRAM will be discarded
        ///     and things like RenderTargets will need to be recreated.  This shdould be 
        ///     overridden by the derived game class to handle this scenario.
        /// </summary>
        protected virtual void OnGraphicsDeviceReset(object sender, EventArgs e)
        {
            Scene.HandleGraphicsDeviceReset();
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
            SpriteBatch = new SpriteBatch(Graphics.Device);
            SpriteBatchExtensions.Initialize(Graphics.Device);
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
            _fps.Update(gameTime);
#endif

            //  Update the input state
            Input.Update(Time);

            //  Update the current scene.
            Scene.Update();

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
            Scene.Draw();

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
    }
}
