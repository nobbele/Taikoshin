using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;

namespace Taikoshin.Framework.Resources
{
    public class TextureStore : IDisposable
    {
        readonly GraphicsDevice m_graphicsDevice;
        readonly ResourceManager m_resourceManager;

        public TextureStore(GraphicsDevice graphics, ResourceManager resourceManager)
        {
            m_graphicsDevice = graphics;
            m_resourceManager = resourceManager;
        }

        Dictionary<string, Texture2D> m_textures = new Dictionary<string, Texture2D>();

        public Texture2D this[string textureName] 
        {
            get {
                return m_textures.GetOrAdd(textureName, () => {
                    using (MemoryStream ms = new MemoryStream(m_resourceManager.GetObject(textureName) as byte[]))
                        return Texture2D.FromStream(m_graphicsDevice, ms);
                });
            }
        }

        public void Dispose()
        {
            foreach (Texture2D texture in m_textures.Values)
                texture.Dispose();
        }
    }

    public static class DictionaryExtensionMethods
    {
        public static TVal GetOrAdd<TKey, TVal>(this Dictionary<TKey, TVal> dict, TKey key, Func<TVal> instatiator)
        {
            if (!dict.TryGetValue(key, out TVal value))
                value = instatiator.Invoke();
            return value;
        }
    }
}
