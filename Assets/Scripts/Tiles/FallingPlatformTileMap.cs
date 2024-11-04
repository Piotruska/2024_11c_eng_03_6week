using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FallingPlatformTileMap : MonoBehaviour
{
    void Start()
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.GetTile(pos) is FallingPlatformTile platformTile)
            {
                platformTile.StartUp(pos, tilemap, null);
            }
        }
    }
    
}
