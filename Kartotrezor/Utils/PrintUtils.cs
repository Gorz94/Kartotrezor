using Kartotrezor.Model;
using Kartotrezor.Model.Entities;
using System.Text;

namespace Kartotrezor.Utils
{
    public static class PrintUtils
    {
        private const int CELL_SIZE = 12;

        public static string DrawMap(this Map map)
        {
            string final = string.Empty;

            for (int j = 0; j < map.Height; j++)
            {
                var s = string.Empty;
                for (int i = 0; i < map.Width; i++)
                {
                    var slot = map[i, j];
                    var c = ".";

                    if (slot.Entities.Any())
                    {
                        if (slot.Entities.Any(e => e is Adventurer))
                        {
                            var adv = slot.Entities.OfType<Adventurer>().First();
                            c = $"A({adv.Name})";
                        }
                        else if (slot.Entities.Any(e => e is Treasure))
                        {
                            var tr = slot.Entities.OfType<Treasure>().First();
                            c = $"T({tr.Count})";
                        }
                    } else if (slot.Level != Level.Plain)
                    {
                        c = slot.Level.ToString().First().ToString();
                    }

                    s += Center(c);
                }

                final += (string.IsNullOrEmpty(final) ? string.Empty : Environment.NewLine) + s;
            }

            return final;
        }

        public static string PrintMap(this Map map)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"C - {map.Width} - {map.Height}");

            foreach (var s in map.Slots.Where(s => s.Level != Level.Plain))
                builder.AppendLine($"{s.Level.ToString().First()} - {s.X} - {s.Y}");

            foreach (var s in map.Slots.Where(s => s.Entities.Any(e => e is Treasure)))
            {
                var treasure = s.Entities.OfType<Treasure>().First();
                builder.AppendLine($"T - {s.X} - {s.Y} - {treasure.Count}");
            }

            foreach (var s in map.Slots.Where(s => s.Entities.Any(e => e is Adventurer)))
            {
                var adv = s.Entities.OfType<Adventurer>().First();
                builder.AppendLine($"A - {adv.Name} - {s.X} - {s.Y} - {adv.Direction.ToString().First()} - {adv.Treasures}");
            }

            return builder.ToString();
        }

        private static string Center(string s)
            => s.PadLeft((CELL_SIZE - s.Length) / 2, ' ').PadRight(CELL_SIZE, ' ');  
    }
}
