using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Taikoshin.Framework.Objects
{
    public class GameObject : IDrawable, IUpdatable, ILoadable
    {
        public bool IsLoaded { get; private set; }

        public virtual void Load()
        {
            IsLoaded = true;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Unload()
        {
        }
    }
}
