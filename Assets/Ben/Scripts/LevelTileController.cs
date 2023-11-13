using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelTileController : MonoBehaviour
{
    [SerializeField] int numRows = 5;
    [SerializeField] int numCols = 5;
    [SerializeField] float spacing = 10f;

    [SerializeField] List<TileWithChange> TilePrefab = new();
    [SerializeField] GameObject edgeTilePrefab;
    [SerializeField] GameObject centerTilePrefab;

    [Serializable]
    public class TileWithChange
    {
        public GameObject tile;
        public float chance;
    }

    public void GenerateGrid()
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

                GameObject tilePrefab;

                if (row == 0 || row == numRows - 1 || col == 0 || col == numCols - 1)
                {
                    // Edge tile
                    tilePrefab = edgeTilePrefab;
                }
                else if (row == numRows / 2 && col == numCols / 2)
                {
                    // Center tile
                    tilePrefab = centerTilePrefab;
                }
                else
                {
                    // Inner tile
                    tilePrefab = PickRandomItem();
                }

                Instantiate(tilePrefab, position, Quaternion.identity);
            }
        }
    }

    GameObject PickRandomItem()
    {
        float totalChances = 0;

        foreach (var gameObjectChance in TilePrefab)
        {
            totalChances += gameObjectChance.chance;
        }

        float randomValue = UnityEngine.Random.Range(0, totalChances);

        foreach (var gameObjectChance in TilePrefab)
        {
            if (randomValue < gameObjectChance.chance)
            {
                return gameObjectChance.tile;
            }

            randomValue -= gameObjectChance.chance;
        }

        // Fallback in case of errors or if the chances do not add up to 1
        return TilePrefab[0].tile;
    }
}
