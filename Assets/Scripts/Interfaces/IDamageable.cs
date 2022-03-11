using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Interfaces
{
    /// <summary>
    /// Interface to implement a damage system
    /// </summary>
    public interface IDamageable
    {
        delegate void DamageTaken(float newHitPoints);
        event DamageTaken OnDamageTaken;
        
        delegate void Destroyed();
        event Destroyed OnDestroyed;

        void Damage(float amount, uint provokerId);

        float StartingHitPoints { set;  }
    }
}
