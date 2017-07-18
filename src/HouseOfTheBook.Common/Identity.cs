using System;

namespace HouseOfTheBook.Common
{
    public abstract class Identity : IEquatable<Identity>
    {
        private readonly string _id;

        protected Identity()
        {
            _id = Guid.NewGuid().ToString();
        }

        protected Identity(string id)
        {
            _id = id;
        }

        public bool Equals(Identity id)
        {
            if (Object.ReferenceEquals(this, id)) return true;
            if (Object.ReferenceEquals(null, id)) return false;
            return _id.Equals(id._id);
        }

        public override bool Equals(object anotherObject)
        {
            return Equals(anotherObject as Identity);
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode() * 907 + _id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + _id + "]";
        }
    }
}
