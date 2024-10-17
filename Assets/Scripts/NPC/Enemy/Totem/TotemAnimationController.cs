using NPC.Enemy.Interfaces;
using UnityEngine;

namespace NPC.Enemy.Totem
{
    public class TotemAnimationController : MonoBehaviour, ITotemAnimation
    {
        private TotemController _totemController;
        private Animator _animator;
        
        private string _shootTrigger = "Shoot";
        private string _hitTrigger = "Hit";

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _totemController = GetComponent<TotemController>();
        }

        public void Shoot()
        {
            _animator.SetTrigger(_shootTrigger);
        }
        
        public void Hit()
        {
            _animator.SetTrigger(_hitTrigger);
        }
    }
}
