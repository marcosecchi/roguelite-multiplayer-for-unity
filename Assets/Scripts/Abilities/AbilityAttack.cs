using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Abilities
{
    [RequireComponent(typeof(BaseCharacter))]
    public abstract class AbilityAttack : NetworkBehaviour
    {
        protected BaseCharacter _character;

        protected bool _isAttacking;
        
        protected virtual void Awake()
        {
            _character = GetComponent<BaseCharacter>();
        }

        protected virtual void AttackStart()
        {
            Debug.Log("Attack Start");

        }

        protected virtual void AttackEnd()
        {
            Debug.Log("Attack End");

        }

        public virtual void Attack()
        {
            if (_isAttacking) return;
            _isAttacking = true;
            AttackStart();
        }
    }
    
}
