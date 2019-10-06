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
using Taikoshin.Map;
using Taikoshin.Framework.Bindables;

namespace Taikoshin.Screens
{
    public class GameplayScreen : Screen
    {
        HitObjectContainer hitObjectContainer;
        Text positionText;

        Bindable<Track> track = new Bindable<Track>();

        public override void Load(TaikoGameBase game, Screen screen, IDrawable parent)
        {
            Contain(track.Value = new Track("Honesty.mp3"));

            Bass.GlobalStreamVolume = 1000;

            Add(hitObjectContainer = new HitObjectContainer(this, TextureStore)
            {
                MinimumSize = new Vector2(-1, 150),
                Size = DrawingSize.XMax | DrawingSize.YMin,
                ScalingMethod = ScalingMethod.DontKeepRatio,
                Offset = new Vector2(100, 100),
                DebugObject = true,
            });

            hitObjectContainer.Track.BindDataFrom(track);

            hitObjectContainer.AddNoteRange(new Note[]
            {
                new Note(1000, NoteType.Don),
                new Note(2000, NoteType.Don),
                new Note(3000, NoteType.Katsu),
                new Note(4000, NoteType.Katsu),
            });

            /*for(int i = 0; i < 40; i++)
            {
                hitObjectContainer.Add(new HitObject(hitObjectContainer, TextureStore, track, 5250 + (185f / 60 / 32 * 1000 * i), i, i % 4 == 0 ? NoteType.Don : NoteType.Katsu)
                {
                    Offset = new Vector2(100, 25),
                    MinimumSize = new Vector2(100, 100),
                    Size = DrawingSize.XMin,
                    ScalingMethod = ScalingMethod.KeepRatio,
                });
            }*/

            Add(positionText = new Text(Fonts.MenuFont, "0"));

            base.Load(game, screen, parent);
        }

        public override void Update(GameTime gameTime)
        {
            positionText.Content = $"Position: {track.Value.Position}";

            base.Update(gameTime);
        }

        public override void Start()
        {
            track.Value.Play();

            base.Start();
        }
    }
}
