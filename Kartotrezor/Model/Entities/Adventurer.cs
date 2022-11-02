namespace Kartotrezor.Model.Entities
{
    public class Adventurer : Entity
    {
        public Direction Direction { get; set; }

        public string Name { get; set; }

        public override CollisionResult OnWalkedOn() => CollisionResult.Null;
    }

    public enum Direction { N = 0, S, E, W };
}
