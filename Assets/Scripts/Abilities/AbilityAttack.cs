using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Abilities
{
    /// <summary>
    /// Base component used to make an attack (cast a spell, close combat attack, etc.)
    /// The attack is activated/deactivated by the character animator in order to synchronize it with
    /// the corresponding animation.
    /// </summary>
    [RequireComponent(typeof(BaseCharacter))]
    public abstract class AbilityAttack : NetworkBehaviour
    {
        protected BaseCharacter _character;
        protected AnimationAttackEventsSender _animationEventSender;

        [SyncVar]
        protected bool _isAttacking;

        protected string _animatorParameter;
        
        /// <summary>
        /// Initializes the needed components.
        /// </summary>
        protected virtual void Awake()
        {
            _character = GetComponent<BaseCharacter>();
            _animationEventSender = GetComponentInChildren<AnimationAttackEventsSender>();
        }

        public override void OnStartClient()
        {
            base.OnStartClient();
            if (!isLocalPlayer) return;
            if (_animationEventSender != null) _animationEventSender.OnAttackStart += AttackStart;
            if (_animationEventSender != null) _animationEventSender.OnAttackEnd += AttackEnd;
        }

        public override void OnStopClient()
        {
            base.OnStopClient();
            if (!isLocalPlayer) return;
            if (_animationEventSender != null) _animationEventSender.OnAttackStart -= AttackStart;
            if (_animationEventSender != null) _animationEventSender.OnAttackEnd -= AttackEnd;
        }

        /// <summary>
        /// Executed by an event dispatched by the animator
        /// </summary>
        protected virtual void AttackStart() { }

        /// <summary>
        /// Executed by an event dispatched by the animator
        /// </summary>
        protected virtual void AttackEnd()
        {
            _isAttacking = false;
        }

        /// <summary>
        /// Starts the attack phase by activating the animator parameter
        /// </summary>
        public virtual void Attack()
        {
            if (_isAttacking) return;
            _isAttacking = true;
            _character.NetworkAnimator.SetTrigger(_animatorParameter);
        }
    }
    
}
