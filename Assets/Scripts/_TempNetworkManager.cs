using Mirror;
using TheBitCave.MultiplayerRoguelite;
using TheBitCave.MultiplayerRoguelite.Utils;
using UnityEngine;

public class _TempNetworkManager : NetworkManager
{
    
    public override void OnStartServer()
    {
        base.OnStartServer();
        
        NetworkServer.RegisterHandler<TempMessage>(OnCreateCharacter);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();

        foreach (var type in C.characterTypes)
        {
            var go = AssetManager.Instance.GetCharacterPrefab(type);
            if(go == null) continue;
            NetworkClient.RegisterPrefab(go);
        }
        var message = new TempMessage
        {
            characterType = C.GetRandomCharacter()
        };

        NetworkClient.Send(message);
    }

    void OnCreateCharacter(NetworkConnection conn, TempMessage message)
    {
        Debug.Log(message.characterType);
        var prefab = AssetManager.Instance.GetCharacterPrefab(message.characterType);
        var go = Instantiate(prefab);
    //    go.name = prefab.name + " - " + conn.identity.netId;
        NetworkServer.AddPlayerForConnection(conn, go);
    }
}

public struct TempMessage : NetworkMessage
{
    public string characterType;
}
