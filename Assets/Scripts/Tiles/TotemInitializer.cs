using Tiles.PalmFront;
using Tiles.Totem;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles
{
    public class TotemInitializer : MonoBehaviour
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
                    if (tile is Totem1Tile totem1Tile)
                    {
                        totem1Tile.StartUp(position, tilemap, null);
                    }
                    if (tile is Totem2Tile totem2Tile)
                    {
                        totem2Tile.StartUp(position, tilemap, null);
                    }
                    if (tile is Totem3Tile totem3Tile)
                    {
                        totem3Tile.StartUp(position, tilemap, null);
                    }
                    if (tile is Totem1HTile totem1HTile)
                    {
                        totem1HTile.StartUp(position, tilemap, null);
                    }
                    if (tile is Totem2HTile totem2HTile)
                    {
                        totem2HTile.StartUp(position, tilemap, null);
                    }
                    if (tile is Totem3HTile totem3HTile)
                    {
                        totem3HTile.StartUp(position, tilemap, null);
                    }
                }
            }
        }
    }
}