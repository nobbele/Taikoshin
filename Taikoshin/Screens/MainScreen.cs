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

            int offset = 269;

            hitObjectContainer.AddNoteRange(new Note[]
            {
                new Note(5343 + offset, NoteType.Don),
                new Note(5424 + offset, NoteType.Don),
                new Note(5505 + offset, NoteType.Don),
                new Note(5829 + offset, NoteType.Katsu),
                new Note(6153 + offset, NoteType.Don),
                new Note(6477 + offset, NoteType.Don),
                new Note(6801 + offset, NoteType.Don),
                new Note(7126 + offset, NoteType.Katsu),
                new Note(7612 + offset, NoteType.Don),
                new Note(7937 + offset, NoteType.Don),
                new Note(8423 + offset, NoteType.Don),
            });

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
