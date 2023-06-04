using System;
using System.Collections.Generic;
public static class HashSetExtensions
{
    public static T GetRandomItem<T>(this HashSet<T> set)
    {
        if (set == null)
            throw new ArgumentNullException(nameof(set));

        if (set.Count == 0)
            throw new InvalidOperationException("The set is empty.");

        int index = UnityEngine.Random.Range(0, set.Count);
        using var enumerator = set.GetEnumerator();

        for (int i = 0; i <= index; i++)
        {
            enumerator.MoveNext();
        }

        return enumerator.Current;
    }
}
