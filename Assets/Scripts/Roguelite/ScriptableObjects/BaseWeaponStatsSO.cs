using System.Collections;
using System.Collections.Generic;
using TheBitCave.BattleRoyale.WeaponSystem;
using UnityEngine;

namespace TheBitCave.BattleRoyale.Data
{
    public abstract class BaseWeaponStatsSO : ScriptableObject
    {
        /// <summary>
        /// This parameter will be used to enable the corresponding animation in the Animator 
        /// </summary>
        [SerializeField]
        protected AttackAnimatorParameter animatorParameter;

        /// <summary>
        /// The weapon that will be attached to the character's hand
        /// </summary>
        [SerializeField]
        protected GameObject weaponModelPrefab;

        /// <summary>
        /// A gameObject that should be spawned on attack activation
        /// </summary>
        [SerializeField]
        protected GameObject vfx;

        public AttackAnimatorParameter AnimatorParameter => animatorParameter;
        public string StringifiedAnimatorParameter => animatorParameter.ToString();
        public GameObject WeaponPrefab => weaponModelPrefab;
        public GameObject Vfx => vfx;
    }
    
}
