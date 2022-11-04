using Kartotrezor.Model;
using Kartotrezor.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kartotrezor.Executor
{
    public class MapExecutor
    {
        private IDictionary<Type, int> _commandPriority = new Dictionary<Type, int>
        {
            { typeof(InitMapCommand), 0 },
            { typeof(LevelCommand), 1 },
            { typeof(SetTreasureCommand), 2 },
            { typeof(InitPlayerCommand), 3 },
            { typeof(MovePlayerForwardCommand), 4 },
            { typeof(ChangePlayerDirectionCommand), 4 }
        };

        public Map ExecuteMap(Command[] command, Map map = null)
        {
            if (command.IsNullOrEmpty() || command.Any(c => c is null)) throw new ArgumentNullException(nameof(command));

            var order = command.Select(c => _commandPriority[c.GetType()]);

            if (order.Zip(order.Skip(1), (prev, next) => next - prev).Any(k => k < 0))
                throw new ArgumentException("Your command is not in the correct order");

            try
            {
                foreach (var cmd in command)
                    map = ExecuteCommand(cmd, map);

                return map;
            } catch (Exception e)
            {
                Console.WriteLine($"Something bad happened in {nameof(ExecuteMap)}: {e.Message}");
                throw new InvalidOperationException($"[{nameof(ExecuteMap)}] {e.Message}");
            }
        }

        public static Map ExecuteCommand(dynamic command, Map map) => Execute(command, map);

        public static Map Execute(InitMapCommand command, Map map)
        {
            if (map != null) throw new InvalidOperationException($"[{nameof(Execute)}] Map is already set.");

            return new Map(command.Width, command.Height);
        }

        private static Map Execute(LevelCommand command, Map map)
        {
            if (map == null) throw new ArgumentNullException(nameof(map));

            var slot = map[command.X, command.Y];

            // On accepte la redefinition, on a le droit de se tromper ...
            slot.Level = command.Level;

            return map;
        }

        private static Map Execute(SetTreasureCommand command, Map map)
        {
            if (map == null) throw new ArgumentNullException(nameof(map));
            return map;
        }

        private static Map Execute(InitPlayerCommand command, Map map)
        {
            if (map == null) throw new ArgumentNullException(nameof(map));
            return map;
        }

        private static Map Execute(MovePlayerForwardCommand command, Map map)
        {
            if (map == null) throw new ArgumentNullException(nameof(map));
            return map;
        }

        private static Map Execute(ChangePlayerDirectionCommand command, Map map)
        {
            if (map == null) throw new ArgumentNullException(nameof(map));
            return map;
        }
    }
}
