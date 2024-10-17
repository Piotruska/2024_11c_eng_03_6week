using System.Collections;
using NPC.Enemy.Interfaces;
using UnityEngine;

namespace NPC.Enemy.Totem
{
    public class TotemController : MonoBehaviour
    {
        private ITotemAnimation _totemAnimationController;
        [SerializeField] private GameObject _WoodSpike_Projectile;
        [SerializeField] private float _cooldown = 2;
        
        void Awake()
        {
            _totemAnimationController = GetComponent<TotemAnimationController>();
        }

        private void Start()
        {
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                _totemAnimationController.Shoot();
                yield return new WaitForSeconds((float)0.12);
                Instantiate(_WoodSpike_Projectile, transform.position, transform.rotation);
                yield return new WaitForSeconds(_cooldown);
            }
        }
    }
}
