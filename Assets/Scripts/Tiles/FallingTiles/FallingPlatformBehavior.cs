using System.Collections;
using UnityEngine;

namespace Tiles.FallingTiles
{
    public class FallingPlatformBehavior : MonoBehaviour
    {
        [SerializeField] private float fallDelay = 0.5f;       
        [SerializeField] private float despawnDelay = 1.5f;   
        [SerializeField] private float respawnDelay = 3.0f;   
        [SerializeField] private float fadeOutDuration = 1.0f; 
        [SerializeField] private LayerMask groundLayer; 

        private Rigidbody2D _rb;
        private Collider2D _col;
        private SpriteRenderer _spriteRenderer;
        private Vector3 _initialPosition;
        private Quaternion _initialRotation;
        private bool _isFalling = false;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<Collider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rb.isKinematic = true; 
            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!_isFalling && collision.gameObject.CompareTag("Player"))
            {
                _isFalling = true;
                Invoke("DropPlatform", fallDelay);
            }
        }

        private void DropPlatform()
        {
            AllowOnlyGroundCollisions(); 
            _rb.bodyType = RigidbodyType2D.Dynamic; 
            StartCoroutine(DespawnAndRespawn());   
        }

        private IEnumerator DespawnAndRespawn()
        {
            yield return new WaitForSeconds(despawnDelay);
            StartCoroutine(FadeOut()); 

            yield return new WaitForSeconds(fadeOutDuration); 
            HidePlatform(); 

            yield return new WaitForSeconds(respawnDelay); 
            RespawnPlatform(); 
            EnableAllCollisions(); 
        }

        private IEnumerator FadeOut()
        {
            Color initialColor = _spriteRenderer.color;
            float fadeOutRate = 1.0f / fadeOutDuration;

            for (float t = 0; t < 1; t += Time.deltaTime * fadeOutRate)
            {
                Color newColor = initialColor;
                newColor.a = Mathf.Lerp(1, 0, t); 
                _spriteRenderer.color = newColor;
                yield return null;
            }

            _spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
        }

        private void AllowOnlyGroundCollisions()
        {
            Collider2D[] allColliders = FindObjectsOfType<Collider2D>();

            foreach (Collider2D other in allColliders)
            {
                if (other != _col) 
                {
                    bool isGround = groundLayer == (groundLayer | (1 << other.gameObject.layer));
                    Physics2D.IgnoreCollision(_col, other, !isGround); 
                }
            }
        }

        private void EnableAllCollisions()
        {
            Collider2D[] allColliders = FindObjectsOfType<Collider2D>();

            foreach (Collider2D other in allColliders)
            {
                if (other != _col) 
                {
                    Physics2D.IgnoreCollision(_col, other, false); 
                }
            }
        }

        private void HidePlatform()
        {
            _rb.bodyType = RigidbodyType2D.Kinematic;
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0f; 
            _rb.rotation = 0f; 

            transform.position = _initialPosition;
            transform.rotation = _initialRotation;

            _spriteRenderer.enabled = false;
            _col.enabled = false; 
        }

        private void RespawnPlatform()
        {
        
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1); 
            _spriteRenderer.enabled = true;

        
            _rb.isKinematic = true;
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0f;
            _rb.rotation = 0f; 
            _rb.bodyType = RigidbodyType2D.Kinematic; 

        
            _col.enabled = true; 
            _isFalling = false;
        }
    }
}
