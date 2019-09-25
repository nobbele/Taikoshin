using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Taikoshin.Framework.Screens;

namespace Taikoshin.Framework.Objects.Containers
{
    public class Container<T> : GameObject, IContainer<T> where T : GameObject
    {
        public IEnumerable<T> Children => m_children;
        List<T> m_children { get; set; } = new List<T>();

        public Container(Screen screen) : base()
        {
            this.screen = screen;
        }

        public void Add(T child)
        {
            m_children.Add(child);
            if (IsLoaded)
                child.Load(game, screen, parent);
#if DEBUG
            Console.WriteLine($"Added a child of type {child.GetType().Name} to screen of type {screen.GetType().Name}");
#endif
        }

        public override void Load(TaikoGameBase game, Screen screen, IDrawable parent)
        {
            foreach (ILoadable child in Children)
            {
                child.Load(game, screen, parent);
            }

            base.Load(game, screen, parent);
        }

        protected override void DoDraw(SpriteBatch spriteBatch, Rectangle drawRect, GameTime gameTime)
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

        public void Remove(T child)
        {
            m_children.Remove(child);
            child.Unload();
#if DEBUG
            Console.WriteLine($"Removed a child of type {child.GetType().Name} from screen of type {screen.GetType().Name}");
#endif
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
