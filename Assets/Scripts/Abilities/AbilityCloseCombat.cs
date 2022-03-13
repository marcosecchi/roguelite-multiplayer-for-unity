using Mirror;
using TheBitCave.MultiplayerRoguelite.WeaponSystem;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Abilities
{
    [AddComponentMenu(menuName: "Roguelite/Ability Close Combat")]
    public class AbilityCloseCombat : AbilityRangedAttack
    {
        protected override void Awake()
        {
            base.Awake();
            animatorParameter = C.ANIMATOR_PARAMETER_MELEE;
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
        
        public override WeaponType WeaponType => WeaponType.Melee;

    }
}
