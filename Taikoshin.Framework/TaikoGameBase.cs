using ManagedBass;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Taikoshin.Framework.Input;
using Taikoshin.Framework.Resources;
using Taikoshin.Framework.Screens;

namespace Taikoshin.Framework
{
    public abstract class TaikoGameBase : Game
    {
        readonly GraphicsDeviceManager m_graphics;
        SpriteBatch m_spriteBatch;

        public InputManager InputManager { get; set; }
        public Queue<Action> EndOfFrame { get; set; }

        protected ScreenManager screenManager { get; set; }

        public TaikoGameBase()
        {
            m_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Actual game's setup method. Called before Load
        /// </summary>
        protected virtual void Setup() { }

        protected sealed override void Initialize()
        {
            EndOfFrame = new Queue<Action>();
            screenManager = new ScreenManager(this);
            InputManager = new InputManager();

            Setup();

            base.Initialize();
        }

        protected sealed override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);

            Fonts.Load();
            Bass.Init();

            screenManager.Load(this, null, screenManager);

            Load();
        }

        /// <summary>
        /// Actual game's loading method
        /// </summary>
        protected virtual void Load() { }

        protected sealed override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            m_spriteBatch.Begin();

            screenManager.Draw(m_spriteBatch, new Rectangle(new Point(0, 0), Window.ClientBounds.Size), gameTime);

            m_spriteBatch.End();
        }

        protected sealed override void Update(GameTime gameTime)
        {
            InputManager.Update(gameTime);
            screenManager.Update(gameTime);

            while(EndOfFrame.Count > 0)
                EndOfFrame.Dequeue()?.Invoke();
        }

        protected sealed override void UnloadContent()
        {
            Unload();

            Bass.Free();
        }

        /// <summary>
        /// Actual game's unloading method
        /// </summary>
        protected virtual void Unload() { }
    }
}
