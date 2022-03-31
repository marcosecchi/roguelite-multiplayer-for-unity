using Mirror;
using TheBitCave.BattleRoyale.Interfaces;
using UnityEngine;

namespace TheBitCave.BattleRoyale.WeaponSystem
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class RangedWeapon : BaseWeapon
    {
        [SerializeField]
        private float moveForce = 8;

        [SerializeField]
        private float damageAmount = 3;

        [SerializeField]
        private float pushbackForce = 0;

        [SerializeField]
        private GameObject vfx;

        private Rigidbody _rigidbody;
        private Collider _collider;

        /// <summary>
        /// Registers all needed components
        /// </summary>
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity = transform.forward * moveForce;
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }
        
        /// <summary>
        /// Handles the collision (though a trigger) of the damageable element
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            var t = transform;
            RpcCreateVfx(t.position, t.rotation);
            if (!isServer) return; 
            var damageable = other.GetComponent<IDamageable>();
            damageable?.Damage(damageAmount, OwnerId);
            var otherRigidbody = other.GetComponent<Rigidbody>();
            var otherCharacterController = other.GetComponent<CharacterController>();
            switch (pushbackForce)
            {
                case > 0 when otherRigidbody != null:
                    otherRigidbody.AddForce(transform.forward * pushbackForce);
                    break;
                case > 0 when otherCharacterController != null:
                    otherCharacterController.SimpleMove(transform.forward * pushbackForce);
                    break;
            }
            NetworkServer.Destroy(gameObject);
        }
        
        /// <summary>
        /// Instantiate a visual effect (if any) on the client
        /// <param name="position">The position of the generated effect</param>
        /// <param name="rotation">The rotation of the generated effect</param>
        /// </summary>
        [Client]
        protected virtual void RpcCreateVfx(Vector3 position, Quaternion rotation)
        {
            if (vfx == null) return;
            Instantiate(vfx, position, rotation);
        }

    }
}
