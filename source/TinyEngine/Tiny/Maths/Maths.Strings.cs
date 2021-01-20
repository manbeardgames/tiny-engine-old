using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tiny
{
    public static partial class Maths
    {
        private static StringBuilder _tempBuilder = new StringBuilder();

        public static string WordWrap(SpriteFont font, string text, int maxWidth)
        {
            return WordWrap(text, maxWidth, font.MeasureString);
        }

        public static string WordWrap(PixelFontSize font, string text, int maxWidth)
        {
            return WordWrap(text, maxWidth, font.Measure);
        }

        private static string WordWrap(string text, int width, Func<string, Vector2> measureFunc)
        {
            //  Return immediatly if the text is empty.
            if(string.IsNullOrEmpty(text))
            {
                return text;
            }

            _tempBuilder.Clear();

            //  Split on all spaces
            string[] words = Regex.Split(text, @"(\s)");

            //  Used to track the current width of the line
            float lineWidth = 0.0f;

            for (int w = 0; w < words.Length; w++)
            {
                //  Get the width of the word
                float wordWith = measureFunc(words[w]).X;

                //  If we add the width of the word to the current line width
                //  and we go over the alloted width, then we need to insert a
                //  new line
                if (wordWith + lineWidth > width)
                {
                    _tempBuilder.Append('\n');
                    lineWidth = 0.0f;

                    //  If the current word is just a space, then we continue so
                    //  we don't put a space at the beginning of the next line.
                    if (words[w].Equals(" "))
                    {
                        continue;
                    }
                }

                //  Check if the word is longer than the alloted width. If it is,
                //  fit as much of the word in as possible before splitting it.
                if (wordWith > width)
                {
                    int start = 0;
                    for (int i = 0; w < words[w].Length; w++)
                    {
                        if (i - start > 1 && measureFunc(words[w].Substring(start, i - start - 1)).X > width)
                        {
                            _tempBuilder.Append(words[w].Substring(start, i - start - 1));
                            _tempBuilder.Append('\n');
                            start = i - 1;
                        }
                    }

                    string remaining = words[w].Substring(start, words[w].Length - start);
                    _tempBuilder.Append(remaining);
                    lineWidth += measureFunc(remaining).X;
                }
                else
                {
                    //  Word fits on the current line, so just add it
                    lineWidth += wordWith;
                    _tempBuilder.Append(words[w]);
                }
            }

            return _tempBuilder.ToString();
        }
    }
}
