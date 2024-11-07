using GameController;
using Interactables;
using UI;
using UnityEngine;

namespace NPC.Friendly.Johnny
{
    public class JohnnyEnding : MonoBehaviour, IInteractable
    {
        [Header("Dialogue Line Number")]
        [SerializeField] private int dialogueLine;
        [Header("Destroy on exit?")]
        [SerializeField] private bool destroyOnExit;
        
        private DialogueEndingDisplay _dialogueMenu;
        private ShowKeyBind _showKeyBind;

        private void Awake()
        {
            _dialogueMenu = FindObjectOfType<DialogueEndingDisplay>();
            _showKeyBind = GetComponent<ShowKeyBind>();
        }

        public void OnInteractAction()
        {
            _dialogueMenu.EnterDialogue(this.gameObject, DialogueScripts.GetScript(dialogueLine), destroyOnExit);
            _showKeyBind.ForceDestroy();
        }
    }
}