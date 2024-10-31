using Interactables;
using UI;
using UnityEngine;

namespace NPC.Friendly.Johnny
{
    public class JohnnyTutorial : MonoBehaviour, IInteractable
    {
        [Header("Dialogue Line Number")]
        [SerializeField] private int dialogueLine;
        private DialogueMenuDisplay _dialogueMenu;

        private void Awake()
        {
            _dialogueMenu = FindObjectOfType<DialogueMenuDisplay>();
        }

        public void OnInteractAction()
        {
            _dialogueMenu.EnterDialogue(this.transform, DialogueScripts.GetScript(dialogueLine-1));
        }
    }
}