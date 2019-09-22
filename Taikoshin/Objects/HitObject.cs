using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Taikoshin.Framework;
using Taikoshin.Framework.Audio;
using Taikoshin.Framework.Input;
using Taikoshin.Framework.Objects;
using Taikoshin.Framework.Resources;
using Taikoshin.Framework.Screens;

namespace Taikoshin.Objects
{
    public class HitObject : Sprite
    {
        Track m_track;
        float m_hitTime;
        HitObjectType m_hitObjectType;

        public float Speed { get; set; } = 4000;
        public float HitLock { get; set; } = 1000;

        float timeToObject => -(m_track.Position - m_hitTime);

        public HitObject(TextureStore textureStore, Track track, float hitTime, HitObjectType hitObjectType) : base(textureStore, "Duck")
        {
            m_track = track;
            m_hitTime = hitTime;
            m_hitObjectType = hitObjectType;
        }

        public override void Load(TaikoGameBase game, Screen screen)
        {
            if (m_hitObjectType == HitObjectType.Don)
                game.InputManager.OnDon += OnClick;
            else if (m_hitObjectType == HitObjectType.Katsu)
                game.InputManager.OnKatsu += OnClick;
            else
                throw new ArgumentException();

            base.Load(game, screen);
        }

        private void OnClick()
        {
            if (timeToObject <= HitLock)
                OnHit();
        }

        private void OnHit()
        {
            Console.WriteLine($"Hit! {timeToObject}ms off");
            screen.Remove(this);
        }

        public override Rectangle CalculateDrawRect(Rectangle parent)
        {
            Rectangle rect = GetDefaultRect(parent);

            float progress = timeToObject / Speed;

            rect.X += (int)(progress * game.Window.ClientBounds.Width);

            return rect;
        }

        public override void Unload()
        {
            if (m_hitObjectType == HitObjectType.Don)
                game.InputManager.OnDon -= OnClick;
            else if (m_hitObjectType == HitObjectType.Katsu)
                game.InputManager.OnKatsu -= OnClick;
            else
                throw new ArgumentException();

            base.Unload();
        }
    }

    public enum HitObjectType
    {
        Don, Katsu
    }
}
