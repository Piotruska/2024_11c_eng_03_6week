using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles.Enemies
{
    [CreateAssetMenu(fileName = "TrainingDummyTile", menuName = "Tiles/TrainingDummy ")]
    public class TrainingDummyTile : Tile
    {
        public GameObject TrainingDummyPrefab;

        public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
        {
            if (Application.isPlaying && TrainingDummyPrefab != null)
            {
                Tilemap tilemapComponent = tilemap.GetComponent<Tilemap>();
                Vector3 worldPos = tilemapComponent.CellToWorld(position) + new Vector3(0.5f, 0.5f, 0);
            
                if (!PlatformExistsAtPosition(worldPos))
                {
                    GameObject platformInstance = Instantiate(TrainingDummyPrefab, worldPos, Quaternion.identity);
                    platformInstance.transform.parent = tilemapComponent.transform;
                }
            
                tilemapComponent.SetTile(position, null);
            }
            return base.StartUp(position, tilemap, go);
        }

        private bool PlatformExistsAtPosition(Vector3 position)
        {
            Collider2D collider = Physics2D.OverlapPoint(position);
            return collider != null && collider.gameObject.CompareTag("TrainingDummy");
        }
    }
}