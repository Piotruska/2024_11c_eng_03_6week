using UnityEngine;
using UnityEngine.Tilemaps;
using Tiles.CheckPoint;

[RequireComponent(typeof(Tilemap))]
public class CheckPointTileInitializer : MonoBehaviour
{
    private void Start()
    {
        Tilemap tilemap = GetComponent<Tilemap>();

        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(position);
            if (tile is CheckPointTile checkPointTile)
            {
                checkPointTile.StartUp(position, tilemap, null);
            }
        }
    }
}