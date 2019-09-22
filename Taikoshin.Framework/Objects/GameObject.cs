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
        public Vector2 Origin { get; set; } = new Vector2(0, 0);

        protected virtual float ratio { get; } = 1;

        public virtual void Load(TaikoGameBase game)
        {
            this.game = game;

            if(MaximumSize.X == -1 && MaximumSize.Y == -1)
                MaximumSize = game.Window.ClientBounds.Size.ToVector2();

            IsLoaded = true;
        }

        protected Rectangle GetDefaultRect(Rectangle parent)
        {
            Rectangle drawRect = new Rectangle(Position.ToPoint(), new Point(0, 0));

            if (Size.HasFlag(DrawingSize._X))
            {
                if (Size.HasFlag(DrawingSize._Min))
                    drawRect.Width = (int)MinimumSize.X;
                if (Size.HasFlag(DrawingSize._Max))
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

            drawRect.Location = (drawRect.Size.ToVector2() * -Origin).ToPoint();

            return drawRect;
        }

        public virtual Rectangle CalculateDrawRect(Rectangle parent)
            => GetDefaultRect(parent);

        void IDrawable.Draw(SpriteBatch spriteBatch, Rectangle parent, GameTime gameTime)
            => Draw(spriteBatch, CalculateDrawRect(parent), gameTime);

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
        _X = 1 << 0,
        _Y = 1 << 1,
        _Max = 1 << 2,
        _Min = 1 << 3,
        XMax = _Max | _X,
        XMin = _Min | _X,
    }
    public enum ScalingMethod
    {
        DontKeepRatio,
        KeepRatio
    }
}
