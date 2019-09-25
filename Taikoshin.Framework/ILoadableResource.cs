namespace Taikoshin.Framework
{
    public interface ILoadableResource
    {
        bool IsLoaded { get; }

        void Load();
        void Unload();
    }
}