using Kartotrezor.Model;

namespace Kartotrezor.Back.Utils
{
    public static class MapUtils
    {
        public static object ToConcrete(this Map map)
            => new
            {
                Width = map.Width,
                Height = map.Height,
                Slots = map.Slots.Select(s => new
                {
                    Level = s.Level,
                    X = s.X,
                    Y = s.Y,
                    Entities = s.Entities.Select(e => new { Name = e.GetType().Name, Entity = (object)e })
                })
            };
    }
}
