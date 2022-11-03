using Kartotrezor.Model.Entities;

namespace Kartotrezor.Model
{
    public abstract class Command
    {
        public abstract Map Execute(Map map);
    }

    public class InitMapCommand : Command
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public override Map Execute(Map map)
        {
            if (map != null) throw new InvalidOperationException("The map can only be initiated once.");

            // Faire le taff
            return map;
        }
    }

    public class LevelCommand : Command
    {
        public Level Level { get; set; }

        public override Map Execute(Map map)
        {
            throw new NotImplementedException();
        }
    }

    public class SetTreasureCommand : Command
    {
        public Treasure Treasure { get; set; }

        public override Map Execute(Map map)
        {
            throw new NotImplementedException();
        }
    }

    public class InitPlayerCommand : Command
    {
        public string PlayerName { get; set; }

        public int X { set; get; }

        public int Y { set; get; }

        public override Map Execute(Map map)
        {
            throw new NotImplementedException();
        }
    }

    public class MovePlayerForwardCommand : Command
    {
        public string PlayerName { get; set; }

        public override Map Execute(Map map)
        {
            throw new NotImplementedException();
        }
    }

    public class ChangePlayerDirectionCommand : Command
    {
        public string PlayerName { get; set; }

        public Direction Direction { get; set; }

        public override Map Execute(Map map)
        {
            throw new NotImplementedException();
        }
    }
}
