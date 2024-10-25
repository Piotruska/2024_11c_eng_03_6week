using System.Collections;
using UnityEngine;

public class FallingPlatformBehavior : MonoBehaviour
{
    public float fallDelay = 0.5f;       
    public float despawnDelay = 1.5f;   
    public float respawnDelay = 3.0f;   
    public float fadeOutDuration = 1.0f; 
    [SerializeField] private LayerMask excludedLayers; 

    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer spriteRenderer;
    private Vector3 initialPosition;
    private bool isFalling = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.isKinematic = true; 
        initialPosition = transform.position; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isFalling && collision.gameObject.CompareTag("Player"))
        {
            isFalling = true;
            Invoke("DropPlatform", fallDelay);
        }
    }

    private void DropPlatform()
    {
        rb.bodyType = RigidbodyType2D.Dynamic; 
        StartCoroutine(DespawnAndRespawn());   
    }

    private IEnumerator DespawnAndRespawn()
    {
        AdjustCollisionsWithExcludedLayers(true);
        yield return new WaitForSeconds(despawnDelay);
        StartCoroutine(FadeOut()); 

        yield return new WaitForSeconds(fadeOutDuration); 
       
        HidePlatform(); 

        yield return new WaitForSeconds(respawnDelay); 
        RespawnPlatform();
        AdjustCollisionsWithExcludedLayers(false); 
    }

    private IEnumerator FadeOut()
    {
        Color initialColor = spriteRenderer.color;
        float fadeOutRate = 1.0f / fadeOutDuration;

        for (float t = 0; t < 1; t += Time.deltaTime * fadeOutRate)
        {
            Color newColor = initialColor;
            newColor.a = Mathf.Lerp(1, 0, t); 
            spriteRenderer.color = newColor;
            yield return null;
        }

        spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
    }

    private void AdjustCollisionsWithExcludedLayers(bool ignore)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.0f); 

        foreach (Collider2D other in colliders)
        {
           
            if (((1 << other.gameObject.layer) & excludedLayers) != 0)
            {
                Physics2D.IgnoreCollision(col, other, ignore);
            }
        }
    }

    private void HidePlatform()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero; 
        transform.position = initialPosition;
        spriteRenderer.enabled = false;
    }

    private void RespawnPlatform()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1); // Reset opacity
        spriteRenderer.enabled = true; 
        isFalling = false;
    }
}
