using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tiny
{
    public static class TextureUtilities
    {
        public static Texture2D FromFile(GraphicsDevice device, string path, bool preMultiplyAlpha = true)
        {
            Texture2D texture = Texture2D.FromFile(device, path);

            if (preMultiplyAlpha)
            {
                Color[] buffer = new Color[texture.Width * texture.Height];
                texture.GetData<Color>(buffer);

                for (int i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = Color.FromNonPremultiplied(buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A);
                }

                texture.SetData<Color>(buffer);
            }

            return texture;
        }
    }
}
