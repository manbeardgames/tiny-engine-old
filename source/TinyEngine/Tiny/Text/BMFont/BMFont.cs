using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tiny
{
    public class BMFont
    {
        private readonly Dictionary<int, BMFontCharacter> _characters;
        public string Name { get; private set; }
        public int LineHeight { get; private set; }
        public int LetterSpacing { get; private set; }
        public bool UseKernings { get; set; }
        public Texture2D[] Textures { get; private set; }

        public BMFont()
        {
            _characters = new Dictionary<int, BMFontCharacter>();
            UseKernings = true;
        }

        public static BMFont FromFile(GraphicsDevice device, string filepath)
        {
            Xml.DeserializeAs<BMFontFile>(filepath, out BMFontFile file);

            BMFont font = new BMFont();
            font.Name = file.Info.Face;
            font.LineHeight = file.Common.LineHeight;

            font.Textures = new Texture2D[file.Common.Pages];
            for (int i = 0; i < file.Common.Pages; i++)
            {
                string texturePath = Path.Combine(Path.GetDirectoryName(filepath), file.Pages[i].File);
                font.Textures[i] = Texture2D.FromFile(device, texturePath);
            }

            for (int i = 0; i < file.Chars.Count; i++)
            {
                BMFontCharacter character = new BMFontCharacter(page: font.Textures[file.Chars[i].Page],
                                                                bounds: new Rectangle(file.Chars[i].X, file.Chars[i].Y, file.Chars[i].Width, file.Chars[i].Height),
                                                                character: file.Chars[i].ID,
                                                                xOffset: file.Chars[i].XOffset,
                                                                yOffset: file.Chars[i].YOffset,
                                                                xAdvance: file.Chars[i].XAdvance);

                font._characters.Add(character.Character, character);
            }

            for (int i = 0; i < file.Kernings.Count; i++)
            {
                if (font._characters.TryGetValue(file.Kernings[i].First, out BMFontCharacter value))
                {
                    value.Kernings.Add(file.Kernings[i].Second, file.Kernings[i].Amount);
                }
            }

            return font;
        }

        public Rectangle GetBounds(string text)
        {
            return GetBounds(text, Vector2.Zero);
        }

        public Rectangle GetBounds(string text, Vector2 at)
        {
            int lines = text.Split(new char[] { '\n', '\r' }, StringSplitOptions.None).Length;

            Rectangle rect = new Rectangle((int)at.X, (int)at.Y, 0, LineHeight * lines);

            List<BMFontGlyph> glyphs = GetGlyphs(text, Vector2.Zero);

            for (int i = 0; i < glyphs.Count; i++)
            {
                float right = glyphs[i].Position.X + glyphs[i].SourceRectangle.Width;

                if (right > rect.Right)
                {
                    rect.Width = (int)(right - rect.Left);
                }
            }

            return rect;
        }
        
        public List<BMFontGlyph> GetGlyphs(string text, Vector2 position)
        {
            return GetGlyphs(text, position, Vector2.One);
        }

        public List<BMFontGlyph> GetGlyphs(string text, Vector2 position, Vector2 scale)
        {
            Vector2 delatPos = Vector2.Zero;
            BMFontCharacter currentCharacter;
            BMFontCharacter previousCharacter = null;
            bool firstGlyphOfLine = true;



            List<BMFontGlyph> glyphs = new List<BMFontGlyph>();

            for (int c = 0; c < text.Length; c++)
            {
                char character = text[c];

                //  TODO: Figure out how to handle \t tabstops properly.
                //        For now, I'm just removing it all together.
                if (character == '\t')
                {
                    continue;
                }

                if (character == '\n' || character == '\r')
                {
                    //  New line or carraige returns we increase delta y by the
                    //  line height, and set x back to left most 0 position.
                    delatPos.X = 0;
                    delatPos.Y += LineHeight * scale.Y;
                    firstGlyphOfLine = true;
                    continue;
                }
                else
                {
                    if (_characters.TryGetValue(text[c], out currentCharacter))
                    {
                        BMFontGlyph glyph = new BMFontGlyph();
                        glyph.Scale = scale;
                        glyph.Character = (char)currentCharacter.Character;
                        glyph.SourceRectangle = currentCharacter.SourceRectange;
                        glyph.Texture = currentCharacter.Texture;

                        glyph.Position = position + delatPos;
                        glyph.Position.X += currentCharacter.Offset.X * scale.X;
                        glyph.Position.Y += currentCharacter.Offset.Y * scale.Y;

                        delatPos.X += (currentCharacter.XAdvance * scale.X) + LetterSpacing;

                        if (UseKernings && previousCharacter != null)
                        {
                            if (previousCharacter.Kernings.TryGetValue(text[c], out int kerning))
                            {
                                glyph.Position.X += kerning * scale.X;
                                delatPos.X += kerning * scale.X;
                            }
                        }

                        previousCharacter = currentCharacter;

                        glyph.Position.Round();

                        glyphs.Add(glyph);
                        firstGlyphOfLine = false;
                    }
                }
            }

            return glyphs;
        }



    }

    //public static BMFont FromFile(GraphicsDevice device, string filepath)
    //{
    //    if (!File.Exists(filepath))
    //    {
    //        throw new FileNotFoundException("Unable to located file", filepath);
    //    }

    //    string directory = Path.GetDirectoryName(filepath);

    //    //  Read the file in one go, no need for open stream connections :p
    //    byte[] buffer = File.ReadAllBytes(filepath);


    //    BMFont font = new BMFont();

    //    using (MemoryStream stream = new MemoryStream(buffer))
    //    {
    //        using (BinaryReader reader = new BinaryReader(stream))
    //        {
    //            //  Get the lenght of the entire stream
    //            long remaining = reader.BaseStream.Length;

    //            //  The first 3 bytes are the file identifier,
    //            _ = reader.ReadBytes(3);

    //            //  The fourth byte is the format version, which we do care about. As of
    //            //  this code base, that version must be 3
    //            byte version = reader.ReadByte();
    //            if (version != 3)
    //            {
    //                throw new Exception($"Invalid BMFont file format version. Expected version 3, version read was {version}");
    //            }

    //            //  BMFont doesn't supply how many blocks there are to read.  So reduce
    //            //  the remaining value by 4 since we've read 4 bytes
    //            remaining -= 4;

    //            //  Now we'll loop until remaining <= 0
    //            while (remaining > 0)
    //            {
    //                //  The first byte of every block is the identifier
    //                byte identifier = reader.ReadByte();

    //                //  The next four bytes (32-bits) is the size of the block
    //                //  The next four bytes is the size of the block
    //                //byte[] sizeArray = reader.ReadBytes(4);
    //                //int size = BitConverter.ToInt32(sizeArray, 0);
    //                int size = reader.ReadInt32();

    //                //  reduce the remaining by the 5 bytes we've read
    //                remaining -= 5;

    //                //  handle the block base on the identifier
    //                if (identifier == 1)
    //                {
    //                    BMFontInfo info = new BMFontInfo();

    //                    //  fontSize is a 2-byte (16-bit) int value.
    //                    info.Size = reader.ReadInt16();

    //                    //  bitField is 1 byte (8-bit) bits value, where each bit
    //                    //  represents a true/false for a certain property
    //                    //  bit 0 : smooth
    //                    //  bit 1 : unicode
    //                    //  bit 2 : italic
    //                    //  bit 3 : bold
    //                    //  bit 4: fixedHeight
    //                    //  bit 5-7 : reserved
    //                    //  Those are the bits from the documentation, but we need to
    //                    //  reverse order to check them since they are MSB 0
    //                    byte bitField = reader.ReadByte();

    //                    info.IsSmooth = (bitField & (1 << 7)) != 0;
    //                    info.IsUnicode = (bitField & (1 << 6)) != 0;
    //                    info.IsItalic = (bitField & (1 << 5)) != 0;
    //                    info.IsBold = (bitField & (1 << 4)) != 0;
    //                    info.IsFixedHeight = (bitField & (1 << 3)) != 0;

    //                    //  charSet is a 1-byte (8-bit) uint value.
    //                    info.CharSet = reader.ReadByte();

    //                    //  stretchH is a 2-byte (16-bit) int value
    //                    info.StretchHeight = reader.ReadInt16();

    //                    //  aa is a 1-byte (8-bit) uint value
    //                    info.AA = reader.ReadByte();

    //                    //  paddingUp is a 1-byte (8-bit) uint value
    //                    info.PaddingTop = reader.ReadByte();

    //                    //  paddingRight is a 1-byte (8-bit) uint value
    //                    info.PaddingRight = reader.ReadByte();

    //                    //  paddingDown is a 1-byte (8-bit) uint value
    //                    info.PaddingBottom = reader.ReadByte();

    //                    //  paddingLeft is a 1-byte (8-bit) uint value
    //                    info.PaddingLeft = reader.ReadByte();

    //                    //  spacingHoriz is a 1-byte (8-bit) uint value
    //                    int spacingHoriz = reader.ReadByte();

    //                    //  spacingVert is a 1-byte (8-bit) uint value
    //                    int spacingVert = reader.ReadByte();

    //                    info.Spacing = new Point(spacingHoriz, spacingVert);

    //                    //  outline is a 1-byte (8-bit) uint value
    //                    info.Outline = reader.ReadByte();

    //                    //  fontName is a string with a byte-length of n+1 where nis the length of the string,
    //                    //  a the +1 is a null terminator at the end of the string.   To determine the n length
    //                    //  of the string, we'll take the size of the whole bock and subtract the total bytes read so
    //                    //  far and subtract an additional 1 for the null terminator.  We don't care about it.
    //                    int len = size - 14 - 1;
    //                    byte[] stringBuffer = reader.ReadBytes(len);
    //                    info.FontName = Encoding.ASCII.GetString(stringBuffer);

    //                    //  Read the null terminator to get the binary stream position into the correct place
    //                    _ = reader.ReadByte();

    //                    font._info = info;
    //                }
    //                else if (identifier == 2)
    //                {
    //                    BMFontCommon common = new BMFontCommon();

    //                    //  lineHeight is a 2-byte (16-bit) uint value
    //                    common.LineHeight = reader.ReadUInt16();

    //                    //  base is a 2-byte (16-bit) uint value
    //                    common.Base = reader.ReadUInt16();

    //                    //  scaleW is a 2-byte (16-bit) uint value
    //                    int scaleW = reader.ReadUInt16();

    //                    //  scaleH is a 2-byte (16-bit) uint value
    //                    int scaleH = reader.ReadUInt16();

    //                    common.Scale = new Point(scaleW, scaleH);

    //                    //  pages is a 2-byte (16-bit) uint value
    //                    common.Pages = reader.ReadUInt16();

    //                    //  bitField is a 1-byte (8-bit) value where the following
    //                    //  bits coorelate to a value
    //                    //  bits 0 -> 6 : resrved
    //                    //  bit 7 : packed
    //                    //  bitField is in order of MSB 0
    //                    byte bitField = reader.ReadByte();

    //                    common.Packed = (bitField & (1 << 7)) != 0;

    //                    //  alphaChnl is a 1-byte (8-bit) uint field
    //                    common.AlphaChannel = reader.ReadByte();

    //                    //  redChnl is a 1-byte (8-bit) uint field
    //                    common.RedChannel = reader.ReadByte();

    //                    //  greenChnl is a 1-byte (8-bit) uint field
    //                    common.GreenChannel = reader.ReadByte();

    //                    //  blueChnl is a 1-byte (8-bit) uint field
    //                    common.BlueChannel = reader.ReadByte();

    //                    font._common = common;
    //                }
    //                else if (identifier == 3)
    //                {
    //                    //  To get the pageNames, we can read the entire block as a string.
    //                    //  Each page name is seperated by a null terminator character \0.
    //                    //  So once we read them all in, we can just split on the null
    //                    //  terminator to get the array of page names.
    //                    string value = Encoding.ASCII.GetString(reader.ReadBytes(size));

    //                    string[] pageNames = value.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);

    //                    //  Now that we have the page names, we need to create the textures
    //                    for (int i = 0; i < pageNames.Length; i++)
    //                    {
    //                        string texturePath = Path.Combine(directory, pageNames[i]);

    //                        Texture2D page = Texture2D.FromFile(device, texturePath);
    //                        font.Pages.Add(i, page);
    //                    }
    //                }
    //                else if (identifier == 4)
    //                {
    //                    //  First we need to determine how many characters there are to read.
    //                    //  Each character in this block is exactly 20-bytes. Knowing this we
    //                    //  can just divide the size by 20 to get the number of characters
    //                    int charCount = size / 20;

    //                    //  Process each character
    //                    for (int i = 0; i < charCount; i++)
    //                    {
    //                        BMFontCharacter character = new BMFontCharacter();

    //                        //  id  is a 4-byte (32-bit) uint value.
    //                        character.ID = (int)reader.ReadUInt32();

    //                        //  X is a 2-byte (16-bit) uint value.
    //                        int x = reader.ReadUInt16();

    //                        //  y is a 2-byte (16-bit) uint value.
    //                        int y = reader.ReadUInt16();

    //                        //  width is a 2-byte (16-bit) uint value.
    //                        int width = reader.ReadUInt16();

    //                        //  height is a 2-byte (16-bit) uint value.
    //                        int height = reader.ReadUInt16();

    //                        character.Bounds = new Rectangle(x, y, width, height);

    //                        //  xoffset is a 2-byte (16-bit) int value
    //                        int offsetX = reader.ReadInt16();

    //                        //  yoffset is a 2-byte (16-bit) int value
    //                        int offsetY = reader.ReadInt16();

    //                        character.Offset = new Point(offsetX, offsetY);

    //                        //  xadvance is a 2-byte (16-bit) int value.
    //                        character.XAdvance = reader.ReadInt16();

    //                        //  page is a 1-byte (8-bit) uint value
    //                        character.Page = reader.ReadByte();

    //                        //  chnl is a 1-byte (8-bit) uint value.
    //                        character.Channel = reader.ReadByte();

    //                        font.Characters.Add(character.ID, character);
    //                    }
    //                }
    //                else if (identifier == 5)
    //                {
    //                    ///  First we need to determine how many kerning pairs there are to read.
    //                    //  Each pair in this block is exactly 10-bytes. Knowing this we
    //                    //  can just divide the size by 10 to get the number of pairs.
    //                    int pairCount = size / 10;

    //                    for (int i = 0; i < pairCount; i++)
    //                    {
    //                        //  first is a 4-byte (32-bit) uint value
    //                        int first = (int)reader.ReadUInt32();

    //                        //  second is a 4-byte (32-bit) uint value
    //                        int second = (int)reader.ReadUInt32();

    //                        //  amount is a 2-byte (8-bit) int value
    //                        int amount = reader.ReadInt16();

    //                        font.Characters[first].Kerning.Add(second, amount);


    //                    }

    //                }


    //                //  Decrease the remaining bytes to read by the size of the block
    //                remaining -= size;
    //            }



    //        }
    //    }

    //    return font;
    //}
}

