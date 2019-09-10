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

        IContainer<GameObject> m_childContainer = new Container<GameObject>();
        List<IDisposable> m_disposables = new List<IDisposable>();

        public void Add(GameObject child)
        {
            m_childContainer.Add(child);
            child.Load();
        }

        public void Contain(IDisposable disposable)
        {
            m_disposables.Add(disposable);
        }

        public void Setup(TaikoGameBase gameBase)
        {
            game = gameBase;
            Contain(textureStore = new TextureStore(game.GraphicsDevice, Taikoshin.Resources.Textures.ResourceManager));
        }

        public virtual void Load()
        {
            IsLoaded = true;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            m_childContainer.Draw(spriteBatch, gameTime);
        }

        public virtual void Update(GameTime gameTime)
        {
            m_childContainer.Update(gameTime);
        }

        public virtual void Unload()
        {
            m_childContainer.Unload();
            m_childContainer.Clear();
        }
    }
}
