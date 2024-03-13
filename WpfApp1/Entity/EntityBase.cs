namespace Entity
{
    public abstract class EntityBase<T>
    {
        Guid Identifier { get; }

        public EntityBase()
        {
            Identifier = Guid.NewGuid();
        }

        public bool IsSameAs(EntityBase<T> other)
        {
            return Identifier == other.Identifier;
        }

        virtual public T Clone()
        {
            return (T)MemberwiseClone();
        }
    }
}
