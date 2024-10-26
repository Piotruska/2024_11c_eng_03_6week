using System.Collections;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private PlayerHealthScript _playerHealthScript;
    [SerializeField] private Transform _respawnPoint;
    [SerializeField] private Transform _flag;
    private Collider2D _collider2D;
    [SerializeField] private float _flagRaiseSpeed = 4.0f;

    private void Awake()
    {
        _playerHealthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthScript>();
        _collider2D = gameObject.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerHealthScript.UpdateCheckpointPosition(_respawnPoint.position);
            _collider2D.enabled = false; 
            StartCoroutine(RaiseFlag());
        }
    }

    private IEnumerator RaiseFlag()
    {
        Vector3 startPosition = _flag.position;
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y + 1.03f);

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            _flag.position = Vector3.Lerp(startPosition, endPosition, elapsedTime);
            elapsedTime += Time.deltaTime * _flagRaiseSpeed;
            yield return null;
        }

        _flag.position = endPosition;
    }
}