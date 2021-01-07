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
    ///     Manages the scenes for TinyEngine.
    /// </summary>
    public class SceneManager
    {
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

        //  A cached refernece to the engine.
        private Engine _engine;

        /// <summary>
        ///     Creates a new <see cref="SceneManager"/> instance.
        /// </summary>
        /// <param name="engine">
        ///     A refernce to the <see cref="Game"/> instance that is derived
        ///     from <see cref="Engine"/>.
        /// </param>
        public SceneManager(Engine engine)
        {
            _engine = engine;
        }

        /// <summary>
        ///     Performs the update logic for scenes.
        /// </summary>
        internal void Update()
        {
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
            }

            //  If there is a current active scene, update it.
            if (_activeScene != null)
            {
                _activeScene.Update();
            }
        }

        /// <summary>
        ///     Performs the rendering logic for the scenes.
        /// </summary>
        internal void Draw()
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
            _engine.Graphics.Device.Clear(_engine.Graphics.ClearColor);

            //  Begin the spritebatch
            _engine.SpriteBatch.Begin(blendState: BlendState.AlphaBlend,
                                      samplerState: SamplerState.PointClamp,
                                      transformMatrix: _engine.Graphics.ScreenMatrix);

            //  If there is an active transition we draw its render target; otherwise, we draw 
            //  the render target of the active scene
            if (_activeTransition != null && _activeTransition.IsTransitioning)
            {
                _engine.SpriteBatch.Draw(texture: _activeTransition.RenderTarget,
                                         destinationRectangle: _activeTransition.RenderTarget.Bounds,
                                         sourceRectangle: _activeTransition.RenderTarget.Bounds,
                                         color: Color.White);
            }
            else if (_activeScene != null)
            {
                _engine.SpriteBatch.Draw(texture: _activeScene.RenderTarget,
                                         destinationRectangle: _activeScene.RenderTarget.Bounds,
                                         sourceRectangle: _activeScene.RenderTarget.Bounds,
                                         color: Color.White);
            }

            //  End the sprite batch
            _engine.SpriteBatch.End();
        }

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
                    _transitionOut.Kind = SceneTransitionKind.Out;

                    _transitionIn = transitionIn;
                    _transitionIn.Kind = SceneTransitionKind.In;

                    //  Subscribe to the transition compoleted events for each
                    _transitionOut.TransitionCompleted += TransitionOutCompleted;
                    _transitionIn.TransitionCompleted += TransitionInCompleted;

                    //  Set the current active transition to the out transition first
                    _activeTransition = _transitionOut;

                    //  Start the current transition
                    _activeTransition.Start(_activeScene.RenderTarget);
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

            //  Set the current transition to the in transition and start it
            _activeTransition = _transitionIn;
            _activeTransition.Start(_activeScene.RenderTarget);
        }

        /// <summary>
        ///     Handles the logic when the <see cref="_transitionIn"/> instance
        ///     triggers the <see cref="SceneTransition.TransitionCompleted"/> event.
        /// </summary>
        private void TransitionInCompleted(object sender, EventArgs e)
        {
            //  Unsubscribe from the event so we don't leave any lingering references
            _transitionIn.TransitionCompleted -= TransitionInCompleted;

            //  Dipsoe of the instance
            _transitionIn.Dispose();
            _transitionIn = null;

            //  Set the active transition to null
            _activeTransition = null;
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
            _engine.Time.TimeRate = 1.0f;

            //  Null out the _nextScene field so we don't keep trying to transition
            _nextScene = null;

            //  If the now active scene is not null, initilize it here.
            //  Reminder that the Initialize(0 method calls the LoadContnet method
            if (_activeScene != null)
            {
                _activeScene.Initialize();
            }
        }

        /// <summary>
        ///     Handles the <see cref="GraphicsDeviceManager.DeviceCreated"/> event for the
        ///     scene.
        /// </summary>
        public void HandleGraphicsDeviceCreated()
        {
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
        ///     Handles the <see cref="GraphicsDeviceManager.DeviceReset"/> event for the
        ///     scene.
        /// </summary>
        public void HandleGraphicsDeviceReset()
        {
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
        ///     Handles the <see cref="GameWindow.ClientSizeChanged"/> event for the
        ///     scene.
        /// </summary>
        public void HandleClientSizeChanged()
        {
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
    }
}
