using System;

public static class EnumExtensions {
    public static int GetLength<T>() {
        return Enum.GetValues(typeof(T)).Length;
    }
    public static T[] GetValues<T>() {
        T[] values = new T[GetLength<T>()];
        Enum.GetValues(typeof(T)).CopyTo(values, 0);
        return values;
    }
}
