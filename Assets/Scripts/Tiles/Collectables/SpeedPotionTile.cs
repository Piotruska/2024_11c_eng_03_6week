namespace Tiles.Collectibles
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Tilemaps;

    [CreateAssetMenu(fileName = "SpeedPotionTile", menuName = "Tiles/SpeedPotionTile ")]
    public class SpeedPotionTile : Tile
    {
        public GameObject SpeedPotionPrefab;

        public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
        {
            if (Application.isPlaying && SpeedPotionPrefab != null)
            {
                Tilemap tilemapComponent = tilemap.GetComponent<Tilemap>();
                Vector3 worldPos = tilemapComponent.CellToWorld(position) + new Vector3(0.5f, 0.5f, 0);
            
                if (!PlatformExistsAtPosition(worldPos))
                {
                    GameObject platformInstance = Instantiate(SpeedPotionPrefab, worldPos, Quaternion.identity);
                    platformInstance.transform.parent = tilemapComponent.transform;
                }
            
                tilemapComponent.SetTile(position, null);
            }
            return base.StartUp(position, tilemap, go);
        }

        private bool PlatformExistsAtPosition(Vector3 position)
        {
            Collider2D collider = Physics2D.OverlapPoint(position);
            return collider != null && collider.gameObject.CompareTag("SpeedPotion");
        }
    }
}