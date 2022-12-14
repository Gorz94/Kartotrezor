using Kartotrezor.Utils;

namespace Kartotrezor.Model
{
    public class Map
    {
        public const int MAX_SIZE = 12;

        public Map(int w, int h)
        {
            if (w <= 0 || h <= 0 || w > MAX_SIZE || h > MAX_SIZE) throw new ArgumentException($"Cannot create map with {w}x{h}");
            Width = w;
            Height = h;
            Slots = Enumerable.Range(0, w * h).Select(i => new MapSlot(i % w, i / w)).ToArray();
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public MapSlot[] Slots { get; set; }

        public MapSlot this[int x, int y] => Slots[y * Width + x];

        public override string ToString()
            => this.PrintMap();
    }

    public class MapSlot
    {
        public MapSlot(int x, int y)
        {
            X = x;
            Y = y;
        }

        public IEnumerable<Entity> Entities { get; set; } = Enumerable.Empty<Entity>();

        public Level Level { get; set; } = Level.Plain;

        public int X { get; set; }

        public int Y { get; set; }
    }
}
