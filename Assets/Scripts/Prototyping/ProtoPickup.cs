using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Prototype
{
    [RequireComponent(typeof(Collider))]
    [AddComponentMenu("")]
    public class ProtoPickup : NetworkBehaviour
    {
        private Collider _collider;

        public int points = 1;

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
            var controller = other.GetComponent<ProtoCharacter>();
            if (controller == null) return;
            controller.AddPoints(points);
            NetworkServer.Destroy(gameObject);            
        }
    }
}
