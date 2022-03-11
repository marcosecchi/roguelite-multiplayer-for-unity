using Mirror;
using TheBitCave.MultiplayerRoguelite.Utils;

namespace TheBitCave.MultiplayerRoguelite.Prototype
{
    public class ProtoNetworkManager : NetworkManager
    {
    
        public override void OnStartServer()
        {
            base.OnStartServer();
        
            NetworkServer.RegisterHandler<ProtoMessage>(OnCreateCharacter);
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
            var message = new ProtoMessage
            {
                characterType = C.GetRandomCharacter()
            };

            NetworkClient.Send(message);
        }

        private static void OnCreateCharacter(NetworkConnection conn, ProtoMessage message)
        {
            var prefab = AssetManager.Instance.GetCharacterPrefab(message.characterType);
            var go = Instantiate(prefab);
            //    go.name = prefab.name + " - " + conn.identity.netId;
            NetworkServer.AddPlayerForConnection(conn, go);
        }
    }

    public struct ProtoMessage : NetworkMessage
    {
        public string characterType;
    }

}

