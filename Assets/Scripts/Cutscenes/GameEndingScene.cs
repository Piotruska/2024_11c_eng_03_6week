using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UI;
using UnityEngine;

namespace Cutscenes
{
    public class GameEndingScene : MonoBehaviour
    {
        private DialogueMenuDisplay _dialogueMenu;
        private BlackScreen _blackScreen;
        private GameObject _HUD;
        private CinemachineVirtualCamera _vcam;
        
        private GameObject _player;
        
        [SerializeField]
        private Transform _playerTransform;
        [SerializeField]
        private Transform _johnnyTransform;
        [SerializeField]
        private GameObject _johnnyEnding;
        private void Awake()
        {
            _dialogueMenu = FindObjectOfType<DialogueMenuDisplay>();
            _blackScreen = FindObjectOfType<BlackScreen>();
            
            _vcam = FindObjectOfType<CinemachineVirtualCamera>();
            
            _HUD = GameObject.Find("HUD");
            _player = GameObject.Find("Player");
        }

        private void Update()
        {
        
        }

        public void StartEndingScene()
        {
            Debug.Log("GameEndingScene::Start");
            _HUD.SetActive(false);
            InputManager.PlayerDisable();
            InputManager.MenuEnable();
            StartCoroutine(EndingScene());
        }

        public IEnumerator EndingScene()
        {
            // Screen Fade In animation
            StartCoroutine(_blackScreen.FadeIn(4));
            yield return new WaitForSeconds(4f);
            // Transport Player
            _player.transform.position = _playerTransform.position;
            _vcam.PreviousStateIsValid = false;
            // Spawn Johnny
            Instantiate(_johnnyEnding, _johnnyTransform.position, _johnnyTransform.rotation);
            yield return new WaitForSeconds(2f);
            //Scree Fade Out animation
            StartCoroutine(_blackScreen.FadeOut(4));
        }
    }
}
