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

        protected override void AttackStart()
        {
            base.AttackStart();
            Debug.Log("Melee Start");

        }

        protected override void AttackEnd()
        {
            base.AttackEnd();
            Debug.Log("Melee End");

        }
    }
}
