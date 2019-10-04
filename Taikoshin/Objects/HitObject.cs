using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Taikoshin.Framework;
using Taikoshin.Framework.Audio;
using Taikoshin.Framework.Input;
using Taikoshin.Framework.Objects;
using Taikoshin.Framework.Resources;
using Taikoshin.Framework.Screens;
using Taikoshin.Objects.Containers;
using IDrawable = Taikoshin.Framework.Objects.IDrawable;

namespace Taikoshin.Objects
{
    public class HitObject : Sprite
    {
        readonly Track m_track;
        readonly float m_hitTime;
        readonly HitObjectType m_hitObjectType;
        readonly int m_index;
        readonly HitObjectContainer m_container;

        public float Speed { get; set; } = 4000;
        public float HitLock { get; set; } = 1000;

        float m_timeToObject => -(m_track.Position - m_hitTime);

        public HitObject(HitObjectContainer container, TextureStore textureStore, Track track, float hitTime, int index, HitObjectType hitObjectType) : base(textureStore, "Circle")
        {
            m_container = container;
            m_track = track;
            m_hitTime = hitTime;
            m_index = index;
            m_hitObjectType = hitObjectType;

            Color = m_hitObjectType == HitObjectType.Don ? Color.Red : Color.Blue;
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
            if (m_timeToObject <= HitLock && m_container.NextIndex == m_index)
                game.EndOfFrame.Enqueue(OnHit);
        }

        private void OnHit()
        {
            Console.WriteLine($"Hit! {m_timeToObject}ms off");
            m_container.Remove(this);
            m_container.NextIndex += 1;
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
