using System;

namespace Taikoshin
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (TaikoGame game = new TaikoGame())
                game.Run();
        }
    }
}