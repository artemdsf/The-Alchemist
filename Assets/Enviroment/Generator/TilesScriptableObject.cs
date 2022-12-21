using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Tiles", menuName = "ScriptableObjects/Tiles", order = 1)]
public class TilesScriptableObject : ScriptableObject
{
    public List<Tile> Tiles = new List<Tile>();
}
