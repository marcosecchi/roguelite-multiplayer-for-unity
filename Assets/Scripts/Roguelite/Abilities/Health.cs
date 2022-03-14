using System;
using Mirror;
using TheBitCave.MultiplayerRoguelite.Interfaces;
using TheBitCave.MultiplayerRoguelite.Utils;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Abilities
{
    [AddComponentMenu(menuName: "Roguelite/Health")]
    public class Health : NetworkBehaviour, IDamageable
    {
        public event IDamageable.DamageTaken OnDamageTaken;
        public event IDamageable.Destroyed OnDestroyed;

        [Header("Health Settings")]
        [SyncVar]
        [SerializeField]
        protected float hitPoints;
        
        public float HitPoints => hitPoints;
        
        public float StartingHitPoints
        {
            set => hitPoints = value;
        }

        [Server]
        public virtual void Damage(float amount, uint provoker)
        {
            Debug.Log("DamagedBy " + provoker );
            hitPoints -= Mathf.Clamp(amount, 0, hitPoints);
            OnDamageTaken?.Invoke(hitPoints);
            if (hitPoints != 0) return;
            OnDestroyed?.Invoke();
            Destroy();
        }

        protected virtual void Destroy()
        {
            if (!isServer) return;
            NetworkServer.Destroy(gameObject);
        }
    }
    
}