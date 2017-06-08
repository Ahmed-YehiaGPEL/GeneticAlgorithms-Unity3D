using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm.Base
{
    
    public interface IGene { }
    public abstract class GeneBase<T> : IGene where T : IEquatable<T> 
    {
        protected bool Equals(GeneBase<T> other)
        {
            return Equals((T)Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((GeneBase<T>) obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public object Value;

        public static bool operator== (GeneBase<T> gene1, GeneBase<T> gene2)
        {
            return gene1 != null && gene1.Value.Equals(gene2.Value);
        }
        public static bool operator ==(GeneBase<T> gene1, T gene2)
        {
            return gene1 != null && gene1.Value.Equals(gene2);
        }
        public static bool operator !=(GeneBase<T> gene1, T gene2)
        {
            return gene1 != null && gene1.Value.Equals(gene2);
        }
        public static bool operator !=(GeneBase<T> gene1, GeneBase<T> gene2)
        {
            return !gene1.Value.Equals(gene2.Value);
        }
    }
}
