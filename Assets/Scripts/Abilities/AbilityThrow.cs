using Mirror;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Abilities
{
    [AddComponentMenu(menuName: "Roguelite/Ability Throw")]
    public class AbilityThrow : AbilityAttack
    {
        [SerializeField] protected GameObject weaponPrefab;

        protected override void Awake()
        {
            base.Awake();
            _animatorParameter = C.ANIMATOR_PARAMETER_THROW;
        }

        [Command]
        protected override void CmdAttackStart()
        {
            base.CmdAttackStart();
            Debug.Log("Attack Throw");

        }

        [Command]
        protected override void CmdAttackEnd()
        {
            base.CmdAttackEnd();
            Debug.Log("Attack Throw");

        }
    }
}
