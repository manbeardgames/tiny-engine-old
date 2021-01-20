using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tiny
{
    public static partial class SpriteBatchExtensions
    {
        private const char NEWLINE_CHAR = '\n';
        private const char CARRIAGERETURN_CHAR = '\r';

        /// <summary>
        ///     Draws a <see cref="Text"/> instance to the screen.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="text">
        ///     A <see cref="Text"/> instance that describes the text to render.
        /// </param>
        public static void DrawString(this SpriteBatch spriteBatch, Text text)
        {
            spriteBatch.DrawString(text.Font, text.Value, text.Position, text.Color, text.Rotation, text.Origin, text.Scale, text.Effect, text.LayerDepth);
        }

        /// <summary>
        ///     Draws a <see cref="TextBuilder"/> instance to the screen.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="text">
        ///     A <see cref="TextBuilder"/> instance that describes the text to render.
        /// </param>
        public static void DrawString(this SpriteBatch spriteBatch, TextBuilder text)
        {
            for (int i = 0; i < text.Text.Count; i++)
            {
                spriteBatch.DrawString(text.Text[i]);
            }
        }

        public static void DrawString(this SpriteBatch spriteBatch, PixelFontSize font, string text, Vector2 position, Color color, Vector2 origin, Vector2 scale, float layerDepth)
        {
            //  If the string is empty, just return early
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            Vector2 offset = Vector2.Zero;

            for (int c = 0; c < text.Length; c++)
            {
                //  Handle newline/carriage return
                if (text[c] == '\n' || text[c] == '\r')
                {
                    offset.X = 0;
                    offset.Y += font.LineHeight;
                    continue;
                }

                if (font.TryGetCharacter(text[c], out PixelFontCharacter character))
                {
                    Vector2 renderPosition = position + (offset + character.Offset) * scale;
                    renderPosition -= origin;

                    renderPosition.Round();

                    spriteBatch.Draw(texture: character.Texture,
                                     position: renderPosition,
                                     sourceRectangle: character.SourceRectangle,
                                     color: color,
                                     rotation: 0.0f,
                                     origin: Vector2.Zero,
                                     scale: scale,
                                     effects: SpriteEffects.None,
                                     layerDepth: layerDepth);

                    offset.X += character.XAdvance;

                    if (c < text.Length - 1 && character.TryGetKerning(text[c + 1], out int kerning))
                    {
                        offset.X += kerning;
                    }
                }

            }
        }


        /// <summary>
        ///     Draws a <see cref="string"/> value with an outline applied to it.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="text">
        ///     A <see cref="Text"/> instance that describes the text to render.
        /// </param>
        /// <param name="outlineColor">
        ///     A <see cref="Color"/> value that defines the color mask to apply to the
        ///     outline of the text when drawing.
        /// </param>
        public static void DrawStringOutlined(this SpriteBatch spriteBatch, Text text, Color outlineColor)
        {
            spriteBatch.DrawStringOutlined(text.Font, text.Value, text.Position, text.Color, outlineColor, text.Rotation, text.Origin, text.Scale, text.Effect, text.LayerDepth);
        }



        /// <summary>
        ///     Draws a <see cref="string"/> value with an outline applied to it.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="spriteFont">
        ///     A <see cref="SpriteFont"/> instance that represents the font to use
        ///     when rendering.
        /// </param>
        /// <param name="text">
        ///     A <see cref="string"/> value that contains the text to draw.
        /// </param>
        /// <param name="position">
        ///     A <see cref="Vector2"/> value that defines the xy-coordinate location
        ///     to draw the text.
        /// </param>
        /// <param name="fontColor">
        ///     A <see cref="Color"/> value that defines the color mask to apply to the
        ///     text when drawing.
        /// </param>
        /// <param name="outlineColor">
        ///     A <see cref="Color"/> value that defines the color mask to apply to the
        ///     outline of the text when drawing.
        /// </param>
        public static void DrawStringOutlined(this SpriteBatch spriteBatch,
                                             SpriteFont spriteFont,
                                             string text,
                                             Vector2 position,
                                             Color fontColor,
                                             Color outlineColor)
        {
            spriteBatch.DrawStringOutlined(spriteFont, text, position, fontColor, outlineColor, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
        }

        /// <summary>
        ///     Draws a <see cref="string"/> value with an outline applied to it.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="spriteFont">
        ///     A <see cref="SpriteFont"/> instance that represents the font to use
        ///     when rendering.
        /// </param>
        /// <param name="text">
        ///     A <see cref="string"/> value that contains the text to draw.
        /// </param>
        /// <param name="position">
        ///     A <see cref="Vector2"/> value that defines the xy-coordinate location
        ///     to draw the text.
        /// </param>
        /// <param name="fontColor">
        ///     A <see cref="Color"/> value that defines the color mask to apply to the
        ///     text when drawing.
        /// </param>
        /// <param name="outlineColor">
        ///     A <see cref="Color"/> value that defines the color mask to apply to the
        ///     outline of the text when drawing.
        /// </param>
        /// <param name="rotation">
        ///     A <see cref="float"/> value that defines that angle, in radians, to draw
        ///     the text around the origin point.
        /// </param>
        /// <param name="origin">
        ///     A <see cref="Vector2"/> value that defines the center of rotation when
        ///     drawing the text.
        /// </param>
        /// <param name="scale">
        ///     A <see cref="float"/> value that defines the scale at which to draw
        ///     the text.
        /// </param>
        /// <param name="effects">
        ///     A <see cref="SpriteEffects"/> value that defines the effects to apply
        ///     when drawing the text.
        /// </param>
        /// <param name="layerDepth">
        ///     A <see cref="float"/> value that defines the z-buffer depth at which to
        ///     draw the text.
        /// </param>
        public static void DrawStringOutlined(this SpriteBatch spriteBatch,
                                             SpriteFont spriteFont,
                                             string text,
                                             Vector2 position,
                                             Color fontColor,
                                             Color outlineColor,
                                             float rotation,
                                             Vector2 origin,
                                             float scale,
                                             SpriteEffects effects,
                                             float layerDepth)
        {
            spriteBatch.DrawStringOutlined(spriteFont, text, position, fontColor, outlineColor, rotation, origin, new Vector2(scale, scale), effects, layerDepth);
        }

        /// <summary>
        ///     Draws a <see cref="string"/> value with an outline applied to it.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="spriteFont">
        ///     A <see cref="SpriteFont"/> instance that represents the font to use
        ///     when rendering.
        /// </param>
        /// <param name="text">
        ///     A <see cref="string"/> value that contains the text to draw.
        /// </param>
        /// <param name="position">
        ///     A <see cref="Vector2"/> value that defines the xy-coordinate location
        ///     to draw the text.
        /// </param>
        /// <param name="fontColor">
        ///     A <see cref="Color"/> value that defines the color mask to apply to the
        ///     text when drawing.
        /// </param>
        /// <param name="outlineColor">
        ///     A <see cref="Color"/> value that defines the color mask to apply to the
        ///     outline of the text when drawing.
        /// </param>
        /// <param name="rotation">
        ///     A <see cref="float"/> value that defines that angle, in radians, to draw
        ///     the text around the origin point.
        /// </param>
        /// <param name="origin">
        ///     A <see cref="Vector2"/> value that defines the center of rotation when
        ///     drawing the text.
        /// </param>
        /// <param name="scale">
        ///     A <see cref="Vector2"/> value that defines the scale at which to draw
        ///     the text on the x and y axes.
        /// </param>
        /// <param name="effects">
        ///     A <see cref="SpriteEffects"/> value that defines the effects to apply
        ///     when drawing the text.
        /// </param>
        /// <param name="layerDepth">
        ///     A <see cref="float"/> value that defines the z-buffer depth at which to
        ///     draw the text.
        /// </param>
        public static void DrawStringOutlined(this SpriteBatch spriteBatch,
                                             SpriteFont spriteFont,
                                             string text,
                                             Vector2 position,
                                             Color fontColor,
                                             Color outlineColor,
                                             float rotation,
                                             Vector2 origin,
                                             Vector2 scale,
                                             SpriteEffects effects,
                                             float layerDepth)

        {
            //  Strings should always be drawn at int position to prevent
            //  artifacts, so we ensure this by flooring the position.
            position.Floor();

            //  A reusable Vector2 struct for the position of the outline
            Vector2 outlinePos;

            for (int x = -1; x < 2; x += 2)
            {
                for (int y = -1; y < 2; y += 2)
                {
                    outlinePos = new Vector2(x, y) + position;
                    outlinePos.Floor(); //  Incase some weird floating point issues.
                    spriteBatch.DrawString(spriteFont, text, outlinePos, outlineColor, rotation, origin, scale, effects, layerDepth);
                }
            }

            spriteBatch.DrawString(spriteFont, text, position, fontColor, rotation, origin, scale, effects, layerDepth);
        }


        /// <summary>
        ///     Draws a <see cref="string"/> value with an outline applied to it.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="spriteFont">
        ///     A <see cref="SpriteFont"/> instance that represents the font to use
        ///     when rendering.
        /// </param>
        /// <param name="text">
        ///     A <see cref="StringBuilder"/> value that contains the text to draw.
        /// </param>
        /// <param name="position">
        ///     A <see cref="Vector2"/> value that defines the xy-coordinate location
        ///     to draw the text.
        /// </param>
        /// <param name="fontColor">
        ///     A <see cref="Color"/> value that defines the color mask to apply to the
        ///     text when drawing.
        /// </param>
        /// <param name="outlineColor">
        ///     A <see cref="Color"/> value that defines the color mask to apply to the
        ///     outline of the text when drawing.
        /// </param>
        public static void DrawStringOutlined(this SpriteBatch spriteBatch,
                                             SpriteFont spriteFont,
                                             StringBuilder text,
                                             Vector2 position,
                                             Color fontColor,
                                             Color outlineColor)
        {
            spriteBatch.DrawStringOutlined(spriteFont, text, position, fontColor, outlineColor, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
        }

        /// <summary>
        ///     Draws a <see cref="string"/> value with an outline applied to it.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="spriteFont">
        ///     A <see cref="SpriteFont"/> instance that represents the font to use
        ///     when rendering.
        /// </param>
        /// <param name="text">
        ///     A <see cref="StringBuilder"/> value that contains the text to draw.
        /// </param>
        /// <param name="position">
        ///     A <see cref="Vector2"/> value that defines the xy-coordinate location
        ///     to draw the text.
        /// </param>
        /// <param name="fontColor">
        ///     A <see cref="Color"/> value that defines the color mask to apply to the
        ///     text when drawing.
        /// </param>
        /// <param name="outlineColor">
        ///     A <see cref="Color"/> value that defines the color mask to apply to the
        ///     outline of the text when drawing.
        /// </param>
        /// <param name="rotation">
        ///     A <see cref="float"/> value that defines that angle, in radians, to draw
        ///     the text around the origin point.
        /// </param>
        /// <param name="origin">
        ///     A <see cref="Vector2"/> value that defines the center of rotation when
        ///     drawing the text.
        /// </param>
        /// <param name="scale">
        ///     A <see cref="float"/> value that defines the scale at which to draw
        ///     the text.
        /// </param>
        /// <param name="effects">
        ///     A <see cref="SpriteEffects"/> value that defines the effects to apply
        ///     when drawing the text.
        /// </param>
        /// <param name="layerDepth">
        ///     A <see cref="float"/> value that defines the z-buffer depth at which to
        ///     draw the text.
        /// </param>
        public static void DrawStringOutlined(this SpriteBatch spriteBatch,
                                             SpriteFont spriteFont,
                                             StringBuilder text,
                                             Vector2 position,
                                             Color fontColor,
                                             Color outlineColor,
                                             float rotation,
                                             Vector2 origin,
                                             float scale,
                                             SpriteEffects effects,
                                             float layerDepth)
        {
            spriteBatch.DrawStringOutlined(spriteFont, text, position, fontColor, outlineColor, rotation, origin, new Vector2(scale, scale), effects, layerDepth);
        }

        /// <summary>
        ///     Draws a <see cref="string"/> value with an outline applied to it.
        /// </summary>
        /// <param name="spriteBatch">
        ///     The <see cref="SpriteBatch"/> instance being used for rendering.
        /// </param>
        /// <param name="spriteFont">
        ///     A <see cref="SpriteFont"/> instance that represents the font to use
        ///     when rendering.
        /// </param>
        /// <param name="text">
        ///     A <see cref="StringBuilder"/> value that contains the text to draw.
        /// </param>
        /// <param name="position">
        ///     A <see cref="Vector2"/> value that defines the xy-coordinate location
        ///     to draw the text.
        /// </param>
        /// <param name="fontColor">
        ///     A <see cref="Color"/> value that defines the color mask to apply to the
        ///     text when drawing.
        /// </param>
        /// <param name="outlineColor">
        ///     A <see cref="Color"/> value that defines the color mask to apply to the
        ///     outline of the text when drawing.
        /// </param>
        /// <param name="rotation">
        ///     A <see cref="float"/> value that defines that angle, in radians, to draw
        ///     the text around the origin point.
        /// </param>
        /// <param name="origin">
        ///     A <see cref="Vector2"/> value that defines the center of rotation when
        ///     drawing the text.
        /// </param>
        /// <param name="scale">
        ///     A <see cref="Vector2"/> value that defines the scale at which to draw
        ///     the text on the x and y axes.
        /// </param>
        /// <param name="effects">
        ///     A <see cref="SpriteEffects"/> value that defines the effects to apply
        ///     when drawing the text.
        /// </param>
        /// <param name="layerDepth">
        ///     A <see cref="float"/> value that defines the z-buffer depth at which to
        ///     draw the text.
        /// </param>
        public static void DrawStringOutlined(this SpriteBatch spriteBatch,
                                             SpriteFont spriteFont,
                                             StringBuilder text,
                                             Vector2 position,
                                             Color fontColor,
                                             Color outlineColor,
                                             float rotation,
                                             Vector2 origin,
                                             Vector2 scale,
                                             SpriteEffects effects,
                                             float layerDepth)

        {
            //  Strings should always be drawn at int position to prevent
            //  artifacts, so we ensure this by flooring the position.
            position.Floor();

            //  A reusable Vector2 struct for the position of the outline
            Vector2 outlinePos;

            for (int x = -1; x < 2; x += 2)
            {
                for (int y = -1; y < 2; y += 2)
                {
                    outlinePos = new Vector2(x, y) + position;
                    outlinePos.Floor(); //  Incase some weird floating point issues.
                    spriteBatch.DrawString(spriteFont, text, outlinePos, outlineColor, rotation, origin, scale, effects, layerDepth);
                }
            }

            spriteBatch.DrawString(spriteFont, text, position, fontColor, rotation, origin, scale, effects, layerDepth);
        }

    }
}
