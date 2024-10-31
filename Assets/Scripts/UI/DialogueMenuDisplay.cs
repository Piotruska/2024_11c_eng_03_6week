using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DialogueMenuDisplay : MonoBehaviour
    {
        private Canvas _dialogueMenu;
        private CinemachineVirtualCamera _vcam;
        private TMP_Text _text;

        private List<string> _dialogueText;
        private int _dialogueIndex;
        
        private bool _confirmInput;
        
        private void Awake()
        {
            _dialogueMenu = GameObject.Find("DialogueMenu").GetComponent<Canvas>();
            _text = GameObject.Find("Dialogue_Text").GetComponent<TMP_Text>();
            _vcam = FindObjectOfType<CinemachineVirtualCamera>();
            Hide();
        }

        private void Update()
        {
            _confirmInput = Input.GetButtonDown(InputManager.Confirm);
            
            if (_confirmInput)
            {
                Advance();
            }
        }

        private void Advance()
        {
            if(_dialogueText.ElementAtOrDefault(_dialogueIndex) != null)
            {
                SetText(_dialogueText[_dialogueIndex]);
                _dialogueIndex++;
            }
            else
            {
                ExitDialogue();
            }
        }

        public void EnterDialogue(Transform followTransform, List<string> dialogueText)
        {
            // Set
            _dialogueIndex = 0;
            _dialogueText = dialogueText;
            SetText(_dialogueText[_dialogueIndex]);
            _dialogueIndex++;
            
            // Display
            InputManager.PlayerDisable();
            InputManager.MenuEnable();
            SetCamera(followTransform);
            Show();
        }

        private void ExitDialogue()
        {
            Hide();
            ResetCamera();
            InputManager.MenuDisable();
            InputManager.PlayerEnable();
        }

        private void SetText(string dialogueText)
        {
            _text.text = dialogueText;
        }

        private void SetCamera(Transform followTransform)
        {
            _vcam.Follow = followTransform;
            _vcam.PreviousStateIsValid = false;
        }

        private void ResetCamera()
        {
            _vcam.Follow = GameObject.FindGameObjectWithTag("Player").transform;
            _vcam.PreviousStateIsValid = false;
        }

        private void Hide() {
            _dialogueMenu.enabled = false;
        }
        
        private void Show() {
            _dialogueMenu.enabled = true;
        }
    }
}
