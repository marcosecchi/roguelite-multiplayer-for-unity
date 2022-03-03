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
            _inputActions.Player.Attack.started += OnAttack;
        }
        
        public override void OnStopClient()
        {
            if (!isLocalPlayer) return;
            _inputActions.Player.Attack.started -= OnAttack;
        }
        
        private void OnAttack(InputAction.CallbackContext obj)
        {
            Debug.Log("Attack!");
            _attack.Attack();
        }

    }
}
