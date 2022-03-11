using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  TheBitCave.MultiplayerRoguelite.Prototype
{
    [CreateAssetMenu(fileName = "CharacterStats", menuName = "Roguelite/Data/Character Stats")]
    public class CharacterStatsSO : ScriptableObject
    {
        [SerializeField]
        private int level = 1;

        [Header("Movement")]
        [SerializeField]
        private float walkSpeed = 3;

        [SerializeField]
        private float runSpeed = 3;

        [SerializeField]
        private float rotationSpeed = 3;

        [Header("Health")]
        [SerializeField]
        private float startingHitPoints = 10;

        public float Level => level;
        public float WalkSpeed => walkSpeed;
        public float RunSpeed => runSpeed;
        public float RotationSpeed => rotationSpeed;
        public float StartingHitPoints => startingHitPoints;
    }
    
}
