using Mirror;
using TheBitCave.MultiplayerRoguelite.WeaponSystem;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Abilities
{
    [AddComponentMenu(menuName: "Roguelite/Ability Throw")]
    public class AbilityThrow : AbilityAttack
    {
        [SerializeField] protected ThrowableWeapon weaponPrefab;
        [SerializeField] protected Transform spawnPoint;
        
        protected override void Awake()
        {
            base.Awake();
            animatorParameter = C.ANIMATOR_PARAMETER_THROW;
        }

        protected override void AttackStart()
        {
            CmdThrow();
        }

        [Command]
        private void CmdThrow()
        {
            var weapon = Instantiate(weaponPrefab, spawnPoint.position, spawnPoint.rotation);
            weapon.OwnerId = netId;
            NetworkServer.Spawn(weapon.gameObject);
        }

        public override WeaponType WeaponType => WeaponType.Thrown;
    }
}
