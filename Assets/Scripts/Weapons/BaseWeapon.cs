using Mirror;
using TheBitCave.MultiplayerRoguelite.Interfaces;

namespace TheBitCave.MultiplayerRoguelite.WeaponSystem
{
    public abstract class BaseWeapon : NetworkBehaviour, IOwnable
    {
        [field: SyncVar]
        public uint OwnerId { get; set; }

        public abstract WeaponType Type { get; }
    }
}
