using Kartotrezor.Model;
using Kartotrezor.Model.Entities;

namespace Kartotrezor.Utils
{
    public static class MapUtils
    {
        public static void AddPlayer(this Map map, int x, int y, string name, Direction dir)
        {
            map[x, y].Entities = map[x, y].Entities.Concat(new Entity[] { new Adventurer(name, dir) });
        }

        public static void AddTreasure(this Map map, int x, int y, int count)
        {
            map[x, y].Entities = map[x, y].Entities.Concat(new Entity[] { new Treasure(count) });
        }

        public static void SetLevel(this Map map, int x, int y, Level level)
        {
            map[x, y].Level = level;
        }
    }
}
