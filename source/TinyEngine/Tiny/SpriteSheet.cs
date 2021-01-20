using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework.Graphics;

namespace Tiny
{
    public class SpriteSheet : IDisposable
    {
        //  Dictionary of all sprites within this sprite sheet.
        private Dictionary<string, TinyTexture> _sprites;

        public string Name { get; }

        /// <summary>
        ///     Gets a <see cref="bool"/> value indicating if this instance
        ///     has been disposed of.
        /// </summary>
        public bool IsDisposed { get; }

        /// <summary>
        ///     Gets a <see cref="TinyTexture"/> instance that contains the entire
        ///     spritesheet.
        /// </summary>
        public TinyTexture Texture { get; private set; }

        /// <summary>
        ///     Creates a new <see cref="SpriteSheet"/> instance.
        /// </summary>
        public SpriteSheet(string name)
        {
            Name = name;
            _sprites = new Dictionary<string, TinyTexture>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        ///     Loads the textures and data for the sprite sheet from an xml file.
        /// </summary>
        /// <param name="device"></param>
        /// <param name="xmlFilePath"></param>
        public void Load(GraphicsDevice device, string xmlFilePath)
        {
            //  Load the XML Document from the file
            XmlDocument document = Xml.LoadXmlDocument(xmlFilePath);

            //  Get a refrence to the <TextureAtlas> element
            XmlElement textureAtlasElement = document["TextureAtlas"];

            //  Get the imagePath attribute from the <TextureAtlas> element
            string imagePath = textureAtlasElement.GetStringAttribute("imagePath");

            //  The texture that we'll load for the sprite sheet needs to be located in the same
            //  directory as the xml file.
            string directory = Path.GetDirectoryName(xmlFilePath);
            imagePath = Path.Combine(directory, imagePath);

            //  Load the texture
            Texture = new TinyTexture(TextureUtilities.FromFile(device, imagePath, preMultiplyAlpha: true));

            //  Process each of the <sprite> child elements.
            foreach (XmlElement spriteElement in textureAtlasElement)
            {
                //  We'll use the name of the sprite image sans file extension as the name of the sprite.
                string name = Path.GetFileNameWithoutExtension(spriteElement.GetStringAttribute("n"));

                //  Get the x, y, width, and height boundries of the sprite.
                int x = spriteElement.GetIntAttribute("x");
                int y = spriteElement.GetIntAttribute("y");
                int width = spriteElement.GetIntAttribute("w");
                int height = spriteElement.GetIntAttribute("h");

                //  Add the sprite to the collection.
                _sprites.Add(name, Texture.GetSubtexture(x, y, width, height));

            }
        }

        /// <summary>
        ///     Safe method for getting a sprite from this spritesheet by name.
        /// </summary>
        /// <param name="name">
        ///     A <see cref="string"/> value that contains the name of the sprite
        ///     to get.
        /// </param>
        /// <param name="texture">
        ///     When this method returns, if the return value is <c>true</c>, this will
        ///     contain the <see cref="TinyTexture"/> instance that represents the sprite
        ///     within the sprite sheet. If <c>false</c> is return, then this will be
        ///     <c>null</c>.
        /// </param>
        /// <returns>
        ///     <c>true</c> if a sprite exists within this sprite sheet with the given name;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool TryGetSprite(string name, out TinyTexture texture)
        {
            return _sprites.TryGetValue(name, out texture);
        }

        /// <summary>
        ///     Diposes of resources managed by this instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Disposes of resources managed by this instance.
        /// </summary>
        /// <param name="isDisposing">
        ///     A <see cref="bool"/> value that indicates if resources should
        ///     be disposed of.
        /// </param>
        private void Dispose(bool isDisposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (isDisposing)
            {
                _sprites.Clear();
                _sprites = null;

                Texture.Dispose();
                Texture = null;
            }
        }
    }
}
