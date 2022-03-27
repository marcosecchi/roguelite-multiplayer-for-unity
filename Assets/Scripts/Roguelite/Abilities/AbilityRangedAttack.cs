using Mirror;
using TheBitCave.BattleRoyale.Data;
using TheBitCave.BattleRoyale.WeaponSystem;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TheBitCave.BattleRoyale.Abilities
{
    /// <summary>
    /// Base component used to make a ranged attack (cast a spell, throw a knife, etc.)
    /// The attack is activated/deactivated by the character animator in order to synchronize it with
    /// the corresponding animation.
    /// </summary>
    [RequireComponent(typeof(Character))]
    [AddComponentMenu(menuName: "BattleRoyale/Ability Ranged Attack")]
    public class AbilityRangedAttack : AbilityBaseAttack
    {
        [SyncVar(hook = "OnWeaponChangeRequest")] protected string weaponPath;

        [SerializeField] protected Transform spawnPoint;

        protected RangedWeaponStatsSO dataAsRanged => data as RangedWeaponStatsSO;
        
        protected override void AttackStart()
        {
            if (!isAttacking) return;
            CmdAttackExecute();
            // Hides the weapon model, but only if it is a throwing weapon
            if (weaponModel != null && dataAsRanged.AnimatorParameter == AttackAnimatorParameter.Throw)
            {
                weaponModel.SetActive(false);
            }
        }

        protected override void AttackEnd()
        {
            base.AttackEnd();
            if (weaponModel != null && dataAsRanged.AnimatorParameter == AttackAnimatorParameter.Throw)
            {
                weaponModel.SetActive(true);
            }
        }

        [Command]
        private void CmdAttackExecute()
        {
            var weapon = Instantiate(dataAsRanged.BulletPrefab, spawnPoint.position, spawnPoint.rotation);
            weapon.OwnerId = netId;
            NetworkServer.Spawn(weapon.gameObject);
            var t = spawnPoint.transform;
            RpcCreateVfx(t.position, t.rotation);
        }
        
        /// <summary>
        /// Handles the weapon data just loaded by the Addressables system
        /// </summary>
        /// <param name="operation">The async operation containing the weapon data</param>
        protected override void OnWeaponDataLoaded(AsyncOperationHandle<BaseWeaponStatsSO> operation)
        {
            base.OnWeaponDataLoaded(operation);
            var ni = dataAsRanged.BulletPrefab.GetComponent<NetworkIdentity>();
            if (ni != null && !NetworkClient.prefabs.ContainsKey(ni.assetId))
            {
                NetworkClient.RegisterPrefab(dataAsRanged.BulletPrefab.gameObject);
            }
        }

        protected virtual void OnWeaponChangeRequest(string _, string newValue)
        {
            ChangeWeapon(newValue);
        }
    }
}
