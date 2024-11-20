using UnityEngine;

namespace Tiles.CheckPoint
{
    [CreateAssetMenu(menuName = "Scriptable Objects/CheckPoint Config",fileName = "Checkpoint config")]
    public class CheckPointConfig : ScriptableObject
    {
        public float _respawnDuration = 0.5f;
    }
}

