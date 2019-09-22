using Taikoshin.Framework.Screens;

namespace Taikoshin.Framework.Objects
{
    public interface ILoadable
    {
        bool IsLoaded { get; }
        void Load(TaikoGameBase game, Screen screen);
        void Unload();
    }
}
