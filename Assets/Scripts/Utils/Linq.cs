using System;
using System.Collections.Generic;

namespace Utils.Linq {
    public static class IEnumerableExtensions {
        /// <summary>
        /// Returns all elements of a sequence which satisfy a condition.
        /// </summary>
        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, Func<T, bool> predicate) {
            List<T> newList = new List<T>();
            foreach (T t in source) {
                if (predicate(t)) {
                    newList.Add(t);
                }
            }
            return newList;
        }

        /// <summary>
        /// Projects each element of a sequence into a new form.
        /// </summary>
        public static IEnumerable<U> Select<T, U>(this IEnumerable<T> source, Func<T, U> predicate) {
            List<U> list = new List<U>();
            foreach (T t in source) {
                list.Add(predicate(t));
            }
            return list;
        }

		/// <summary>
		/// Projects each element of a sequence into an IEnumerable and unites them into one sequence.
		/// </summary>
        public static IEnumerable<U> SelectMany<T, U>(this IEnumerable<T> source, Func<T, IEnumerable<U>> predicate) {
			HashSet<U> set = new HashSet<U>();
			foreach (T t in source) {
				set.UnionWith(predicate(t));
			}
			return set.ToList();
        }

        /// <summary>
        /// Determines whether any element of a sequence satisfies a condition.
        /// </summary>
        public static bool Any<T>(this IEnumerable<T> source, Func<T, bool> predicate) {
            foreach (T t in source) {
                if (predicate(t)) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Computes the sum of a sequence using a given function.
        /// </summary>
        public static decimal Sum<T>(this IEnumerable<T> source, Func<T, decimal> predicate) {
            decimal sum = 0;
            foreach (T t in source) {
                sum += predicate(t);
            }
            return sum;
        }

        /// <summary>
        /// Determines whether a sequence contains a certain item.
        /// </summary>
        public static bool Contains<T>(this IEnumerable<T> source, T item) {
            foreach (T t in source) {
                if (t.Equals(item)) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether a sequence contains an item satisfying a condition.
        /// </summary>
        public static bool Contains<T>(this IEnumerable<T> source, Func<T, bool> predicate) {
            foreach (T t in source) {
                if (predicate(t)) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns an element from a sequence which satisfies a condition. Default if no element is found.
        /// </summary>
        public static T Find<T>(this IEnumerable<T> source, Func<T, bool> predicate) {
            foreach (T t in source) {
                if (predicate(t)) {
                    return t;
                }
            }
            return default(T);
        }

        /// <summary>
        /// Returns an array containing all items from a sequence.
        /// </summary>
        public static T[] ToArray<T>(this IEnumerable<T> source) {
            int length = 0;
#pragma warning disable 0219
            foreach (T t in source) {
                ++length;
            }
#pragma warning restore 0219

            T[] array = new T[length];
            int i = 0;
            foreach (T t in source) {
                array[i] = t;
                ++i;
            }
            return array;
        }

        /// <summary>
        /// Returns a list containing all items from a sequence.
        /// </summary>
        public static List<T> ToList<T>(this IEnumerable<T> source) {
            List<T> list = new List<T>();
            foreach (T t in source) {
                list.Add(t);
            }
            return list;
        }

        /// <summary>
        /// Transforms the sequence in a dictionary, according to a transformer function
        /// </summary>
        public static Dictionary<T, U> ToDictionary<T, U>(this IEnumerable<T> source, Func<T, U> predicate) {
            Dictionary<T, U> dictionary = new Dictionary<T, U>();
            foreach (T t in source) {
                dictionary.Add(t, predicate(t));
            }
            return dictionary;
        }

        /// <summary>
        /// Returns elements from a sequence in ascending order according to some key
        /// </summary>
        public static IEnumerable<T> OrderBy<T, U>(this IEnumerable<T> source, Func<T, U> predicate) where U : IComparable {
            List<T> list = new List<T>();
            foreach (T t in source) {
                list.Add(t);
            }
            list.Sort((x, y) => predicate(x).CompareTo(predicate(y)));
            return list;
        }
	}
}
