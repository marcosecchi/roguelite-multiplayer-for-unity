using System.Linq;
using Mirror;
using TheBitCave.BattleRoyale.Abilities;
using TheBitCave.BattleRoyale.Data;
using UnityEngine;

namespace TheBitCave.BattleRoyale
{
    [AddComponentMenu(menuName: "BattleRoyale/WeaponPickup")]
    public class WeaponPickup : BasePickup
    {
        [SerializeField] protected CloseCombatWeaponStatsSO closeCombatWeapon;       
        [SerializeField] protected RangedWeaponStatsSO rangedWeapon;       
        [SerializeField] protected CharacterType[] pickableBy;
        
        [ServerCallback]
        protected override void OnTriggerEnter(Collider other)
        {
            var character = other.GetComponent<Character>();
            if (character == null) return;
            if (pickableBy.Any(characterType => character.Type == characterType))
            {
                Pick(other.gameObject);
                // TODO: implement activation system (A button)
            }
        }

        [Server]
        protected override void Pick(GameObject picker)
        {
            if (rangedWeapon != null)
            {
                var attack = picker.GetComponent<AbilityRangedAttack>();
                if (attack == null) return;
                attack.ChangeWeapon(rangedWeapon.name);
            }
            if (closeCombatWeapon != null)
            {
                var attack = picker.GetComponent<AbilityCloseCombatAttack>();
                if (attack == null) return;
                attack.ChangeWeapon(closeCombatWeapon.name);
            }
            RpcUpdateWeaponModels(picker);
            NetworkServer.Destroy(gameObject);
        }

        [ClientRpc]
        protected void RpcUpdateWeaponModels(GameObject picker)
        {
            picker.GetComponent<Character>()?.UpdateWeaponModels();
        }
    }
}
