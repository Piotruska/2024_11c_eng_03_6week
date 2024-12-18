using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level_Control
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Animator _transition;

        public void LoadNextLevel(int index)
        {
            StartCoroutine(LoadLevel(index));
        }

        IEnumerator LoadLevel(int index)
        {
            _transition.SetTrigger("Start");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(index);
        }
    }
}
