using Kartotrezor.Model.Entities;
using Kartotrezor.Utils;

namespace Kartotrezor.Model
{
    public abstract class Command { }

    public class InitMapCommand : Command
    {
        public InitMapCommand(int w, int h)
        {
            Width = w;
            Height = h; 
        }

        public int Width { get; set; }

        public int Height { get; set; }
    }

    public class LevelCommand : Command
    {
        public LevelCommand(Level level, int x, int y)
        {
            if (!(x, y).IsPositionValid()) throw new ArgumentException("Treasure pos is not valid");

            Level = level;
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public Level Level { get; set; }
    }

    public class SetTreasureCommand : Command
    {
        public SetTreasureCommand(int x, int y, int value)
        {
            if (!(x, y).IsPositionValid()) throw new ArgumentException("Player is not valid");

            X = x;
            Y = y;
            Treasure = new Treasure(value);
        }

        public int X { get; set; }

        public int Y { get; set; }

        public Treasure Treasure { get; set; }
    }

    public class InitPlayerCommand : Command
    {
        public InitPlayerCommand(string playerName, Direction dir, int x, int y)
        {
            if (string.IsNullOrEmpty(playerName) || !(x,y).IsPositionValid()) throw new ArgumentException("Player is not valid");

            Direction = dir;
            X = x;
            Y = y;
            PlayerName = playerName;
        }

        public string PlayerName { get; set; }

        public int X { set; get; }

        public int Y { set; get; }

        public Direction Direction { get; set; }
    }

    public class MovePlayerForwardCommand : Command
    {
        public MovePlayerForwardCommand(string name)
        {
            PlayerName = name;
        }

        public string PlayerName { get; set; }
    }

    public class ChangePlayerDirectionCommand : Command
    {
        public ChangePlayerDirectionCommand(string name, Turn turn)
        {
            PlayerName = name;
            Turn = turn;
        }

        public string PlayerName { get; set; }

        public Turn Turn { get; set; }
    }
}
