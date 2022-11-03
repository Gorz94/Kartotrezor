using Kartotrezor.Model.Entities;

namespace Kartotrezor.Model
{
    public abstract class Command
    {
        public abstract Map Execute(Map map);
    }

    public class InitMapCommand : Command
    {
        public InitMapCommand(int w, int h)
        {
            
        }

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
        public LevelCommand(Level level, int x, int y)
        {
            
        }

        public int X { get; set; }

        public int Y { get; set; }

        public Level Level { get; set; }

        public override Map Execute(Map map)
        {
            throw new NotImplementedException();
        }
    }

    public class SetTreasureCommand : Command
    {
        public SetTreasureCommand(int x, int y, int value)
        {
            
        }

        public int X { get; set; }

        public int Y { get; set; }

        public Treasure Treasure { get; set; }

        public override Map Execute(Map map)
        {
            throw new NotImplementedException();
        }
    }

    public class InitPlayerCommand : Command
    {
        public InitPlayerCommand(string playerName, Direction dir, int x, int y)
        {
            
        }

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
        public MovePlayerForwardCommand(string name)
        {
            
        }

        public string PlayerName { get; set; }

        public override Map Execute(Map map)
        {
            throw new NotImplementedException();
        }
    }

    public class ChangePlayerDirectionCommand : Command
    {
        public ChangePlayerDirectionCommand(string name, Direction dir)
        {

        }

        public string PlayerName { get; set; }

        public Direction Direction { get; set; }

        public override Map Execute(Map map)
        {
            throw new NotImplementedException();
        }
    }
}
