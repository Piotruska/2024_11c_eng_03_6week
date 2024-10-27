using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class KeyCount : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = gameObject.GetComponent<TMP_Text>();
        }

        private void Update()
        {
            _text.text = PlayerCollectibles.GetKeyCount().ToString();
        }
    }
}
