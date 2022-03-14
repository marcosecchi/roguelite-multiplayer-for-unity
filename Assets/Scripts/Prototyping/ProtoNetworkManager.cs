using Mirror;
using TheBitCave.MultiplayerRoguelite.Utils;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Prototype
{
    public class ProtoNetworkManager : NetworkManager
    {
    
        public override void OnStartServer()
        {
            base.OnStartServer();
        
            NetworkServer.RegisterHandler<ProtoMessage>(OnCharacterCreationReady);
        }

        public override void OnClientConnect()
        {
            base.OnClientConnect();

            foreach (var type in C.characterTypes)
            {
                Debug.Log("Registering character: " + type);
                var go = AssetManager.Instance.GetCharacterPrefab(type);
                if(go == null) continue;
                NetworkClient.RegisterPrefab(go);
            }
            var message = new ProtoMessage
            {
                characterType = C.GetRandomCharacterLabel()
            };
            NetworkClient.Send(message);
        }

        private void OnCharacterCreationReady(NetworkConnection conn, ProtoMessage message)
        {
            var prefab = AssetManager.Instance.GetCharacterPrefab(message.characterType);
            var go = Instantiate(prefab);
            go.transform.position = GetStartPosition().position;
            NetworkServer.AddPlayerForConnection(conn, go);
        }
    }

    public struct ProtoMessage : NetworkMessage
    {
        public string characterType;
    }

}

