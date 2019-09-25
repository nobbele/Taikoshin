using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Taikoshin.Framework.Audio;
using Taikoshin.Framework.Objects;
using Taikoshin.Framework.Objects.Containers;
using Taikoshin.Framework.Resources;
using IDrawable = Taikoshin.Framework.Objects.IDrawable;

namespace Taikoshin.Framework.Screens
{
    public class Screen : GameObject
    {
        protected TextureStore textureStore { get; private set; }

        IContainer<GameObject> m_childContainer;
        List<IDisposable> m_disposables = new List<IDisposable>();
        List<ILoadableResource> m_loadableResources = new List<ILoadableResource>();

        public void Add(GameObject child)
        {
            m_childContainer.Add(child);
        }

        public void Contain(IDisposable disposable)
        {
            m_disposables.Add(disposable);
        }

        public void Contain(ILoadableResource loadable)
        {
            m_loadableResources.Add(loadable);
        }

        public void Setup(TaikoGameBase game)
        {
            this.game = game;

            m_childContainer = new Container<GameObject>(this);
            Contain(textureStore = new TextureStore(game.GraphicsDevice, Taikoshin.Resources.Textures.ResourceManager));
        }

        public override void Load(TaikoGameBase game, Screen screen, IDrawable parent)
        {
            m_childContainer.Load(game, this, this);
            for (int i = 0; i < m_loadableResources.Count; i++)
                m_loadableResources[i].Load();

            Start();

            base.Load(game, screen, parent);
        }

        public virtual void Start()
        {

        }

        public override void CalculateDrawRect(Rectangle parent)
        {
            DrawRect = game.Window.ClientBounds;
        }

        protected override void DoDraw(SpriteBatch spriteBatch, Rectangle parent, GameTime gameTime)
        {
            m_childContainer.Draw(spriteBatch, DrawRect, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            m_childContainer.Update(gameTime);
        }

        public void Remove(GameObject child)
        {
            m_childContainer.Remove(child);
        }

        public override void Unload()
        {
            m_childContainer.Unload();
            m_childContainer.Clear();

            for (int i = 0; i < m_loadableResources.Count; i++)
                m_loadableResources[i].Unload();
            m_loadableResources.Clear();

            base.Unload();
        }
    }
}
