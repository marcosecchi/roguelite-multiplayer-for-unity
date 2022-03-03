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
        public float speed = 1;
        
        public override void OnStartClient()
        {
            var velocity = transform.forward * speed;
            GetComponent<Rigidbody>().velocity = velocity;
        }

        [ServerCallback]
        private void OnCollisionEnter(Collision collision)
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}
