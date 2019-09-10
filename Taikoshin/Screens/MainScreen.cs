using System;
using System.Collections.Generic;
using System.Text;
using Taikoshin.Framework.Objects;
using Taikoshin.Framework.Objects.Text;
using Taikoshin.Framework.Screens;
using Taikoshin.Framework.Resources;

namespace Taikoshin.Screens
{
    public class MainScreen : Screen
    {
        DynamicText text;
        Sprite duck;

        public override void Load()
        {
            Add(text = new DynamicText(Fonts.MenuFont, "あなたの名前は？ Hello Duck"));
            Add(duck = new Sprite(textureStore, "Duck"));

            base.Load();
        }
    }
}
