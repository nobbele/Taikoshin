using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Taikoshin.Framework.Objects.Containers
{
    public class Container : GameObject, IContainer<GameObject>
    {
        public IEnumerable<GameObject> Children => m_children;
        List<GameObject> m_children { get; set; } = new List<GameObject>();

        public void Add(GameObject child)
        {
            m_children.Add(child);
#if DEBUG
            Console.WriteLine($"Added a child of type {child.GetType().Name} to container of type {GetType().Name}");
#endif
        }

        public override void Load(TaikoGameBase game)
        {
            foreach (ILoadable child in Children)
            {
                child.Load(game);
            }

            base.Load(game);
        }

        protected override void Draw(SpriteBatch spriteBatch, Rectangle drawRect, GameTime gameTime)
        {
            foreach(IDrawable child in Children)
            {
                child.Draw(spriteBatch, drawRect, gameTime);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (IUpdatable child in Children)
            {
                child.Update(gameTime);
            }
        }

        public override void Unload()
        {
            foreach (ILoadable child in Children)
            {
                child.Unload();
            }
        }

        public void Clear()
        {
            m_children.Clear();
        }
    }
}
