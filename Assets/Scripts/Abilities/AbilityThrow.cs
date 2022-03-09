using Mirror;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Abilities
{
    [AddComponentMenu(menuName: "Roguelite/Ability Throw")]
    public class AbilityThrow : AbilityAttack
    {
        [SerializeField] protected GameObject weaponPrefab;
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
            var go = Instantiate(weaponPrefab, spawnPoint.position, spawnPoint.rotation);
            NetworkServer.Spawn(go);
        }
    }
}
