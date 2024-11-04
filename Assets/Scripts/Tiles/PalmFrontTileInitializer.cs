using UnityEngine;
using UnityEngine.Tilemaps;
using Tiles.Interactables;

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