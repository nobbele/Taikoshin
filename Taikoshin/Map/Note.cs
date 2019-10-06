using System;
using System.Collections.Generic;
using System.Text;

namespace Taikoshin.Map
{
    public struct Note
    {
        public int Time;
        public NoteType Type;

        public Note(int time, NoteType type)
        {
            Time = time;
            Type = type;
        }
    }
}
