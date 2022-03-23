using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  TheBitCave.BattleRoyale.Data
{
    [CreateAssetMenu(fileName = "CharacterStats", menuName = "Roguelite/Data/Character Stats")]
    public class CharacterStatsSO : ScriptableObject
    {
        [Header("Main Settings")]
        [SerializeField]
        [Range(1, 5)]
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
