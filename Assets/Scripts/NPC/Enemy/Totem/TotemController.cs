using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemController : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private GameObject _WoodSpike_Projectile;
    [SerializeField] private float _cooldown = 2;
    
    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            Instantiate(_WoodSpike_Projectile, transform.position, transform.rotation);
            yield return new WaitForSeconds(_cooldown);
        }
    }
}
