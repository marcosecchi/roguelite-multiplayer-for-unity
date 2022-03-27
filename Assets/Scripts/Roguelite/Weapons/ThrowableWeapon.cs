using Mirror;
using TheBitCave.BattleRoyale.Abilities;
using TheBitCave.BattleRoyale.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

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

        private Rigidbody _rigidbody;
        private Collider _collider;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity = transform.forward * moveForce;
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }
        
        [ServerCallback]
        private void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            damageable?.Damage(damageAmount, OwnerId);
            NetworkServer.Destroy(gameObject);
        }
    }
}
