using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Taikoshin.Framework.Resources;
using Taikoshin.Framework.Screens;

namespace Taikoshin.Framework
{
    public abstract class TaikoGameBase : Game
    {
        readonly GraphicsDeviceManager m_graphics;
        SpriteBatch m_spriteBatch;

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
            screenManager = new ScreenManager(this);

            Setup();

            base.Initialize();
        }

        protected sealed override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);

            Fonts.Load();

            screenManager.Load();

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

            screenManager.Draw(m_spriteBatch, gameTime);

            m_spriteBatch.End();
        }

        protected sealed override void Update(GameTime gameTime)
        {
            screenManager.Update(gameTime);
        }

        protected sealed override void UnloadContent()
        {
            Unload();
        }

        /// <summary>
        /// Actual game's unloading method
        /// </summary>
        protected virtual void Unload() { }
    }
}
