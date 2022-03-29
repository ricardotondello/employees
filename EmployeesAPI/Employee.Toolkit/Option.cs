namespace Employee.Toolkit
{
    public struct Option<T>
    {
        public static Option<T> None => default;
        public static Option<T> Some(T value) => new Option<T>(value);

        readonly bool isSome;
        readonly T value;

        Option(T value)
        {
            this.value = value;
            isSome = this.value is { };
        }

        public bool IsSome()
        {
            return isSome;
        }

        public T Value()
        {
            return this.value;
        }
    }
}