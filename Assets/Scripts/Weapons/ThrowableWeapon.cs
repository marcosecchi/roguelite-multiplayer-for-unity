using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.WeaponSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class ThrowableWeapon : BaseWeapon
    {
        [SerializeField]
        protected float force = 8;

        protected Rigidbody rigidbody;

        protected virtual void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = transform.forward * force;
        }
        
        [ServerCallback]
        private void OnCollisionEnter(Collision collision)
        {
            NetworkServer.Destroy(gameObject);
        }
    }
    
}
