using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Cutscenes
{
    public class GameEndingScene : MonoBehaviour
    {
        private DialogueMenuDisplay _dialogueMenu;
        private BlackScreen _blackScreen;
        private GameObject _HUD;
        
        private GameObject _player;
        
        [SerializeField]
        private Transform _playerTransform;
        [SerializeField]
        private Transform _johnnyTransform;
        private void Awake()
        {
            _dialogueMenu = FindObjectOfType<DialogueMenuDisplay>();
            _blackScreen = FindObjectOfType<BlackScreen>();
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
            StartCoroutine(EndingScene());
        }

        public IEnumerator EndingScene()
        {
            StartCoroutine(_blackScreen.FadeIn(4));
            yield return new WaitForSeconds(4f);
            _player.transform.position = _playerTransform.position;
            yield return new WaitForSeconds(2f);
            StartCoroutine(_blackScreen.FadeOut(4));
        }
    }
}
