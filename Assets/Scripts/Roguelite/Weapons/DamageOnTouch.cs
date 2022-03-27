using Mirror;
using TheBitCave.BattleRoyale.Interfaces;
using UnityEngine;

namespace TheBitCave.BattleRoyale.WeaponSystem
{
    /// <summary>
    /// Ability used to damage an IDamageable element on touch.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class DamageOnTouch : MonoBehaviour
    {
        private Collider _collider;

        private float _damageAmount;
        private uint _ownerId;
        
        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
            _collider.enabled = false;
        }

        public void StartAttack(float damageAmount, uint ownerId)
        {
            _damageAmount = damageAmount;
            _ownerId = ownerId;
            _collider.enabled = true;
        }

        public void EndAttack()
        {
            _collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            damageable?.Damage(_damageAmount, _ownerId);
        }
    }
}
