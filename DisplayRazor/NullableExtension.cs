using System;
using System.ComponentModel;

namespace DisplayRazor {
    public static class NullableExtension {
        public static Nullable<T> ToNullable<T>(this string s) where T : struct {
            try {
                if (string.IsNullOrWhiteSpace(s) || s.Trim().Length == 0)
                    return null;

                var conv = TypeDescriptor.GetConverter(typeof(T));
                return (T)conv.ConvertFrom(s);
            }
            catch {
                return null;
            }
        }

        public static Nullable<DateTime> ToNullableDateTime(this string s) {
            try {
                if (string.IsNullOrWhiteSpace(s) || s.Trim().Length ==0)
                    return null;
                return DateTime.Parse(s).ToUniversalTime();
            }
            catch {
                return null;
            }
        }
    }
}
