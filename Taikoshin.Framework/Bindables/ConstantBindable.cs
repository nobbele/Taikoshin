using System;
using System.Collections.Generic;
using System.Text;

namespace Taikoshin.Framework.Bindables
{
    public class ConstantBindable<T> : IBindable<T>
    {
        public T Value { get => Accessor(); set => throw new NotImplementedException(); }
        public Func<T> Accessor { get; set; }

        public event Bindable<T>.OnValueChangeEvent OnValueChange;

        public ConstantBindable(Func<T> accessor)
        {
            Accessor = accessor;
        }

        /// <inheritdoc/>
        public void BindDataFrom(IBindable<T> other)
        {
            other.OnValueChange += oldValue =>
            {
                Value = other.Value;
            };
        }

        /// <inheritdoc/>
        public void BindDataTo(IBindable<T> other)
        {
            OnValueChange += oldValue =>
            {
                other.Value = Value;
            };
        }
    }
}
