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
using System;


namespace Tiny
{
    /// <summary>
    ///     An abstract class used to create transition effects when switching from
    ///     one scene to another. 
    /// </summary>
    public abstract class SceneTransition : IDisposable
    {
        /// <summary>
        ///     Gets a <see cref="bool"/> value indicating if this instance has
        ///     been properly disposed of.
        /// </summary>
        public bool IsDisposed { get; protected set; }

        /// <summary>
        ///     Gets a <see cref="bool"/> value indicating this is currently in
        ///     the middle of a transition.
        /// </summary>
        public bool IsTransitioning { get; private set; }

        /// <summary>
        ///     Gets a <see cref="SceneTransitionKind"/> value describing they
        ///     kind of transition that is occuring.
        /// </summary>
        public SceneTransitionKind Kind { get; internal set; }

        /// <summary>
        ///     Gets a <see cref="TimeSpan"/> value indicating the total amount
        ///     of time required for this transition to complete.
        /// </summary>
        public TimeSpan TransitionTime { get; private set; }

        /// <summary>
        ///     Gets a <see cref="TimeSpan"/> value indicating the amount of
        ///     time remaining for this transition to complete.
        /// </summary>
        public TimeSpan TransitionTimeRemaining { get; private set; }

        /// <summary>
        ///     Gets the <see cref="RenderTarget2D"/> instance of the scene that will
        ///     be used for the transition.
        /// </summary>
        public TinyRenderTarget SceneTexture { get; private set; }

        /// <summary>
        ///     Gets the <see cref="RenderTarget2D"/> instance that this transition
        ///     will be rendered to.
        /// </summary>
        public TinyRenderTarget RenderTarget { get; private set; }

        /// <summary>
        ///     Gets the <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/>
        ///     instance used for rendering.
        /// </summary>
        public SpriteBatch SpriteBatch => Engine.Instance.SpriteBatch;

        /// <summary>
        ///     An <see cref="event"/> that will be triggered when this transition
        ///     has been completed.
        /// </summary>
        public event EventHandler TransitionCompleted;

        /// <summary>
        ///     Creates a new <see cref="SceneTransition"/> instance.
        /// </summary>
        /// <param name="transitionTime">
        ///     A <see cref="TimeSpan"/> value that represents the total amount of
        ///     time this transition should take to complete.
        /// </param>
        public SceneTransition(TimeSpan transitionTime)
        {
            TransitionTimeRemaining = TransitionTime = transitionTime;

            RenderTarget = new TinyRenderTarget(width: Engine.Resolution.X,
                                                height: Engine.Resolution.Y,
                                                multiSampleCount: 0,
                                                depth: true,
                                                preserve: true);
        }

        /// <summary>
        ///     Starts this transition.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         When overriding this, ensure that base.Start() is called so that the 
        ///         <see cref="SceneTexture"/> property can be initialized properly and that
        ///         <see cref="IsTransitioning"/> can be set.
        ///     </para>
        /// </remarks>
        /// <param name="sceneTexture">
        ///     A reference to the <see cref="RenderTarget2D"/> instance that the scene being
        ///     transitioned is rendered to.
        /// </param>
        public virtual void Start(TinyRenderTarget sceneTexture)
        {
            SceneTexture = sceneTexture;
            IsTransitioning = true;
        }

        /// <summary>
        ///     Perform all update logic for the transition here.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         When overiding this, ensure that base.Update() is called so that the
        ///         timing values for the transition can be updated properly, or you will 
        ///         need to update them manually.
        ///     </para>
        /// </remarks>
        public virtual void Update()
        {
            TransitionTimeRemaining -= Engine.Time.ElapsedGameTime;

            if (TransitionTimeRemaining <= TimeSpan.Zero)
            {
                IsTransitioning = false;

                if (TransitionCompleted != null)
                {
                    TransitionCompleted(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Renders this transition to its render target.
        /// </summary>
        public void Render()
        {
            BeginDraw();
            Draw();
            EndDraw();
        }

        /// <summary>
        ///     Prepares the transition to begin drawing by preparing the graphics
        ///     device and beginning the spritebatch.
        /// </summary>
        /// <remarks>
        ///     <para>  
        ///         Override this method if you need to prepare the graphics device 
        ///         or set custom parameters for the spritebatch begin.
        ///     </para>
        /// </remarks>
        protected virtual void BeginDraw()
        {
            //  Prepare the graphics device
            Engine.Instance.GraphicsDevice.SetRenderTarget(RenderTarget);
            Engine.Instance.GraphicsDevice.Viewport = new Viewport(RenderTarget.Bounds);
            Engine.Instance.GraphicsDevice.Clear(Engine.ClearColor);

            SpriteBatch.Begin(blendState: BlendState.AlphaBlend,
                              samplerState: SamplerState.PointClamp);
        }

        /// <summary>
        ///     Draws this transition.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Override this method to provide the custom drawing for the transition.
        ///     </para>
        /// </remarks>
        protected virtual void Draw() { }

        /// <summary>
        ///     Prepares the transition to end drawing by ending the spritebatch and
        ///     setting the graphics rendertarget to null.
        /// </summary>
        ///     <para>
        ///         Override this method if you need to perform any custom logic for
        ///         ending the draw cycle for the transition.
        ///     </para>
        ///     <para>
        ///         Ensure you call base.EndDraw() to end the spritebatch and set the
        ///         graphics rendertarget to null; otherwise, you'll need to manually do
        ///         this the overridden method.
        ///     </para>
        protected virtual void EndDraw()
        {
            SpriteBatch.End();
            Engine.Instance.GraphicsDevice.SetRenderTarget(null);
        }

        /// <summary>
        ///     Handles the logic when the <see cref="GraphicsDeviceManager.DeviceCreated"/>
        ///     event is triggered.
        /// </summary>
        public virtual void HandleGraphicsDeviceCreated()
        {
            RenderTarget.Reload();
        }

        /// <summary>
        ///     Handles the logic when the <see cref="GraphicsDeviceManager.DeviceReset"/>
        ///     event is triggered.
        /// </summary>
        public virtual void HandleGraphicsDeviceReset()
        {
            RenderTarget.Reload();
        }

        /// <summary>
        ///     Handles the logic when the <see cref="GameWindow.ClientSizeChanged"/>
        ///     event is triggered.
        /// </summary>
        public virtual void HandleClientSizeChanged() { }

        /// <summary>
        ///     Handles properly disposing of this instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Handles properly disposing of this instance.
        /// </summary>
        /// <param name="isDisposing">
        ///     A <see cref="bool"/> value indicating if the managed resources
        ///     should be disposed of.
        /// </param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (isDisposing)
            {
                //  If the render target instnace is not null and it has not been disposed of yet
                //  then we dispose of it here.
                if (RenderTarget != null && !RenderTarget.IsDisposed)
                {
                    RenderTarget.Dispose();
                    RenderTarget = null;
                }
            }

            IsDisposed = true;
        }

    }
}
