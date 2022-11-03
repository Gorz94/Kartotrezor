using Kartotrezor.Model;
using Kartotrezor.Model.Entities;

namespace Kartotrezor.Utils
{
    public static class Utils
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

        public static Turn ToTurn(this string s)
            => s switch
            {
                "D" => Turn.D,
                "G" => Turn.G,
                _ => throw new ArgumentException($"{s} is not a valid {typeof(Turn).Name}")
            };

        public static Level ToLevel(this string s)
            => s switch
            {
                "M" => Level.Mountain,
                _ => Level.Plain
            };
    }
}
