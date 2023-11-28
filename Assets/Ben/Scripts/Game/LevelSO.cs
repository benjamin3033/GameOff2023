using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelTileController;

[CreateAssetMenu()]
public class LevelSO : ScriptableObject
{
    [Header("Info")]
    public string Name;
    public string Description;

    [Header("Waves")]
    public List<WaveOption> waveOptions = new();

    [Header("Tiles")]
    public int numRows = 5;
    public int numCols = 5;
    public float spacing = 10f;

    public List<TileWithChange> TilePrefab = new();
    public GameObject edgeTilePrefab;
    public GameObject centerTilePrefab;

    [Header("Other")]
    public float length;
}
