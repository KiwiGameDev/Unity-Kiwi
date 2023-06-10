using System;

namespace Kiwi.Data
{
    public class Observable<T> where T : struct
    {
        public event Action<T> ValueChanged;

        public T Value
        {
            get => t;
            set
            {
                if (value.Equals(t))
                    return;

                t = value;

                ValueChanged?.Invoke(t);
            }
        }

        T t;
    }
}
