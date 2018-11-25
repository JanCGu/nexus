using System.Collections.Generic;

namespace Core.Service {
    public static class HashSetExtension {
        public static HashSet<T> ToHashSet<T>(
            this IEnumerable<T> source,
            IEqualityComparer<T> comparer = null) {
            return new HashSet<T>(source, comparer);
        }
    }
}
