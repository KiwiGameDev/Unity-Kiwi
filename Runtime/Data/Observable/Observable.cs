namespace Kiwi.Data
{
    public struct Observable<T> where T : struct
    {
        public delegate void OnValueChanged(T prevValue, T nextValue);

        public event OnValueChanged ValueChanged;

        public T Value
        {
            get => value;
            set
            {
                if (Equals(this.value, value))
                    return;

                T previousValue = this.value;

                this.value = value;

                ValueChanged?.Invoke(previousValue, value);
            }
        }

        T value;
    }
}