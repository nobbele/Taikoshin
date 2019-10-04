using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Taikoshin.Framework;
using Taikoshin.Framework.Objects;
using Taikoshin.Framework.Objects.Containers;
using Taikoshin.Framework.Screens;
using IDrawable = Taikoshin.Framework.Objects.IDrawable;

namespace Taikoshin.Objects.Containers
{
    public class HitObjectContainer : Container
    {
        public int NextIndex { get; set; } = 0;

        public HitObjectContainer(Screen screen) : base(screen) { }

        public override void Load(TaikoGameBase game, Screen screen, IDrawable parent)
        {
            Add(new Sprite(screen.TextureStore, "Circle")
            {
                Offset = new Vector2(100, 0),
                MinimumSize = new Vector2(100, 100),
                Size = DrawingSize.XMin,
                ScalingMethod = ScalingMethod.KeepRatio,
            });

            base.Load(game, screen, parent);
        }
    }
}
