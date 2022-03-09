using Mirror;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Abilities
{
    public class AbilityCast : AbilityAttack
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
    }
}
