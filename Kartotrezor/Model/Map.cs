using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kartotrezor.Model
{
    public class Map
    {

    }

    public class MapSlot
    {
        public IEnumerable<Entity> Entities { get; set; }

        public Level Level { get; set; }
    }
}
