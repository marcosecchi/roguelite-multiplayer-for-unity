using System;
using Mirror;
using TheBitCave.BattleRoyale.Interfaces;
using TheBitCave.BattleRoyale.Utils;
using UnityEngine;

namespace TheBitCave.BattleRoyale.Abilities
{
    [AddComponentMenu(menuName: "BattleRoyale/Health")]
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
