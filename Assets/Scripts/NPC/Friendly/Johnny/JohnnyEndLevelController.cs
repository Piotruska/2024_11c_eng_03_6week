using GameController;
using Interactables;
using Player;
using UI;
using UnityEngine;

namespace NPC.Friendly.Johnny
{
    public class JohnnyEndLevelController : MonoBehaviour, IInteractable
    {
        [SerializeField] SceneSwitchControler _sceneSwitchControler;
        [Header("Destroy on exit?")]
        [SerializeField] private bool destroyOnExit;
        
        private DialogueMenuDisplay _dialogueMenu;
        private ShowKeyBind _showKeyBind;
        
        private BlueDiamondDisplay _blueDiamondDisplay;
        private RedDiamondDisplay _redDiamondDisplay;
        private GreenDiamondDisplay _greenDiamondDisplay;
        
        private bool _isPlayerInTrigger = false;

        private void Awake()
        {
            _dialogueMenu = FindObjectOfType<DialogueMenuDisplay>();
            _showKeyBind = GetComponent<ShowKeyBind>();
            _blueDiamondDisplay = FindObjectOfType<BlueDiamondDisplay>();
            _greenDiamondDisplay = FindObjectOfType<GreenDiamondDisplay>();
            _redDiamondDisplay = FindObjectOfType<RedDiamondDisplay>();
        }

        private void NotEnoughDiamonds()
        {
            //Debug.Log("Not Enough Diamonds, collect all of them");
            DialogueDisplay(3);
        }

        private void EnoughDiamonds()
        {
            PlayerCollectibles.ResetDiamonds();
            _blueDiamondDisplay.Reset();
            _redDiamondDisplay.Reset();
            _greenDiamondDisplay.Reset();
            _sceneSwitchControler.levelComplete = true;
            //Debug.Log("You have enough Diamonds, go rest now");
            DialogueDisplay(2);
        }

        private void DialogueDisplay(int scriptNumber)
        {
            _dialogueMenu.EnterDialogue(this.gameObject, DialogueScripts.GetScript(scriptNumber), destroyOnExit);
            _showKeyBind.ForceDestroy();
        }

        public void OnInteractAction()
        {
            if (!PlayerCollectibles.HasAllDiamonds())
            {
                NotEnoughDiamonds();
            }
            else
            {
                EnoughDiamonds();
            }
        }
    }
}