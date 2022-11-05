namespace Kartotrezor.Model.Entities
{
    public class Adventurer : Entity
    {
        public Adventurer(string name, Direction direction)
        {
            Name = name;
            Direction = direction;
        }

        public Direction Direction { get; set; }

        public string Name { get; set; }

        public int Treasures { get; set; }

        public override CollisionResult OnWalkedOn() => CollisionResult.Null;
    }

    public enum Direction { N = 0, S, E, W };

    public enum Turn { D = 0, G };
}
