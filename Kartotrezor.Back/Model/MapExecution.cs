using Kartotrezor.Model;

namespace Kartotrezor.Back.Model
{
    public class MapExecution
    {
        public Map Map { get; set; }

        public Command[] Commands { get; set; }

        public int Done { get; set; }
    }
}
