using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour
{
    public static GameControler controler;

    [SerializeField] private PlayerConfig _playerConfig;
    
    public float playerHealth;
    public int goldCoins;
    public int healthPotions;
    public int bluePotions;
    public bool playerHasSword;
    
    private void Awake(){
        if (controler == null)
        {
            controler = this;
            DontDestroyOnLoad(gameObject);
        }else if (controler != this)
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    
}
