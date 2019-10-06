using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Taikoshin.Framework;
using Taikoshin.Framework.Audio;
using Taikoshin.Framework.Bindables;
using Taikoshin.Framework.Input;
using Taikoshin.Framework.Objects;
using Taikoshin.Framework.Resources;
using Taikoshin.Framework.Screens;
using Taikoshin.Map;
using Taikoshin.Objects.Containers;
using IDrawable = Taikoshin.Framework.Objects.IDrawable;

namespace Taikoshin.Objects
{
    public class HitObject : Sprite
    {
        public int Index { get; }

        readonly Bindable<float> m_trackPosition = new Bindable<float>();
        readonly float m_hitTime;
        readonly NoteType m_hitObjectType;
        readonly HitObjectContainer m_container;

        public float Speed { get; set; } = 1500;
        public float HitLock { get; set; } = 500;
        public float MissTime { get; set; } = 50;

        float m_timeToObject => -(m_trackPosition.Value - m_hitTime);

        public HitObject(HitObjectContainer container, TextureStore textureStore, IBindable<float> trackPosition, float hitTime, int index, NoteType hitObjectType) : base(textureStore, "Circle")
        {
            m_container = container;
            m_trackPosition.BindDataFrom(trackPosition);
            m_hitTime = hitTime;
            Index = index;
            m_hitObjectType = hitObjectType;

            Color = m_hitObjectType == NoteType.Don ? Color.Red : Color.Blue;
        }

        public override void Load(TaikoGameBase game, Screen screen, IDrawable parent)
        {
            if (m_hitObjectType == NoteType.Don)
                game.InputManager.OnDon += OnClick;
            else if (m_hitObjectType == NoteType.Katsu)
                game.InputManager.OnKatsu += OnClick;
            else
                throw new ArgumentException();

            base.Load(game, screen, parent);
        }

        private void OnClick()
        {
            if (m_timeToObject <= HitLock && m_container.NextIndex == Index)
                game.EndOfFrame.Enqueue(OnHit);
        }

        private void OnHit()
        {
            Console.WriteLine($"Hit! {m_timeToObject}ms off");
            m_container.Remove(this);
            m_container.NextIndex += 1;
        }

        private void OnMiss()
        {
            Console.WriteLine($"Miss!");
            m_container.Remove(this);
            m_container.NextIndex += 1;
        }

        public override void Update(GameTime gameTime)
        {
            float progress = m_timeToObject / Speed;

            Offset = new Vector2((int)(progress * parent.DrawRect.Width), 25);

            if (m_timeToObject <= -MissTime)
                game.EndOfFrame.Enqueue(OnMiss);
        }

        protected override string GetDebugDataString()
            => $"{base.GetDebugDataString()}\n" +
               $"Object hit time: {m_hitTime}\n" +
               $"Time to object: {m_timeToObject}";

        public override void Unload()
        {
            if (m_hitObjectType == NoteType.Don)
                game.InputManager.OnDon -= OnClick;
            else if (m_hitObjectType == NoteType.Katsu)
                game.InputManager.OnKatsu -= OnClick;
            else
                throw new ArgumentException();

            base.Unload();
        }
    }
}
