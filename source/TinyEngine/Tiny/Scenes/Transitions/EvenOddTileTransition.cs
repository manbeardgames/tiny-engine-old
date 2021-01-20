using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tiny
{
    /// <summary>
    ///     A transition that divides the scene into a checkerboard and spins out/in the odd tiles
    ///     then the even tiles
    /// </summary>
    public class EvenOddTileTransition : SceneTransition
    {
        //  Have the transition time.
        private double _transitionHalfTime;

        //  The width and height, in pixels,  of a tile.
        private int _tileSize;

        //  The total number of columns.
        private int _columns;

        //  The total number of rows.
        private int _rows;

        /// <summary>
        ///     Creates a new <see cref="EvenOddTileTransition"/> instance with a default
        ///     time of 1 second.
        /// </summary>
        /// <remarks>
        ///     WARNING: This constructor uses the <see cref="Graphics.PixelPerUnit"/> value
        ///     as the tilesize value for this transition. This constructor will only
        ///     work if you have set that value. 
        /// </remarks>
        public EvenOddTileTransition() : this(Engine.PixelPerUnit, TimeSpan.FromSeconds(1)) { }

        /// <summary>
        ///     Creates a new <see cref="EvenOddTileTransition"/> instance.
        /// </summary>
        /// <param name="tileSize">
        ///     A <see cref="int"/> value that defines the width and height,
        ///     in pixels, of the generated tiles when transitioning.
        /// </param>
        /// <param name="transitionTime">
        ///     A <see cref="TimeSpan"/> value that represents the total amount of
        ///     time this transition should take to complete.
        /// </param>
        public EvenOddTileTransition(int tileSize, TimeSpan transitionTime) : base(transitionTime)
        {
            _transitionHalfTime = TransitionTime.TotalSeconds / 2;
            _tileSize = tileSize;
        }

        /// <summary>
        ///     Starts this transition.
        /// </summary>
        /// <param name="sceneTexture">
        ///     A reference to the <see cref="TinyRenderTarget"/> instance that the scene being
        ///     transitioned is rendered to.
        /// </param>
        public override void Start(TinyRenderTarget sceneTexture)
        {
            base.Start(sceneTexture);

            _columns = (int)Math.Ceiling(SceneTexture.Width / (float)_tileSize);
            _rows = (int)Math.Ceiling(SceneTexture.Height / (float)_tileSize);
        }

        /// <summary>
        ///     Draws this transition.
        /// </summary>
        protected override void Draw()
        {
            for (int row = 0; row < _rows; row++)
            {
                for (int column = 0; column < _columns; column++)
                {
                    int size = GetSize(Maths.IsOdd(column, row));
                    int xPos = ((column * _tileSize) + (_tileSize - size) / 2) + (size / 2);
                    int yPos = ((row * _tileSize) + (_tileSize - size) / 2) + (size / 2);

                    SpriteBatch.Draw(texture: SceneTexture,
                                     destinationRectangle: new Rectangle(xPos, yPos, size, size),
                                     sourceRectangle: new Rectangle(column * _tileSize, row * _tileSize, _tileSize, _tileSize),
                                     color: Color.White,
                                     rotation: GetRotation(Maths.IsOdd(column, row)),
                                     origin: new Vector2(_tileSize, _tileSize) * 0.5f,
                                     effects: SpriteEffects.None,
                                     layerDepth: 0.0f);
                }
            }
        }

        /// <summary>
        ///     Calculates the rotation of a tile based on the remaining time in the transition.
        /// </summary>
        /// <param name="isOdd">
        ///     A <see cref="bool"/> value that indicates if the tile we are getting the rotation for
        ///     is an odd tile. An odd tile is one that is in a row and column where both are even number, or
        ///     where both are odd numbers.
        /// </param>
        /// <returns>
        ///     A <see cref="float"/> value that represents the rotation of the tile, in radians.
        ///     The rotation value to use for the tile.
        /// </returns>
        private float GetRotation(bool isOdd)
        {
            double timeLeft = TransitionTimeRemaining.TotalSeconds;

            if (isOdd)
            {
                timeLeft = Math.Min(timeLeft, _transitionHalfTime);
            }
            else
            {
                timeLeft = Math.Max(timeLeft - _transitionHalfTime, 0);
            }

            if (Kind == SceneTransitionKind.Out)
            {
                return 5.0f * (float)Math.Sin((timeLeft / _transitionHalfTime) - 1.0);
            }
            else
            {
                return 5.0f * (float)Math.Sin((timeLeft / _transitionHalfTime));
            }
        }

        /// <summary>
        ///     Calculates the size of a tile based on the remaining time in the transition.
        /// </summary>
        /// <param name="isOdd">
        ///     A <see cref="bool"/> value that indicates if the tile we are getting the rotation for
        ///     is an odd tile. An odd tile is one that is in a row and column where both are even number, or
        ///     where both are odd numbers.
        /// </param>
        /// <returns>
        ///     A <see cref="int"/> value that represents the size of the tile.
        /// </returns>
        private int GetSize(bool isOdd)
        {
            double timeLeft = TransitionTimeRemaining.TotalSeconds;

            if (isOdd)
            {
                timeLeft = Math.Min(timeLeft, _transitionHalfTime);
            }
            else
            {
                timeLeft = Math.Max(timeLeft - _transitionHalfTime, 0);
            }

            if (Kind == SceneTransitionKind.Out)
            {
                return (int)((_tileSize) * (timeLeft / _transitionHalfTime));
            }
            else
            {
                return (int)((_tileSize) * (1 - (timeLeft / _transitionHalfTime)));
            }
        }
    }
}
