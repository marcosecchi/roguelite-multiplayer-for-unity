using Mirror;
using TheBitCave.MultiplayerRoguelite.Data;
using TheBitCave.MultiplayerRoguelite.WeaponSystem;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Abilities
{
    /// <summary>
    /// Basic system to activate a close combat attack.
    /// The attack is activated/deactivated by the character animator in order to synchronize it with
    /// the corresponding animation.
    /// </summary>
    [AddComponentMenu(menuName: "Roguelite/Ability Close Combat")]
    public class AbilityCloseCombatAttack: AbilityBaseAttack
    {
        protected CloseCombatWeaponStatsSO dataAsCloseCombat;

        /// <summary>
        /// Initializes the needed components.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            dataAsCloseCombat = data as CloseCombatWeaponStatsSO;
        }

        /// <summary>
        /// Executed by an event dispatched by the animator
        /// </summary>
        protected override void AttackStart()
        {
            // TODO: Implement close combat attack 
            Debug.Log("Attack Starts");
        }

    }
}
