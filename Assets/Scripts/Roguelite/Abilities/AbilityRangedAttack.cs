using Mirror;
using TheBitCave.MultiplayerRoguelite.Data;
using TheBitCave.MultiplayerRoguelite.WeaponSystem;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Abilities
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
        
        protected RangedWeaponStatsSO dataAsRanged;

        /// <summary>
        /// Initializes the needed components.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            dataAsRanged = data as RangedWeaponStatsSO;
        }

        /// <summary>
        /// Executed by an event dispatched by the animator
        /// </summary>
        protected override void AttackStart()
        {
            CmdAttackExecute();
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
            Debug.Log("Change Weapon: "  +newValue);
        }

        public override void ChangeWeapon(string weaponName)
        {
            base.ChangeWeapon(weaponName);
        }
    }
}