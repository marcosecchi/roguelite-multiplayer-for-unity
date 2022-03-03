using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite
{
    public class CharacterSkin : NetworkBehaviour
    {
        [Header("Sockets")]
        [SerializeField] protected Transform headSocket;
        [SerializeField] protected Transform bodySocket;
        [SerializeField] protected Transform armLeftSocket;
        [SerializeField] protected Transform armRightSocket;

        public override void OnStartClient()
        {
            base.OnStartClient();
        }
    }
}
