using Kartotrezor.Entities;
using Kartotrezor.Model;
using Kartotrezor.Model.Entities;

namespace Kartotrezor.Utils
{
    public static class MapUtils
    {
        private static IDictionary<Direction, Direction> _nextDirections = new Dictionary<Direction, Direction>
        {
            { Direction.N, Direction.E }, { Direction.E, Direction.S }, { Direction.S, Direction.W }, { Direction.W, Direction.N }
        };

        public static void AddPlayer(this Map map, int x, int y, string name, Direction dir, int treasures = 0)
            => AddPlayer(map, x, y, new Adventurer(name, dir) { Treasures = treasures });

        public static void AddPlayer(this Map map, int x, int y, Adventurer adv)
        {
            map[x, y].Entities = map[x, y].Entities.Concat(new Entity[] { adv });
        }

        public static void AddTreasure(this Map map, int x, int y, int count)
        {
            map[x, y].Entities = map[x, y].Entities.Concat(new Entity[] { new Treasure(count) });
        }

        public static void SetLevel(this Map map, int x, int y, Level level)
        {
            map[x, y].Level = level;
        }

        public static (Adventurer adventurer, int x, int y) FindAdventurer (this Map map, string name)
        {
            foreach (var slot in map.Slots.Where(s => !s.Entities.IsNullOrEmpty()))
            {
                var player = slot.Entities.OfType<Adventurer>().FirstOrDefault(a => a.Name == name);

                if (player != null) return (player, slot.X, slot.Y);
            }

            return (null, 0, 0);
        }

        public static (int X, int Y) CalculateNextPos(this Map map, int x, int y, Direction dir)
        {
            var (x2, y2) = dir switch
            {
                Direction.W => (x - 1, y),
                Direction.E => (x + 1, y),
                Direction.N => (x, y - 1),
                Direction.S => (x, y + 1),
                _ => throw new NotSupportedException($"Direction {dir} is not supported")
            };

            return (x2, y2).IsPositionValid() && x2 < map.Width && y2 < map.Height && map[x2, y2].Level != Level.Mountain
                ?
                (x2, y2) : (x, y);
        }

        public static Direction Turn(this Direction d, Turn t)
            => t == Model.Entities.Turn.D ? _nextDirections[d] : _nextDirections.FirstOrDefault(k => k.Value == d).Key;
    }
}
