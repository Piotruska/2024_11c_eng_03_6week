using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Player Config", fileName = "Player config")]
public class PlayerConfig : ScriptableObject
{
    //Left Right Movement config
    public float movementSpeed = 10f;
    //Jump config
    public float jumpForce = 2.0f;
    public int extraJumpCount = 1;
    //Dash config
    public float dashSpeed = 10f;
    public float dashDuration = 1f;
    public float dashCooldown = 1f;
}