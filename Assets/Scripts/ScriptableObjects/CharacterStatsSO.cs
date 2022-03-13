using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  TheBitCave.MultiplayerRoguelite.Prototype
{
    [CreateAssetMenu(fileName = "CharacterStats", menuName = "Roguelite/Data/Character Stats")]
    [Serializable]
    public class CharacterStatsSO : ScriptableObject
    {
        [Header("Main Settings")]
        [SerializeField]
        private int level = 1;

        [SerializeField]
        private CharacterType characterType;

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
        public CharacterType Type => characterType;
        public float WalkSpeed => walkSpeed;
        public float RunSpeed => runSpeed;
        public float RotationSpeed => rotationSpeed;
        public float StartingHitPoints => startingHitPoints;
    }
    
}
