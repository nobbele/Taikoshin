using System.Collections.Generic;

namespace Taikoshin.Framework.Objects.Containers
{
    public interface IContainer : IDrawable, IUpdatable, ILoadable
    {
        IEnumerable<GameObject> Children { get; }

        void Add(GameObject child);
        void Remove(GameObject child);
        void Clear();
    }
}