using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheBitCave.BattleRoyale
{
    public class AnimationAttackEventsSender : MonoBehaviour
    {
        public delegate void AnimationAttack();
        public event AnimationAttack OnAttackStart;
        public event AnimationAttack OnAttackEnd;
            
        /// <summary>
        /// Executed by an event dispatched by the animator
        /// </summary>
        public virtual void AttackStart()
        {
            OnAttackStart?.Invoke();
        }

        /// <summary>
        /// Executed by an event dispatched by the animator
        /// </summary>
        public virtual void AttackEnd()
        {
            OnAttackEnd?.Invoke();
        }

    }
}
