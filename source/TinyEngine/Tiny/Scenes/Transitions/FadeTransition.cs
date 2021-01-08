using System;
using Microsoft.Xna.Framework;

namespace Tiny
{
    /// <summary>
    ///     A transition that fades the scene out/in.
    /// </summary>
    public class FadeTransition : SceneTransition
    {
        /// <summary>
        ///     Creates a new <see cref="FadeTransition"/> instance with with
        ///     a default time of 1 second.
        /// </summary>
        /// <param name="engine">
        ///     A reference to the <see cref="Engine"/> instance.
        /// </param>
        public FadeTransition(Engine engine)
            : this(engine, TimeSpan.FromSeconds(1)) { }

        /// <summary>
        ///     Creates a new <see cref="FadeTransition"/> instance.
        /// </summary>
        /// <param name="engine">
        ///     A reference to the <see cref="Engine"/> instance.
        /// </param>
        /// <param name="transitionTime">
        ///     A <see cref="TimeSpan"/> value that represents the total amount of
        ///     time this transition should take to complete.
        /// </param>
        public FadeTransition(Engine engine, TimeSpan transitionTime)
            : base(engine, transitionTime) { }

        /// <summary>
        ///     Draws this transition.
        /// </summary>
        protected override void Draw()
        {
            SpriteBatch.Draw(texture: SceneTexture,
                                destinationRectangle: SceneTexture.Bounds,
                                sourceRectangle: SceneTexture.Bounds,
                                color: Color.White * GetAlpha());
        }

        /// <summary>
        ///     Gets the alpha value to use for the color mask when rendering.
        /// </summary>
        /// <returns>
        ///     The value to use for the color mask alpha
        /// </returns>
        private float GetAlpha()
        {
            double timeLeft = TransitionTimeRemaining.TotalSeconds;

            if (Kind == SceneTransitionKind.Out)
            {
                return (float)(timeLeft / TransitionTime.TotalSeconds);
            }
            else
            {
                return (float)(1.0 - (timeLeft / TransitionTime.TotalSeconds));
            }
        }
    }
}
