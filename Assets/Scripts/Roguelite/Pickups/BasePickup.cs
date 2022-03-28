using Mirror;
using UnityEngine;

namespace TheBitCave.BattleRoyale
{
    /// <summary>
    /// An abstract class implementing all base pickup logic
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public abstract class BasePickup : NetworkBehaviour
    {
        private Collider _collider;

        /// <summary>
        /// Sets the collider to a trigger, just to be sure that
        /// everything will work properly
        /// </summary>
        protected virtual void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }
        
        /// <summary>
        /// The trigger should be activated only on the server.
        /// After that, all clients will be notified
        /// </summary>
        /// <param name="other"></param>
        protected virtual void OnTriggerEnter(Collider other)
        {
            if(!isServer) return;
            Pick(other.gameObject);
        }

        /// <summary>
        /// The abstract method of picking the object.
        /// Implement this for pickup logic
        /// </summary>
        /// <param name="picker">The gameObject picking up the item</param>
        protected abstract void Pick(GameObject picker);
    }
}
