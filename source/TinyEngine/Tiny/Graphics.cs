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
using Microsoft.Xna.Framework.Graphics;

namespace Tiny
{
    /// <summary>
    ///     Manages the graphics and presentation of graphics for the game.
    /// </summary>
    public class Graphics
    {
        //  A reference to the game instance.
        private readonly Game _game;

        //  The amount of padding to apply to the outside of the viewport.
        private int _viewPadding;

        //  Indicates if the client (window) is currently resizing.
        private bool _isResizing;

        //  The size of the backbuffer resolution.
        private Point _resolution;

        //  The size of the virtual rendering resolution.
        private Point _vResolution;

        //  The viewport that describes the bounds for rendering to the screen.
        private Viewport _viewPort;

        //  The screen matrix that describes the cale to use when rendring to the screen.
        private Matrix _screenMatrix;

        //  The number of pixels that make up one graphics unit.
        private int _pixelsPerUnit;

        //  The number of tiles that can fit on the x and y axis based on 
        //  the pixels per unit
        private Point _tileCount;

        /// <summary>
        ///     Gets a <see cref="bool"/> value indicating if this instance has
        ///     been properly disposed of.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        ///     Gets the <see cref="GraphicsDeviceManager"/> instance used to control
        ///     the presentation of grpahics.
        /// </summary>
        public GraphicsDeviceManager DeviceManager { get; private set; }

        /// <summary>
        ///     Gets the <see cref="GraphicsDevice"/> instance used to present the
        ///     graphics.
        /// </summary>
        public GraphicsDevice Device => DeviceManager.GraphicsDevice;

        /// <summary>
        ///     Gets the <see cref="GameWindow"/> instance of the client game
        ///     window the game is being displed on.
        /// </summary>
        public GameWindow Window => _game.Window;

        /// <summary>
        ///     Gets a <see cref="Point"/> value that describes the width and
        ///     height, in pixels, of the rendering resolution of the back buffer.
        /// </summary>
        public Point Resolution => _resolution;

        /// <summary>
        ///     Gets a <see cref="Point"/> value that desctives the width and
        ///     height, in pixels, of the virtual rendering resolution.
        /// </summary>
        public Point VirtualResolution => _vResolution;

        /// <summary>
        ///     Gets or Sets an <see cref="int"/> vlaue that describes the amount
        ///     of padding, in pixels, to apply to the border of the viewport.
        /// </summary>
        public int ViewPadding
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
        public Viewport Viewport => _viewPort;

        /// <summary>
        ///     Gets a <see cref="Matrix"/> value that describes the scale to use
        ///     when rendering graphics to the game window.
        /// </summary>
        /// <remarks>
        ///     This value should be set as the <c>transformationMatrix</c> parameter
        ///     of the <see cref="SpriteBatch"/> when calling <c>SpriteBatch.Begin()</c>
        ///     when rendering to the game window.
        /// </remarks>
        public Matrix ScreenMatrix => _screenMatrix;

        /// <summary>
        ///     Gets or Sets a <see cref="Color"/> value to use by default when clearing
        ///     the back buffer.
        /// </summary>
        public Color ClearColor { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="int"/> value that describes the number of pixels
        ///     that equal one graphical unit.
        /// </summary>
        public int PixelPerUnit
        {
            get { return _pixelsPerUnit; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Pixels per unit must be greater than 0");
                }

                _pixelsPerUnit = value;
                _tileCount.X = Resolution.X / value;
                _tileCount.Y = Resolution.Y / value;
            }
        }

        /// <summary>
        ///     Gets a <see cref="Point"/> value that describes the total number of
        ///     tiles that can fit within the bounds of the <see cref="Resolution"/>
        ///     on the x and y axes, based on the <see cref="PixelPerUnit"/> value.
        /// </summary>
        public Point TileCount => _tileCount;

        /// <summary>
        ///     An <see cref="event"/> that is triggered whenever the
        ///     <see cref="GraphicsDeviceManager.DeviceReset"/> event is triggered.
        /// </summary>
        /// <remarks>
        ///     When the graphics device is reset, all contents of VRAM are wiped clean, so
        ///     things like RenderTargets will need to be recreated.
        /// </remarks>
        public event EventHandler GraphicsDeviceReset;

        /// <summary>
        ///     An <see cref="event"/> that is triggered whenever the
        ///     <see cref="GraphicsDeviceManager.DeviceCreated"/> event is triggered.
        /// </summary>
        public event EventHandler GraphicsDeviceCreated;

        /// <summary>
        ///     An <see cref="event"/> that is triggered whenever the
        ///     <see cref="GameWindow.ClientSizeChanged"/> event is triggered.
        /// </summary>
        public event EventHandler ClientSizeChanged;

        /// <summary>
        ///     Creates a new <see cref="Graphics"/> instance.
        /// </summary>
        /// <param name="game">
        ///     The <see cref="Game"/> instance.
        /// </param>
        /// <param name="options">
        ///     A <see cref="GraphicsOptions"/> value that describes the settings to use
        ///     for the graphics.
        /// </param>
        public Graphics(Game game, GraphicsOptions options)
        {
            _game = game;
            ClearColor = Color.Black;

            //  If there is already a GraphicsDeviceManager instnace, se it insetead of
            //  creating a new one.
            if (game.Services.GetService<IGraphicsDeviceManager>() is GraphicsDeviceManager manager)
            {
                DeviceManager = manager;
            }
            else
            {
                DeviceManager = new GraphicsDeviceManager(game);
            }

            //  Bind to events
            DeviceManager.DeviceReset += OnGraphicsDeviceReset;
            DeviceManager.DeviceCreated += OnGraphicsDeviceCreated;
            Window.ClientSizeChanged += OnClientSizeChanged;

            //  Set options
            DeviceManager.SynchronizeWithVerticalRetrace = options.SynchronizeWithVerticalRetrace;
            DeviceManager.PreferMultiSampling = options.PreferMultiSampling;
            DeviceManager.GraphicsProfile = options.GraphicsProfile;
            DeviceManager.PreferredBackBufferFormat = options.PreferredBackBufferFormat;
            DeviceManager.PreferredDepthStencilFormat = options.PrefferedDepthStencilFormat;
            Window.AllowUserResizing = options.AllowUserResizeWindow;
            _game.IsMouseVisible = options.IsMouseVisible;
        }

        /// <summary>
        ///     Initializes the graphics.
        /// </summary>
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
        public void Initialize(int width, int height, int windowWidth, int windowHeight, bool fullscreen)
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
                DeviceManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                DeviceManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                DeviceManager.IsFullScreen = true;
            }
            else
            {
                DeviceManager.PreferredBackBufferWidth = windowWidth;
                DeviceManager.PreferredBackBufferHeight = windowHeight;
                DeviceManager.IsFullScreen = false;
            }

            //  Apply the changes.
            DeviceManager.ApplyChanges();
        }

        /// <summary>
        ///     This is called when the <see cref="GameWindow.ClientSizeChanged"/> event is 
        ///     triggered.
        /// </summary>
        private void OnClientSizeChanged(object sender, EventArgs e)
        {
            if (Window.ClientBounds.Width > 0 && Window.ClientBounds.Height > 0 && !_isResizing)
            {
                _isResizing = true;

                DeviceManager.PreferredBackBufferWidth = Window.ClientBounds.Width;
                DeviceManager.PreferredBackBufferHeight = Window.ClientBounds.Height;

                UpdateView();

                _isResizing = false;

                ClientSizeChanged?.Invoke(sender, e);
            }
        }

        /// <summary>
        ///     This is called when the <see cref="GraphicsDeviceManager.DeviceReset"/> event
        ///     is triggered.
        /// </summary>
        private void OnGraphicsDeviceReset(object sender, EventArgs e)
        {
            UpdateView();
            GraphicsDeviceReset?.Invoke(sender, e);
        }

        /// <summary>
        ///     This is called when the <see cref="GraphicsDeviceManager.DeviceCreated"/> event
        ///     is triggered.
        /// </summary>
        private void OnGraphicsDeviceCreated(object sender, EventArgs e)
        {
            UpdateView();
            GraphicsDeviceCreated?.Invoke(sender, e);
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
        public void SetWindowed(int width, int height)
        {
            _isResizing = true;

            DeviceManager.PreferredBackBufferWidth = width > 0 ? width
                : throw new ArgumentOutOfRangeException(nameof(width), "The client window width must be greater than zero");
            DeviceManager.PreferredBackBufferHeight = height > 0 ? height
                : throw new ArgumentOutOfRangeException(nameof(height), "The client window height must be greater than zero");
            DeviceManager.IsFullScreen = false;

            DeviceManager.ApplyChanges();

            _isResizing = false;
        }

        /// <summary>
        ///     Sets the grpahics to render the game in fullscreen mode.
        /// </summary>
        public void SetFullscren()
        {
            _isResizing = true;

            DeviceManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            DeviceManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            DeviceManager.IsFullScreen = true;

            DeviceManager.ApplyChanges();

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
        private void UpdateView()
        {
            float screenWidth = Device.PresentationParameters.BackBufferWidth;
            float screenHeight = Device.PresentationParameters.BackBufferHeight;

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

            _screenMatrix = Matrix.CreateScale(_vResolution.X / (float)_resolution.X);

            _viewPort = new Viewport
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
        ///     Disposes of resources managed by this instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Disposes of resources managed by this instance.
        /// </summary>
        /// <param name="isDisposing">
        ///     A <see cref="bool"/> value that indicates if the managed resources
        ///     sohould be disposed of.
        /// </param>
        private void Dispose(bool isDisposing)
        {
            if (IsDisposed) { return; }

            if (isDisposing)
            {
                DeviceManager.DeviceCreated -= OnGraphicsDeviceCreated;
                DeviceManager.DeviceReset -= OnGraphicsDeviceReset;
                Window.ClientSizeChanged -= OnClientSizeChanged;
            }

            IsDisposed = true;
        }
    }
}
