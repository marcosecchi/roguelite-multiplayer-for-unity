using Mirror;
using TheBitCave.MultiplayerRoguelite.Interfaces;

namespace TheBitCave.MultiplayerRoguelite.WeaponSystem
{
    public abstract class BaseWeapon : NetworkBehaviour, IOwnable
    {
        [SyncVar]
        protected uint ownerId;

        public uint OwnerId
        {
            get => ownerId;
            set => ownerId = value;
        }
    }
    
}
