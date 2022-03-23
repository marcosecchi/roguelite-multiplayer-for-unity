using Mirror;
using TheBitCave.BattleRoyale.Data;
using UnityEngine;

namespace TheBitCave.BattleRoyale.Abilities
{
    /// <summary>
    /// Basic system to activate a close combat attack.
    /// The attack is activated/deactivated by the character animator in order to synchronize it with
    /// the corresponding animation.
    /// </summary>
    [AddComponentMenu(menuName: "Roguelite/Ability Close Combat")]
    public class AbilityCloseCombatAttack: AbilityBaseAttack
    {
        [SyncVar(hook = "OnWeaponChangeRequest")] protected string weaponPath;
        
        protected CloseCombatWeaponStatsSO dataAsCloseCombat => data as CloseCombatWeaponStatsSO;

        /// <summary>
        /// Executed by an event dispatched by the animator
        /// </summary>
        protected override void AttackStart()
        {
            // TODO: Implement close combat attack 
   //         Debug.Log("Attack Starts");
        }

        protected virtual void OnWeaponChangeRequest(string _, string newValue)
        {
            Debug.Log("Change Weapon: "  +newValue);
            ChangeWeapon(newValue);
        }

        public override void ChangeWeapon(string weaponName)
        {
            base.ChangeWeapon(weaponName);
        }
    }
}
