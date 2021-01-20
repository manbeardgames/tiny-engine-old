using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework.Graphics;

namespace Tiny
{
    public class PixelFont
    {
        private List<PixelFontSize> _sizes;
        public string Face { get; }

        public PixelFont(string face)
        {
            Face = face;
            _sizes = new List<PixelFontSize>();
        }

        public PixelFontSize AddFontSize(string path)
        {
            XmlDocument xml = Xml.LoadXmlDocument(path);
            XmlElement fontElement = xml["font"];
            return AddFontSize(path, fontElement);
        }

        public PixelFontSize AddFontSize(string path, XmlElement fontElement)
        {
            //  Get the size of the font
            float size = fontElement["info"].GetIntAttribute("size");

            //  If the size has already been added, we just return that back.
            for (int i = 0; i < _sizes.Count; i++)
            {
                if (_sizes[i].Size == size)
                {
                    return _sizes[i];
                }
            }

            //  Get the absolute path to the directory the XML file is in. This will
            //  be the same directory as the .png files contining the character glyphs.
            string directory = Path.GetDirectoryName(path);

            //  Load the textures
            List<Texture2D> pages = new List<Texture2D>();
            XmlElement pagesElement = fontElement["pages"];
            foreach (XmlElement pageElement in pagesElement)
            {
                //  Get the name of the png file that represents this page
                string pagePath = pageElement.GetStringAttribute("file");

                //  Generate the fully qualified absolute path to the page file
                pagePath = Path.Combine(directory, pagePath);

                //  Load and add the texture to the list of textures
                //  Stupid texture loading from file doesn't premultiply alpha, stupid
                //  premultiply alpha...uuuuggghhhh
                pages.Add(TextureUtilities.FromFile(pagePath, preMultiplyAlpha: true));
                //pages.Add(Texture2D.FromFile(device, pagePath));
            }

            //  Create the PixelFontSize instance
            PixelFontSize fontSize = new PixelFontSize()
            {
                Textures = pages,
                LineHeight = fontElement["common"].GetIntAttribute("lineHeight"),
                Size = size
            };

            //  Add the character data for the font size
            foreach (XmlElement characterElement in fontElement["chars"])
            {
                int id = characterElement.GetIntAttribute("id");
                int pageId = characterElement.GetIntAttribute("page", 0);
                fontSize.AddCharacter(new PixelFontCharacter(id, pages[pageId], characterElement));
            }

            //  Add the kerning values for each character in the font size
            if (fontElement.ContainsChildElement("kernings"))
            {
                foreach (XmlElement kerningElement in fontElement["kernings"])
                {
                    int first = kerningElement.GetIntAttribute("first");
                    int second = kerningElement.GetIntAttribute("second");
                    int amount = kerningElement.GetIntAttribute("amount");

                    if (fontSize.TryGetCharacter(first, out PixelFontCharacter character))
                    {
                        character.AddKerning(second, amount);
                    }
                }
            }

            //  Add the size
            _sizes.Add(fontSize);
            _sizes.Sort((a, b) => { return Math.Sign(a.Size - b.Size); });

            return fontSize;
        }

        public PixelFontSize Get(float size)
        {
            for(int s = 0; s < _sizes.Count; s++)
            {
                if(_sizes[s].Size >= size)
                {
                    return _sizes[s];
                }
            }

            return _sizes[_sizes.Count -1];
        }


    }
}
