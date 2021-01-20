using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tiny
{
    public struct BMFontGlyph
    {
        public char Character;
        public Texture2D Texture;
        public Vector2 Position;
        public Rectangle SourceRectangle;
        public Vector2 Scale;
    }
}
