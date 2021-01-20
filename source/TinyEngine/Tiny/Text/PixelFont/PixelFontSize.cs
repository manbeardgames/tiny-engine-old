using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tiny
{
    public class PixelFontSize
    {
        private StringBuilder _builder;
        private Dictionary<int, PixelFontCharacter> _characters;

        public List<Texture2D> Textures;
        public int LineHeight;
        public float Size;
        public bool Outline;

        public PixelFontCharacter this[int id]
        {
            get
            {
                if(_characters.TryGetValue(id, out PixelFontCharacter value))
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }
        }

        public PixelFontSize()
        {
            _builder = new StringBuilder();
            _characters = new Dictionary<int, PixelFontCharacter>();
        }

        public bool TryGetCharacter(int id, out PixelFontCharacter character)
        {
            return _characters.TryGetValue(id, out character);
        }

        public void AddCharacter(PixelFontCharacter character)
        {
            if(_characters.ContainsKey(character.Character))
            {
                return;
            }

            _characters.Add(character.Character, character);

        } 

        public Rectangle GetBounds(string text, Vector2 at)
        {
            Vector2 size = Measure(text);
            size.Round();
            at.Round();
            return new Rectangle((int)at.X, (int)at.Y, (int)size.X, (int)size.Y);
        }

        public Vector2 Measure(char character)
        {
            if(TryGetCharacter(character, out PixelFontCharacter result))
            {
                return new Vector2(result.XAdvance, LineHeight);
            }
            else
            {
                return Vector2.Zero;
            }
        }

        public Vector2 Measure(string text)
        {
            //  Immediatly check for empty string and return early
            if(string.IsNullOrEmpty(text))
            {
                return Vector2.Zero;
            }

            Vector2 result = new Vector2(0, LineHeight);
            float lineWidth = 0.0f;


            for(int i = 0; i < text.Length; i++)
            {
                if(text[i] == '\n' || text[i] == '\r')
                {
                    result.X = Math.Max(lineWidth, result.X);
                    result.Y += LineHeight;

                    lineWidth = 0.0f;
                }
                else
                {
                    if(_characters.TryGetValue(text[i], out PixelFontCharacter character))
                    {
                        lineWidth += character.XAdvance;

                        if(i < text.Length -1 && character.TryGetKerning(text[i + 1], out int kerning))
                        {
                            lineWidth += kerning;
                        }

                    }
                }

            }

            result.X = Math.Max(lineWidth, result.X);

            return result;
        }

        public float HeightOf(string text)
        {
            //  Immediatly check for empty string and return early
            if (string.IsNullOrEmpty(text))
            {
                return 0;
            }

            int lines = text.Split(new char[] { '\n', '\r' }).Length;
            return lines * LineHeight;
        }


    }
}
