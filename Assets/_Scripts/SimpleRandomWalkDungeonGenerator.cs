using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

public class SimpleRandomWalkGenerator : AbstractDungeonGenerator
{
    //[SerializeField]
    //protected Vector2Int startPosition = Vector2Int.zero;

    [SerializeField]
    protected SimpleRandomWalkSO randomWalkParameters;

    //[SerializeField]
    //private TileMapVisualizer tmv;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPosition);
        tmv.Clear();
        tmv.PaintFloortiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tmv);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO parameters, Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < randomWalkParameters.iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, randomWalkParameters.walkLength);
            floorPositions.UnionWith(path);
            if (randomWalkParameters.startRandomlyEachIteration)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0,floorPositions.Count));
            }

        }
        return floorPositions;
    }
}
