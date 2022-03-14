using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Abilities
{
    public abstract class AbstractAbility : NetworkBehaviour
    {
        public bool isActive = true;

        private bool _inProgress;
        
        /// <summary>
        /// Initializes all needed component references.
        /// </summary>
        protected virtual void Awake()
        {
        }

        public virtual void StartAbility()
        {
            if (!isActive || _inProgress) return;
            _inProgress = true;
            Debug.Log("Ability Start");
        }

        public virtual void EndAbility()
        {
            _inProgress = false;
            Debug.Log("Ability End");
        }
    }
}
