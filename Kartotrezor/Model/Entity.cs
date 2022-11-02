namespace Kartotrezor.Model
{
    public abstract class Entity
    {
        public abstract CollisionResult OnWalkedOn();
    }

    public enum CollisionResult
    {
        Null = 0, NeedDeletion
    }

    public enum EntityType
    {
        Null = 0, Treasure
    }
}
