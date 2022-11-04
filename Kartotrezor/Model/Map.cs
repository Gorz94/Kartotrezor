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
            Slots = Enumerable.Range(0, w * h).Select(_ => new MapSlot()).ToArray();
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public MapSlot[] Slots { get; set; }

        public MapSlot this[int x, int y] => Slots[x * Width + y];
    }

    public class MapSlot
    {
        public IEnumerable<Entity> Entities { get; set; }

        public Level Level { get; set; }
    }
}
