using Tiles.Interactables;
using Tiles.PalmFront;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles
{
    [RequireComponent(typeof(Tilemap))]
    public class PalmFrontTileInitializer : MonoBehaviour
    {
        private void Start()
        {
            Tilemap tilemap = GetComponent<Tilemap>();

            foreach (var position in tilemap.cellBounds.allPositionsWithin)
            {
                TileBase tile = tilemap.GetTile(position);
                if (tile is PalmFrontTile palmFrontTile)
                {
                    palmFrontTile.StartUp(position, tilemap, null);
                }
            }
        }
    }
}