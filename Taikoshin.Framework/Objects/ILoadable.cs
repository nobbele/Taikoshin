namespace Taikoshin.Framework.Objects
{
    public interface ILoadable
    {
        bool IsLoaded { get; }
        void Load(TaikoGameBase game);
        void Unload();
    }
}
