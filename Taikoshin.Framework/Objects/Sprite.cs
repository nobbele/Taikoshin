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

        public Color Color { get; set; } = Color.White;

        public Sprite(TextureStore fontStore, string textureName) : base()
        {
            m_fontStore = fontStore;
            m_textureName = textureName;
        }

        protected Texture2D texture;

        public override void Load(TaikoGameBase game, Screen screen, IDrawable parent)
        {
            texture = m_fontStore[m_textureName];

            base.Load(game, screen, parent);
        }

        protected override void DoDraw(SpriteBatch spriteBatch, Rectangle drawRect, GameTime gameTime)
            => spriteBatch.Draw(texture, drawRect, Color);
    }
}
