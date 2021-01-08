using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tiny
{
    /// <summary>
    ///     A class that represents text to render using the spritebatch.
    /// </summary>
    public class Text
    {
        //  The string value of this text instance.
        private string _value;

        //  The font used when rendering this text.
        private SpriteFont _font;

        //  The width and height size of the text when rendered.
        private Vector2 _size;

        //  Half the size of the text when rendered.
        private Vector2 _halfSize;

        //  The scale to render the text at.
        private Vector2 _scale;

        //  The position to render the text at.
        private Vector2 _position;

        /// <summary>
        ///     Gets or Sets a <see cref="string"/> value that represents
        ///     the text to use when rendering.
        /// </summary>
        public string Value
        {
            get { return _value; }
            set
            {
                if (_value.Equals(value)) { return; }

                _value = value;
                SetSize();
            }
        }

        /// <summary>
        ///     Gets or Sets the <see cref="SpriteFont"/> value to use
        ///     when rendering.
        /// </summary>
        public SpriteFont Font
        {
            get { return _font; }
            set
            {
                if (_font.Equals(value)) { return; }

                _font = value;
                SetSize();
            }
        }

        /// <summary>
        ///     Gets or Sets a <see cref="Vector2"/> value that describes
        ///     the xy-coordinate position to render this at.
        /// </summary>
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                _position.Round();
            }
        }

        /// <summary>
        ///     Gets a <see cref="Vector2"/> value that describes the width
        ///     and height, in pixels, of this text when rendered.
        /// </summary>
        public Vector2 Size => _size;

        /// <summary>
        ///     Gets a <see cref="Vector2"/> value that describes half the
        ///     width and height, in pixels, of this text when rendered.
        /// </summary>
        public Vector2 HalfMeasurement => _halfSize;

        /// <summary>
        ///     Gets a <see cref="Color"/> value that describes the color
        ///     of the text when rendered.
        /// </summary>
        public Color Color;

        /// <summary>
        ///     Gets or Sets a <see cref="Vector2"/> value that desribes the
        ///     scale of the text when rendered on the x and y axes.
        /// </summary>
        public Vector2 Scale
        {
            get { return _scale; }
            set
            {
                if (_scale.Equals(value)) { return; }
                _scale = value;
                SetSize();
            }
        }

        /// <summary>
        ///     Gets or Sets a <see cref="Vector2"/> value that describes the
        ///     center of rotation when rendering this text.
        /// </summary>
        public Vector2 Origin { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="float"/> that describes the angle of
        ///     rotation, in radians, to render the text at.
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="SpriteEffects"/> value that describes
        ///     the horizontal and/or vertical flip effect to use when rendering
        ///     this text.
        /// </summary>
        public SpriteEffects Effect { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="float"/> value that describes the z-buffer
        ///     depth of the text when rendered.
        /// </summary>
        public float LayerDepth { get; set; }

        /// <summary>
        ///     Creates a new <see cref="Text"/> instance.
        /// </summary>
        /// <param name="font">
        ///     A <see cref="SpriteFont"/> value that represents the font to
        ///     use when rendering.
        /// </param>
        /// <param name="text">
        ///     A <see cref="string"/> value that represents the text to
        ///     use when rendering.
        /// </param>
        /// <param name="position">
        ///     A <see cref="Vector2"/> value that describes the xy-coordinate
        ///     position to render this at.
        /// </param>
        public Text(SpriteFont font, string text)
            : this(font, text, Vector2.Zero, Color.White, 0.0f, Vector2.Zero, Vector2.One) { }

        /// <summary>
        ///     Creates a new <see cref="Text"/> instance.
        /// </summary>
        /// <param name="font">
        ///     A <see cref="SpriteFont"/> value that represents the font to
        ///     use when rendering.
        /// </param>
        /// <param name="text">
        ///     A <see cref="string"/> value that represents the text to
        ///     use when rendering.
        /// </param>
        /// <param name="position">
        ///     A <see cref="Vector2"/> value that describes the xy-coordinate
        ///     position to render this at.
        /// </param>
        public Text(SpriteFont font, string text, Vector2 position)
            : this(font, text, position, Color.White, 0.0f, Vector2.Zero, Vector2.One) { }

        /// <summary>
        ///     Creates a new <see cref="Text"/> instance.
        /// </summary>
        /// <param name="font">
        ///     A <see cref="SpriteFont"/> value that represents the font to
        ///     use when rendering.
        /// </param>
        /// <param name="text">
        ///     A <see cref="string"/> value that represents the text to
        ///     use when rendering.
        /// </param>
        /// <param name="position">
        ///     A <see cref="Vector2"/> value that describes the xy-coordinate
        ///     position to render this at.
        /// </param>
        public Text(SpriteFont font, string text, Vector2 position, Color color)
            : this(font, text, position, color, 0.0f, Vector2.Zero, Vector2.One) { }

        /// <summary>
        ///     Creates a new <see cref="Text"/> instance.
        /// </summary>
        /// <param name="font">
        ///     A <see cref="SpriteFont"/> value that represents the font to
        ///     use when rendering.
        /// </param>
        /// <param name="text">
        ///     A <see cref="string"/> value that represents the text to
        ///     use when rendering.
        /// </param>
        /// <param name="position">
        ///     A <see cref="Vector2"/> value that describes the xy-coordinate
        ///     position to render this at.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Color"/> value that describes the color of the
        ///     text when rendered.
        /// </param>
        /// <param name="rotation">
        ///     A <see cref="float"/> that describes the angle of rotation,
        ///     in radians, to render the text at.
        /// </param>
        /// <param name="origin">
        ///     A <see cref="Vector2"/> value that describes the center of
        ///     rotation when rendering this text.
        /// </param>
        /// <param name="scale">
        ///     A <see cref="float"/> value that desribes the scale of the
        ///     text when rendered on the x and y axes.
        /// </param>
        public Text(SpriteFont font, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale)
            : this(font, text, position, color, rotation, origin, new Vector2(scale, scale)) { }

        /// <summary>
        ///     Creates a new <see cref="Text"/> instance.
        /// </summary>
        /// <param name="font">
        ///     A <see cref="SpriteFont"/> value that represents the font to
        ///     use when rendering.
        /// </param>
        /// <param name="text">
        ///     A <see cref="string"/> value that represents the text to
        ///     use when rendering.
        /// </param>
        /// <param name="position">
        ///     A <see cref="Vector2"/> value that describes the xy-coordinate
        ///     position to render this at.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Color"/> value that describes the color of the
        ///     text when rendered.
        /// </param>
        /// <param name="rotation">
        ///     A <see cref="float"/> that describes the angle of rotation,
        ///     in radians, to render the text at.
        /// </param>
        /// <param name="origin">
        ///     A <see cref="Vector2"/> value that describes the center of
        ///     rotation when rendering this text.
        /// </param>
        /// <param name="scale">
        ///     A <see cref="Vector2"/> value that desribes the scale of the
        ///     text when rendered on the x and y axes.
        /// </param>
        public Text(SpriteFont font, string text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale)
        {
            _font = font;
            _value = text;
            Position = position;
            Color = color;
            Rotation = rotation;
            Origin = origin;
            _scale = scale;

            SetSize();
        }

        /// <summary>
        ///     Sets the size and half size measurements of this <see cref="Text"/>.
        /// </summary>
        private void SetSize()
        {
            _size = _font.MeasureString(_value) * _scale;
            _halfSize = _size * 0.5f;
        }

        /// <summary>
        ///     Center the <see cref="Origin"/> point of the text.
        /// </summary>
        public void CenterOrigin()
        {
            Origin = _halfSize;
        }

    }
}
