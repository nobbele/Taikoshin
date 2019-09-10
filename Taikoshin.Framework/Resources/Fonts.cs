using SpriteFontPlus;

namespace Taikoshin.Framework.Resources
{
    public class Fonts
    {
        public const uint FontSize = 32;

        public static DynamicSpriteFont MenuFont;

        public static void Load()
        {
            MenuFont = DynamicSpriteFont.FromTtf(Taikoshin.Resources.Fonts.Koruri, FontSize);
        }
    }
}
