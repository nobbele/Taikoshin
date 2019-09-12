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

namespace Taikoshin.Screens
{
    public class MainScreen : Screen
    {
        Text text;
        Sprite duck;

        Container container;

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

            base.Load(game);
        }
    }
}
