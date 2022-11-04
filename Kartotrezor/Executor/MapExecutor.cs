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
        public Map ExecuteMap(Command[] command, Map map = null)
        {
            if (command.IsNullOrEmpty()) throw new ArgumentNullException(nameof(command));

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
            return map;
        }

        private static Map Execute(LevelCommand command, Map map)
        {
            return map;
        }

        private static Map Execute(SetTreasureCommand command, Map map)
        {
            return map;
        }

        private static Map Execute(InitPlayerCommand command, Map map)
        {
            return map;
        }

        private static Map Execute(MovePlayerForwardCommand command, Map map)
        {
            return map;
        }

        private static Map Execute(ChangePlayerDirectionCommand command, Map map)
        {
            return map;
        }
    }
}
