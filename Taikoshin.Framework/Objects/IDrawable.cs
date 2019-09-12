using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Taikoshin.Framework.Objects
{
    public interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch, Rectangle parent, GameTime gameTime);
    }
}
