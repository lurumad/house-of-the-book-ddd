using System;
using System.Collections.Generic;
using System.Reflection;

namespace HouseOfTheBook.Common.Domain.Model
{
    public abstract class ValueObject<T> : IEquatable<T> where T : ValueObject<T>
    {
        protected bool Equals(ValueObject<T> other)
        {
            var fields = GetFields();
            foreach (var field in fields)
            {
                var value1 = field.GetValue(this);
                var value2 = field.GetValue(other);

                if (value1 == null)
                {
                    if (value2 != null)
                    {
                        return false;
                    }
                }
                else if (!value1.Equals(value2))
                {
                    return false;
                }
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ValueObject<T>)obj);
        }

        public override int GetHashCode()
        {
            var fields = GetFields();
            var hash = 12;
            foreach (var field in fields)
            {
                var value = field.GetValue(this);
                if (value != null)
                {
                    hash += hash * 7 + value.GetHashCode();
                }
            }
            return hash;
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            var type = GetType();
            var fields = new List<FieldInfo>();

            while (type != typeof(object))
            {
                fields.AddRange(type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));
                type = type.BaseType;
            }

            return fields;
        }

        public bool Equals(T other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.GetType() != GetType()) return false;
            return Equals((ValueObject<T>)other);
        }
    }
}
