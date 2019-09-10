using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Taikoshin.Framework.Resources;

namespace Taikoshin.Framework.Objects
{
    public class Sprite : GameObject
    {
        public Vector2 Position { get; set; } = new Vector2(0, 0);
        public Vector2 Size { get; set; } = new Vector2(1, 1);

        readonly TextureStore m_fontStore;
        readonly string m_textureName;

        public Sprite(TextureStore fontStore, string textureName)
        {
            m_fontStore = fontStore;
            m_textureName = textureName;
        }

        Texture2D texture;

        public override void Load()
        {
            texture = m_fontStore[m_textureName];

            base.Load();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(texture, new Rectangle(Position.ToPoint(), (Size * 100).ToPoint()), Color.White);

            base.Draw(spriteBatch, gameTime);
        }
    }
}
