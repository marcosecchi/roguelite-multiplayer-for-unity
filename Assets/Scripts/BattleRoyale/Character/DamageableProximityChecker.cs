using System.Collections;
using System.Collections.Generic;
using Mirror;
using TheBitCave.BattleRoyale.Interfaces;
using UnityEngine;

namespace TheBitCave.BattleRoyale
{
    public class DamageableProximityChecker : NetworkBehaviour
    {
        [SerializeField] private float checkDistance = 1;
        
        public delegate void DamageableInteraction();

        public event DamageableInteraction OnDamageableEnter;
        public event DamageableInteraction OnDamageableExit;

        private bool _inSight;
        
        [ServerCallback]
        private void Update()
        {
            var tr = transform;
            var cast = Physics.BoxCast(tr.position, Vector3.one / 2, tr.forward, out var hit,
                    Quaternion.identity, checkDistance);
            if (_inSight == cast || (hit.collider != null && hit.collider.GetComponent<IDamageable>() == null)) return;
            switch (cast)
            {
                case true when OnDamageableEnter != null:
                    OnDamageableEnter.Invoke();
                    break;
                case false when OnDamageableExit != null:
                    OnDamageableExit.Invoke();
                    break;
            }
            _inSight = cast;
        }
    }
}
