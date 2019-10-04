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
        HitObjectContainer hitObjectContainer;
        Text positionText;

        Track track;

        public override void Load(TaikoGameBase game, Screen screen, IDrawable parent)
        {
            Contain(track = new Track("Honesty.mp3"));

            Bass.GlobalStreamVolume = 1000;

            Add(hitObjectContainer = new HitObjectContainer(this)
            {
                MaximumSize = new Vector2(game.Window.ClientBounds.Width - 100, 150),
                Size = DrawingSize.XYMax,
                ScalingMethod = ScalingMethod.DontKeepRatio,
                Offset = new Vector2(100, 100),
                DebugObject = true,
            });

            for(int i = 0; i < 20; i++)
            {
                hitObjectContainer.Add(new HitObject(hitObjectContainer, TextureStore, track, 5250 + (308 * i), i, i % 4 == 0 ? HitObjectType.Don : HitObjectType.Katsu)
                {
                    Offset = new Vector2(100, 0),
                    MinimumSize = new Vector2(100, 100),
                    Size = DrawingSize.XMin,
                    ScalingMethod = ScalingMethod.KeepRatio,
                });
            }

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
