using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Cutscenes;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DialogueEndingDisplay : MonoBehaviour
    {
        private Canvas _dialogueMenu;
        private CinemachineVirtualCamera _vcam;
        private TMP_Text _text;

        private List<string> _dialogueText;
        private int _dialogueIndex;
        
        private bool _dialogueActive = false;

        private GameObject _target;
        private bool _destroyObjectOnExit;

        private float _lensOriginal;
        
        private bool _confirmInput;
        
        private GameEndingScene _gameEndingScene;
        
        private void Awake()
        {
            _dialogueMenu = GameObject.Find("DialogueMenu").GetComponent<Canvas>();
            _text = GameObject.Find("Dialogue_Text").GetComponent<TMP_Text>();
            _vcam = FindObjectOfType<CinemachineVirtualCamera>();
            
            _gameEndingScene = FindObjectOfType<GameEndingScene>();
            
            _lensOriginal = 7;
            Hide();
        }

        private void Update()
        {
            _confirmInput = Input.GetButtonDown(InputManager.Confirm);

            if (_dialogueActive)
            {
                if (_confirmInput)
                {
                    Advance();
                }
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

        public void EnterEndingDialogue(GameObject targetObject, List<string> dialogueText, bool destroyOnExit)
        {
            // Set
            _dialogueActive = true;
            _target = targetObject;
            _destroyObjectOnExit = destroyOnExit;
            _dialogueIndex = 0;
            _dialogueText = dialogueText;
            SetText(_dialogueText[_dialogueIndex]);
            _dialogueIndex++;
            
            // Display
            InputManager.PlayerDisable();
            InputManager.MenuEnable();
            SetCamera(_target.transform);
            Show();
        }

        private void ExitDialogue()
        {
            _dialogueActive = false;
            Hide();

            if (_destroyObjectOnExit)
            {
                StartCoroutine(FadeOutAndDestroy(_target));
            }
            
            _gameEndingScene.StartEndingScenePart2();
        }
        
        private IEnumerator FadeOutAndDestroy(GameObject targetObject)
        {
            float fadeDuration = 3;
            if (targetObject == null) yield break;

            SpriteRenderer spriteRenderer = targetObject.GetComponent<SpriteRenderer>();
            float elapsedTime = 0f;
            Color color = spriteRenderer.color;
        
            while (elapsedTime < fadeDuration)
            {
                if (targetObject == null) yield break;
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
                spriteRenderer.color = color;
                yield return null;
            }
            
            Destroy(targetObject);
        }

        private void SetText(string dialogueText)
        {
            _text.text = dialogueText;
        }

        private void SetCamera(Transform followTransform)
        {
            _vcam.Follow = followTransform;
            StartCoroutine(CameraZoomIn(4));
            _vcam.PreviousStateIsValid = false;
        }
        
        IEnumerator CameraZoomIn(float target)
        {
            for (float lens = _lensOriginal; lens >= target; lens-=0.04f)
            {
                _vcam.m_Lens.OrthographicSize = lens;
                yield return null;
            }
        }
        
        IEnumerator CameraZoomOut()
        {
            for (float lens = _vcam.m_Lens.OrthographicSize; lens <= _lensOriginal; lens+=0.01f)
            {
                _vcam.m_Lens.OrthographicSize = lens;
                yield return null;
            }
        }

        private void Hide() {
            _dialogueMenu.enabled = false;
        }
        
        private void Show() {
            _dialogueMenu.enabled = true;
        }
    }
}
