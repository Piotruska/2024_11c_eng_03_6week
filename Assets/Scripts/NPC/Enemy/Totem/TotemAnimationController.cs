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
        
        private string _shootTrigger1 = "Shoot";
        private string _hitTrigger1 = "Hit";



        void Awake()
        {
            _animator = GetComponent<Animator>();
            _totemController = GetComponent<TotemController>();
        }

        public void Shoot()
        {
            
            _animator.SetTrigger(_shootTrigger1);
             
            
        }
        
        public void Hit()
        {
            _animator.SetTrigger(_hitTrigger1);
        }
    }
}
