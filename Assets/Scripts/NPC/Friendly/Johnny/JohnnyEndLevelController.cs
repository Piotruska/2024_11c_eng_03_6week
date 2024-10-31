using System.Collections;
using Interactables;
using Player;
using UnityEngine;

public class JohnnyEndLevelController : MonoBehaviour, IInteractable
{
    [SerializeField] SceneSwitchControler _sceneSwitchControler;
    private PlayerCollectibles _playerCollectables;
    private bool _isPlayerInTrigger = false;

    private void Awake()
    {
        _playerCollectables = FindObjectOfType<PlayerCollectibles>();
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

    public void OnInteractAction()
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