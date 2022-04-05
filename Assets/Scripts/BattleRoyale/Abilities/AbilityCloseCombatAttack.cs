using Mirror;
using TheBitCave.BattleRoyale.Data;
using TheBitCave.BattleRoyale.WeaponSystem;
using UnityEngine;

namespace TheBitCave.BattleRoyale.Abilities
{
    /// <summary>
    /// Basic system to activate a close combat attack.
    /// The attack is activated/deactivated by the character animator in order to synchronize it with
    /// the corresponding animation.
    /// </summary>
    [AddComponentMenu(menuName: "BattleRoyale/Ability Close Combat")]
    public class AbilityCloseCombatAttack: AbilityBaseAttack
    {
        [SerializeField] protected DamageOnTouch damageArea;
        
        [SyncVar(hook = "OnWeaponChangeRequest")] protected string weaponPath;
        
        protected CloseCombatWeaponStatsSO dataAsCloseCombat => data as CloseCombatWeaponStatsSO;

        /// <summary>
        /// Executed by an event dispatched by the animator
        /// </summary>
        protected override void AttackStart()
        {
            if (!isAttacking) return;
            CmdAttackExecute();
        }

        /// <summary>
        /// Executed by an event dispatched by the animator
        /// </summary>
        protected override void AttackEnd()
        {
            base.AttackEnd();
            if(damageArea != null) damageArea.EndAttack();
        }

        protected virtual void OnWeaponChangeRequest(string _, string newValue)
        {
            Debug.Log("Change Weapon: " + newValue);
            ChangeWeapon(newValue);
        }

        public override void ChangeWeapon(string weaponName)
        {
            base.ChangeWeapon(weaponName);
        }
        
        [Command]
        private void CmdAttackExecute()
        {
            if(damageArea != null)
            {
                var t = damageArea.transform;
                RpcCreateVfx(t.position, t.rotation);
            }
            if(damageArea != null) damageArea.StartAttack(dataAsCloseCombat.Damage, netId);
        }

    }
}
