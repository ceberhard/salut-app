namespace SalutAPI.Util;

public class RandomUtil {
    private static readonly System.Random _getRandom = new System.Random();

    public static int Get(int min, int max) {
        lock(_getRandom) {
            return _getRandom.Next(min, max);
        }
    }

    public static bool CheckPercent(int percent) {
        lock(_getRandom) {
            int val = _getRandom.Next(1, 100);
            return (val <= percent);
        }
    }
}

public static class IEnumerable {
    public static void AddRange<T>(this IEnumerable<T> addTo, IEnumerable<T> items ) {
        foreach(var item in items) {
            _ = addTo.Append(item);
        }
    }
}

