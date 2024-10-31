using Cinemachine;
using Interactables;
using Player;
using UI;
using UnityEngine;

namespace NPC.Friendly.Johnny
{
    public class JohnyyTutorial : MonoBehaviour, IInteractable
    {
        private DialogueMenuDisplay _dialogueMenu;

        private void Awake()
        {
            _dialogueMenu = FindObjectOfType<DialogueMenuDisplay>();
        }

        public void OnInteractAction()
        {
            _dialogueMenu.ShowDialogue(this.transform);
        }
    }
}