using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Taikoshin.Framework.Objects.Containers
{
    public class Container<T> : GameObject, IContainer<T> 
        where T : GameObject
    {
        public IEnumerable<T> Children => m_children;

        List<T> m_children { get; set; } = new List<T>();

        public void Add(T child)
        {
            m_children.Add(child);
#if DEBUG
            Console.WriteLine($"Added a child of type {child.GetType().Name} to container of type {GetType().Name}");
#endif
        }

        public override void Load()
        {
            foreach (ILoadable child in Children)
            {
                child.Load();
            }

            base.Load();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach(IDrawable child in Children)
            {
                child.Draw(spriteBatch, gameTime);
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
