using Cinemachine;
using UnityEngine;

namespace UI
{
    public class DialogueMenuDisplay : MonoBehaviour
    {
        private Canvas _dialogueMenu;
        private CinemachineVirtualCamera _vcam;
        void Awake()
        {
            _dialogueMenu = GameObject.Find("DialogueMenu").GetComponent<Canvas>();
            _vcam = FindObjectOfType<CinemachineVirtualCamera>();
            _dialogueMenu.enabled = false;
        }

        public void ShowDialogue(Transform followTransform)
        {
            _vcam.Follow = followTransform;
            _vcam.PreviousStateIsValid = false;
            Show();
        }

        public void Hide() {
            _dialogueMenu.enabled = false;
        }
        
        public void Show() {
            _dialogueMenu.enabled = true;
        }
    }
}
