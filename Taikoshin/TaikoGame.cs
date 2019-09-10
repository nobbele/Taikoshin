using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Taikoshin.Framework;
using Taikoshin.Resources;
using Taikoshin.Screens;

namespace Taikoshin
{
    public class TaikoGame : TaikoGameBase
    {
        protected override void Setup()
        {
            screenManager.SetScreen(new MainScreen());
            screenManager.Push(new Overlay());
        }
    }
}
