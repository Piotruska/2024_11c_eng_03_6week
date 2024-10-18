using System;
using NPC.Enemy.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NPC.Enemy.Totem
{
    public class TotemAnimationController : MonoBehaviour, ITotemAnimation
    {
        private TotemController _totemController;
        private Animator _animator;
        
        private string _shootTrigger1 = "Shoot1";
        private string _shootTrigger2 = "Shoot2";
        private string _hitTrigger1 = "Hit1";
        private string _hitTrigger2 = "Hit2";


        void Awake()
        {
            _animator = GetComponent<Animator>();
            _totemController = GetComponent<TotemController>();
        }

        public void Shoot()
        {
            int rand = Random.Range(0, 2);
            switch (rand)
            {
                case 1:
                    _animator.SetTrigger(_shootTrigger1);
                    return;
                    
                default:
                    _animator.SetTrigger(_shootTrigger2);
                    return;
            }
        }
        
        public void Hit()
        {
            int rand = Random.Range(0, 2);
            _animator.SetTrigger(_hitTrigger1);
        }
    }
}
