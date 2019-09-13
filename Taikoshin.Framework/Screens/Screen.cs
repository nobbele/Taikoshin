using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Taikoshin.Framework.Objects;
using Taikoshin.Framework.Objects.Containers;
using Taikoshin.Framework.Resources;
using IDrawable = Taikoshin.Framework.Objects.IDrawable;

namespace Taikoshin.Framework.Screens
{
    public class Screen : IDrawable, IUpdatable, ILoadable
    {
        public bool IsLoaded { get; private set; }

        protected TaikoGameBase game { get; private set; }
        protected TextureStore textureStore { get; private set; }

        public Rectangle DrawRect => game.Window.ClientBounds;

        IContainer<GameObject> m_childContainer;
        List<IDisposable> m_disposables = new List<IDisposable>();
        List<ILoadable> m_loadables = new List<ILoadable>();

        public void Add(GameObject child)
        {
            m_childContainer.Add(child);
            child.Load(game);
        }

        public void Contain(IDisposable disposable)
        {
            m_disposables.Add(disposable);
        }

        public void Contain(ILoadable loadable)
        {
            m_loadables.Add(loadable);
        }

        public void Setup(TaikoGameBase game)
        {
            this.game = game;

            m_childContainer = new Container();
            Contain(textureStore = new TextureStore(game.GraphicsDevice, Taikoshin.Resources.Textures.ResourceManager));
        }

        public virtual void Load(TaikoGameBase game)
        {
            m_childContainer.Load(game);
            for (int i = 0; i < m_loadables.Count; i++)
                m_loadables[i].Load(game);

            IsLoaded = true;

            Start();
        }

        public virtual void Start()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch, Rectangle parent, GameTime gameTime)
        {
            m_childContainer.Draw(spriteBatch, DrawRect, gameTime);
        }

        public virtual void Update(GameTime gameTime)
        {
            m_childContainer.Update(gameTime);
        }

        public virtual void Unload()
        {
            m_childContainer.Unload();
            m_childContainer.Clear();

            for (int i = 0; i < m_loadables.Count; i++)
                m_loadables[i].Unload();
            m_loadables.Clear();

            IsLoaded = false;
        }
    }
}
