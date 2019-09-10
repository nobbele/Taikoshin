namespace Taikoshin.Framework.Objects
{
    public interface ILoadable
    {
        bool IsLoaded { get; }
        void Load();
        void Unload();
    }
}
