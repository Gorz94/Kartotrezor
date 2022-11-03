using Kartotrezor.Model.Entities;

namespace Kartotrezor.Utils
{
    public static class EntityUtils
    {
        public static Direction ToDirection(this string s)
            => s switch
            {
                "S" => Direction.S,
                "N" => Direction.N,
                "E" => Direction.E,
                "W" => Direction.W,
                _ => throw new ArgumentException($"{s} is not a valid {typeof(Direction).Name}")
            };
    }
}
