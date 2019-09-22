using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Taikoshin.Framework.Resources;
using Taikoshin.Framework.Screens;

namespace Taikoshin.Framework.Objects
{
    public class Sprite : GameObject
    {
        readonly TextureStore m_fontStore;
        readonly string m_textureName;

        public Sprite(TextureStore fontStore, string textureName)
        {
            m_fontStore = fontStore;
            m_textureName = textureName;
        }

        Texture2D texture;

        public override void Load(TaikoGameBase game, Screen screen)
        {
            texture = m_fontStore[m_textureName];

            base.Load(game, screen);
        }

        protected override void Draw(SpriteBatch spriteBatch, Rectangle drawRect, GameTime gameTime)
            => spriteBatch.Draw(texture, drawRect, Color.White);
    }
}
