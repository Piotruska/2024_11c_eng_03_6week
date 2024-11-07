using GameController;
using Interactables;
using UI;
using UnityEngine;

namespace NPC.Friendly.Johnny
{
    public class JohnnyEnding : MonoBehaviour, IInteractable
    {
        private DialogueEndingDisplay _dialogueMenu;
        private ShowKeyBind _showKeyBind;

        private void Awake()
        {
            _dialogueMenu = FindObjectOfType<DialogueEndingDisplay>();
            _showKeyBind = GetComponent<ShowKeyBind>();
        }

        public void OnInteractAction()
        {
            _dialogueMenu.EnterEndingDialogue(this.gameObject, DialogueScripts.GetScript(9), true);
            _showKeyBind.ForceDestroy();
        }
    }
}