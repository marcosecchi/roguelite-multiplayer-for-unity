using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Data
{
    /// <summary>
    /// Data used to create melee attacks
    /// </summary>
    [CreateAssetMenu(fileName = "CharacterStats", menuName = "Roguelite/Data/Close Combat Weapon Stats")]
    public class CloseCombatWeaponStatsSO : BaseWeaponStatsSO
    {
        [SerializeField] protected float damage = 1;
    }
}