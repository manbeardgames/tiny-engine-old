using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tiny
{
    public static partial class SpriteBatchExtensions
    {
        /// <summary>
        ///     Draws a <see cref="string"/> value with an outline applied to it.
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
