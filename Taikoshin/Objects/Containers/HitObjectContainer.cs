using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taikoshin.Framework;
using Taikoshin.Framework.Audio;
using Taikoshin.Framework.Bindables;
using Taikoshin.Framework.Objects;
using Taikoshin.Framework.Objects.Containers;
using Taikoshin.Framework.Resources;
using Taikoshin.Framework.Screens;
using Taikoshin.Map;
using IDrawable = Taikoshin.Framework.Objects.IDrawable;

namespace Taikoshin.Objects.Containers
{
    public class HitObjectContainer : Container
    {
        readonly TextureStore m_textureStore;

        public int NextIndex { get; set; } = 0;
        public Bindable<Track> Track { get; } = new Bindable<Track>();

        private Sprite nextObjectPointer;

        public HitObjectContainer(Screen screen, TextureStore textureStore) : base(screen)
        {
            m_textureStore = textureStore;
        }

        public override void Load(TaikoGameBase game, Screen screen, IDrawable parent)
        {
            AddAt(0, new Sprite(screen.TextureStore, "Circle")
            {
                Offset = new Vector2(100, 25),
                MinimumSize = new Vector2(100, 100),
                Size = DrawingSize.XMin,
                ScalingMethod = ScalingMethod.KeepRatio,
            });

            Add(nextObjectPointer = new Sprite(screen.TextureStore, "Duck")
            {
                MinimumSize = new Vector2(10, 10),
                Size = DrawingSize.XMin,
                ScalingMethod = ScalingMethod.KeepRatio,
            });

            base.Load(game, screen, parent);
        }

        public override void Update(GameTime gameTime)
        {
            if (ChildCount > 2)
            {
                HitObject child = Children.ElementAt(1) as HitObject;
                nextObjectPointer.LocalPosition = child.DrawRect.Location.ToVector2() - new Vector2((float)child.DrawRect.Width / 2, 125);
            }

            base.Update(gameTime);
        }

        public void AddNote(Note note)
        {
            Add(new HitObject(this, m_textureStore, Track.Value.PositionBindable, note.Time, ChildCount, note.Type)
            {
                Offset = new Vector2(100, 25),
                MinimumSize = new Vector2(100, 100),
                Size = DrawingSize.XMin,
                ScalingMethod = ScalingMethod.KeepRatio,
            });
        }

        public void AddNoteRange(IEnumerable<Note> notes)
        {
            foreach (Note note in notes)
                AddNote(note);
        }
    }
}
