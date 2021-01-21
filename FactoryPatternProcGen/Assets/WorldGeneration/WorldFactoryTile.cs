using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.WorldGeneration
{
    public interface IWorldTile {
        int X { get; set; }
        int Y { get; set; }
        int TypeId { get; set; }
    }
    internal class WorldTile : IWorldTile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int TypeId { get; set; }
    }
}
