namespace Taikoshin.Framework.Bindables
{
    public interface IBindable<T>
    {
        T Value { get; set; }

        event Bindable<T>.OnValueChangeEvent OnValueChange;

        /// <summary>
        /// Sets this bindable's data to whatever is in the other bindable
        /// </summary>
        void BindDataFrom(IBindable<T> other);
        /// <summary>
        /// Sets the other bindable's data to whatever is in this bindable
        /// </summary>
        void BindDataTo(IBindable<T> other);
    }
}