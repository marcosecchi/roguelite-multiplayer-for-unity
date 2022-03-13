using Mirror;
using TheBitCave.MultiplayerRoguelite.WeaponSystem;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Abilities
{
    [AddComponentMenu(menuName: "Roguelite/Ability Cast")]
    public class AbilityCast : AbilityRangedAttack
    {
        [SerializeField] protected GameObject spellPrefab;
        
        protected override void Awake()
        {
            base.Awake();
            animatorParameter = C.ANIMATOR_PARAMETER_CAST;
        }

        protected override void AttackStart()
        {
            base.AttackStart();
            Debug.Log("Cast Start");

        }

        protected override void AttackEnd()
        {
            base.AttackEnd();
            Debug.Log("Cast End");

        }
        
        public override WeaponType WeaponType => WeaponType.Spell;

    }
}
