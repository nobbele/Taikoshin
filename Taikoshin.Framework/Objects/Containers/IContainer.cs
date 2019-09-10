using System.Collections.Generic;

namespace Taikoshin.Framework.Objects.Containers
{
    public interface IContainer<T> : IDrawable, IUpdatable, ILoadable
        where T : GameObject
    {
        IEnumerable<T> Children { get; }

        void Add(T child);
        void Clear();
    }
}