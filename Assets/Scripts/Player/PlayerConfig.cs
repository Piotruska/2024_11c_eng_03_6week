using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Player Config",fileName = "Player config")]
public class PlayerConfig : ScriptableObject
{
    public float movementSpeed = 5;
    public float jumpForce = 2.0f;
    public int extraJumps = 1;
}
