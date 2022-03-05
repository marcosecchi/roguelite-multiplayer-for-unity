using System.Collections;
using System.Collections.Generic;
using Mirror;
using TheBitCave.MultiplayerRoguelite.Abilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheBitCave.MultiplayerRoguelite
{
    public partial class BaseCharacter : NetworkBehaviour
    {
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
            if(_abilityAttack != null) _abilityAttack.Attack();
        }

    }
}
