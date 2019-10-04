using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Taikoshin.Framework.Objects;

namespace Taikoshin.Framework.Input
{
    public class InputManager : IUpdatable
    {
        public KeyboardState KbState { get; private set; }
        public MouseState MState { get; private set; }
        public GamePadState GPState { get; private set; }

        public Keys[] DonKeys { get; set; } = new Keys[]
        {
            Keys.X, Keys.OemComma
        };
        public Buttons[] DonButtons { get; set; } = new Buttons[]
        {
            Buttons.X, Buttons.Y
        };

        private bool m_donDown = false;

        public event Action OnDon;
        private void Don()
        {
            m_donDown = true;
            OnDon?.Invoke();
        }

        public Keys[] KatsuKeys { get; set; } = new Keys[]
        {
            Keys.Z, Keys.OemPeriod
        };
        public Buttons[] KatsuButtons { get; set; } = new Buttons[]
        {
            Buttons.A, Buttons.B
        };

        private bool m_katsuDown = false;

        public event Action OnKatsu;
        private void Katsu()
        {
            m_katsuDown = true;
            OnKatsu?.Invoke();
        }

        public void Update(GameTime gameTime)
        {
            KbState = Keyboard.GetState();
            MState = Mouse.GetState();
            GPState = GamePad.GetState(PlayerIndex.One);

            Keys[] keys = KbState.GetPressedKeys();

            bool donButtonDown = false;
            foreach(Buttons button in DonButtons)
            {
                if (GPState.IsButtonDown(button))
                {
                    donButtonDown = true;
                    break;
                }
            }

            bool katsuButtonDown = false;
            foreach (Buttons button in KatsuButtons)
            {
                if (GPState.IsButtonDown(button))
                {
                    katsuButtonDown = true;
                    break;
                }
            }

            if (keys.Any(DonKeys.Contains) || donButtonDown)
            {
                if (!m_donDown)
                    Don();
            }
            else
                m_donDown = false;
            if (keys.Any(KatsuKeys.Contains) || katsuButtonDown)
            {
                if (!m_katsuDown)
                    Katsu();
            }
            else
                m_katsuDown = false;
        }
    }
}
