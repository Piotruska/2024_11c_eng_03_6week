using System;
using UI;
using UnityEngine;

namespace Cutscenes
{
    public class GameEndingScene : MonoBehaviour
    {
        private DialogueMenuDisplay _dialogueMenu;
        // Start is called before the first frame update
        private void Awake()
        {
            _dialogueMenu = FindObjectOfType<DialogueMenuDisplay>();
        }

        // Update is called once per frame
        private void Update()
        {
        
        }

        public void StartEndingScene()
        {
            Debug.Log("GameEndingScene::Start");
        }
    }
}
