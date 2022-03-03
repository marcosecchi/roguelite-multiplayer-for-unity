using Mirror;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Abilities
{
    [AddComponentMenu(menuName: "Roguelite/Ability Melee")]
    public class AbilityMelee : AbilityAttack
    {
        protected override void Awake()
        {
            base.Awake();
            _animatorParameter = C.ANIMATOR_PARAMETER_MELEE;
        }

        [Command]
        protected override void CmdAttackStart()
        {
            base.CmdAttackStart();
            Debug.Log("Melee Start");

        }

        [Command]
        protected override void CmdAttackEnd()
        {
            base.CmdAttackEnd();
            Debug.Log("Melee End");

        }
    }
}
