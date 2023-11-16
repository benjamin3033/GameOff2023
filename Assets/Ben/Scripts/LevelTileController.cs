using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelTileController : MonoBehaviour
{
    private LevelSO levelSO;

    

    [Serializable]
    public class TileWithChange
    {
        public GameObject tile;
        public float chance;
    }

    private void Start()
    {
        levelSO = GameController.Instance.levelSO;
    }

    public void GenerateGrid()
    {
        for (int row = 0; row < levelSO.numRows; row++)
        {
            for (int col = 0; col < levelSO.numCols; col++)
            {
                Vector3 position = new Vector3(
                    col * levelSO.spacing - (levelSO.numCols - 1) * levelSO.spacing * 0.5f,
                    0,
                    row * levelSO.spacing - (levelSO.numRows - 1) * levelSO.spacing * 0.5f
                ) + transform.position;

                GameObject tilePrefab;

                if (row == 0 || row == levelSO.numRows - 1 || col == 0 || col == levelSO.numCols - 1)
                {
                    // Edge tile
                    tilePrefab = levelSO.edgeTilePrefab;
                }
                else if (row == levelSO.numRows / 2 && col == levelSO.numCols / 2)
                {
                    // Center tile
                    tilePrefab = levelSO.centerTilePrefab;
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

        foreach (var gameObjectChance in levelSO.TilePrefab)
        {
            totalChances += gameObjectChance.chance;
        }

        float randomValue = UnityEngine.Random.Range(0, totalChances);

        foreach (var gameObjectChance in levelSO.TilePrefab)
        {
            if (randomValue < gameObjectChance.chance)
            {
                return gameObjectChance.tile;
            }

            randomValue -= gameObjectChance.chance;
        }

        // Fallback in case of errors or if the chances do not add up to 1
        return levelSO.TilePrefab[0].tile;
    }
}
