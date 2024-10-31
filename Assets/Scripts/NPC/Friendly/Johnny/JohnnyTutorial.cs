using Interactables;
using Player;
using UnityEngine;

namespace NPC.Friendly.Johnny
{
    public class JohnyyTutorial : MonoBehaviour, IInteractable
    {
        //[SerializeField] SceneSwitchControler _sceneSwitchControler;
        //private PlayerCollectibles _playerCollectables;
        private bool _isPlayerInTrigger = false;

        private void Awake()
        {
            //_playerCollectables = FindObjectOfType<PlayerCollectibles>();
        }

        private void TutorialDisplay(string text)
        {
            
        }

        public void OnInteractAction()
        {
            
        }
    }
}