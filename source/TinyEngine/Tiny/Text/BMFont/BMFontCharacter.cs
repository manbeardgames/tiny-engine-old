using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tiny
{

    public class BMFontCharacter
    {
        public int Character { get; }
        public Texture2D Texture { get; }
        public Point Offset { get; }
        public int XAdvance { get; }
        public Rectangle SourceRectange { get; }
        public Dictionary<int, int> Kernings { get; }

        public BMFontCharacter(Texture2D page, Rectangle bounds, int character, int xOffset, int yOffset, int xAdvance)
        {
            Texture = page;
            SourceRectange = bounds;
            Character = character;
            Offset = new Point(xOffset, yOffset);
            XAdvance = xAdvance;
            Kernings = new Dictionary<int, int>();
        }
    }
}
