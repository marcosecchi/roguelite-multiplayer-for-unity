using System.Collections;
using System.Collections.Generic;
using Mirror;
using TheBitCave.MultiplayerRoguelite.Data;
using TheBitCave.MultiplayerRoguelite.Utils;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TheBitCave.MultiplayerRoguelite.Abilities
{
    [RequireComponent(typeof(NetworkAnimator))]
    public abstract class AbilityBaseAttack : NetworkBehaviour
    {
        [SerializeField] protected BaseWeaponStatsSO data;
        [SerializeField] protected Transform handSlot;
        protected GameObject weaponModel;

        protected NetworkAnimator networkAnimator;
        protected AnimationAttackEventsSender animationEventSender;
        [SyncVar] protected bool isAttacking;
        
        public bool IsAttacking => isAttacking;
                
        /// <summary>
        /// Initializes the needed components.
        /// </summary>
        protected virtual void Awake()
        {
            networkAnimator = GetComponent<NetworkAnimator>();
            animationEventSender = GetComponentInChildren<AnimationAttackEventsSender>();

            if (data != null && data.WeaponPrefab != null)
            {
                ChangeWeapon(data.name);
            }
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
     //       handSlot.RemoveAllChildren();
     //       Instantiate(data.WeaponPrefab, handSlot);
            networkAnimator.SetTrigger(data.StringifiedAnimatorParameter);
        }

        public virtual void ChangeWeapon(string weaponName)
        {
            var handle = Addressables.LoadAssetAsync<BaseWeaponStatsSO>(weaponName);
            handle.Completed += OnWeaponDataLoaded;
        }

        /// <summary>
        /// Handles the weapon data just loaded by the Addressables system
        /// </summary>
        /// <param name="operation">The async operation containing the weapon data</param>
        protected virtual void OnWeaponDataLoaded(AsyncOperationHandle<BaseWeaponStatsSO> operation)
        {
            // TODO: Implement a fallback in case the weapon has not been successfully loaded
            if (operation.Status != AsyncOperationStatus.Succeeded) return;
            data = operation.Result;
            ChangeWeaponModel(data.WeaponPrefab);
        }

        /// <summary>
        /// Adds an instance of the weapon model on the player's hand
        /// </summary>
        /// <param name="prefab">The weapon model prefab</param>
        protected virtual void ChangeWeaponModel(GameObject prefab)
        {
            if (weaponModel != null)
            {
                Destroy(weaponModel);
            }

            weaponModel = Instantiate(prefab, handSlot);
            prefab.transform.position = Vector3.zero;
            prefab.transform.rotation = quaternion.identity;
            ;
        }
    }
}
