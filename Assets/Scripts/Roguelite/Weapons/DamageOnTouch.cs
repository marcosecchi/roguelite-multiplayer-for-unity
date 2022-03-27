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

        private float _damageValue;
        private uint _provoker;
        
        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
            _collider.enabled = false;
        }

        public void StartAttack(float damageValue, uint provoker)
        {
            _damageValue = damageValue;
            _provoker = provoker;
            _collider.enabled = true;
        }

        public void EndAttack()
        {
            _collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            damageable?.Damage(_damageValue, _provoker);
        }
    }
}
