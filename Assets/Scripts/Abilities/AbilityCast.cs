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
            _animatorParameter = C.ANIMATOR_PARAMETER_CAST;
        }

        [Command]
        protected override void CmdAttackStart()
        {
            base.CmdAttackStart();
            Debug.Log("Cast Start");

        }

        [Command]
        protected override void CmdAttackEnd()
        {
            base.CmdAttackEnd();
            Debug.Log("Cast End");

        }
    }
}
