using System;
using System.Collections.Generic;
using System.Text;
using Taikoshin.Framework.Objects.Containers;
using Taikoshin.Framework.Screens;

namespace Taikoshin.Objects.Containers
{
    public class HitObjectContainer : Container<HitObject>
    {
        public HitObjectContainer(Screen screen) : base(screen) { }
    }
}
