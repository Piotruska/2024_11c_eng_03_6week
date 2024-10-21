using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Player Config", fileName = "Player config")]
public class PlayerConfig : ScriptableObject
{
    //
    [Header("Health")]
    public float maxHealth = 20.0f;
    
    //Left Right Movement config
    [Header("Movement config")]
    public float movementSpeed = 10f;
    //Jump config
    [Header("Jump config")]
    public float jumpForce = 5.0f;
    public int extraJumpCount = 1;
    //Dash config
    [Header("Dash config")]
    public float dashSpeed = 10f;
    public float dashDuration = 1f;
    public float dashCooldown = 1f;
    public int amountOfAirDash = 1;
    //Attack config
    [Header("Attack config")]
    public float attackInterval = 2.0f; //CooldownTime
    public float resetTime = 5.0f; // Time before resetting to first attack
    public float dammageAmount = 1;
    public float generalKnockbackStrength = 2;
    public float yAxisKnockbackStrength = 2;
    
    [Header("Hit by Enemy Config")]
    public float stunTime = 2;

}