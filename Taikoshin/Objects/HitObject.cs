using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Taikoshin.Framework.Audio;
using Taikoshin.Framework.Objects;
using Taikoshin.Framework.Resources;

namespace Taikoshin.Objects
{
    public class HitObject : Sprite
    {
        Track m_track;
        float m_hitTime;

        public float Speed { get; set; } = 4000;

        public HitObject(TextureStore textureStore, Track track, float hitTime) : base(textureStore, "Duck")
        {
            m_track = track;
            m_hitTime = hitTime;
        }

        public override Rectangle CalculateDrawRect(Rectangle parent)
        {
            Rectangle rect = GetDefaultRect(parent);

            float timeFromObject = m_track.Position - m_hitTime;
            float progress = -timeFromObject / Speed;

            rect.X += (int)(progress * game.Window.ClientBounds.Width);

            return rect;
        }
    }
}
