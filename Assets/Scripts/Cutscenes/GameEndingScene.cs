using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using Cinemachine;
using Player;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cutscenes
{
    public class GameEndingScene : MonoBehaviour
    {
        private DialogueMenuDisplay _dialogueMenu;
        private BlackScreen _blackScreen;
        private CreditsDisplay _creditsDisplay;
        private GameObject _HUD;
        private CinemachineVirtualCamera _vcam;
        private PlayerAnimationScript _playerAnim;
        private AudioManeger _audioManeger;
        
        private GameObject _player;
        
        [Header("Characters")]
        [SerializeField]
        private Transform _playerTransform;
        [SerializeField]
        private Transform _johnnyTransform;
        [SerializeField]
        private GameObject _johnnyEnding;
        
        [Header("Exploisons")]
        [SerializeField]
        private GameObject _explosion;
        [SerializeField]
        private Transform _explosion1Position;
        [SerializeField]
        private Transform _explosion2Position;
        [SerializeField]
        private Transform _explosion3Position;
        [SerializeField]
        private Transform _explosion4Position;
        [SerializeField]
        private Transform _explosion5Position;
        [SerializeField]
        private Transform _explosion6Position;
        [SerializeField]
        private Transform _explosion7Position;
        private void Awake()
        {
            _dialogueMenu = FindObjectOfType<DialogueMenuDisplay>();
            _blackScreen = FindObjectOfType<BlackScreen>();
            _creditsDisplay = FindObjectOfType<CreditsDisplay>();
            _playerAnim = FindObjectOfType<PlayerAnimationScript>();
            _audioManeger = FindObjectOfType<AudioManeger>();
            
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
            // Music Start
            _audioManeger.PlayEndingMusic();
            yield return new WaitForSeconds(2f);
            // Spawning EXPLOSIONS
            Instantiate(_explosion, _explosion1Position.position, _explosion1Position.rotation);
            Instantiate(_explosion, _explosion2Position.position, _explosion2Position.rotation);
            Instantiate(_explosion, _explosion3Position.position, _explosion3Position.rotation);
            yield return new WaitForSeconds(0.5f);
            Instantiate(_explosion, _explosion4Position.position, _explosion4Position.rotation);
            Instantiate(_explosion, _explosion5Position.position, _explosion5Position.rotation);
            yield return new WaitForSeconds(0.5f);
            Instantiate(_explosion, _explosion6Position.position, _explosion6Position.rotation);
            Instantiate(_explosion, _explosion7Position.position, _explosion7Position.rotation);
            // Screen Fade In animation
            StartCoroutine(_blackScreen.FadeIn(4));
            yield return new WaitForSeconds(4f);
            //Names display
            _creditsDisplay.SetText("A game by: <br> Piotr Rutkowski <br> Mark Matveyenka <br> Ilya Provarau");
            StartCoroutine(_creditsDisplay.FadeIn(3));
            yield return new WaitForSeconds(6f);
            StartCoroutine(_creditsDisplay.FadeOut(3));
            yield return new WaitForSeconds(4f);
            //Names display
            _creditsDisplay.SetText("Made in collaboration with PJATK");
            StartCoroutine(_creditsDisplay.FadeIn(3));
            yield return new WaitForSeconds(6f);
            StartCoroutine(_creditsDisplay.FadeOut(3));
            yield return new WaitForSeconds(4f);
            //Names display
            _creditsDisplay.SetText("'A bunch of students' Studios presents:" );
            StartCoroutine(_creditsDisplay.FadeIn(3));
            yield return new WaitForSeconds(6f);
            StartCoroutine(_creditsDisplay.FadeOut(3));
            yield return new WaitForSeconds(4f);
            //Names display
            _creditsDisplay.SetText("THE GLORY OF PIRATE KINGDOM");
            StartCoroutine(_creditsDisplay.FadeIn(3));
            yield return new WaitForSeconds(8f);
            StartCoroutine(_creditsDisplay.FadeOut(3));
            yield return new WaitForSeconds(4f);
            //Message display
            _creditsDisplay.SetText("Thank you for playing!");
            StartCoroutine(_creditsDisplay.FadeIn(3));
            yield return new WaitForSeconds(6f);
            StartCoroutine(_creditsDisplay.FadeOut(3));
            yield return new WaitForSeconds(6f);
            //Return to Main Menu
            SceneManager.LoadScene(0);
        }
    }
}
