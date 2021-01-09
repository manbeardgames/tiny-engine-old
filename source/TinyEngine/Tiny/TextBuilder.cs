using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tiny
{
    public class TextBuilder
    {
        private List<Text> _text;
        private Vector2 _position;
        private Vector2 _size;
        private Vector2 _origin;
        private Vector2 _renderPosition;

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                if (_position.Equals(value)) { return; }
                _position = value;
                SetRenderPosition();
            }
        }

        public Vector2 Origin
        {
            get { return _origin; }
            set
            {
                if (_origin.Equals(value)) { return; }
                _origin = value;
                SetRenderPosition();
            }
        }

        public Vector2 Size => _size;

        public ReadOnlyCollection<Text> Text { get; private set; }

        public TextBuilder() : this(Vector2.Zero) { }

        public TextBuilder(Vector2 position)
        {
            _text = new List<Text>();
            Text = _text.AsReadOnly();

            _position = position;
            SetSize();
            SetRenderPosition();
        }

        public void Add(SpriteFont font, string text)
        {
            Add(font, text, Color.White);
        }

        public void Add(SpriteFont font, string text, Color color)
        {
            Text toAdd = new Text(font, text, color);

            _text.Add(toAdd);
            SetSize();
            SetRenderPosition();
        }


        public void Clear()
        {
            _text.Clear();
            SetSize();
            SetRenderPosition();
        }

        private void SetRenderPosition()
        {
            _renderPosition.X = _position.X - _origin.X;
            _renderPosition.Y = _position.Y - _origin.Y;

            Vector2 nextPosition = Vector2.Zero;

            for (int i = 0; i < _text.Count; i++)
            {
                Text text = _text[i];

                if (i == 0)
                {
                    text.Position = _renderPosition;
                }
                else
                {
                    text.Position = nextPosition;
                }

                nextPosition = new Vector2
                {
                    X = text.Position.X + text.Size.X,
                    Y = _renderPosition.Y
                };
            }
        }

        private void SetSize()
        {
            _size = Vector2.Zero;
            for (int i = 0; i < _text.Count; i++)
            {
                _size.X += _text[i].Size.X;
                _size.Y = (float)Math.Max(_size.Y, _text[i].Size.Y);
            }
        }



    }
}
