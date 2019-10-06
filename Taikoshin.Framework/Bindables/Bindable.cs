using System;
using System.Collections.Generic;
using System.Text;

namespace Taikoshin.Framework.Bindables
{
    public class Bindable<T> : IBindable<T>
    {
        public delegate void OnValueChangeEvent(T oldValue);
        public event OnValueChangeEvent OnValueChange;

        Func<T> constantAccessor = null;

        private T m_value;
        public T Value {
            get {
                if (constantAccessor != null)
                    return constantAccessor();
                return m_value;
            }
            set {
                if (constantAccessor != null)
                    Console.WriteLine("Cannot update constant bindable");
                T oldValue = m_value;
                m_value = value;
                OnValueChange?.Invoke(oldValue);
            }
        }

        /// <inheritdoc/>
        public void BindDataFrom(IBindable<T> other)
        {
            if (other is ConstantBindable<T> constantBindable)
            {
                constantAccessor = constantBindable.Accessor;
            }
            else
            {
                constantAccessor = null;
                other.OnValueChange += oldValue =>
                {
                    Value = other.Value;
                };
                Value = other.Value;
            }
        }

        /// <inheritdoc/>
        public void BindDataTo(IBindable<T> other)
        {
            OnValueChange += oldValue =>
            {
                other.Value = Value;
            };
            other.Value = Value;
        }
    }
}
