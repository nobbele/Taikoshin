using System;
using System.Collections.Generic;
using System.Text;
using Taikoshin.Framework.Objects;
using Taikoshin.Framework.Objects.Text;
using Taikoshin.Framework.Screens;
using Taikoshin.Framework.Resources;
using Microsoft.Xna.Framework;
using Taikoshin.Framework;
using Taikoshin.Framework.Objects.Containers;
using ManagedBass;
using Taikoshin.Framework.Audio;
using Taikoshin.Objects;
using IDrawable = Taikoshin.Framework.Objects.IDrawable;
using Taikoshin.Objects.Containers;

namespace Taikoshin.Screens
{
    public class GameplayScreen : Screen
    {
        HitObjectContainer container;
        Text positionText;

        Track track;

        public override void Load(TaikoGameBase game, Screen screen, IDrawable parent)
        {
            Contain(track = new Track("Honesty.mp3"));

            Bass.GlobalStreamVolume = 1000;

            Add(container = new HitObjectContainer(this)
            {
                MaximumSize = new Vector2(game.Window.ClientBounds.Width - 100, 300),
                Size = DrawingSize.XYMax,
                ScalingMethod = ScalingMethod.DontKeepRatio,
                Offset = new Vector2(100, 100),
                DebugObject = true,
            });

            container.
                Add(new HitObject(textureStore, track, 5250, HitObjectType.Don)
            {
                Origin = new Vector2(0.5f, 0.5f),
                MinimumSize = new Vector2(100, 100),
                Size = DrawingSize.XMin,
                ScalingMethod = ScalingMethod.KeepRatio,
            });

            container.
                Add(new HitObject(textureStore, track, 5558, HitObjectType.Don)
            {
                Origin = new Vector2(0.5f, 0.5f),
                MinimumSize = new Vector2(100, 100),
                Size = DrawingSize.XMin,
                ScalingMethod = ScalingMethod.KeepRatio,
            });

            Add(positionText = new Text(Fonts.MenuFont, "0"));

            base.Load(game, screen, parent);
        }

        public override void Update(GameTime gameTime)
        {
            positionText.Content = $"Position: {track.Position}";

            base.Update(gameTime);
        }

        public override void Start()
        {
            track.Play();

            base.Start();
        }
    }
}
