using Mirror;
using TheBitCave.MultiplayerRoguelite.Abilities;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.WeaponSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class ThrowableWeapon : BaseWeapon
    {
        [SerializeField]
        protected float force = 8;

        [SerializeField]
        protected float damageAmount = 3;

        protected Rigidbody rigidbody;

        protected virtual void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = transform.forward * force;
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
