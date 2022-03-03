using Mirror;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Abilities
{
    [AddComponentMenu(menuName: "Roguelite/Ability Shoot")]
    public class AbilityShoot : AbilityAttack
    {
        [SerializeField] protected GameObject bulletPrefab;

        protected override void Awake()
        {
            base.Awake();
            _animatorParameter = C.ANIMATOR_PARAMETER_SHOOT;
        }

        [Command]
        protected override void CmdAttackStart()
        {
            base.CmdAttackStart();
            Debug.Log("Shoot Start");

        }

        [Command]
        protected override void CmdAttackEnd()
        {
            base.CmdAttackEnd();
            Debug.Log("Shoot End");

        }
    }
}
