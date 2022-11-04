namespace Kartotrezor.Model
{
    public class Map
    {
        public const int MAX_SIZE = 12;

        public Map(int w, int h)
        {
            Width = w;
            Height = h;
            Slots = Enumerable.Range(0, w * h).Select(_ => new MapSlot()).ToArray();
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public MapSlot[] Slots { get; set; }
    }

    public class MapSlot
    {
        public IEnumerable<Entity> Entities { get; set; }

        public Level Level { get; set; }
    }
}
