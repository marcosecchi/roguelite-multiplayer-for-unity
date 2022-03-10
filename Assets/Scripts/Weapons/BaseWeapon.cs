using System.Collections;
using System.Collections.Generic;
using Mirror;
using TheBitCave.MultiplayerRoguelite.Interfaces;
using UnityEngine;

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
