using System.Collections;
using Player;
using UnityEngine;

public class JohnnyEndLevelController : MonoBehaviour
{
    [SerializeField] SceneSwitchControler _sceneSwitchControler;
    private PlayerCollectibles _playerCollectables;
    private bool _isPlayerInTrigger = false;

    private void Awake()
    {
        _playerCollectables = FindObjectOfType<PlayerCollectibles>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInTrigger = false;
        }
    }

    private void Update()
    {
        if (_isPlayerInTrigger && Input.GetButtonDown("Interact"))
        {
            if (!_playerCollectables.HasAllDiamonds())
            {
                NotEnoughDiamonds();
            }
            else
            {
                EnoughDiamonds();
            }
        }
    }

    private void NotEnoughDiamonds()
    {
        Debug.Log("Not Enough Diamonds, collect all of them");
        // TODO: Add Dialog Mark
    }

    private void EnoughDiamonds()
    {
        _playerCollectables.ResetDiamonds();
        _sceneSwitchControler.levelComplete = true;
        Debug.Log("You have enough Diamonds, go rest now");
        // TODO: Add Dialog Mark
    }
}