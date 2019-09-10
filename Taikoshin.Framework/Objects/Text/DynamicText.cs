using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;

namespace Taikoshin.Framework.Objects.Text
{
    public class DynamicText : Text
    {
        public string Text { get; set; }

        readonly DynamicSpriteFont m_font;

        public DynamicText(DynamicSpriteFont font, string text = "")
        {
            m_font = font;
            Text = text;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(m_font, Text, new Vector2(0, 0), Color.White);
        }
    }
}
