using Mirror;
using TheBitCave.MultiplayerRoguelite.Abilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace TheBitCave.MultiplayerRoguelite.WeaponSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class ThrowableWeapon : BaseWeapon
    {
        [SerializeField]
        private float moveForce = 8;

        [SerializeField]
        private float damageAmount = 3;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity = transform.forward * moveForce;
        }
        
        [ServerCallback]
        private void OnCollisionEnter(Collision collision)
        {
            var health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.Damage(damageAmount, OwnerId);
            }
            NetworkServer.Destroy(gameObject);
        }

    }
    
}
