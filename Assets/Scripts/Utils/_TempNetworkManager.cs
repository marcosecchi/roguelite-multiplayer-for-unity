using System.Collections;
using System.Collections.Generic;
using Mirror;
using TheBitCave.MultiplayerRoguelite;
using UnityEngine;

public class _TempNetworkManager : NetworkManager
{

    public BaseCharacter[] characters;
    
    public override void OnStartServer()
    {
        base.OnStartServer();
        
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
    }

    void OnCreateCharacter()
    {
        
    }
    
}
