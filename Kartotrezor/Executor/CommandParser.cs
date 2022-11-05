using Kartotrezor.Model;
using Kartotrezor.Utils;

namespace Kartotrezor.Executor
{
    public class CommandParser
    {
        private readonly IDictionary<string, int> _mapLength = 
            new Dictionary<string, int>
            {
                { "A", 6 },
                { "C", 3 },
                { "M", 3 },
                { "T", 4 }
            };

        public Command[] ParseCommands(string[] lines)
        {
            if (lines.IsNullOrEmpty()) throw new ArgumentException(nameof(lines));

            return lines.Select(l => ParseCommand(l)).SelectMany(k => k).ToArray();
        }

        private Command[] ParseCommand(string line)
        {
            string[] split = line.Split(new[] { ' ', '-' }, StringSplitOptions.RemoveEmptyEntries).ToArray() ?? new string[0];

            if (split.Length == 0 || split.Length != _mapLength[split[0]]) throw new ArgumentException(line);

            switch (split[0])
            {
                case "A": return ParseInitPlayer(split);
                case "M": return ToList(ParseLevel(split));
                case "T": return ToList(ParseTreasure(split));
                case "C": return ToList(ParseInitMap(split));
                default: throw new ArgumentException(line);
            }

            Command[] ToList(Command c) => new[] { c };
        }

        private Command ParseInitMap(string[] items)
        {
            var i = items.Skip(1).Select(k => int.Parse(k)).ToArray();

            return new InitMapCommand(i[0], i[1]);
        }

        private Command ParseLevel(string[] items)
        {
            var i = items.Skip(1).Select(k => int.Parse(k)).ToArray();

            return new LevelCommand(items[0].ToLevel(), i[0], i[1]);
        }

        private Command ParseTreasure(string[] items)
        {
            var i = items.Skip(1).Select(k => int.Parse(k)).ToArray();

            return new SetTreasureCommand(i[0], i[1], i[2]);
        }

        private Command[] ParseInitPlayer(string[] items)
        {
            return new[]
            {
                new InitPlayerCommand(items[1], items[4].ToDirection(), int.Parse(items[2]), int.Parse(items[3]))
            }.Concat(items.Skip(5).First().Select(i => ParseSubPlayerCommand(items[1], i))).ToArray();
        }

        /// <summary>i: A pour FORWARD ou une DIRECTION</summary>
        private Command ParseSubPlayerCommand(string name, char i)
            => i switch
            {
                'A' => new MovePlayerForwardCommand(name),
                _ => ParseChangeDirection(name, i)
            };

        private Command ParseChangeDirection(string name, char dir)
            =>new ChangePlayerDirectionCommand(name, dir.ToString().ToTurn());
    }
}
