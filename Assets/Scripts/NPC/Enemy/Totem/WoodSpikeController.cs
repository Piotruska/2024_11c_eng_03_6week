using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSpikeController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _speed = 5;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rb.velocity = (transform.right * -1 * _speed);
    }
    
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
