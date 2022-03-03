using Mirror;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Abilities
{
    [AddComponentMenu(menuName: "Roguelite/Ability Shoot")]
    public class AbilityShoot : AbilityAttack
    {
        [SerializeField] protected GameObject bulletPrefab;
        [SerializeField] protected Transform spawnPoint;

        
        protected override void Awake()
        {
            base.Awake();
            _animatorParameter = C.ANIMATOR_PARAMETER_SHOOT;
        }

        protected override void AttackStart()
        {
            base.AttackStart();
            CmdShoot();
        }

        [Command]
        private void CmdShoot()
        {
            var go = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            NetworkServer.Spawn(go);
        }
    }
}
