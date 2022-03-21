using System.Linq;
using Mirror;
using TheBitCave.MultiplayerRoguelite.Abilities;
using TheBitCave.MultiplayerRoguelite.Data;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite
{
    [AddComponentMenu(menuName: "Roguelite/WeaponPickup")]
    public class WeaponPickup : BasePickup
    {
        private enum Type
        {
            Ranged,
            CloseCombat
        }
        
        [SerializeField] protected BaseWeaponStatsSO weapon;       
        [SerializeField] protected CharacterType[] pickableBy;

        private Type _type;
        
        protected override void Awake()
        {
            base.Awake();
            _type = weapon as RangedWeaponStatsSO != null ? Type.Ranged : Type.CloseCombat;
        }

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
            if (_type == Type.Ranged)
            {
                var attack = picker.GetComponent<AbilityRangedAttack>();
                if (attack == null) return;
                attack.ChangeWeapon(weapon.name);
            }
            else
            {
                var attack = picker.GetComponent<AbilityCloseCombatAttack>();
                if (attack == null) return;
                attack.ChangeWeapon(weapon.name);
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
