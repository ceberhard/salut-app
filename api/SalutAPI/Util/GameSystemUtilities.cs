namespace SalutAPI.Util;

public class RandomUtil {
    private static readonly System.Random _getRandom = new System.Random();

    public static int Get(int min, int max) {
        lock(_getRandom) {
            return _getRandom.Next(min, max);
        }
    }
}

