using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TheBitCave.MultiplayerRoguelite.WeaponSystem;
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
        protected BaseCharacter character;
        protected AnimationAttackEventsSender animationEventSender;

        [SyncVar]
        protected bool isAttacking;

        protected string animatorParameter;
 
        public abstract WeaponType WeaponType { get; }

        /// <summary>
        /// Initializes the needed components.
        /// </summary>
        protected virtual void Awake()
        {
            character = GetComponent<BaseCharacter>();
            animationEventSender = GetComponentInChildren<AnimationAttackEventsSender>();
        }

        public override void OnStartClient()
        {
            base.OnStartClient();
            if (!isLocalPlayer) return;
            if (animationEventSender != null) animationEventSender.OnAttackStart += AttackStart;
            if (animationEventSender != null) animationEventSender.OnAttackEnd += AttackEnd;
        }

        public override void OnStopClient()
        {
            base.OnStopClient();
            if (!isLocalPlayer) return;
            if (animationEventSender != null) animationEventSender.OnAttackStart -= AttackStart;
            if (animationEventSender != null) animationEventSender.OnAttackEnd -= AttackEnd;
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
            isAttacking = false;
        }

        /// <summary>
        /// Starts the attack phase by activating the animator parameter
        /// </summary>
        public virtual void Attack()
        {
            if (isAttacking) return;
            isAttacking = true;
            character.NetworkAnimator.SetTrigger(animatorParameter);
        }
    }
    
}
