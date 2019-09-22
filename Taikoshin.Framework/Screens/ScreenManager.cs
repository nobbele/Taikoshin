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
        public bool IsLoaded { get; private set; }
        public Rectangle DrawRect => m_game.Window.ClientBounds;

        public Queue<Screen> ScreenStack { get; private set; } = new Queue<Screen>();

        private readonly TaikoGameBase m_game;

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
                screen.Load(m_game, screen);
        }

        public void Pop()
        {
            Screen screen = ScreenStack.Dequeue();
            screen.Unload();
        }

        public void Load(TaikoGameBase game, Screen _screen)
        {
            foreach (Screen screen in ScreenStack)
            {
                screen.Load(game, screen);
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

        public void Draw(SpriteBatch spriteBatch, Rectangle rect, GameTime gameTime)
        {
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
