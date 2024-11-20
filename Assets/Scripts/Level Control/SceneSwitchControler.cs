using Interactables;
using Interactables.Interfaces;
using UnityEngine;

namespace Level_Control
{
    public class SceneSwitchControler : MonoBehaviour,IInteractable
    {
        public int sceneBuildingIndex;
        public bool levelComplete;
        private LevelLoader _levelLoader;

        private void Awake()
        {
            levelComplete = false;
            _levelLoader = FindObjectOfType<LevelLoader>();
        }

        public void OnInteractAction()
        {
            if (levelComplete)
            {
                _levelLoader.LoadNextLevel(sceneBuildingIndex);
            }
        }
    }
}