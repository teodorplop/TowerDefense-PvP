using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Collections {
    #region List
    public static void Shuffle<T>(this IList<T> list) {
        if (IsNullOrEmpty(list)) return;

        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static T Random<T>(this IList<T> list) {
        if (IsNullOrEmpty(list)) return default(T);

        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static bool IsNullOrEmpty<T>(this IEnumerable<T> list) {
        return list == null || !list.Any();
    }

    public static void Swap<T>(this IList<T> list, int x, int y) {
        T aux = list[x];
        list[x] = list[y];
        list[y] = aux;
    }
    #endregion

    #region Other collections
    public static bool IsNullOrEmpty<T>(this Queue<T> collection) {
        return collection == null || collection.Count == 0;
    }

    public static void Update<T, U>(this Dictionary<T, U> dict, T t, U u) {
        if (dict.ContainsKey(t)) dict[t] = u;
        else dict.Add(t, u);
    }

    public static string Print<T>(this IEnumerable<T> enumerable) {
        if (enumerable == null) return "Null";
        if (!enumerable.Any()) return "Empty";

        var sb = new StringBuilder();
        foreach (var iter in enumerable) {
            if (sb.Length > 0)
                sb.Append(", ");
            sb.Append(iter);
        }
        return sb.ToString();
    }

    public static T Last<T>(this List<T> enumerable)
    {
        if (enumerable == null) return default(T);
        if (!enumerable.Any()) return default(T);
        return enumerable[enumerable.Count - 1];
    }

    public static T Last<T>(this T[] enumerable)
    {
        if (enumerable == null) return default(T);
        if (!enumerable.Any()) return default(T);
        return enumerable[enumerable.Length - 1];
    }
    #endregion
}
