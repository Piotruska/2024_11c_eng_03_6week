using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "FallingPlatformTile", menuName = "Tiles/Falling Platform Tile")]
public class FallingPlatformTile : Tile
{
    public GameObject fallingPlatformPrefab;

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        if (Application.isPlaying && fallingPlatformPrefab != null)
        {
            Tilemap tilemapComponent = tilemap.GetComponent<Tilemap>();
            Vector3 worldPos = tilemapComponent.CellToWorld(position) + new Vector3(0.5f, 0.5f, 0);
            
            if (!PlatformExistsAtPosition(worldPos))
            {
                GameObject platformInstance = Instantiate(fallingPlatformPrefab, worldPos, Quaternion.identity);
                platformInstance.transform.parent = tilemapComponent.transform;
            }
            
            tilemapComponent.SetTile(position, null);
        }
        return base.StartUp(position, tilemap, go);
    }

    private bool PlatformExistsAtPosition(Vector3 position)
    {
        Collider2D collider = Physics2D.OverlapPoint(position);
        return collider != null && collider.gameObject.CompareTag("FallingPlatform");
    }
}