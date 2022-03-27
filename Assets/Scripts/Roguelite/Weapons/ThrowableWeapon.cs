using Mirror;
using TheBitCave.BattleRoyale.Interfaces;
using UnityEngine;

namespace TheBitCave.BattleRoyale.WeaponSystem
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class ThrowableWeapon : BaseWeapon
    {
        [SerializeField]
        private float moveForce = 8;

        [SerializeField]
        private float damageAmount = 3;

        [SerializeField]
        private GameObject vfx;

        private Rigidbody _rigidbody;
        private Collider _collider;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity = transform.forward * moveForce;
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var t = transform;
            RpcCreateVfx(t.position, t.rotation);
            if (!isServer) return;
            var damageable = other.GetComponent<IDamageable>();
            damageable?.Damage(damageAmount, OwnerId);
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
            Debug.Log(vfx);
            if (vfx == null) return;
            Instantiate(vfx, position, rotation);
        }

    }
}
