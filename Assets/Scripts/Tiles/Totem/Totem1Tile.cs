using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Totem1TileTile", menuName = "Tiles/Totem 1 Tile ")]
public class Totem1Tile : Tile
{
    public GameObject totwm1Prefab;

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        if (Application.isPlaying && totwm1Prefab != null)
        {
            Tilemap tilemapComponent = tilemap.GetComponent<Tilemap>();
            Vector3 worldPos = tilemapComponent.CellToWorld(position) + new Vector3(0.5f, 0.5f, 0);
            
            if (!PlatformExistsAtPosition(worldPos))
            {
                GameObject platformInstance = Instantiate(totwm1Prefab, worldPos, Quaternion.identity);
                platformInstance.transform.parent = tilemapComponent.transform;
            }
            
            tilemapComponent.SetTile(position, null);
        }
        return base.StartUp(position, tilemap, go);
    }

    private bool PlatformExistsAtPosition(Vector3 position)
    {
        Collider2D collider = Physics2D.OverlapPoint(position);
        return collider != null && collider.gameObject.CompareTag("Totem1");
    }
}