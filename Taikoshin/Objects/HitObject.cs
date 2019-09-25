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
using IDrawable = Taikoshin.Framework.Objects.IDrawable;

namespace Taikoshin.Objects
{
    public class HitObject : Sprite
    {
        Track m_track;
        float m_hitTime;
        HitObjectType m_hitObjectType;

        public float Speed { get; set; } = 4000;
        public float HitLock { get; set; } = 1000;

        float m_timeToObject => -(m_track.Position - m_hitTime);

        public HitObject(TextureStore textureStore, Track track, float hitTime, HitObjectType hitObjectType) : base(textureStore, "Duck")
        {
            m_track = track;
            m_hitTime = hitTime;
            m_hitObjectType = hitObjectType;
        }

        public override void Load(TaikoGameBase game, Screen screen, IDrawable parent)
        {
            if (m_hitObjectType == HitObjectType.Don)
                game.InputManager.OnDon += OnClick;
            else if (m_hitObjectType == HitObjectType.Katsu)
                game.InputManager.OnKatsu += OnClick;
            else
                throw new ArgumentException();

            base.Load(game, screen, parent);
        }

        private void OnClick()
        {
            if (m_timeToObject <= HitLock)
                OnHit();
        }

        private void OnHit()
        {
            Console.WriteLine($"Hit! {m_timeToObject}ms off");
            screen.Remove(this);
        }

        public override void Update(GameTime gameTime)
        {
            float progress = m_timeToObject / Speed;

            Position = new Vector2((int)(progress * parent.DrawRect.Width), 0);
        }

        protected override string GetDebugDataString()
            => $"{base.GetDebugDataString()}\n" +
               $"Object hit time: {m_hitTime}\n" +
               $"Time to object: {m_timeToObject}";

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
