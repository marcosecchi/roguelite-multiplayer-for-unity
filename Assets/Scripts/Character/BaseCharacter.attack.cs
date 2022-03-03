using System.Collections;
using System.Collections.Generic;
using Mirror;
using TheBitCave.MultiplayerRoguelite.Abilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheBitCave.MultiplayerRoguelite
{
    [RequireComponent(typeof(AbstractAbility))]
    public partial class BaseCharacter : NetworkBehaviour
    {
        private AbstractAbility _activeAttack;
        
        public override void OnStartClient()
        {
            if (!isLocalPlayer) return;
            _inputActions.Player.Attack.started += OnAttackStarted;
        }
        
        public override void OnStopClient()
        {
            if (!isLocalPlayer) return;
            _inputActions.Player.Attack.started -= OnAttackStarted;
        }
        
        private void OnAttackStarted(InputAction.CallbackContext obj)
        {
            _abilityAttack.Attack();
        }

    }
}
