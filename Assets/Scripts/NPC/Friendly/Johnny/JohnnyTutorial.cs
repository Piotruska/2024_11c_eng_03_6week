using Interactables;
using UI;
using UnityEngine;

namespace NPC.Friendly.Johnny
{
    public class JohnnyTutorial : MonoBehaviour, IInteractable
    {
        private DialogueMenuDisplay _dialogueMenu;

        private void Awake()
        {
            _dialogueMenu = FindObjectOfType<DialogueMenuDisplay>();
        }

        public void OnInteractAction()
        {
            _dialogueMenu.EnterDialogue(this.transform, DialogueScripts.GetScript(0));
        }
    }
}