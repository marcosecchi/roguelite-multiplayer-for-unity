using Mirror;
using TheBitCave.BattleRoyale.Data;
using TheBitCave.BattleRoyale.WeaponSystem;
using UnityEngine;

namespace TheBitCave.BattleRoyale.Abilities
{
    /// <summary>
    /// Base component used to make a ranged attack (cast a spell, throw a knife, etc.)
    /// The attack is activated/deactivated by the character animator in order to synchronize it with
    /// the corresponding animation.
    /// </summary>
    [RequireComponent(typeof(Character))]
    [AddComponentMenu(menuName: "Roguelite/Ability Ranged Attack")]
    public class AbilityRangedAttack : AbilityBaseAttack
    {
        [SyncVar(hook = "OnWeaponChangeRequest")] protected string weaponPath;

        [SerializeField] protected Transform spawnPoint;

        protected RangedWeaponStatsSO dataAsRanged => data as RangedWeaponStatsSO;
        
        protected override void AttackStart()
        {
            CmdAttackExecute();
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
        }
        
        protected virtual void OnWeaponChangeRequest(string _, string newValue)
        {
            ChangeWeapon(newValue);
        }
    }
}
