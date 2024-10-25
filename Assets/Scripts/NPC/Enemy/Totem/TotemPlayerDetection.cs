using UnityEngine;

namespace NPC.Enemy.Totem
{
    public class TotemPlayerDetection : MonoBehaviour
    {
        private Collider2D _collider2D;
        private TotemController _controller;

        private void Start()
        {
            _collider2D = GetComponent<Collider2D>();
            _controller = this.GetComponent<Collider2D>().GetComponentInParent<TotemController>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _controller.SetPlayerInRange(true);
            }
        }
    
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _controller.SetPlayerInRange(false);
            }
        }
    }
}
