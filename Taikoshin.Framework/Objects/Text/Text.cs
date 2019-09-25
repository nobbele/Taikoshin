using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;

namespace Taikoshin.Framework.Objects.Text
{
    public class Text : GameObject
    {
        public string Content;
        readonly DynamicSpriteFont m_font;

        readonly Vector2 contentSize;

        public Text(DynamicSpriteFont font, string text)
        {
            m_font = font;
            Content = text;

            contentSize = font.MeasureString(text);
        }

        protected override void DoDraw(SpriteBatch spriteBatch, Rectangle drawRect, GameTime gameTime)
        {
            spriteBatch.DrawString(m_font, Content, drawRect.Location.ToVector2(), Color.White);
        }
    }
}
