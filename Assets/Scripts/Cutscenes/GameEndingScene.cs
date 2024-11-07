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
        private PlayerAnimationScript _playerAnim;
        
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
            _playerAnim = FindObjectOfType<PlayerAnimationScript>();
            
            _vcam = FindObjectOfType<CinemachineVirtualCamera>();
            
            _HUD = GameObject.Find("HUD");
            _player = GameObject.Find("Player");
        }

        private void Update()
        {
        
        }

        public void StartEndingScene()
        {
            _HUD.SetActive(false);
            InputManager.PlayerDisable();
            InputManager.EndingEnable();
            StartCoroutine(EndingScene());
        }

        public void StartEndingScenePart2()
        {
            StartCoroutine(EndingScenePart2());
        }

        public IEnumerator EndingScene()
        {
            // Screen Fade In animation
            StartCoroutine(_blackScreen.FadeIn(4));
            yield return new WaitForSeconds(4f);
            // Spawn Johnny
            Instantiate(_johnnyEnding, _johnnyTransform.position, _johnnyTransform.rotation);
            // Transport Player
            _player.transform.position = _playerTransform.position;
            // Make sure player is facing right
            if (!_playerAnim._facingRight)
            {
                _playerAnim.Flip();
            }
            // Camera reset
            _vcam.PreviousStateIsValid = false;
            yield return new WaitForSeconds(2f);
            //Scree Fade Out animation
            StartCoroutine(_blackScreen.FadeOut(4));
        }

        // Ending Part 2: Electric Boogaloo
        public IEnumerator EndingScenePart2()
        {
            Debug.Log("GameEndingScene::Start");
            yield return new WaitForSeconds(2f);
        }
    }
}
