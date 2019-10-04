using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Taikoshin.Framework.Resources;
using Taikoshin.Framework.Screens;

namespace Taikoshin.Framework.Objects
{
    public class GameObject : IDrawable, IUpdatable, ILoadable
    {
        public bool IsLoaded { get; private set; }
        protected TaikoGameBase game;
        protected Screen screen;
        protected IDrawable parent;

        public Vector2 Position { get; set; } = new Vector2(0, 0);
        public Vector2 MinimumSize { get; set; } = new Vector2(-1, -1);
        public Vector2 MaximumSize { get; set; } = new Vector2(-1, -1);
        public DrawingSize Size { get; set; } = DrawingSize.XYMax;
        public ScalingMethod ScalingMethod { get; set; } = ScalingMethod.DontKeepRatio;
        public Vector2 Origin { get; set; } = new Vector2(0, 0);
        public Vector2 Offset { get; set; } = new Vector2(0, 0);

#if DEBUG
        public bool DebugObject { get; set; } = false;
#endif

        protected virtual float ratio { get; } = 1;

        private Texture2D panelPixel;

        public virtual void Load(TaikoGameBase game, Screen screen, IDrawable parent)
        {
            this.game = game;
            this.screen = screen;
            this.parent = parent;

            if(MaximumSize.X == -1 && MaximumSize.Y == -1)
                MaximumSize = game.Window.ClientBounds.Size.ToVector2();

            panelPixel = new Texture2D(game.GraphicsDevice, 1, 1);
            panelPixel.SetData(new Color[] { Color.DarkGray });

            IsLoaded = true;
        }

        protected Rectangle GetDefaultRect(Rectangle parent)
        {
            Rectangle drawRect = new Rectangle(Position.ToPoint(), new Point(0, 0));

            if (Size.HasFlag(DrawingSize.XMin))
                drawRect.Width = (int)MinimumSize.X;
            else if (Size.HasFlag(DrawingSize.XMax))
                drawRect.Width = parent.Size.X;

            if (Size.HasFlag(DrawingSize.YMin))
                drawRect.Height = (int)MinimumSize.Y;
            else if (Size.HasFlag(DrawingSize.YMax))
                drawRect.Height = parent.Size.Y;

            if (drawRect.Width > MaximumSize.X && MaximumSize.X != -1)
                drawRect.Width = (int)MaximumSize.X;
            if (drawRect.Height > MaximumSize.Y && MaximumSize.Y != -1)
                drawRect.Height = (int)MaximumSize.Y;

            if (ScalingMethod == ScalingMethod.KeepRatio)
            {
                if (drawRect.Width * ratio > MaximumSize.Y)
                    drawRect.Width = (int)(MaximumSize.Y / ratio);

                drawRect.Height = (int)(drawRect.Width * ratio);
            }

            if (drawRect.Width < MinimumSize.X && MinimumSize.X != -1)
                drawRect.Width = (int)MinimumSize.X;
            if (drawRect.Height < MinimumSize.Y && MinimumSize.Y != -1)
                drawRect.Height = (int)MinimumSize.Y;

            drawRect.Location = Position.ToPoint() + parent.Location;
            drawRect.Location += Offset.ToPoint();
            drawRect.Location += (drawRect.Size.ToVector2() * -Origin).ToPoint(); // Origin

            return drawRect;
        }

        public Rectangle DrawRect { get; protected set; }

        public virtual void CalculateDrawRect(Rectangle parent)
        {
            DrawRect = GetDefaultRect(parent);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle parent, GameTime gameTime)
        {
            CalculateDrawRect(parent);

#if DEBUG
            if (DebugObject)
                DrawDebugData(spriteBatch, DrawRect, gameTime);
#endif

            DoDraw(spriteBatch, DrawRect, gameTime);
        }

        protected virtual string GetDebugDataString()
            => $"Position: {Position}";

        public void DrawDebugData(SpriteBatch spriteBatch, Rectangle drawRect, GameTime gameTime)
        {
            spriteBatch.Draw(panelPixel, DrawRect, Color.Purple);

            string debugContent = GetDebugDataString();

            // Can't draw new line yet, to be fixed

            /*Rectangle debugWindow = new Rectangle(drawRect.Location + drawRect.Size, Fonts.MenuFont.MeasureString(debugContent).ToPoint());

            spriteBatch.Draw(panelPixel, debugWindow, Color.White);
            Fonts.MenuFont.DrawString(spriteBatch, debugContent, debugWindow.Location.ToVector2(), Color.Black);*/
        }

        protected virtual void DoDraw(SpriteBatch spriteBatch, Rectangle drawRect, GameTime gameTime) { }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Unload()
        {
            IsLoaded = false;
        }
    }

    [Flags]
    public enum DrawingSize
    {
        InvalidSize = 0,
        XMax = 1 << 0,
        XMin = 1 << 1,
        YMax = 1 << 2,
        YMin = 1 << 3,
        XYMax = XMax | YMax,
    }
    public enum ScalingMethod
    {
        DontKeepRatio,
        KeepRatio
    }
}
