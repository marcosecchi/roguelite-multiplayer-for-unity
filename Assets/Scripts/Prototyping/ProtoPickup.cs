using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Prototype
{
    [RequireComponent(typeof(Collider))]
    public class ProtoPickup : NetworkBehaviour
    {
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }
        
        // ServerCallback because we don't want a warning
        // if OnTriggerEnter is called on the client
        [ServerCallback]
        private void OnTriggerEnter(Collider other)
        {
            var nb = other.GetComponent<AbstractPlayerController>();
            if (nb == null) return;
            
            NetworkServer.Destroy(gameObject);            
        }
    }
}
