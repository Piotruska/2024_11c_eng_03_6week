using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class RedPotionCount : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = gameObject.GetComponent<TMP_Text>();
        }

        private void Update()
        {
            _text.text = PlayerCollectibles.GetRedPotionCount().ToString();
        }
    }
}
