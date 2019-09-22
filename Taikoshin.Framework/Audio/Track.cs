using ManagedBass;
using System;
using System.Collections.Generic;
using System.Text;
using Taikoshin.Framework.Objects;
using Taikoshin.Framework.Screens;

namespace Taikoshin.Framework.Audio
{
    public class Track : ILoadable
    {
        public bool IsLoaded { get; private set; }

        public string TrackName { get; private set; }

        public float Position => (float)Bass.ChannelBytes2Seconds(m_handle, Bass.ChannelGetPosition(m_handle)) * 1000;

        private int m_handle = -1;

        public Track(string trackName)
        {
            TrackName = trackName;
        }

        public void Load(TaikoGameBase game, Screen screen)
        {
            m_handle = Bass.CreateStream(TrackName);

            IsLoaded = true;
        }

        public void Play(bool restart = true)
        {
            Bass.ChannelPlay(m_handle, restart);
        }

        public void Unload()
        {
            Bass.StreamFree(m_handle);

            IsLoaded = false;
        }
    }
}
