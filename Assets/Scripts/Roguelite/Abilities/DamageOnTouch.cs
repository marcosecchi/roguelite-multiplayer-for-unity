using Mirror;
using TheBitCave.MultiplayerRoguelite.Interfaces;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Abilities
{
    /// <summary>
    /// Ability used to damage an IDamageable element on touch.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class DamageOnTouch : NetworkBehaviour
    {
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
            _collider.enabled = false;
        }

        public void StartAttack()
        {
            _collider.enabled = true;
        }

        public void EndAttack()
        {
            _collider.enabled = false;
        }

        [ServerCallback]
        private void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
                
            }
        }
    }
}
