using GameController;
using Interactables;
using UI;
using UnityEngine;

namespace NPC.Friendly.Johnny
{
    public class JohnnyEnding : MonoBehaviour
    {
        [Header("Dialogue Line Number")]
        [SerializeField] private int dialogueLine;
        [Header("Destroy on exit?")]
        [SerializeField] private bool destroyOnExit;
        
        private DialogueMenuDisplay _dialogueMenu;
        private ShowKeyBind _showKeyBind;

        private void Awake()
        {
            _dialogueMenu = FindObjectOfType<DialogueMenuDisplay>();
            _showKeyBind = GetComponent<ShowKeyBind>();
        }

        public void StartEndingDialogue()
        {
            _dialogueMenu.EnterDialogue(this.gameObject, DialogueScripts.GetScript(dialogueLine), destroyOnExit);
            _showKeyBind.ForceDestroy();
        }
    }
}