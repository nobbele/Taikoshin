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
        }

        public void Pop()
        {
            Screen screen = ScreenStack.Dequeue();
            screen.Unload();
        }

        public void Load()
        {
            foreach (Screen screen in ScreenStack)
            {
                screen.Load();
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

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach(Screen screen in ScreenStack)
            {
                screen.Draw(spriteBatch, gameTime);
            }
        }

        public void Unload()
        {
            while (ScreenStack.TryDequeue(out Screen stackedScreen))
            {
                stackedScreen.Unload();
            }
        }
    }
}
