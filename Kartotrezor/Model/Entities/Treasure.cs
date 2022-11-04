using Kartotrezor.Model;

namespace Kartotrezor.Entities
{
    public class Treasure : Entity
    {
        public Treasure(int count)
        {
            if (count == 0)
                throw new ArgumentException("You need at least one teasure ...");

            Count = count;
        }

        public int Count { get; set; }

        public override CollisionResult OnWalkedOn()
        {
            Count--;

            return Count > 0 ? CollisionResult.Null : CollisionResult.NeedDeletion;
        }
    }
}
