using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite
{
    /// <summary>
    /// An abstract class implementing all base pickup logic
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public abstract class BasePickup : NetworkBehaviour
    {
        private Collider _collider;

        protected virtual void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }
        
        [ServerCallback]
        protected virtual void OnTriggerEnter(Collider other)
        {
            Pick(other.gameObject);
        }

        protected abstract void Pick(GameObject picker);
    }
}
