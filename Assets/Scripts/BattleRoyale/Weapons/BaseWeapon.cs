using Mirror;
using TheBitCave.BattleRoyale.Interfaces;

namespace TheBitCave.BattleRoyale.WeaponSystem
{
    public abstract class BaseWeapon : NetworkBehaviour, IOwnable
    {
        [field: SyncVar]
        public uint OwnerId { get; set; }
    }
}
