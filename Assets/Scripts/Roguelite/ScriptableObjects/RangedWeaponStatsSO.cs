using TheBitCave.BattleRoyale.WeaponSystem;
using UnityEngine;

namespace TheBitCave.BattleRoyale.Data
{
    /// <summary>
    /// Data used to create ranged attacks (i.e.: throw daggers, cast spells, etc.)
    /// </summary>
    [CreateAssetMenu(fileName = "CharacterStats", menuName = "Roguelite/Data/Ranged Weapon Stats")]
    public class RangedWeaponStatsSO : BaseWeaponStatsSO
    {
        /// <summary>
        /// The bullet that will be spawned
        /// </summary>
        [SerializeField]
        protected BaseWeapon bulletPrefab;

        public BaseWeapon BulletPrefab => bulletPrefab;
    }
    
}
