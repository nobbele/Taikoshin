using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Taikoshin.Framework.Objects
{
    public interface IDrawable
    {
        Rectangle DrawRect { get; }
        void CalculateDrawRect(Rectangle parent);
        void Draw(SpriteBatch spriteBatch, Rectangle parent, GameTime gameTime);
    }
}
