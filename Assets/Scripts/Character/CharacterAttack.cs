using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheBitCave.MultiplayerRoguelite.Abilities
{
    
    [RequireComponent(typeof(AbstractCharacter))]
    [RequireComponent(typeof(AbilityCloseCombat))]
    [AddComponentMenu(menuName: "Roguelite/Character Attack")]
    public class CharacterAttack : NetworkBehaviour
    {
        private AbstractCharacter _character;
        private AbilityCloseCombat _closeCombat;
        private AbilityShoot _shoot;
        private AbilityThrow _throw;

        private AbstractAbility _activeAttack;
        
        /// <summary>
        /// Initializes all needed component references.
        /// </summary>
        protected virtual void Awake()
        {
            _character = GetComponent<AbstractCharacter>();
            _closeCombat = GetComponent<AbilityCloseCombat>();
            _shoot = GetComponent<AbilityShoot>();
            _throw = GetComponent<AbilityThrow>();
            _activeAttack = _closeCombat;
        }

        public override void OnStartClient()
        {
            if (!isLocalPlayer) return;
            _character.InputActions.Player.Attack.started += OnAttack;
        }
        
        public override void OnStopClient()
        {
            if (!isLocalPlayer) return;
            _character.InputActions.Player.Attack.started -= OnAttack;
        }
        
        private void OnAttack(InputAction.CallbackContext obj)
        {
            Debug.Log("Attack");
        }

    }
}
