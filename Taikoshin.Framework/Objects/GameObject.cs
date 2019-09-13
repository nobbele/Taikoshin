using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Taikoshin.Framework.Objects
{
    public class GameObject : IDrawable, IUpdatable, ILoadable
    {
        public bool IsLoaded { get; private set; }
        protected Game game;

        public Vector2 Position { get; set; } = new Vector2(0, 0);
        //public Vector2 Size { get => m_drawRect.Size.ToVector2(); set => m_drawRect.Size = value.ToPoint(); }
        public Vector2 MinimumSize { get; set; } = new Vector2(0, 0);
        public Vector2 MaximumSize { get; set; } = new Vector2(-1, -1);
        public DrawingSize Size { get; set; } = DrawingSize.XMax;
        public ScalingMethod ScalingMethod { get; set; } = ScalingMethod.KeepRatio;

        protected virtual float ratio { get; } = 1;

        public virtual void Load(TaikoGameBase game)
        {
            this.game = game;

            if(MaximumSize.X == -1 && MaximumSize.Y == -1)
                MaximumSize = game.Window.ClientBounds.Size.ToVector2();

            IsLoaded = true;
        }

        public virtual Rectangle CalculateDrawRect(Rectangle parent)
        {
            Rectangle drawRect = new Rectangle(Position.ToPoint(), new Point(0, 0));

            if (Size.HasFlag(DrawingSize.X))
            {
                if (Size.HasFlag(DrawingSize.Min))
                    drawRect.Width = (int)MinimumSize.X;
                if (Size.HasFlag(DrawingSize.Max))
                    drawRect.Width = parent.Size.X;

                if (drawRect.Width > MaximumSize.X)
                    drawRect.Width = (int)MaximumSize.X;

                if (ScalingMethod == ScalingMethod.KeepRatio)
                {
                    if (drawRect.Width * ratio > MaximumSize.Y)
                        drawRect.Width = (int)(MaximumSize.Y / ratio);

                    drawRect.Height = (int)(drawRect.Width * ratio);
                }
            }

            return drawRect;
        }

        void IDrawable.Draw(SpriteBatch spriteBatch, Rectangle parent, GameTime gameTime)
        {
            Rectangle drawRect = CalculateDrawRect(parent);

            Draw(spriteBatch, drawRect, gameTime);
        }

        protected virtual void Draw(SpriteBatch spriteBatch, Rectangle drawRect, GameTime gameTime)
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Unload()
        {
            IsLoaded = false;
        }
    }

    [Flags]
    public enum DrawingSize
    {
        InvalidSize = 0,
        X = 1 << 0,
        Y = 1 << 1,
        Max = 1 << 2,
        Min = 1 << 3,
        XMax = Max | X,
        XMin = Min | X,
    }
    public enum ScalingMethod
    {
        DontKeepRatio,
        KeepRatio
    }
}
