using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class LevelTileController : MonoBehaviour
{
    [SerializeField] int numRows = 5;
    [SerializeField] int numCols = 5;
    [SerializeField] float spacing = 10f;

    [SerializeField] List<GameObject> TilePrefab = new();
    [SerializeField] GameObject TileEdge;
    [SerializeField] NavMeshSurface navMesh;

    private void Start()
    {
        GenerateGrid();
        navMesh.BuildNavMesh();
    }

    void GenerateGrid()
    {
        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                Vector3 position = new Vector3(
                    col * spacing - (numCols - 1) * spacing * 0.5f,
                    0,
                    row * spacing - (numRows - 1) * spacing * 0.5f
                ) + transform.position;

                GameObject tilePrefab = (row == 0 || row == numRows - 1 || col == 0 || col == numCols - 1) ? TileEdge : TilePrefab[Random.Range(0, TilePrefab.Count)];

                Instantiate(tilePrefab, position, Quaternion.identity);
            }
        }
    }
}
