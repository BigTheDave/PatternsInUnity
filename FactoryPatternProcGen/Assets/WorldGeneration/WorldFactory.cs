using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.WorldGeneration
{
    public interface IWorldFactory {
        IWorldFactory SetSeed(int seed);
        IWorldFactory GridSize(int width, int height);
        IWorldFactory CreateTileTemplate(int id, int probabilty);
        List<IWorldTile> BuildWorld();
    }
    public class WorldFactory
    {
        public enum WorldFactoryGeneratorType
        {
            Basic
        }
        public static IWorldFactory Get(WorldFactoryGeneratorType generatorType)
        {
            switch (generatorType)
            {
                case WorldFactoryGeneratorType.Basic:
                    return new BasicWorldFactory();
            }
            return null;
        }
    }
    internal class BasicWorldFactory : IWorldFactory
    {
        private class TileTemplate
        {
            public int Probability { get; set; }
            public int Id { get; set; }
        }

        private List<TileTemplate> tileTemplates = new List<TileTemplate>();
        private int MaximumProbabilty;

        private Random mRng = new Random();
        private int mWidth = 10;
        private int mHeight = 10;

        public IWorldFactory CreateTileTemplate(int id, int probability)
        {
            tileTemplates.Add(new TileTemplate()
            {
                Id = id,
                Probability = probability
            });
            return this;
        }

        public IWorldFactory GridSize(int width, int height)
        {
            mWidth = width;
            mHeight = height;
            return this;
        }

        public List<IWorldTile> BuildWorld()
        {
            MaximumProbabilty = tileTemplates.Sum(tt => tt.Probability);
            List<IWorldTile> world = new List<IWorldTile>();
            for(int x = 0; x < mWidth; x++)
            {
                for(int y = 0; y < mHeight; y++)
                {
                    var tile = new WorldTile()
                    {
                        X = x,
                        Y = y,
                        TypeId = GetNextTypeId()
                    };
                    
                    world.Add(tile);
                }
            }
            return world;
        }

        /* Worked through 'test' data for GetNextTypeId()
         [0], 10
         [1], 5
         [2], 7
         total = 22

            roll 12
            [0], 12 - 10 = 2
            [1], 2 - 5 = -3 <= return 1

            roll 10
            [0], 10 - 10 = 0
            [1], 0 - 5 = -5 <= return 1

            roll 0
            [0], 0 - 10 = -10 <= return 0

            roll 21
            [0], 21 - 10 = 11
            [1], 11 - 5 = 6
            [2], 6 - 7 = -1 <= return 2
         */
        private int GetNextTypeId()
        {
            var roll = mRng.Next(0, MaximumProbabilty);
            var originalRoll = roll;
            foreach(var tileTemplate in tileTemplates)
            {
                roll -= tileTemplate.Probability;
                if (roll < 0) return tileTemplate.Id;
            }
            throw new Exception($"Invalid roll '{originalRoll}'. Maximum probability = '{MaximumProbabilty}'. Actual Maximum Probability = '{tileTemplates.Sum(tt=>tt.Probability)}'");
        }
        public IWorldFactory SetSeed(int seed)
        {            
            mRng = new Random(seed);
            return this;
        }
    }
}
