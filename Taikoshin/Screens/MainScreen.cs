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

namespace Taikoshin.Screens
{
    public class MainScreen : Screen
    {
        Text text;
        Sprite duck;

        Container container;

        Track track;

        public override void Load(TaikoGameBase game)
        {
            Add(container = new Container()
            {
                MaximumSize = game.Window.ClientBounds.Size.ToVector2() / 2,
            });
            container.Add(text = new Text(Fonts.MenuFont, "あなたの名前は？ Hello Duck")
            {
                Position = new Vector2(0, 0),
            });
            container.Add(duck = new Sprite(textureStore, "Duck")
            {
                Position = new Vector2(0, 0),
                Size = DrawingSize.XMax,
            });

            Contain(track = new Track("Honesty.mp3"));

            Bass.GlobalStreamVolume = 1000;

            base.Load(game);
        }

        public override void Update(GameTime gameTime)
        {
            Console.WriteLine($"Position: {track.Position}");

            base.Update(gameTime);
        }

        public override void Start()
        {
            track.Play();

            base.Start();
        }
    }
}
