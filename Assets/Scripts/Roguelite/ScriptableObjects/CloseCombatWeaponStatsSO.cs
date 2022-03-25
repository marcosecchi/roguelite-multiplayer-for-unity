using UnityEngine;

namespace TheBitCave.BattleRoyale.Data
{
    /// <summary>
    /// Data used to create melee attacks
    /// </summary>
    [CreateAssetMenu(fileName = "CharacterStats", menuName = "Roguelite/Data/Close Combat Weapon Stats")]
    public class CloseCombatWeaponStatsSO : BaseWeaponStatsSO
    {
        [SerializeField] protected float damage = 1;

        public float Damage => damage;
    }
}