using Tiles.FallingTiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles
{
    public class FallingPlatformTileInitializer : MonoBehaviour
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
}
