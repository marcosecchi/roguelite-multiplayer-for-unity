using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  TheBitCave.MultiplayerRoguelite.Prototype
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Roguelite/Data/Player Stats")]
    public class PlayerStatsSO : ScriptableObject
    {
        [SerializeField]
        private float walkSpeed = 3;

        [SerializeField]
        private float runSpeed = 3;

        [SerializeField]
        private float rotationSpeed = 3;

        public float WalkSpeed => walkSpeed;
        public float RunSpeed => runSpeed;
        public float RotationSpeed => rotationSpeed;
    }
    
}
