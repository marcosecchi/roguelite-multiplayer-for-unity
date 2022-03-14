using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TheBitCave.MultiplayerRoguelite.Abilities;
using TheBitCave.MultiplayerRoguelite.WeaponSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace TheBitCave.MultiplayerRoguelite
{
    public class WeaponPickup : BasePickup
    {
        [SerializeField]
        protected CharacterType pickableBy;

        // TODO: Add Weapon identifier
        
        [ServerCallback]
        protected override void OnTriggerEnter(Collider other)
        {
            var character = other.GetComponent<Character>();
            if (character == null || character.Type != pickableBy) return;

            var attack = other.GetComponent<AbilityRangedAttack>();
            if (attack == null) return;
            
            Pick(other.gameObject);
        }

        [Server]
        protected override void Pick(GameObject picker)
        {
            // TODO: Complete process
            Debug.Log("Picked");
        }
    }
    
}
