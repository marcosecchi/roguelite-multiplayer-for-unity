using System.Collections;
using System.Collections.Generic;
using Mirror;
using TheBitCave.MultiplayerRoguelite.Data;
using TheBitCave.MultiplayerRoguelite.Utils;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Abilities
{
    [RequireComponent(typeof(NetworkAnimator))]
    public abstract class AbilityBaseAttack : NetworkBehaviour
    {
        [SerializeField] protected BaseWeaponStatsSO data;
        [SerializeField] protected Transform handSlot;

        protected NetworkAnimator networkAnimator;
        protected AnimationAttackEventsSender animationEventSender;

        [SyncVar]
        protected bool isAttacking;
        
        public bool IsAttacking => isAttacking;
                
        /// <summary>
        /// Initializes the needed components.
        /// </summary>
        protected virtual void Awake()
        {
            networkAnimator = GetComponent<NetworkAnimator>();
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
        protected abstract void AttackStart();
        
        /// <summary>
        /// Executed by an event dispatched by the animator
        /// </summary>
        protected virtual void AttackEnd()
        {
            isAttacking = false;
        }
        
        /// <summary>
        /// Starts the attack phase by activating the animator parameter.
        /// Attack sequence is guided by the Animator, so it is important to have both an
        /// Animator and a NetworkAnimator (in Mirror animation triggers are guided by the NetworkAnimator)
        /// </summary>
        public virtual void Attack()
        {
            if (isAttacking || data == null) return;
            isAttacking = true;
            handSlot.RemoveAllChildren();
            Instantiate(data.WeaponPrefab, handSlot);
            networkAnimator.SetTrigger(data.AnimatorParameter);
        }
    }
}
