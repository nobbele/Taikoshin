using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;

namespace Taikoshin.Framework.Objects.Text
{
    public class Text : GameObject
    {
        public string m_content;
        readonly DynamicSpriteFont m_font;

        readonly Vector2 contentSize;

        public Text(DynamicSpriteFont font, string text)
        {
            m_font = font;
            m_content = text;

            contentSize = font.MeasureString(text);
        }

        protected override void Draw(SpriteBatch spriteBatch, Rectangle drawRect, GameTime gameTime)
        {
            spriteBatch.DrawString(m_font, m_content, drawRect.Location.ToVector2(), Color.White);
        }
    }
}
