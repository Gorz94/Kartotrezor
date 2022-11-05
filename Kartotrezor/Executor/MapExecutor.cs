using Kartotrezor.Entities;
using Kartotrezor.Model;
using Kartotrezor.Model.Entities;
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

        private IList<Type> _resetPriorities = new List<Type>() { typeof(InitPlayerCommand) };

        public Map ExecuteMap(Command[] command, Map map = null)
        {
            if (command.IsNullOrEmpty() || command.Any(c => c is null)) throw new ArgumentNullException(nameof(command));

            var order = command.Select(c => (priority: _commandPriority[c.GetType()], command: c));

            if (order.Zip(order.Skip(1), (prev, next) => (next: next.command, diff: next.priority - prev.priority))
                     .Any(k => k.diff < 0 && !_resetPriorities.Contains(k.next.GetType())))
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

            var slot = map[command.X, command.Y];

            if (slot.Entities.Any(e => e is Treasure)) throw new InvalidOperationException("There is already a treasure !");

            map.AddTreasure(command.X, command.Y, command.Treasure.Count);
            
            return map;
        }

        private static Map Execute(InitPlayerCommand command, Map map)
        {
            if (map == null) throw new ArgumentNullException(nameof(map));

            var slot = map[command.X, command.Y];

            if (slot.Entities.Any(e => e is Adventurer))
                throw new InvalidOperationException("There is already a player !");
            if (map.Slots.SelectMany(s => s.Entities).Any(e => e is Adventurer a && a.Name == command.PlayerName))
                throw new InvalidOperationException($"{command.PlayerName} is already looking for the treasure ...");

            map.AddPlayer(command.X, command.Y, command.PlayerName, command.Direction);

            return map;
        }

        private static Map Execute(MovePlayerForwardCommand command, Map map)
        {
            if (map == null) throw new ArgumentNullException(nameof(map));

            var (player, x, y) = map.FindAdventurer(command.PlayerName);

            if (player == null) throw new InvalidOperationException("Cannot move an inexisting player");

            var nextPos = map.CalculateNextPos(x, y, player.Direction);

            if (nextPos != (x, y) && !map[nextPos.X, nextPos.Y].Entities.Any(e => e is Adventurer))
            {
                // Il n'y a qu'un joueur par case, mais on sait jamais
                map[x, y].Entities = map[x, y].Entities.Where(e => !(e is Adventurer a && a.Name == command.PlayerName)).ToArray();

                var remainingEntities = map[nextPos.X, nextPos.Y].Entities.ToArray();

                player.Treasures += remainingEntities.Any(t => t is Treasure) ? 1 : 0;

                // Devrait être ailleurs ...
                if (remainingEntities.Any())
                    map[nextPos.X, nextPos.Y].Entities = remainingEntities.Select(e => (Entity: e, Result: e.OnWalkedOn()))
                        .Where(o => o.Result != CollisionResult.NeedDeletion)
                                                    .Select(o => o.Entity).ToArray();

                map.AddPlayer(nextPos.X, nextPos.Y, player);
            }

            return map;
        }

        private static Map Execute(ChangePlayerDirectionCommand command, Map map)
        {
            if (map == null) throw new ArgumentNullException(nameof(map));

            var (player, x, y) = map.FindAdventurer(command.PlayerName);

            if (player == null) throw new InvalidOperationException("Cannot change direction an inexisting player");

            player.Direction = player.Direction.Turn(command.Turn);

            return map;
        }
    }
}
