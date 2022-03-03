using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Prototype
{
    [RequireComponent(typeof(Rigidbody))]
    public class ProtoDagger : NetworkBehaviour
    {
        public float force = 8;
        public Rigidbody rigidbody;
        
        private void Start()
        {
            rigidbody.velocity = transform.forward * force;
        }

        [ServerCallback]
        private void OnCollisionEnter(Collision collision)
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}
