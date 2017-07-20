using System;

namespace HouseOfTheBook.Common.Domain.Model
{
    public abstract class Identity<T> : IEquatable<Identity<T>>
    {
        private readonly T _id;

        protected Identity()
        {
            _id = default(T);
        }

        protected Identity(T id)
        {
            _id = id;
        }

        public bool Equals(Identity<T> id)
        {
            if (Object.ReferenceEquals(this, id)) return true;
            if (Object.ReferenceEquals(null, id)) return false;
            return _id.Equals(id._id);
        }

        public override bool Equals(object anotherObject)
        {
            return Equals(anotherObject as Identity<T>);
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode() * 907 + _id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + _id + "]";
        }

        public static implicit operator T(Identity<T> id)
        {
            return id._id;
        }
    }
}
