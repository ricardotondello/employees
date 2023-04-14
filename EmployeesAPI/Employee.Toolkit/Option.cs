namespace Employee.Toolkit
{
    public readonly struct Option<T>
    {
        public static Option<T> None => default;
        public static Option<T> Some(T value) => new Option<T>(value);

        private readonly bool _isSome;
        private readonly T _value;

        private Option(T value)
        {
            _value = value;
            _isSome = _value is not null;
        }

        public bool IsSome()
        {
            return _isSome;
        }

        public T Value()
        {
            return _value;
        }
    }
}