using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Taikoshin.Framework.Objects;
using IDrawable = Taikoshin.Framework.Objects.IDrawable;

namespace Taikoshin.Framework.Screens
{
    public class ScreenManager : IDrawable, IUpdatable, ILoadable
    {
        public Rectangle DrawRect { get; private set; }

        public bool IsLoaded { get; private set; }
        public Queue<Screen> ScreenStack { get; private set; } = new Queue<Screen>();

        private readonly TaikoGameBase m_game;

        private IDrawable m_parent;

        public ScreenManager(TaikoGameBase game)
        {
            m_game = game;
        }

        public void SetScreen(Screen screen)
        {
            while(ScreenStack.TryDequeue(out Screen stackedScreen))
            {
                stackedScreen.Unload();
            }
            Push(screen);
        }

        public void Push(Screen screen)
        {
            ScreenStack.Enqueue(screen);
            screen.Setup(m_game);

            // Load instantly if screen manager is already loaded
            if (IsLoaded)
                screen.Load(m_game, screen, m_parent);
        }

        public void Pop()
        {
            Screen screen = ScreenStack.Dequeue();
            screen.Unload();
        }

        public void Load(TaikoGameBase game, Screen _screen, IDrawable parent)
        {
            m_parent = parent;

            foreach (Screen screen in ScreenStack)
            {
                screen.Load(game, screen, parent);
            }

            IsLoaded = true;
        }

        public void Update(GameTime gameTime)
        {
            foreach (Screen screen in ScreenStack)
            {
                screen.Update(gameTime);
            }
        }

        public void CalculateDrawRect(Rectangle parent)
        {
            DrawRect = parent;
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle rect, GameTime gameTime)
        {
            CalculateDrawRect(rect);
            foreach(Screen screen in ScreenStack)
            {
                screen.Draw(spriteBatch, rect, gameTime);
            }
        }

        public void Unload()
        {
            while (ScreenStack.TryDequeue(out Screen stackedScreen))
            {
                stackedScreen.Unload();
            }

            IsLoaded = false;
        }
    }
}
