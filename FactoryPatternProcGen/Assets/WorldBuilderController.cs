using Assets.WorldGeneration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldBuilderController : MonoBehaviour
{
    public int Seed;
    public int Width, Height;
    public List<TileProbability> TilePrefabs;
    [System.Serializable]
    public class TileProbability
    {
        public GameObject Prefab;
        public int Probability;
    }
    public float TileSize = 1;
    [ContextMenu("Rebuild World")]
    public void RebuildWorld()
    {
        for(int i = this.transform.childCount-1; i >= 0; i--)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
        BuildWorld();
    }
    public void BuildWorld()
    {
        IWorldFactory factory = WorldFactory.Get(WorldFactory.WorldFactoryGeneratorType.Basic)
            .SetSeed(Seed)
            .GridSize(Width, Height);
        for(int id = 0; id < TilePrefabs.Count; id++)
        {
            factory.CreateTileTemplate(id, TilePrefabs[id].Probability);
        }
        DisplayWorld(factory.BuildWorld());
    }
    public void AnalyseWorld(List<IWorldTile> world)
    {
        var typeIds = world.Select(x => x.TypeId);
        var distinctIds = typeIds.Distinct().ToList();
        foreach(var id in distinctIds)
        {
            Debug.Log($"{id} count = '{typeIds.Count(t => t == id)}'");
        }
    }
    public void DisplayWorld(List<IWorldTile> world)
    {
        AnalyseWorld(world);
        foreach (var tile in world)
        {
            var prefab = TilePrefabs[tile.TypeId].Prefab;
            var tileInstance = Instantiate(prefab,this.transform);
            tileInstance.transform.localPosition = new Vector2(tile.X * TileSize, tile.Y * TileSize);
        }
    }
    private void OnEnable()
    {
        BuildWorld();
    }
}
