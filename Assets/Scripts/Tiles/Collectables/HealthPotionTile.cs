using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles.Collectables
{
    [CreateAssetMenu(fileName = "HealthPotionTile", menuName = "Tiles/Health Potion Tile ")]
    public class HealthPotionTile : Tile
    {
        public GameObject healthPotionPrefab;

        public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
        {
            if (Application.isPlaying && healthPotionPrefab != null)
            {
                Tilemap tilemapComponent = tilemap.GetComponent<Tilemap>();
                Vector3 worldPos = tilemapComponent.CellToWorld(position) + new Vector3(0.5f, 0.5f, 0);
            
                if (!PlatformExistsAtPosition(worldPos))
                {
                    GameObject platformInstance = Instantiate(healthPotionPrefab, worldPos, Quaternion.identity);
                    platformInstance.transform.parent = tilemapComponent.transform;
                }
            
                tilemapComponent.SetTile(position, null);
            }
            return base.StartUp(position, tilemap, go);
        }

        private bool PlatformExistsAtPosition(Vector3 position)
        {
            Collider2D collider = Physics2D.OverlapPoint(position);
            return collider != null && collider.gameObject.CompareTag("HealthPotion");
        }
    }
}